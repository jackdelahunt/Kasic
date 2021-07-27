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
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.ANY,
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var arg = arguments.AsAny(context, 0);
            if (arg.IsError)
            {
                return Helpers.Error(arg.Error);
            }
            
            var result = Types.ToNumber(context, arg.Value);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }
            return new ReturnObject(this, result.Value);
        }
    }
}