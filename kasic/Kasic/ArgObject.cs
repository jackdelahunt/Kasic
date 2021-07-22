using System;
using System.Collections.Generic;
using kasic.Utils;
using OperationResult;

namespace kasic.Kasic
{
    public class ArgObject
    {
        private KasicType type;
        private object arguments;

        public ArgObject(string[] args, KasicType type)
        {
            this.type = type;
            arguments = type switch
            {
                KasicType.NUMBER => ToNumbers(args),
                KasicType.BOOL => ToBools(args),
                _ => args,
                
            };

        }

        public static Result<ArgObject, KasicError> New(Context context ,string[] args, KasicType type)
        {
            try
            {
                return Helpers.Ok(new ArgObject(args, type));
                
            }
            catch (Exception e)
            {
                return Helpers.Error(new KasicError
                {
                    Command = context.Command,
                    Message = e.Message
                });
            }
        }

        public double[] AsNumbers()
        {
            return arguments as double[];
        }
        
        public bool[] AsBools()
        {
            return arguments as bool[];
        }

        private double[] ToNumbers(string[] args)
        {
            var numbers = new List<double>();
            foreach (var arg in args)
            {
                numbers.Add(Double.Parse(arg));
            }

            return numbers.ToArray();
        }
        
        private bool[] ToBools(string[] args)
        {
            var numbers = new List<bool>();
            foreach (var arg in args)
            {
                numbers.Add(Boolean.Parse(arg));
            }

            return numbers.ToArray();
        }
    }
}