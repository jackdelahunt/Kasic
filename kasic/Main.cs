using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Lexing;
using kasic.Logging;
using kasic.Parsing;
using OperationResult;
using System.Web;

namespace kasic
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Headless(Context.HeadlessContext, args[0]);
            }
            else
            {
                CommandLine(Context.CommandLineContext);
            }
        }
        
        public static List<KasicError> Headless(Context context, string filename)
        {
            var errors = new List<KasicError>();
            string[] lines = System.IO.File.ReadAllLines(@filename);
            for (int i = 0; i < lines.Length; i++)
            {
                var currentLine = lines[i].Trim();
                if (currentLine != "" && !currentLine.StartsWith("#"))
                {
                    var result = RunLine(context, currentLine);
                    if (result.IsError)
                    {
                        result.Error.Line = i;
                        Logger.LogError(result.Error);
                        errors.Add(result.Error);
                    }
                }
            }

            return errors;
        }

        public static void CommandLine(Context context)
        {
            while (true)
            {
                Console.Write(">>> ");
                string input = Console.ReadLine().Trim();
                var result = RunLine(context, input);
                if (result.IsError)
                {
                    Logger.LogError(result.Error);
                }
            }
        }

        public static Result<string, KasicError> RunLine(Context context, string line)
        {
            var lexer = new Lexer(line);
            var lexerResult = lexer.Lex();
            if (lexerResult.IsError)
            {
                return Helpers.Error(lexerResult.Error);
            }
                
            var parser = new Parser(lexerResult.Value);
            var parserResult = parser.Parse();
            if (parserResult.IsError)
            {
                return Helpers.Error(parserResult.Error);
            }

            var runtime = new Runtime(parserResult.Value);
            var runtimeResult = runtime.Run(context);
            if (runtimeResult.IsError)
            {
                return Helpers.Error(runtimeResult.Error);
            }

            return Helpers.Ok(runtimeResult.Value);
        }
    }
}