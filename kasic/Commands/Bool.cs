using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Bool : Command
    {
        public Bool() : base("bool")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 2,
                FieldType = KasicType.ANY,
                ReturnType = KasicType.BOOL,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var args = ArgObject.AsAny();

            if (args.Count == 1)
            {
                var singleBoolResult = Types.ToBool(context, args[0]);
                if (singleBoolResult.IsError)
                {
                    return Helpers.Error(singleBoolResult.Error);
                }
                return new ReturnObject(this, singleBoolResult.Value);
            }

            var multiArgResult = Types.ToBool(context, args[1]);
            if (multiArgResult.IsError)
            {
                return Helpers.Error(multiArgResult.Error);
            }

            Heap.Push(args[0], multiArgResult.Value, KasicType.BOOL);
            return new ReturnObject(this, multiArgResult.Value);
        }
    }
}