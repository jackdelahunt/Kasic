using System;
using System.Collections.Generic;
using System.Text;
using kasic.Kasic;
using kasic.Memory;
using OperationResult;

namespace kasic.Lexing
{
    public class Lexer
    {
        public Result<List<List<CommandToken>>, KasicError> Lex(Context context, string input)
        {
            if (input.Equals(""))
                return Helpers.Ok(new List<List<CommandToken>>() { new List<CommandToken>() });
            
            if (context.RuntimeMode == RuntimeMode.HEADLESS)
                return LexFile(context, input);

            var lexLineResult = LexLine(context, input);
            if (lexLineResult.IsError)
            {
                return Helpers.Error(lexLineResult.Error);
            }

            var list = new List<List<CommandToken>> {lexLineResult.Value};
            return Helpers.Ok(list);
        }
        
        public Result<List<List<CommandToken>>, KasicError> LexFile(Context context, string fileName)
        {
            var listOfCommandTokenLists = new List<List<CommandToken>>(); // best name
            var fileLines = System.IO.File.ReadAllLines(fileName);
            for (int i = 0; i < fileLines.Length; i++)
            {
                var line = fileLines[i].Trim();
                if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                {
                    listOfCommandTokenLists.Add(null);
                    continue;
                }

                switch (fileLines[i][0])
                {
                    case '#': 
                        listOfCommandTokenLists.Add(null);
                        break;
                    case '>': 
                        var scopeName = line.Substring(1).Trim().Split(" ")[0];
                        Scope.RegisterGotoScope(context, scopeName, i); 
                        listOfCommandTokenLists.Add(null);
                        break;
                    default:
                        var lexLineResult = LexLine(context, line);
                        if (lexLineResult.IsError)
                        {
                            return Helpers.Error(lexLineResult.Error);
                        }
                        listOfCommandTokenLists.Add(lexLineResult.Value);
                        break;
                        
                }
            }

            return listOfCommandTokenLists;
        }

        public Result<List<CommandToken>, KasicError> LexLine(Context context, string line)
        {
            var splits = line.Trim().Split(" | ");
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

            return Helpers.Ok(commandTokens);
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