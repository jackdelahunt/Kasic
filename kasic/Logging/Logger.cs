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
            string region;
            switch (error.Region)
            {
                case KasicRegion.LEXER: region = "LEXER"; break;
                case KasicRegion.PARSER: region = "PARSER"; break;
                case KasicRegion.RUNTIME: region = "RUNTIME"; break;
                default: region = ""; break;
            }

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
        LEXER,
        PARSER,
        RUNTIME,
    }
}