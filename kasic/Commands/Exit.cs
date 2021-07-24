using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Exit : Command
    {
        public Exit() : base("exit")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 0,
                MaxArgs = 0,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.VOID
                }),
                ReturnType = KasicType.VOID,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            Environment.Exit(0);
            return new ReturnObject(this);
        }
    }
}