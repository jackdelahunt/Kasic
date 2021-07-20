using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Lexing;
using kasic.Logging;
using kasic.Memory;
using OperationResult;
using OperationResult.Tags;

namespace kasic.Parsing
{
    public class Parser
    {
        
        public List<CommandToken> Tokens { get; private set; }
        
        public Parser(List<CommandToken> tokens)
        {
            Tokens = tokens;
        }

        public Result<List<Command>, KasicError> Parse()
        {
            var commands = new List<Command>();
            foreach (var token in Tokens)
            {
                var registerResult = CommandRegister.FindCommand(token.Name);
                if (registerResult.IsError)
                {
                    return Helpers.Error(registerResult.Error);
                }

                var command = registerResult.Value;
                command.Args = token.Args;
                command.Flags = token.Flags;
                commands.Add(command);
            }

            for (int i = 0; i < commands.Count; i++)
            {
                var command = commands[i];
                if (i == 0)
                {
                    var status = ParseFirst(command);
                    if (status.IsError)
                    {
                        return Helpers.Error(status.Error);
                    }
                    continue;
                }
                
                // command always has one before here
                var beforeCommand = commands[i - 1];
                var parseCommandStatus = ParseCommand(beforeCommand, command);
                if (parseCommandStatus.IsError)
                {
                    return Helpers.Error(parseCommandStatus.Error);
                }
            }

            foreach (var command in commands)
            {
                var result = FillReferences(command);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }
            }

            return commands;
        }

        private Status<KasicError> ParseCommand(Command before, Command next)
        {
            if (before.CommandSettings.ReturnType == KasicType.VOID ||
                next.CommandSettings.FieldType == KasicType.VOID)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Region = KasicRegion.PARSER,
                        Command = next,
                        Message = "Cannot chain commands that return or require VOID",
                    });
            }
            
            // TODO: add a any type check here
            if (before.CommandSettings.ReturnType != next.CommandSettings.FieldType)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Region = KasicRegion.PARSER,
                        Command = next,
                        Message = $"Type mismatch got {before.CommandSettings.ReturnType} but expected {next.CommandSettings.FieldType}",
                    });
            }

            int nextTotalArgAmount = next.Args.Count + 1;
            if (nextTotalArgAmount > next.CommandSettings.MaxArgs ||
                nextTotalArgAmount < next.CommandSettings.MinArgs)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Region = KasicRegion.PARSER,
                        Command = next,
                        Message = "Arg count mismatch",
                    });
            }

            return Helpers.Ok();
        }

        private Status<KasicError> ParseFirst(Command command)
        {
            int argCount = command.Args.Count;
            if (argCount > command.CommandSettings.MaxArgs ||
                argCount < command.CommandSettings.MinArgs)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Region = KasicRegion.PARSER,
                        Command = command,
                        Message = "Arg count mismatch",
                    });
            }

            return Helpers.Ok();
        }

        private Status<KasicError> FillReferences(Command command)
        {
            for (int i = 0; i < command.Args.Count; i++)
            {
                var arg = command.Args[i];
                if (arg.StartsWith("*"))
                {
                    var result = Heap.Reference(arg.Substring(1));
                    if (result.IsError)
                    {
                        return Helpers.Error(result.Error);
                    }

                    command.Args[i] = result.Value;
                }
            }

            return Helpers.Ok();
        }
    }
}