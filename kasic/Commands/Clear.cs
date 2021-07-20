using System;
using System.Collections.Generic;
using kasic.Logging;
using OperationResult;

namespace kasic.Commands
{
    public class Clear : Command
    {
        public Clear() : base("clear")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 0,
                MaxArgs = 0,
                FieldType = KasicType.VOID,
                ReturnType = KasicType.VOID,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run()
        {
            Console.Clear();
            return Helpers.Ok("");
        }
    }
}