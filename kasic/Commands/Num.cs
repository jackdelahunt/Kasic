using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Num : Command
    {
        public Num() : base("num")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 2,
                FieldType = KasicType.ANY,
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var args = ArgObject.AsAny();

            if (args.Length == 1)
            {
                var singleNumberResult = Types.ToNumber(context, args[0]);
                if (singleNumberResult.IsError)
                {
                    return Helpers.Error(singleNumberResult.Error);
                }
                return new ReturnObject(this, singleNumberResult.Value);
            }

            var multiArgResult = Types.ToNumber(context, args[1]);
            if (multiArgResult.IsError)
            {
                return Helpers.Error(multiArgResult.Error);
            }

            Heap.Push(args[0], multiArgResult.Value, KasicType.NUMBER);
            return new ReturnObject(this, multiArgResult.Value);
        }
    }
}