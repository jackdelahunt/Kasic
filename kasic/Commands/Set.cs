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

        public override Result<string, KasicError> Run(Context context)
        {
            var name = Args[0];
            var value = Args[1];

            Heap.Push(name, value);
            return Helpers.Ok(value);
        }
    }
}