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
                MinArgs = 1,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.NUMBER,
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var name = ArgObject.AsString(0);
            var value = ArgObject.AsNumber(1);
            Heap.Push(name, value, KasicType.NUMBER);
            return new ReturnObject(this, value);
        }
    }
}