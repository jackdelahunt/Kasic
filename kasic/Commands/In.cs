using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class In : Command
    {
        public In() : base("in")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 0,
                MaxArgs = 0,
                ArgumentList = new ArgumentList(new List<KasicType>()),
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var input = Console.ReadLine();
            if (input is null)
                return new ReturnObject(this, "");

            return new ReturnObject(this, input.ToString());
        }
    }
}