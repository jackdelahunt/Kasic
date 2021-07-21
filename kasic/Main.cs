using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Kasic;
using kasic.Lexing;
using kasic.Logging;
using kasic.Parsing;
using OperationResult;

namespace kasic
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Headless(new Context
                {
                    RuntimeMode = RuntimeMode.HEADLESS,
                    LineNumber = 0
                }, args[0]);
            }
            else
            {
                CommandLine(new Context
                {
                    RuntimeMode = RuntimeMode.COMMANDLINE
                });
            }
        }
        
        public static void Headless(Context context, string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            for (int i = 0; i < lines.Length; i++)
            {
                var currentLine = lines[i].Trim();
                if (currentLine != "")
                {
                    var result = RunLine(currentLine);
                    if (result.IsError)
                    {
                        result.Error.Line = i;
                        Logger.LogError(result.Error);
                    }
                }
            }
        }

        public static void CommandLine(Context context)
        {
            while (true)
            {
                Console.Write(">>> ");
                string input = Console.ReadLine().Trim();
                var result = RunLine(input);
                if (result.IsError)
                {
                    Logger.LogError(result.Error);
                }
            }
        }

        public static Result<string, KasicError> RunLine(string line)
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
            var runtimeResult = runtime.Run();
            if (runtimeResult.IsError)
            {
                return Helpers.Error(runtimeResult.Error);
            }

            return Helpers.Ok(runtimeResult.Value);
        }
    }
}