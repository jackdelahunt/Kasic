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
        private static Dictionary<string, Tuple<object, KasicType>> heap = new Dictionary<string, Tuple<object, KasicType>>();

        public static Status<KasicError> Push(string name, object data, KasicType type)
        {
            if (heap.ContainsKey(name))
            {
                // TODO: this looks dumb
                heap.Remove(name);
            }

            heap.Add(name, new Tuple<object, KasicType>(data, type));
            return Helpers.Ok();
        }
        
        public static Result<Tuple<object, KasicType>, KasicError> Reference(Context context, string name)
        {
            if (heap.TryGetValue(name, out var data))
            {
                return Helpers.Ok(data);
            }
            
            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"Field {name} is not set",
            });
        }
    }
}