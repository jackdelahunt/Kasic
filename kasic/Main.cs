using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Lexing;
using kasic.Logging;
using kasic.Parsing;
using OperationResult;
using kasic.Files;

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
                    Command = null,
                    Reader = new Reader(args[0]),
                    RuntimeMode = RuntimeMode.HEADLESS
                });
            }
            else
            {
                CommandLine(new Context
                {
                    Command = null,
                    Reader = new Reader(),
                    RuntimeMode = RuntimeMode.COMMANDLINE
                });
            }
        }
        
        public static List<KasicError> Headless(Context context)
        {
            var errors = new List<KasicError>();
            bool running = true;
            while (running)
            {
                var readerResult = context.Reader.ReadLine();
                if (readerResult.IsError)
                {
                    return errors;
                }

                if (readerResult.Value == Reader.EOF)
                {
                    running = false;
                    continue;
                }

                var runnerResult = RunLine(context, readerResult.Value);
                if (runnerResult.IsError)
                {
                    runnerResult.Error.Line = context.Reader.LineNumber;
                    Logger.LogError(runnerResult.Error);
                    errors.Add(runnerResult.Error);
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