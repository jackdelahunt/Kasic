using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Lexing;
using kasic.Logging;
using kasic.Parsing;
using OperationResult;

namespace kasic
{
    public static class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Write(">>> ");
                string input = Console.ReadLine().Trim();
                var lexer = new Lexer(input);
                var tokens = lexer.Lex();

                var parser = new Parser(tokens);
                var parserResult = parser.Parse();
                if (parserResult.IsError)
                {
                    Logger.LogError(parserResult.Error);
                    continue;
                }

                var runtime = new Runtime(parserResult.Value);
                runtime.Run();
            }
        }
    }
}