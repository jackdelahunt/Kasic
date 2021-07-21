using System;
using System.Collections.Generic;
using kasic.Kasic;
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
                FieldType = KasicType.ANY,
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            return Helpers.Ok(Args[0]);
        }
    }
}