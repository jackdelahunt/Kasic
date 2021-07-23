using System;
using System.Collections.Generic;
using System.Linq;
using kasic.Utils;
using OperationResult;

namespace kasic.Kasic
{
    public class ArgObject
    {
        private KasicType type;
        private object arguments;
        public int Count { get; private set; }

        public ArgObject(List<string> args, KasicType type)
        {
            this.type = type;
            arguments = type switch
            {
                KasicType.NUMBER => ToNumbers(args),
                KasicType.BOOL => ToBools(args),
                _ => args,
                
            };

            Count = args.Count;
        }

        public static Result<ArgObject, KasicError> New(Context context, List<string> args, KasicType type)
        {
            try
            {
                return Helpers.Ok(new ArgObject(args, type));
                
            }
            catch (Exception e)
            {
                return Helpers.Error(new KasicError
                {
                    Context = context,
                    Message = e.Message
                });
            }
        }

        public List<double> AsNumbers()
        {
            return arguments as List<double>;
        }
        
        public List<bool> AsBools()
        {
            return arguments as List<bool>;
        }
        
        public List<string> AsStrings()
        {
            return arguments as List<string>;
        }
        
        public List<string> AsAny()
        {
            return arguments as List<string>;
        }

        public Status<KasicError> PipeReturn(Context context, IReturnObject returnObject)
        {
            switch(this.type)
            {
                case KasicType.NUMBER:
                    return AddAsNumber(context, returnObject.AsNumber()); break;
                case KasicType.BOOL:
                    return AddAsBool(context, returnObject.AsBool()); break;
                case KasicType.ANY:
                    return AddAsAny(context, returnObject.AsAny()); break;
                case KasicType.STRING:
                    return AddAsString(context, returnObject.AsString()); break;
                default:
                    throw new ArgumentException("Tried to pipe VOID");
            };
        }
        
        private Status<KasicError> AddAsAny(Context context, string str)
        {
            var args = AsAny();
            args.Add(str);
            arguments = args;
            Count++;
            return Helpers.Ok();
        }

        private Status<KasicError> AddAsString(Context context, string any)
        {
            var args = AsStrings();
            args.Add(any);
            arguments = args;
            Count++;
            return Helpers.Ok();
        }

        private Status<KasicError> AddAsNumber(Context context, double value)
        {
            var args = AsNumbers();
            args.Add(value);
            arguments = args;
            Count++;
            return Helpers.Ok();
        }
        
        private Status<KasicError> AddAsBool(Context context, bool value)
        {
            var args = AsBools();
            args.Add(value);
            arguments = args;
            Count++;
            return Helpers.Ok();
        }

        private List<double> ToNumbers(List<string> args)
        {
            var numbers = new List<double>();
            foreach (var arg in args)
            {
                numbers.Add(Double.Parse(arg));
            }

            return numbers;
        }
        
        private List<bool> ToBools(List<string> args)
        {
            var numbers = new List<bool>();
            foreach (var arg in args)
            {
                numbers.Add(Boolean.Parse(arg));
            }

            return numbers;
        }
    }
}