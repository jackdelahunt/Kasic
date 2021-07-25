using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class BoolRef : Command
    {
        public BoolRef() : base("&bool")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.BOOL,
                }),
                ReturnType = KasicType.BOOL,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, ArgObject argObject, List<string> flags)
        {
            var name = argObject.AsString(0);
            var value = argObject.AsBool(1);
            Heap.Push(name, value, KasicType.BOOL);
            return new ReturnObject(this, value);
        }
    }
}