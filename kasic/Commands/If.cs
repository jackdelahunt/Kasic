using System;
using System.Collections.Generic;
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
                MaxArgs = 2,
                FieldType = KasicType.BOOL,
                ReturnType = KasicType.VOID,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            var scopeName = Args[0];
            var condition = Args[1];
            var result = Types.ToBool(context, condition);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }

            if (result.Value)
            {
                var scopeResult = Scope.FindGotoScope(context, scopeName);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                context.Reader.MovePointer(scopeResult.Value);
                return Helpers.Ok("");
            }

            return Helpers.Ok("");
        }
    }
}