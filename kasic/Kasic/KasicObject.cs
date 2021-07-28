using System;
using kasic.Memory;
using OperationResult;

namespace kasic.Kasic
{
    public class KasicObject
    {
        public object Value { get; private set; }
        public KasicType Type;
        
        // if the object is piped into an argument this is false
        public bool IsNative;
        
        // if the value for this is stored on the heap
        public bool IsDynamic;
        
        // object id of the heap object
        public int ObjectId = -1;


        public KasicObject(object value, KasicType type, bool isDynamic = false, bool isNative = true)
        {
            if (isDynamic && !(value is string))
            { 
                throw new ArgumentException("Dynamic types must be set as strings");
            }
            
            this.Value = value;
            this.Type = type;
            this.IsDynamic = isDynamic;
            this.IsNative = isNative;
        }

        public Result<object, KasicError> GetValue(Context context)
        {
            if (!IsDynamic)
                return Value;

            if (ObjectId != -1)
            {
                var heapObjectById = Heap.GetByObjectId(ObjectId);
                return Helpers.Ok(heapObjectById.Data);
            }

            // look for value in heap
            var heapGetByNameResult = Heap.GetByName(context, GetDynamicValueName());
            if (heapGetByNameResult.IsError)
            {
                return Helpers.Error(heapGetByNameResult.Error);
            }

            // match types
            var heapObject = heapGetByNameResult.Value;
            if (!heapObject.Type.Equals(Type))
            {
                return Helpers.Error(new KasicError
                {
                    Context = context,
                    Message = $"Type mismatch, {GetDynamicValueName()} is of type {heapObject.Type} but expected {Type}"
                });
            }

            ObjectId = heapObject.ObjectId;
            
            // return heap value
            return Helpers.Ok(heapObject.Data);
        }

        private string GetDynamicValueName()
        {
            if (Value is string @string)
                return @string;

            throw new Exception($"{Value} is not a variable");
        }
    }
}