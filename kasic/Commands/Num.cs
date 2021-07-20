using System;
using System.Collections.Generic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Num : Command
    {
        public List<string> Args { get; private set; }
        public List<string> Flags { get; private set; }
        private CommandSettings commandSettings;

        public Num()
        {
            Args = new List<string>();
            Flags = new List<string>();
            commandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 1,
                FieldType = KasicType.NUMBER,
                ReturnType = KasicType.NUMBER,
                DefinedFields = null,
            };
        }

        public Result<string, KasicError> Run()
        {
            var result = Types.ToNumber(Args[0]);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }
            
            return Args[0];
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
            return "num";
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