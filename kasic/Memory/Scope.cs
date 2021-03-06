using System.Collections.Generic;
using kasic.Kasic;
using OperationResult;

namespace kasic.Memory
{
    public static class Scope
    {
        public static Result<int, KasicError> RegisterGotoScope(Context context, string name, int line)
        {
            return Heap.Push(context, name, line, KasicType.NUMBER, false);
        }
        
        public static Result<int, KasicError> FindGotoScopeObjectId(Context context, string name)
        {
            var heapResult = Heap.GetByName(context, name);
            if (heapResult.IsError)
            {
                return Helpers.Error(heapResult.Error);
            }

            return Helpers.Ok(heapResult.Value.ObjectId);
        }

        public static Result<int, KasicError> GetScopeById(Context context, int id)
        {
            var heapObject = Heap.GetByObjectId(id);
            if (heapObject.Data is int @int)
            {
                return Helpers.Ok(@int);
            }

            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"Object with id {heapObject.ObjectId} cannot be used as a goto scope"
            });
        }
    }
}