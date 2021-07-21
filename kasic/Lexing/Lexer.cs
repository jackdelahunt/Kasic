using System;
using System.Collections.Generic;
using kasic.Kasic;
using OperationResult;

namespace kasic.Lexing
{
    public class Lexer
    {
        
        public string Input { get; private set; }
        
        public Lexer(string input)
        {
            Input = input;
        }

        public Result<List<CommandToken>, KasicError> Lex()
        {
            var splits = Input.Trim().Split(" | ");
            var commandTokens = new List<CommandToken>();
            foreach (var split in splits)
            {
                commandTokens.Add(LexCommand(split));
            }

            return commandTokens;
        }

        private CommandToken LexCommand(string split)
        {
            var words = split.Split(" ");
            var commandToken = new CommandToken(words[0]);
            
            for (int i = 1; i < words.Length; i++)
            {
                string word = words[i];
                if (word.Substring(0, 1) == "-")
                {
                    try
                    {
                        double num = Double.Parse(word);
                        commandToken.Args.Add(num.ToString());
                    }
                    catch
                    {
                        commandToken.Flags.Add(word);
                    }
                }
                else
                {
                    commandToken.Args.Add(word);
                }
            }

            return commandToken;
        }
    }
}