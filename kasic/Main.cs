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
                    RuntimeMode = RuntimeMode.HEADLESS,
                    LineNumber = 0,
                }, args[0]);
            }
            else
            {
                CommandLine(new Context
                {
                    Command = null,
                    RuntimeMode = RuntimeMode.COMMANDLINE,
                    LineNumber = 0,
                });
            }
        }
        
        public static Status<KasicError> Headless(Context context, string filePath)
        {
            context.Region = KasicRegion.LEXER;
            var lexerResult = new Lexer().Lex(context, filePath);
            if (lexerResult.IsError)
            {
                return Helpers.Error(lexerResult.Error);
            }

            var controller = new Controller(lexerResult.Value);

            while (!controller.IsEOF(context))
            {
                context.Region = KasicRegion.PARSER;
                var parser = new Parser(controller.GetLine(context));
                var parserResult = parser.Parse(context);
                if (parserResult.IsError)
                {
                    return Helpers.Error(parserResult.Error);
                }

                context.Region = KasicRegion.RUNTIME;
                var runtime = new Runtime(parserResult.Value);
                var runtimeResult = runtime.Run(context);
                if (runtimeResult.IsError)
                {
                    return Helpers.Error(runtimeResult.Error);
                }
            }


            return Helpers.Ok();
        }

        public static void CommandLine(Context context)
        {
            while (true)
            {
                Console.Write(">>> ");
                string input = Console.ReadLine().Trim();
                var result = RunSingleLine(context, input);
                if (result.IsError)
                {
                    Logger.LogError(result.Error);
                }
            }
        }

        public static Result<string, KasicError> RunSingleLine(Context context, string line)
        {
            context.Region = KasicRegion.LEXER;
            var lexerResult = new Lexer().Lex(context, line);
            if (lexerResult.IsError)
            {
                return Helpers.Error(lexerResult.Error);
            }
               
            context.Region = KasicRegion.PARSER;
            var parser = new Parser(lexerResult.Value[0]);
            var parserResult = parser.Parse(context);
            if (parserResult.IsError)
            {
                return Helpers.Error(parserResult.Error);
            }

            context.Region = KasicRegion.RUNTIME;
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