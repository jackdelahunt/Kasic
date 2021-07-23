using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Kasic;
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

        public Result<List<Command>, KasicError> Parse(Context context)
        {
            var commands = new List<Command>();
            foreach (var token in Tokens)
            {
                // Find the command based on the token name
                var commandToken = token;
                var registerResult = CommandRegister.FindCommand(context, commandToken.Name);
                if (registerResult.IsError)
                {
                    return Helpers.Error(registerResult.Error);
                }

                var foundCommand = registerResult.Value;
                
                // Fill any references in the token args
                var result = FillReferences(context, commandToken.Args, foundCommand.CommandSettings.FieldType);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                commandToken.Args = result.Value;

                // Create arg object from referenced filled args
                var argumentResult = ArgObject.New(context, commandToken.Args, foundCommand.CommandSettings.FieldType);
                if (argumentResult.IsError)
                {
                    return Helpers.Error(argumentResult.Error);
                }

                registerResult.Value.ArgObject = argumentResult.Value;
                registerResult.Value.Flags = commandToken.Flags;
                commands.Add(registerResult.Value);
            }

            for (int i = 0; i < commands.Count; i++)
            {
                var command = commands[i];
                if (i == 0)
                {
                    var status = ParseFirst(context, command);
                    if (status.IsError)
                    {
                        return Helpers.Error(status.Error);
                    }
                    continue;
                }
                
                // command always has one before here
                var beforeCommand = commands[i - 1];
                var parseCommandStatus = ParseCommand(context, beforeCommand, command);
                if (parseCommandStatus.IsError)
                {
                    return Helpers.Error(parseCommandStatus.Error);
                }
            }

            return commands;
        }

        private Status<KasicError> ParseCommand(Context context, Command before, Command next)
        {
            if (before.CommandSettings.ReturnType == KasicType.VOID ||
                next.CommandSettings.FieldType == KasicType.VOID)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Context = context,
                        Message = "Cannot chain commands that return or require VOID",
                    });
            }

            if ((before.CommandSettings.ReturnType != next.CommandSettings.FieldType) && next.CommandSettings.FieldType != KasicType.ANY)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Context = context,
                        Message = $"Type mismatch got {before.CommandSettings.ReturnType} but expected {next.CommandSettings.FieldType}",
                    });
            }
            
            int nextTotalArgAmount = next.ArgObject.Count + 1;
            if (nextTotalArgAmount > next.CommandSettings.MaxArgs ||
                nextTotalArgAmount < next.CommandSettings.MinArgs)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Context = context,
                        Message = $"Arg count mismatch expected between min:{next.CommandSettings.MinArgs} and max:{next.CommandSettings.MaxArgs} got {nextTotalArgAmount}",
                    });
            }

            return Helpers.Ok();
        }

        private Status<KasicError> ParseFirst(Context context, Command command)
        {
            int argCount = command.ArgObject.Count;
            if (argCount > command.CommandSettings.MaxArgs ||
                argCount < command.CommandSettings.MinArgs)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Context = context,
                        Message = $"Arg count mismatch expected between min:{command.CommandSettings.MinArgs} and max:{command.CommandSettings.MaxArgs} got {argCount}",
                    });
            }

            return Helpers.Ok();
        }

        private Result<List<string>, KasicError> FillReferences(Context context, List<string> args, KasicType type)
        {
            // TODO: this assumes the set command is ran on another line meaning if set is ran on this line this fails
            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("*"))
                {
                    var result = Heap.Reference(arg.Substring(1));
                    if (result.IsError)
                    {
                        return Helpers.Error(result.Error);
                    }

                    if (result.Value.Item2 != type && type != KasicType.ANY)
                    {
                        return Helpers.Error(new KasicError
                        {
                            Context = context,
                            Message = $"expected type {type} but got {result.Value.Item2}"
                        });
                    }
        
                    args[i] = result.Value.Item1.ToString();
                }
            }
        
            return Helpers.Ok(args);
        }
    }
}