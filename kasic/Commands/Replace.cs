using System;
using System.Collections.Generic;
using System.Globalization;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Replace : Command
    {
        public Replace() : base("replace")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 3,
                MaxArgs =  3,
                FieldType = KasicType.STRING,
                ReturnType = KasicType.STRING,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run()
        {
            var oldValue = Args[0];
            var newValue = Args[1];
            var text = Args[2];

            text = text.Replace(oldValue, newValue, Flags.Contains("-i"), CultureInfo.InvariantCulture);

            return Helpers.Ok(text);
        }
    }
}