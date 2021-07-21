using System.Collections.Generic;

namespace kasic.Memory
{
    public static class Scope
    {
        private static Dictionary<string, int> scope = new Dictionary<string, int>();

        public static void RegisterGotoScope(string name, int line)
        {
            scope.Add(name, line);
        }
        
        public static int FindGotoScope(string name)
        {
            scope.TryGetValue(name, out var line);
            return line;
        }
    }
}