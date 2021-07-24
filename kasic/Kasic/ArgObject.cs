using System;
using System.Collections.Generic;
using System.Linq;
using kasic.Utils;
using OperationResult;

namespace kasic.Kasic
{
    public class ArgObject
    {
        private ArgumentList argumentList;
        private List<object> arguments;
        public int Count => arguments.Count;

        public ArgObject(List<string> args, ArgumentList argumentList)
        {
            this.argumentList = argumentList;
            this.arguments = new List<object>();
            for (int i = 0; i < args.Count; i++)
            {
                switch (this.argumentList.argumentTypes[i])
                {
                    case KasicType.NUMBER:
                        this.arguments.Add(ToNumber(args[i])); break;
                    case KasicType.BOOL:
                        this.arguments.Add(ToBool(args[i])); break;
                    default:
                        this.arguments.Add(args[i]); break;
                }
            }
        }

        public static Result<ArgObject, KasicError> New(Context context, List<string> args, ArgumentList argumentList)
        {
            try
            {
                return Helpers.Ok(new ArgObject(args, argumentList));
                
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

        public double AsNumber(int index)
        {
            if (this.arguments[index] is double @return)
                return @return;
            
            Panic(this.arguments[index]);
            return 0;
        }
        
        public bool AsBool(int index)
        {
            if (this.arguments[index] is bool @return)
                return @return;
            
            Panic(this.arguments[index]);
            return false;
        }
        
        public string AsString(int index)
        {
            if (this.arguments[index] is string @return)
                return @return;
            
            Panic(this.arguments[index]);
            return "";
        }
        
        public string AsAny(int index)
        {
            return this.arguments[index].ToString();
        }

        public Status<KasicError> PipeReturn(Context context, IReturnObject returnObject)
        {
            switch(this.argumentList.argumentTypes[this.arguments.Count])
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
            this.arguments.Add(str);
            return Helpers.Ok();
        }

        private Status<KasicError> AddAsString(Context context, string any)
        {
            this.arguments.Add(any);
            return Helpers.Ok();
        }

        private Status<KasicError> AddAsNumber(Context context, double number)
        {
            this.arguments.Add(number);
            return Helpers.Ok();
        }
        
        private Status<KasicError> AddAsBool(Context context, bool value)
        {
            this.arguments.Add(value);
            return Helpers.Ok();
        }

        private double ToNumber(string arg)
        {
            return Double.Parse(arg);
        }
        
        private bool ToBool(string arg)
        {
            return Boolean.Parse(arg);
        }
        
        private void  Panic(object toReturn)
        {
            throw new ArgumentException($"Panic when returning this {toReturn}");
        }
    }
}