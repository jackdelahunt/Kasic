using System;
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
                default: region = "RUNTIME"; break;
            }
            Logln($"{region} ERROR: {error.Message}; with {error.Command}");
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