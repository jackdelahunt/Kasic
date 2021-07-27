using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class NumRef : Command
    {
        public NumRef() : base("&num")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.NUMBER,
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var name = arguments.AsString(context, 0);
            if (name.IsError)
            {
                return Helpers.Error(name.Error);
            }
            
            var value = arguments.AsNumber(context, 1);
            if (value.IsError)
            {
                return Helpers.Error(value.Error);
            }
            
            Heap.Push(name.Value, value.Value, KasicType.NUMBER);
            return new ReturnObject(this, value.Value);
        }
    }
}