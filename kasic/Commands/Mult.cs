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

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            double total = ArgObject.AsNumber(0);
            
            for (int i = 1; i < ArgObject.Count; i++)
            {
                var arg = ArgObject.AsNumber(i);
                total *= arg;
            }

            return new ReturnObject(this, total);
        }
    }
}