using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Strlen : Command
    {
        public Strlen() : base("strlen")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 1,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var strResult = arguments.AsString(context, 0);
            if (strResult.IsError)
            {
                return Helpers.Error(strResult.Error);
            }
            
            return new ReturnObject(this, strResult.Value.Length);
        }
    }
}