using System;
using System.Collections.Generic;
using System.Linq;
using kasic.Utils;
using OperationResult;

namespace kasic.Kasic
{
    public class Arguments
    {
        private ArgumentList argumentList;
        private List<KasicObject> argumentObjects;
        public int Count => argumentObjects.Count;

        public Arguments(ArgumentList argumentList)
        {
            this.argumentList = argumentList;
            this.argumentObjects = new List<KasicObject>(5);
        }

        /*
         * Verifies arguments match the argument list given
         */
        public Status<KasicError> Build(Context context, List<object> arguments)
        {
            for (int i = 0; i < arguments.Count; i++)
            {
                if (IsFieldDynamic(arguments[i], out var name))
                {
                    argumentObjects.Add(new KasicObject(name, argumentList.argumentTypes[i], true));
                    continue;
                }
                
                switch (this.argumentList.argumentTypes[i])
                {
                    case KasicType.NUMBER:
                        var toNumberResult = ToNumber(context, arguments[i]);
                        if (toNumberResult.IsError)
                        {
                            return Helpers.Error(toNumberResult.Error);
                        }
                        argumentObjects.Add(new KasicObject(toNumberResult.Value, argumentList.argumentTypes[i]));
                        break;
                    case KasicType.BOOL:
                        var toBoolResult = ToBool(context, arguments[i]);
                        if (toBoolResult.IsError)
                        {
                            return Helpers.Error(toBoolResult.Error);
                        }
                        argumentObjects.Add(new KasicObject(toBoolResult.Value, argumentList.argumentTypes[i]));
                        break;
                    default:
                        argumentObjects.Add(new KasicObject(arguments[i], argumentList.argumentTypes[i]));
                        break;
                }
            }
            
            return Helpers.Ok();
        }

        public static Result<Arguments, KasicError> New(Context context, List<object> arguments, ArgumentList argumentList)
        {
            var arg = new Arguments(argumentList);
            var buildResult = arg.Build(context, arguments);
            if (buildResult.IsError)
            {
                return Helpers.Error(buildResult.Error);
            }
            return arg;
        }
        
        public KasicObject GetKasicObject(int index)
        {
            return argumentObjects[index];
        } 

        public Result<double, KasicError> AsNumber(Context context, int index)
        {
            var result = argumentObjects[index].GetValue(context);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }

            if (result.Value is double @double)
            {
                return Helpers.Ok(@double);
            }

            throw new Exception($"Used as number for non-number value {result.Value.GetType()}");
        }
        
        public Result<bool, KasicError> AsBool(Context context, int index)
        {
            var result = argumentObjects[index].GetValue(context);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }

            if (result.Value is bool @bool)
            {
                return Helpers.Ok(@bool);
            }

            throw new Exception($"Used as bool for non-bool value {result.Value.GetType()}");
        }
        
        public Result<string, KasicError> AsString(Context context, int index)
        {
            var result = argumentObjects[index].GetValue(context);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }

            if (result.Value is string @string)
            {
                return Helpers.Ok(@string);
            }

            throw new Exception($"Used as string for non-string value {result.Value.GetType()}");
        }
        
        public Result<string, KasicError> AsAny(Context context, int index)
        {
            var result = argumentObjects[index].GetValue(context);
            if (result.IsError)
            {
                return Helpers.Error(result.Error);
            }

            return Helpers.Ok(result.Value.ToString());
        }

        public void PipeReturn(IReturnObject returnObject)
        {
            // TODO: make the type check here maybe?
            if (argumentObjects.Count > 0)
            {
                if (!argumentObjects[^1].IsNative)
                {
                    argumentObjects.RemoveAt(argumentObjects.Count - 1);
                }
            }
            argumentObjects.Add(returnObject.AsKasicObject());
        }

        private bool IsFieldDynamic(object value, out string? name)
        {
            if (value is string @string && @string[0] == '*')
            {
                name = @string.Substring(1);
                return true;
            }

            name = null;
            return false;
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