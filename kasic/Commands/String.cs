using System;
using System.Collections.Generic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class String : Command
    {
        public List<string> Args { get; private set; }
        public List<string> Flags { get; private set; }
        private CommandSettings commandSettings;

        public String()
        {
            Args = new List<string>();
            Flags = new List<string>();
            commandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 1,
                FieldType = KasicType.STRING,
                ReturnType = KasicType.STRING,
                DefinedFields = null,
            };
        }

        public Result<string, KasicError> Run()
        {
            return Helpers.Ok(Args[0]);
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
            return "string";
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