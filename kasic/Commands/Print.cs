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
                    KasicType.ANY
                }),
                ReturnType = KasicType.VOID,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var arg = ArgObject.AsAny(0);
            if (Flags.Contains("-c"))
            {
                Logger.Log(arg);
            }
            else
            {
                Logger.Log(arg + " ");
            }
            Logger.Logln("");
            return new ReturnObject(this);
        }
    }
}