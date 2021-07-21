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
                FieldType = KasicType.VOID,
                ReturnType = KasicType.VOID,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            Environment.Exit(0);
            return Helpers.Ok("");
        }
    }
}