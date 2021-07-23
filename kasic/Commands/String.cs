using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class String : Command
    {
        public String() : base("string")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 2,
                FieldType = KasicType.ANY,
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var args = ArgObject.AsAny();
            if (args.Length == 1)
            {
                return new ReturnObject(this, args[0]);
            }

            Heap.Push(args[0], args[1], KasicType.STRING);
            return new ReturnObject(this, args[1]);
        }
    }
}