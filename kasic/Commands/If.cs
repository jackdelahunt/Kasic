using System;
using System.Collections.Generic;
using System.Linq;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class If : Command
    {
        public If() : base("if")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 3,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.BOOL,
                    KasicType.STRING,
                    KasicType.STRING,
                }),
                ReturnType = KasicType.VOID,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var condition = ArgObject.AsBool(0);
            var ifResult = ArgObject.AsString(1);

            if (condition)
            {
                var ifGotoResult = Scope.FindGotoScope(context, ifResult);
                if (ifGotoResult.IsError)
                {
                    return Helpers.Error(ifGotoResult.Error);
                }
                
                context.Reader.MovePointer(ifGotoResult.Value);
            }
            else
            {
                if (ArgObject.Count > 2)
                {
                    var elseResult = ArgObject.AsString(2);
                    var elseGotoResult = Scope.FindGotoScope(context, elseResult);
                    if (elseGotoResult.IsError)
                    {
                        return Helpers.Error(elseGotoResult.Error);
                    }
                
                    context.Reader.MovePointer(elseGotoResult.Value);
                }
            }

            return new ReturnObject(this);
        }
    }
}