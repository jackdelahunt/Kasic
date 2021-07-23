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
                var registerResult = CommandRegister.FindCommand(context, token.Name);
                if (registerResult.IsError)
                {
                    return Helpers.Error(registerResult.Error);
                }
                
                var argumentResult = ArgObject.New(context, token.Args, registerResult.Value.CommandSettings.FieldType);
                if (argumentResult.IsError)
                {
                    return Helpers.Error(argumentResult.Error);
                }

                var command = registerResult.Value;
                command.ArgObject = argumentResult.Value;
                command.Flags = token.Flags;
                commands.Add(command);
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

            // foreach (var command in commands)
            // {
            //     var result = FillReferences(command);
            //     if (result.IsError)
            //     {
            //         return Helpers.Error(result.Error);
            //     }
            // }

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

        // private Status<KasicError> FillReferences(Command command)
        // {
        //     // TODO: this assumes the set command is ran on another line meaning if set is ran on this line this fails
        //     for (int i = 0; i < command.Args.Count; i++)
        //     {
        //         var arg = command.Args[i];
        //         if (arg.StartsWith("*"))
        //         {
        //             var result = Heap.Reference(arg.Substring(1));
        //             if (result.IsError)
        //             {
        //                 return Helpers.Error(result.Error);
        //             }
        //
        //             command.Args[i] = result.Value;
        //         }
        //     }
        //
        //     return Helpers.Ok();
        // }
    }
}