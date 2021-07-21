using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Num : Command
    {
        public Num() : base("num")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 1,
                FieldType = KasicType.ANY,
                ReturnType = KasicType.NUMBER,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            var result = Types.ToNumber(context, Args[0]);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }
            
            return Args[0];
        }
    }
}