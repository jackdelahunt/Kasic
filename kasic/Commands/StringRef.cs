using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class StringRef : Command
    {
        public StringRef() : base("&string")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.STRING,
                }),
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var name = arguments.AsString(context, 0);
            if (name.IsError)
            {
                return Helpers.Error(name.Error);
            }
            
            var value = arguments.AsString(context, 1);
            if (value.IsError)
            {
                return Helpers.Error(value.Error);
            }
            
            Heap.Push(name.Value, value.Value, KasicType.STRING);
            return new ReturnObject(this, value.Value);
        }
    }
}