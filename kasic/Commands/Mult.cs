using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Mult : Command
    {
        public Mult() : base("mult")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 4,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.NUMBER,
                    KasicType.NUMBER,
                    KasicType.NUMBER,
                    KasicType.NUMBER
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var totalResult = arguments.AsNumber(context, 0);
            if (totalResult.IsError)
            {
                return Helpers.Error(totalResult.Error);
            }

            var total = totalResult.Value;

            for (int i = 1; i < arguments.Count; i++)
            {
                var arg = arguments.AsNumber(context, i);
                if (arg.IsError)
                {
                    return Helpers.Error(arg.Error);
                }
                
                total *= arg.Value;
            }

            return new ReturnObject(this, total);
        }
    }
}