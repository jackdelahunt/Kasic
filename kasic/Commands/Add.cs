using System;
using System.Collections.Generic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class Add : Command
    {
        public Add() : base("add")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = UInt32.MaxValue,
                FieldType = KasicType.NUMBER,
                ReturnType = KasicType.NUMBER,
                DefinedFields = null,
            };
        }

        public override Result<string, KasicError> Run()
        {
            double total = 0;
            foreach (var arg in Args)
            {
                var result = Types.ToNumber(arg);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                total += result.Value;
            }

            return Helpers.Ok(total.ToString());
        }
    }
}