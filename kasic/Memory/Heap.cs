using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Kasic;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Memory
{
    public static class Heap
    {
        // private static Dictionary<string, Tuple<object, KasicType>> heap = new Dictionary<string, Tuple<object, KasicType>>();
        private static List<HeapObject> heap = new List<HeapObject>();

        public static Status<KasicError> Push(string name, object data, KasicType type)
        {
            for (int i = 0; i < heap.Count; i++)
            {
                var currentObject = heap[i];
                if (currentObject.Name.Equals(name))
                {
                    currentObject.Data = data;
                    heap[i] = currentObject;
                    return Helpers.Ok();
                }
            }
            
            heap.Add(new HeapObject
            {
                Data = data,
                Name = name,
                Type = type,
                ObjectId = heap.Count, // count before the data is added
            });
            return Helpers.Ok();
        }
        
        public static Result<HeapObject, KasicError> GetByName(Context context, string name)
        {
            foreach (var heapObject in heap)
            {
                if (heapObject.Name.Equals(name))
                {
                    return Helpers.Ok(heapObject);
                }
            }
            
            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"Field {name} is not set",
            });
        }
        
        public static Result<HeapObject, KasicError> GetByObjectId(Context context, int id)
        {
            if (heap.Count > id)
            {
                return Helpers.Ok(heap[id]);
            }
            
            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"ObjectID:{id} is not set",
            });
        }
    }

    public struct HeapObject
    {
        public int ObjectId;
        public string Name;
        public KasicType Type;
        public object Data;
    }
}