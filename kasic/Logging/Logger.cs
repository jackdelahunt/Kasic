using System;
using System.Text;
using kasic.Kasic;

namespace kasic.Logging
{
    public static class Logger
    {
        public static void Log(string text)
        {
            Console.Write(text);
        }
        
        public static void Logln(string text)
        {
            Console.WriteLine(text);
        }
        
        public static void LogError(KasicError error)
        {
            Logln(error.ToString());
        }
    }
}