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
        private static Dictionary<string, string> heap = new Dictionary<string, string>();

        public static Status<KasicError> Push(string name, string data)
        {
            if (heap.ContainsKey(name))
            {
                // TODO: this looks dumb
                heap.Remove(name);
            }

            heap.Add(name, data);
            return Helpers.Ok();
        }
        
        public static Result<string, KasicError> Reference(string name)
        {
            if (!heap.ContainsKey(name))
            {
                return Helpers.Error(new KasicError
                {
                    Message = $"Field {name} is not set",
                    Region = KasicRegion.HEAP
                });
            }

            heap.TryGetValue(name, out var data);
            return Helpers.Ok(data);
        }
    }
}