using System;
using System.Collections.Generic;
using kasic.Commands.BoolOperators;
using kasic.Kasic;
using kasic.Logging;
using OperationResult;

namespace kasic.Commands
{
    public static class CommandRegister
    {
        private static readonly Dictionary<string, Command> commands;

        static CommandRegister()
        {
            commands = new Dictionary<string, Command>();
            RegisterCommand(new Add());
            RegisterCommand(new Mult());
            RegisterCommand(new Num());
            RegisterCommand(new NumRef());
            RegisterCommand(new String());
            RegisterCommand(new StringRef());
            RegisterCommand(new Bool());
            RegisterCommand(new BoolRef());
            RegisterCommand(new Clear());
            RegisterCommand(new Exit());
            RegisterCommand(new Replace());
            RegisterCommand(new Print());
            RegisterCommand(new Goto());
            RegisterCommand(new If());
            
            // Boolean expressions
            RegisterCommand(new Greater());
            RegisterCommand(new Less());
            RegisterCommand(new And());
            RegisterCommand(new Not());
            RegisterCommand(new Or());
        }
        
        public static Result<Command, KasicError> FindCommand(Context context, string name)
        {
            if (commands.TryGetValue(name, out var command))
            {
                return Helpers.Ok(command);
            }
            
            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"Cannot find command with the name: {name}"
            });
        }

        private static void RegisterCommand(Command command)
        {
            commands.Add(command.Name, command);
        }
    }
}