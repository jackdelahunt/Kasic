using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Lexing;
using kasic.Parsing;

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
                var commands = parser.Parse();

                var runtime = new Runtime(commands);
                runtime.Run();
            }
        }
    }
}