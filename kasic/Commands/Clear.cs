using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using OperationResult;

namespace kasic.Commands
{
    public class Clear : Command
    {
        public Clear() : base("clear")
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

        public override Result<IReturnObject, KasicError> Run(Context context, ArgObject argObject, List<string> flags)
        {
            Console.Clear();
            return new ReturnObject(this);
        }
    }
}