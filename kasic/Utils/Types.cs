using System;
using kasic.Commands;
using kasic.Logging;
using OperationResult;

namespace kasic.Utils
{
    public static class Types
    {
        public static Result<double, KasicError> ToNumber(string text)
        {
            try
            {
                var number = Double.Parse(text);
                return Helpers.Ok(number);
            }
            catch (Exception e)
            {
                return Helpers.Error(new KasicError
                {
                    Message = $"Cannot convert {text} to a number",
                    Region = KasicRegion.UNKNOWN
                });
            }
        }
    }
}