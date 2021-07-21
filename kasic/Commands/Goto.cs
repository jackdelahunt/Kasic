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
                FieldType = KasicType.STRING,
                ReturnType = KasicType.VOID,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            var result = Scope.FindGotoScope(context, Args[0]);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }

            context.Reader.MovePointer(result.Value);
            return Helpers.Ok("");
        }
    }
}