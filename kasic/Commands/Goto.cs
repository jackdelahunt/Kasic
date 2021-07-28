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
            var arg = arguments.AsString(context, 0);
            if (arg.IsError)
            {
                return Helpers.Error(arg.Error);
            }

            var argKasicObject = arguments.GetKasicObject(0);
            // if the object id is not linked to the heap then link
            if (argKasicObject.ObjectId < 0)
            {
                var result = Scope.FindGotoScopeObjectId(context, arg.Value);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                argKasicObject.ObjectId = result.Value;
            }
            
            // once linked get the data on the heap
            var findByIdResult = Scope.GetScopeById(context, argKasicObject.ObjectId);
            if (findByIdResult.IsError)
            {
                return Helpers.Error(findByIdResult.Error);
            }

            context.LineNumber = findByIdResult.Value;


            return new ReturnObject(this);
        }
    }
}