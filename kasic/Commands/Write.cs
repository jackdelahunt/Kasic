using System;
using System.Collections.Generic;
using System.IO;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Write : Command
    {
        public Write() : base("write")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.ANY
                }),
                ReturnType = KasicType.VOID,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var path = arguments.AsString(context, 0);
            if (path.IsError)
            {
                return Helpers.Error(path.Error);
            }
            
            var data = arguments.AsAny(context, 1);
            if (data.IsError)
            {
                return Helpers.Error(data.Error);
            }
            
            WriteToFile(path.Value, data.Value, flags.Contains("-a"));

            return new ReturnObject(this);
        }

        public async void WriteToFile(string path, string data, bool append)
        {
            await using StreamWriter file = new(path, append: append);
            await file.WriteLineAsync(data);
        }
    }
}