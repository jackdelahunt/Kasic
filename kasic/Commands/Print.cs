using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Print : Command
    {
        public Print() : base("print")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = UInt32.MaxValue,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.ANY,
                    KasicType.ANY,
                    KasicType.ANY,
                    KasicType.ANY
                }),
                ReturnType = KasicType.VOID,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            for (int i = 0; i < arguments.Count; i++)
            {
                var arg = arguments.AsAny(context, i);
                if (arg.IsError)
                {
                    return Helpers.Error(arg.Error);
                }    
                
                if (flags.Contains("-c"))
                {
                    Logger.Log(arg.Value);
                }
                else
                {
                    Logger.Log(arg.Value + " ");
                }
            }

            Logger.Logln("");
            return new ReturnObject(this);
        }
    }
}