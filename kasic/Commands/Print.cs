using System;
using System.Collections.Generic;
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
                MaxArgs = 1,
                FieldType = KasicType.ANY,
                ReturnType = KasicType.VOID,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run()
        {
            Console.WriteLine(Args[0]);
            return Helpers.Ok("");
        }
    }
}