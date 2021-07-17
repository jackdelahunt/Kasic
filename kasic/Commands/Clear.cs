using System;
using System.Collections.Generic;

namespace kasic.Commands
{
    public class Clear : Command
    {
        public List<string> Args { get; private set; }
        public List<string> Flags { get; private set; }
        private CommandSettings commandSettings;

        public Clear()
        {
            Args = new List<string>();
            Flags = new List<string>();
            commandSettings = new CommandSettings()
            {
                MinArgs = 0,
                MaxArgs = 0,
                FieldType = KasicType.VOID,
                ReturnType = KasicType.VOID,
                DefinedFields = null,
            };
        }

        public string Run()
        {
            Console.Clear();
            return "";
        }

        public void PassData(List<string> args, List<string> flags)
        {
            Args = args;
            Flags = args;
        }

        public void AddArg(string arg)
        {
            Args.Add(arg);
        }

        public string Name()
        {
            return "clear";
        }

        List<string> Command.Args()
        {
            return Args;
        }

        List<string> Command.Flags()
        {
            return Flags;
        }

        public CommandSettings CommandSettings()
        {
            return this.commandSettings;
        }
    }
}