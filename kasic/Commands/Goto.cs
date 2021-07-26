using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Goto : Command
    {
        public Goto() : base("goto")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 1,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING
                }),
                ReturnType = KasicType.VOID,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var arg = arguments.AsString(0);
            var result = Scope.FindGotoScope(context, arg);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }

            context.LineNumber = result.Value;
            return new ReturnObject(this);
        }
    }
}