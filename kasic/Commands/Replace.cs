using System;
using System.Collections.Generic;
using System.Globalization;
using kasic.Kasic;
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
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context)
        {
            var args = ArgObject.AsStrings();
            var oldValue = args[0];
            var newValue = args[1];
            var text = args[2];

            text = text.Replace(oldValue, newValue, Flags.Contains("-i"), CultureInfo.InvariantCulture);

            return new ReturnObject(this, text);
        }
    }
}