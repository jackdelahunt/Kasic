using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Print : Command
    {
        public Print() : base("print")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = UInt32.MaxValue,
                FieldType = KasicType.ANY,
                ReturnType = KasicType.VOID,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run()
        {
            Logger.Logln("");
            if (Flags.Contains("-c"))
            {
                foreach (var arg in Args)
                {
                    Logger.Log(arg);
                }
            }
            else
            {
                foreach (var arg in Args)
                {
                    Logger.Log(arg + " ");
                } 
            }

            return Helpers.Ok("");
        }
    }
}