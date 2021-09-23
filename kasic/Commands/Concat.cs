using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Concat : Command
    {
        public Concat() : base("concat")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 4,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.STRING,
                    KasicType.STRING,
                    KasicType.STRING,
                }),
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            StringBuilder finalStr = new StringBuilder();
            for (int i = 0; i < arguments.Count; i++)
            { 
                var strResult = arguments.AsString(context, i);
                if (strResult.IsError)
                {
                    return Helpers.Error(strResult.Error);
                }

                finalStr.Append(strResult.Value);
            }
            return new ReturnObject(this, finalStr.ToString());
        }
    }
}