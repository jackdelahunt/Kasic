using System;
using System.Text;
using kasic.Commands;

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
                _ => ""
            };

            StringBuilder builder = new StringBuilder();
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

    public class KasicError
    {
        public string Message;
        public Command Command;
        public KasicRegion Region;
    }

    public enum KasicRegion
    {
        UNKNOWN = -1,
        LEXER,
        PARSER,
        RUNTIME,
        HEAP,
    }
}