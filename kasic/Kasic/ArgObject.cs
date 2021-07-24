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

        public ArgObject(List<object> arguments, ArgumentList argumentList)
        {
            this.argumentList = argumentList;
            this.arguments = arguments;
        }

        /*
         * Verifies arguments match the argument list given
         */
        public Status<KasicError> Build(Context context)
        {
            List<object> builtArguments = new List<object>();
            for (int i = 0; i < this.arguments.Count; i++)
            {
                switch (this.argumentList.argumentTypes[i])
                {
                    case KasicType.NUMBER:
                        var toNumberResult = ToNumber(context, this.arguments[i]);
                        if (toNumberResult.IsError)
                        {
                            return Helpers.Error(toNumberResult.Error);
                        }
                        builtArguments.Add(toNumberResult.Value); break;
                    case KasicType.BOOL:
                        var toBoolResult = ToBool(context, this.arguments[i]);
                        if (toBoolResult.IsError)
                        {
                            return Helpers.Error(toBoolResult.Error);
                        }
                        builtArguments.Add(toBoolResult.Value); break;
                    default:
                        builtArguments.Add(this.arguments[i]); break;
                }
            }

            this.arguments = builtArguments;

            return Helpers.Ok();
        }

        public static Result<ArgObject, KasicError> New(Context context, List<object> arguments, ArgumentList argumentList)
        {
            var arg = new ArgObject(arguments, argumentList);
            var buildResult = arg.Build(context);
            if (buildResult.IsError)
            {
                return Helpers.Error(buildResult.Error);
            }
            return arg;
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

        private Result<double, KasicError> ToNumber(Context context, object arg)
        {
            if (arg is double @num)
            {
                return @num;
            }
            
            if (arg is string @str)
            {
                var stringCastResult = Types.ToNumber(context, @str);
                if (stringCastResult.IsError)
                {
                    return Helpers.Error(stringCastResult.Error);
                }

                return Helpers.Ok(stringCastResult.Value);
            }

            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"{arg} is not NUMBER, BOOL or STRING"
            });
        }
        
        private Result<bool, KasicError> ToBool(Context context, object arg)
        {
            if (arg is bool @bool)
            {
                return @bool;
            }
            
            if (arg is string @str)
            {
                var stringCastResult = Types.ToBool(context, @str);
                if (stringCastResult.IsError)
                {
                    return Helpers.Error(stringCastResult.Error);
                }

                return Helpers.Ok(stringCastResult.Value);
            }
            
            

            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"{arg} is not NUMBER, BOOL or STRING"
            });
        }
        
        private void  Panic(object toReturn)
        {
            throw new ArgumentException($"Panic when returning this {toReturn}");
        }
    }
}