using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class In : Command
    {
        public In() : base("in")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 0,
                MaxArgs = 1,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.ANY,
                }),
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            if (arguments.Count > 0)
            {
                var annotationResult = arguments.AsAny(context, 0);
                if (annotationResult.IsError)
                {
                    return Helpers.Error(annotationResult.Error);
                }

                if (flags.Contains("-l"))
                {
                    Logger.Log(annotationResult.Value.ToString());
                }
                else
                {
                    Logger.Logln(annotationResult.Value.ToString());
                }
            }
            var input = Console.ReadLine();
            if (input is null)
                return new ReturnObject(this, "");

            return new ReturnObject(this, input.ToString());
        }
    }
}