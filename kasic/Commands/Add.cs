using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Add : Command
    {
        public Add() : base("add")
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
            double total = 0;
            for (int i = 0; i < arguments.Count; i++)
            { 
                var result = arguments.AsNumber(context, i);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                total += result.Value;
            }
            return new ReturnObject(this, total);
        }
    }
}