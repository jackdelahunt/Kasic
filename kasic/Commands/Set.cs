using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Set : Command
    {
        public Set() : base("set")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                FieldType = KasicType.ANY,
                ReturnType = KasicType.ANY,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var args = ArgObject.AsAny();
            var name = args[0];
            var value = args[1];

            Heap.Push(name, value);
            return new ReturnObject(this, value);
        }
    }
}