using System;
using kasic.Commands;
using kasic.Kasic;
using csFastFloat;
using OperationResult;

namespace kasic.Utils
{
    public static class Types
    {
        public static Result<double, KasicError> ToNumber(Context context, string text)
        {
            if (FastDoubleParser.TryParseDouble(text, out var value))
            {
                return Helpers.Ok(value);
            }
            return Helpers.Error(new KasicError
            {
                Message = $"Cannot convert '{text}' to a number",
                Context = context
            });
        }
        
        public static Result<double, KasicError> ToNumber(Context context, bool value)
        {
            return value ? 1 : 0;
        }
        
        public static Result<bool, KasicError> ToBool(Context context, string text)
        {
            bool value;
            switch(text.ToLower())
            {
                case "0":
                    value = false; break;
                case "1":
                    value = true; break;
                case "false":
                    value = false; break;
                case "true":
                    value = true; break;
                default:
                    return Helpers.Error(new KasicError
                    {
                        Context = context,
                        Message = $"{text} cannot be converted to a bool"
                    }); break;
            }

            return value;
        }
    }
}