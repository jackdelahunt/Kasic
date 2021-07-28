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
            
            var ifResultName = arguments.AsString(context, 1);
            if (ifResultName.IsError)
            {
                return Helpers.Error(ifResultName.Error);
            }

            if (condition.Value)
            {
                var ifArgumentKasicObject = arguments.GetKasicObject(1);
                // if kasic object has not been linked to the heap yet
                if (ifArgumentKasicObject.ObjectId < 0)
                {
                    // find value on the heap
                    var ifFindGotoScopeResult =
                        Scope.FindGotoScopeObjectId(context, ifResultName.Value);
                    if (ifFindGotoScopeResult.IsError)
                    {
                        return Helpers.Error(ifFindGotoScopeResult.Error);
                    }

                    // link object id with the heap
                    ifArgumentKasicObject.ObjectId = ifFindGotoScopeResult.Value;
                }

                // change line number based on scope
                var ifFindByIdResult = Scope.GetScopeById(context, ifArgumentKasicObject.ObjectId);
                if (ifFindByIdResult.IsError)
                {
                    return Helpers.Error(ifFindByIdResult.Error);
                }
                
                context.LineNumber = ifFindByIdResult.Value;
                return new ReturnObject(this);
            }

            if (arguments.Count > 2)
            {
                var elseResultName = arguments.AsString(context, 2);
                if (elseResultName.IsError)
                {
                    return Helpers.Error(ifResultName.Error);
                }
                var elseArgumentKasicObject = arguments.GetKasicObject(2);
                
                // if kasic object has not been linked to the heap yet
                if (elseArgumentKasicObject.ObjectId < 0)
                {
                    // find value on the heap
                    var elseFindGotoScopeResult =
                        Scope.FindGotoScopeObjectId(context, ifResultName.Value);
                    if (elseFindGotoScopeResult.IsError)
                    {
                        return Helpers.Error(elseFindGotoScopeResult.Error);
                    }
                    
                    // link object id with the heap
                    elseArgumentKasicObject.ObjectId = elseFindGotoScopeResult.Value;
                }

                // change line number based on scope
                var elseFindByIdResult = Scope.GetScopeById(context, elseArgumentKasicObject.ObjectId);
                if (elseFindByIdResult.IsError)
                {
                    return Helpers.Error(elseFindByIdResult.Error);
                }
                
                context.LineNumber = elseFindByIdResult.Value;
                return new ReturnObject(this);
            }
            
            return new ReturnObject(this);
        }
    }
}