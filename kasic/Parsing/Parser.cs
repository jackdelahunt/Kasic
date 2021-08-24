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
            var parserTokens = new List<ParserToken>(15);
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

                if (i == 0)
                {
                    var status = ParseFirst(context, foundCommand, token.Args.Count);
                    if (status.IsError)
                    {
                        return Helpers.Error(status.Error);
                    }
                }
                else
                {
                    var lastParserToken = parserTokens[i - 1];
                    var parseCommandStatus = ParseCommand(context, lastParserToken.Command, foundCommand, token.Args.Count);
                    if (parseCommandStatus.IsError)
                    {
                        return Helpers.Error(parseCommandStatus.Error);
                    }
                }
                
                // Fill any references in the token args and store them as their base type
                var builtArgs = BuildArguments(token.Args);
                
                // Build arg object from built arguments
                var argumentResult = Arguments.New(context, builtArgs, foundCommand.CommandSettings.ArgumentList);
                if (argumentResult.IsError)
                {
                    return Helpers.Error(argumentResult.Error);
                }

                parserTokens.Add(new ParserToken
                {
                    Command = foundCommand,
                    Arguments = argumentResult.Value,
                    Flags = token.Flags
                });
            }
            return parserTokens;
        }

        private Status<KasicError> ParseCommand(Context context, Command before, Command next, int nextCommandArgsCount)
        {
            var returningType = before.CommandSettings.ReturnType;
            var firstInputType = next.CommandSettings.ArgumentList.argumentTypes[0];
            var awaitingInputType = next.CommandSettings.ArgumentList.argumentTypes[nextCommandArgsCount];
            
            
            
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
            
            int nextTotalArgAmount = nextCommandArgsCount + 1;
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

        private Status<KasicError> ParseFirst(Context context, Command command, int commandsArgsCount)
        {
            int argCount = commandsArgsCount;
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

        private List<object> BuildArguments(List<string> args)
        {
            var returningObject = new List<object>(10);
            // TODO: this assumes the set command is ran on another line meaning if set is ran on this line this fails
            for (int i = 0; i < args.Count; i++)
            {
                returningObject.Add(args[i]);
            }
        
            return returningObject;
        }
    }
}