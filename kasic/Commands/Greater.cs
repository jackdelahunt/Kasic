using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Greater : Command
    {
        public Greater() : base("great")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                FieldType = KasicType.NUMBER,
                ReturnType = KasicType.BOOL,
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            var args = ArgObject.AsNumbers();
            if (args[0] < args[1])
                return Helpers.Ok("true");
            
            return Helpers.Ok("false");
        }
    }
}