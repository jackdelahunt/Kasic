using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Lexing;

namespace kasic.Parsing
{
    public class Parser
    {
        
        public List<CommandToken> Tokens { get; private set; }
        
        public Parser(List<CommandToken> tokens)
        {
            Tokens = tokens;
        }

        public List<Command> Parse()
        {
            var commands = new List<Command>();
            foreach (var token in Tokens)
            {
                var command = CommandRegister.FindCommand(token.Name);
                command.PassData(token.Args, token.Flags);
                commands.Add(command);
            }

            for (int i = 0; i < commands.Count; i++)
            {
                var command = commands[i];
                if (i == 0)
                {
                    ParseFirst(command);
                    continue;
                }
                
                // command always has one before here
                var beforeCommand = commands[i - 1];
                ParseCommand(beforeCommand, command);
            }

            return commands;
        }

        private void ParseCommand(Command before, Command next)
        {
            if (before.CommandSettings().ReturnType == KasicType.VOID ||
                next.CommandSettings().FieldType == KasicType.VOID)
            {
                throw new Exception("command has a return of void or takes a void");
            }
            
            // TODO: add a any type check here
            if (before.CommandSettings().ReturnType != next.CommandSettings().FieldType)
            {
                throw new Exception("return type and input type mismatch");
            }

            int nextTotalArgAmount = next.Args().Count + 1;
            if (nextTotalArgAmount > next.CommandSettings().MaxArgs ||
                nextTotalArgAmount < next.CommandSettings().MinArgs)
            {
                throw new Exception("arg count mismatch");
            }
        }

        private void ParseFirst(Command command)
        {
            int argCount = command.Args().Count;
            if (argCount > command.CommandSettings().MaxArgs ||
                argCount < command.CommandSettings().MinArgs)
            {
                throw new Exception("first command needs to populate its values");
            }
        }
    }
}