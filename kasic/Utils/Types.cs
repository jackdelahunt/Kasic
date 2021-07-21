using System;
using kasic.Commands;
using kasic.Kasic;
using kasic.Logging;
using OperationResult;

namespace kasic.Utils
{
    public static class Types
    {
        public static Result<double, KasicError> ToNumber(Context context, string text)
        {
            try
            {
                var number = Double.Parse(text);
                return Helpers.Ok(number);
            }
            catch
            {
                return Helpers.Error(new KasicError
                {
                    Message = $"Cannot convert {text} to a number",
                    Region = KasicRegion.UNKNOWN,
                    Command = context.Command
                });
            }
        }
        
        public static Result<bool, KasicError> ToBool(Context context, string text)
        {
            try
            {
                var boolean = Boolean.Parse(text);
                return Helpers.Ok(boolean);
            }
            catch
            {
                return Helpers.Error(new KasicError
                {
                    Message = $"Cannot convert {text} to a boolean",
                    Region = KasicRegion.UNKNOWN,
                    Command = context.Command
                });
            }
        }
    }
}