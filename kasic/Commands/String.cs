using System;
using System.Collections.Generic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class String : Command
    {
        public String() : base("string")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 1,
                FieldType = KasicType.STRING,
                ReturnType = KasicType.STRING,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run()
        {
            return Helpers.Ok(Args[0]);
        }
    }
}