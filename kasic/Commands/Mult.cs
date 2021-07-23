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
                FieldType = KasicType.NUMBER,
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var args = ArgObject.AsNumbers();
            double total = args[0];
            
            for (int i = 1; i < args.Count; i++)
            {
                var arg = args[i];
                total *= arg;
            }

            return new ReturnObject(this, total);
        }
    }
}