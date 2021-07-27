using System;
using kasic.Memory;
using OperationResult;

namespace kasic.Kasic
{
    public class KasicObject
    {
        public object Value { get; private set; }
        public KasicType Type;
        public bool IsDynamic;

        public KasicObject(object value, KasicType type, bool isDynamic = false)
        {
            if (isDynamic && !(value is string))
            { 
                throw new ArgumentException("Dynamic types must be set as strings");
            }
            
            this.Value = value;
            this.Type = type;
            this.IsDynamic = isDynamic;
        }

        public Result<object, KasicError> GetValue(Context context)
        {
            if (!IsDynamic)
                return Value;

            // look for value in heap
            var heapLookupResult = Heap.Reference(context, GetDynamicValueName());
            if (heapLookupResult.IsError)
            {
                return Helpers.Error(heapLookupResult.Error);
            }

            // match types
            var (heapData, heapType) = heapLookupResult.Value;
            if (heapType != Type)
            {
                return Helpers.Error(new KasicError
                {
                    Context = context,
                    Message = $"Type mismatch, {GetDynamicValueName()} is of type {heapType} but expected {Type}"
                });
            }
            
            // return heap value
            return Helpers.Ok(heapData);
        }

        private string GetDynamicValueName()
        {
            if (IsDynamic && Value is string @string)
                return @string;

            throw new Exception($"{Value} is not a variable");
        }
    }
}