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

        public Result<List<ParserToken>, KasicError> Parse(Context context)
        {
            var parserTokens = new List<ParserToken>();
            for (int i = 0; i < Tokens.Count; i++)
            {
                var token = Tokens[i];
                // Find the command based on the token name
                var registerResult = CommandRegister.FindCommand(context, token.Name);
                if (registerResult.IsError)
                {
                    return Helpers.Error(registerResult.Error);
                }

                var foundCommand = registerResult.Value;
                
                // Fill any references in the token args and store them as their base type
                var builtArgumentsResult = BuildArguments(context, token.Args, foundCommand.CommandSettings.ArgumentList);
                if (builtArgumentsResult.IsError)
                {
                    return Helpers.Error(builtArgumentsResult.Error);
                }
                
                // Build arg object from built arguments
                var argumentResult = ArgObject.New(context, builtArgumentsResult.Value, foundCommand.CommandSettings.ArgumentList);
                if (argumentResult.IsError)
                {
                    return Helpers.Error(argumentResult.Error);
                }

                if (i == 0)
                {
                    var status = ParseFirst(context, foundCommand, argumentResult.Value);
                    if (status.IsError)
                    {
                        return Helpers.Error(status.Error);
                    }
                }
                else
                {
                    var lastParserToken = parserTokens[i - 1];
                    var parseCommandStatus = ParseCommand(context, lastParserToken.Command, foundCommand, argumentResult.Value);
                    if (parseCommandStatus.IsError)
                    {
                        return Helpers.Error(parseCommandStatus.Error);
                    }
                }

                parserTokens.Add(new ParserToken
                {
                    Command = foundCommand,
                    ArgObject = argumentResult.Value,
                    Flags = token.Flags
                });
            }
            return parserTokens;
        }

        private Status<KasicError> ParseCommand(Context context, Command before, Command next, ArgObject nextCommandArgs)
        {
            var returningType = before.CommandSettings.ReturnType;
            var firstInputType = next.CommandSettings.ArgumentList.argumentTypes[0];
            var awaitingInputType = next.CommandSettings.ArgumentList.argumentTypes[nextCommandArgs.Count];
            
            
            
            if (returningType == KasicType.VOID || firstInputType == KasicType.VOID)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Context = context,
                        Message = "Cannot chain commands that return or require VOID",
                    });
            }

            if ((returningType != awaitingInputType) && awaitingInputType != KasicType.ANY)
            {
                return Helpers.Error(
                    new KasicError
                    {
                        Context = context,
                        Message = $"Type mismatch got {before.CommandSettings.ReturnType} but expected {awaitingInputType}",
                    });
            }
            
            int nextTotalArgAmount = nextCommandArgs.Count + 1;
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

        private Status<KasicError> ParseFirst(Context context, Command command, ArgObject commandsArgs)
        {
            int argCount = commandsArgs.Count;
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

        private Result<List<object>, KasicError> BuildArguments(Context context, List<string> args, ArgumentList argumentList)
        {
            var returningObject = new List<object>();
            // TODO: this assumes the set command is ran on another line meaning if set is ran on this line this fails
            for (int i = 0; i < args.Count; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("*"))
                {
                    var argumentType = argumentList.argumentTypes[i];
                    var result = Heap.Reference(context, arg.Substring(1));
                    if (result.IsError)
                    {
                        return Helpers.Error(result.Error);
                    }

                    var objectAndTypeTuple = result.Value;
                    if (objectAndTypeTuple.Item2 != argumentType && argumentType != KasicType.ANY)
                    {
                        return Helpers.Error(new KasicError
                        {
                            Context = context,
                            Message = $"expected type {argumentType} but got {result.Value.Item2}"
                        });
                    }
        
                    returningObject.Add(objectAndTypeTuple.Item1);
                }
                else
                {
                    returningObject.Add(arg);
                }
            }
        
            return Helpers.Ok(returningObject);
        }
    }
}