#nullable enable
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

        public static Result<int, KasicError> Push(Context context, string name, object data, KasicType type)
        {
            int assignedObjectId = heap.Count;
            heap.Add(new HeapObject
            {
                Data = data,
                Name = name,
                Type = type,
                ObjectId = assignedObjectId, // count before the data is added
            });
            
            return Helpers.Ok(assignedObjectId);
        }

        public static void Update(Context context, int objectId, object data,
            KasicType type, out KasicError error)
        {
        
            if (objectId < 0 || objectId >= heap.Count)
            {
                error = new KasicError
                {
                    Context = context,
                    Message = $"ObjectId {objectId} has not yet been allocated on the heap"
                };
            }
            
            var heapObject = heap[objectId];

            if (heapObject.Type != type)
            {
                error = new KasicError
                {
                    Context = context,
                    Message = $"{type} does not match the type on the heap {heapObject.Type}"
                };
            }
            
            heapObject.Data = data;
            heap[objectId] = heapObject;
            error = null;
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