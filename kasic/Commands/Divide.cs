using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Divide : Command
    {
        public Divide() : base("div")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.NUMBER,
                    KasicType.NUMBER
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var firstNumber = arguments.AsNumber(context, 0);
            if (firstNumber.IsError)
            {
                return Helpers.Error(firstNumber.Error);
            }
            
            var secondNumber = arguments.AsNumber(context, 1);
            if (secondNumber.IsError)
            {
                return Helpers.Error(secondNumber.Error);
            }

            return new ReturnObject(this, firstNumber.Value / secondNumber.Value);
        }
    }
}