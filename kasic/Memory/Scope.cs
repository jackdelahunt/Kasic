using System.Collections.Generic;
using kasic.Kasic;
using OperationResult;

namespace kasic.Memory
{
    public static class Scope
    {
        private static Dictionary<string, int> scope = new Dictionary<string, int>();

        public static void RegisterGotoScope(string name, int line)
        {
            scope.Add(name, line);
        }
        
        public static Result<int, KasicError> FindGotoScope(Context context, string name)
        {
            bool exists = scope.TryGetValue(name, out var line);

            if (exists)
            {
                return Helpers.Ok(line);
            }

            return Helpers.Error(new KasicError
            {
                Context = context,
                Message = $"Cannot find goto scope with name {name}"
            });
        }
    }
}