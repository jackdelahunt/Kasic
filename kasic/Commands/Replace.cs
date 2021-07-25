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
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.STRING,
                    KasicType.STRING
                }),
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, ArgObject argObject, List<string> flags)
        {
            var oldValue = argObject.AsString(0);
            var newValue = argObject.AsString(1);
            var text = argObject.AsString(2);

            text = text.Replace(oldValue, newValue, flags.Contains("-i"), CultureInfo.InvariantCulture);

            return new ReturnObject(this, text);
        }
    }
}