using System;
using System.Collections.Generic;

namespace kasic.Commands
{
    public static class CommandRegister
    {
        private static readonly Dictionary<string, Command> commands;

        static CommandRegister()
        {
            commands = new Dictionary<string, Command>();
            RegisterCommand(new Add());
            RegisterCommand(new Num());
        }
        
        public static Command FindCommand(string name)
        {
            commands.TryGetValue(name, out var command);
            var type = command.GetType();
            return Activator.CreateInstance(type) as Command;
        }

        private static void RegisterCommand(Command command)
        {
            commands.Add(command.Name(), command);
        }
    }
}