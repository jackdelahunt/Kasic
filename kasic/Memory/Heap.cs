using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Logging;
using kasic.Utils;
using OperationResult;

namespace kasic.Memory
{
    public static class Heap
    {
        private static Dictionary<Tuple<string, KasicType>, string> heap =
            new Dictionary<Tuple<string, KasicType>, string>();

        public static Status<KasicError> SetNumber(string name, double data)
        {
            var key = new Tuple<string, KasicType>(name, KasicType.NUMBER);
            if (heap.ContainsKey(key))
            {
                // TODO: this looks dumb
                heap.Remove(key);
            }

            heap.Add(key, data.ToString());
            return Helpers.Ok();
        }
        
        public static Status<KasicError> SetString(string name, string data)
        {
            var key = new Tuple<string, KasicType>(name, KasicType.STRING);
            if (heap.ContainsKey(key))
            {
                // TODO: this looks dumb
                heap.Remove(key);
            }

            heap.Add(key, data);
            return Helpers.Ok();
        }
        
        public static Result<double, KasicError> GetNumber(string name)
        {
            var key = new Tuple<string, KasicType>(name, KasicType.NUMBER);
            if (!heap.ContainsKey(key))
            {
                return Helpers.Error(new KasicError
                {
                    Message = $"Field {name} of type NUMBER is not set",
                    Region = KasicRegion.HEAP
                });
            }

            heap.TryGetValue(key, out var data);
            var result = Types.ToNumber(data);

            if (result.IsError)
            {
                return Helpers.Error(new KasicError
                {
                    Message = $"Field {name} cannot be inferred as a NUMBER -> {data}",
                    Region = KasicRegion.HEAP
                });
            }
            
            return Helpers.Ok(result.Value);
        }
        
        public static Result<string, KasicError> GetString(string name)
        {
            var key = new Tuple<string, KasicType>(name, KasicType.STRING);
            if (!heap.ContainsKey(key))
            {
                return Helpers.Error(new KasicError
                {
                    Message = $"Field {name} of type STRING is not set",
                    Region = KasicRegion.HEAP
                });
            }

            heap.TryGetValue(key, out var data);
            return Helpers.Ok(data);
        }
    }
}