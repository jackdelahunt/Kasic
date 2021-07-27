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

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var oldValue = arguments.AsString(context, 0);
            if (oldValue.IsError)
            {
                return Helpers.Error(oldValue.Error);
            }
            
            var newValue = arguments.AsString(context, 1);
            if (newValue.IsError)
            {
                return Helpers.Error(newValue.Error);
            }
            
            var text = arguments.AsString(context, 2);
            if (text.IsError)
            {
                return Helpers.Error(text.Error);
            }

            var replacedText = text.Value.Replace(oldValue.Value, newValue.Value, flags.Contains("-i"), CultureInfo.InvariantCulture);

            return new ReturnObject(this, replacedText);
        }
    }
}