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
                MaxArgs = UInt32.MaxValue,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.NUMBER,
                    KasicType.NUMBER
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, ArgObject argObject, List<string> flags)
        {
            double total = argObject.AsNumber(0);
            
            for (int i = 1; i < argObject.Count; i++)
            {
                var arg = argObject.AsNumber(i);
                total *= arg;
            }

            return new ReturnObject(this, total);
        }
    }
}