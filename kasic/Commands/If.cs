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

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var condition = arguments.AsBool(context, 0);
            if (condition.IsError)
            {
                return Helpers.Error(condition.Error);
            }
            
            var ifResult = arguments.AsString(context, 1);
            if (ifResult.IsError)
            {
                return Helpers.Error(ifResult.Error);
            }

            if (condition.Value)
            {
                var ifGotoResult = Scope.FindGotoScope(context, ifResult.Value);
                if (ifGotoResult.IsError)
                {
                    return Helpers.Error(ifGotoResult.Error);
                }
                
                context.LineNumber = ifGotoResult.Value;
            }
            else
            {
                if (arguments.Count > 2)
                {
                    var elseResult = arguments.AsString(context, 2);
                    if (elseResult.IsError)
                    {
                        return Helpers.Error(elseResult.Error);
                    }
                    
                    var elseGotoResult = Scope.FindGotoScope(context, elseResult.Value);
                    if (elseGotoResult.IsError)
                    {
                        return Helpers.Error(elseGotoResult.Error);
                    }
                
                    context.LineNumber = elseGotoResult.Value;
                }
            }

            return new ReturnObject(this);
        }
    }
}