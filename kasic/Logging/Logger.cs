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
            string region = error.Region switch
            {
                KasicRegion.LEXER => "LEXER",
                KasicRegion.PARSER => "PARSER",
                KasicRegion.RUNTIME => "RUNTIME",
                KasicRegion.HEAP => "HEAP",
                _ => ""
            };

            StringBuilder builder = new StringBuilder();
            
            builder.Append($"[{error.Line}] ");
            if (region != "")
            {
                builder.Append($"{region} ");
            }
            
            if (error.Message != null)
            {
                builder.Append($"ERROR: {error.Message}; ");
            }
            
            if (error.Command != null)
            {
                builder.Append($"{error.Command.ToString()}");
            }

            Logln(builder.ToString());
        }
    }
}