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
                _ => args.ToArray(),
                
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

        public double[] AsNumbers()
        {
            return arguments as double[];
        }
        
        public bool[] AsBools()
        {
            return arguments as bool[];
        }
        
        public string[] AsStrings()
        {
            return arguments as string[];
        }
        
        public string[] AsAny()
        {
            return arguments as string[];
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
            arguments = AsAny().Append(str).ToArray();
            Count++;
            return Helpers.Ok();
        }

        private Status<KasicError> AddAsString(Context context, string str)
        {
            arguments = AsStrings().Append(str).ToArray();
            Count++;
            return Helpers.Ok();
        }

        private Status<KasicError> AddAsNumber(Context context, double value)
        {
            arguments = AsNumbers().Append(value).ToArray();
            Count++;
            return Helpers.Ok();
        }
        
        private Status<KasicError> AddAsBool(Context context, bool value)
        {
            arguments = AsBools().Append(value).ToArray();
            Count++;
            return Helpers.Ok(); 
        }

        private double[] ToNumbers(List<string> args)
        {
            var numbers = new List<double>();
            foreach (var arg in args)
            {
                numbers.Add(Double.Parse(arg));
            }

            return numbers.ToArray();
        }
        
        private bool[] ToBools(List<string> args)
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