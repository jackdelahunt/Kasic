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
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            var resultOne = Types.ToNumber(context, Args[0]);
            if (resultOne.IsError)
            {
                return Helpers.Error(resultOne.Error);
            }
            
            var resultTwo = Types.ToNumber(context, Args[1]);
            if (resultTwo.IsError)
            {
                return Helpers.Error(resultTwo.Error);
            }

            if (resultTwo.Value > resultOne.Value)
                return Helpers.Ok("true");
            
            return Helpers.Ok("false");
        }
    }
}