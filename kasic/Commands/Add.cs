using System;
using System.Collections.Generic;

namespace kasic.Commands
{
    public class Add : Command
    {
        public List<string> Args { get; private set; }
        public List<string> Flags { get; private set; }
        private CommandSettings commandSettings;

        public Add()
        {
            Args = new List<string>();
            Flags = new List<string>();
            commandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = UInt32.MaxValue,
                FieldType = KasicType.NUMBER,
                ReturnType = KasicType.NUMBER,
                DefinedFields = null,
            };
        }

        public string Run()
        {
            double total = 0;
            foreach (var arg in Args)
            {
                total += Double.Parse(arg);
            }

            return total.ToString();
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
            return "add";
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