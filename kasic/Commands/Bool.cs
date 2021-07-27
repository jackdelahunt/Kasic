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
                MaxArgs = 1,
                ArgumentList = new ArgumentList( new List<KasicType>()
                {
                    KasicType.ANY
                }),
                ReturnType = KasicType.BOOL,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var arg = arguments.AsAny(context, 0);
            if (arg.IsError)
            {
                return Helpers.Error(arg.Error);
            }
            
            var result = Types.ToBool(context, arg.Value);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }
            return new ReturnObject(this, result.Value);
        }
    }
}