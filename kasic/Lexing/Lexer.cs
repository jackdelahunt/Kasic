using System;
using System.Collections.Generic;
using System.Text;
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

        public Result<List<CommandToken>, KasicError> Lex(Context context)
        {
            var splits = Input.Trim().Split(" | ");
            var commandTokens = new List<CommandToken>(10);
            foreach (var split in splits)
            {
                var lexResult = LexCommand(context, split);
                if (lexResult.IsError)
                {
                    return Helpers.Error(lexResult.Error);
                }
                
                commandTokens.Add(lexResult.Value);
            }

            return commandTokens;
        }

        private Result<CommandToken, KasicError> LexCommand(Context context, string split)
        {
            var words = SplitIntoWords(split);
            var commandToken = new CommandToken(words[0]);
            
            for (int i = 1; i < words.Count; i++)
            {
                string word = words[i];
                if (word.StartsWith('-'))
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

        private List<string> SplitIntoWords(string expression)
        {
            var words = new List<string>();
            expression = expression.Trim();

            for (int i = 0; i < expression.Length; i++)
            {
                var currentChar = expression[i];
                switch (currentChar)
                {
                    case ' ': break;
                    case '"':
                        var literal = GetWord(expression, '"',i + 1);
                        words.Add(literal);
                        i += literal.Length + 1;
                        break;
                    default:  
                        var word = GetWord(expression, ' ', i);
                        words.Add(word);
                        i += word.Length;
                        break;
                }
            }

            return words;
        }

        private string GetWord(string expression, char stopper, int start)
        {
            int currentIndex = start;
            while (currentIndex < expression.Length)
            {
                if (expression[currentIndex] == stopper)
                {
                    break;
                }
                currentIndex++;
            }

            int length = currentIndex - start;
            if (length == 0)
            {
                return "";
            }
            else
            {
                return expression.Substring(start, currentIndex - start);
            }
        }
    }
}