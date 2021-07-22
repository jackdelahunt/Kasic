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

        public override Result<string, KasicError> Run(Context context)
        {
            var firstResult = Types.ToNumber(context, Args[0]);
            if (firstResult.IsError)
            {
                return Helpers.Error(firstResult.Error);
            }

            double total = firstResult.Value;
            
            for (int i = 1; i < Args.Count; i++)
            {
                var arg = Args[i];
                var result = Types.ToNumber(context, arg);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                total *= result.Value;
            }

            return Helpers.Ok(total.ToString());
        }
    }
}