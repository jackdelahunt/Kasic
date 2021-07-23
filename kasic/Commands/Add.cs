using System;
using System.Collections.Generic;
using kasic.Kasic;
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
            };
        }

        public override Result<string, KasicError> Run(Context context)
        {
            var args = ArgObject.AsNumbers();
            double total = 0;
            foreach (var arg in args)
            {
                total += arg;
            }

            return Helpers.Ok(total.ToString());
        }
    }
}