using System;
using System.Collections.Generic;
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
            RegisterCommand(new String());
            RegisterCommand(new Bool());
            RegisterCommand(new Clear());
            RegisterCommand(new Exit());
            RegisterCommand(new Replace());
            RegisterCommand(new Print());
            RegisterCommand(new Goto());
            RegisterCommand(new Greater());
        }
        
        public static Result<Command, KasicError> FindCommand(Context context, string name)
        {
            commands.TryGetValue(name, out var command);
            var type = command?.GetType();

            if (type == null)
            {
                return Helpers.Error(new KasicError
                {
                    Context = context,
                    Message = $"Cannot find command with the name: {name}"
                });
            }

            return Helpers.Ok(Activator.CreateInstance(type) as Command);
        }

        private static void RegisterCommand(Command command)
        {
            commands.Add(command.Name, command);
        }
    }
}