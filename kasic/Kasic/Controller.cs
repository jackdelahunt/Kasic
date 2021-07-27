using System.Collections.Generic;
using kasic.Lexing;
using kasic.Parsing;
using OperationResult;

namespace kasic.Kasic
{
    public class Controller
    {
        private string Path;
        private List<List<ParserToken>> ParsedTokensList;

        public Controller(string path)
        {
            this.Path = path;
            ParsedTokensList = new List<List<ParserToken>>();
        }

        public Result<List<List<CommandToken>>, KasicError> Lex(Context context)
        {
            context.Region = KasicRegion.LEXER;
            var lexerResult = new Lexer().Lex(context, this.Path);
            if (lexerResult.IsError)
            {
                return Helpers.Error(lexerResult.Error);
            }

            return Helpers.Ok(lexerResult.Value);
        }
        
        public Status<KasicError> Parse(Context context, List<List<CommandToken>> commandTokensList)
        {
            context.Region = KasicRegion.PARSER;
            for (int i = 0; i < commandTokensList.Count; i++)
            {
                if (commandTokensList[i] == null)
                {
                    ParsedTokensList.Add(null);
                    continue;
                }
                
                var parser = new Parser(commandTokensList[i]);
                var parserResult = parser.Parse(context);
                if (parserResult.IsError)
                {
                    return Helpers.Error(parserResult.Error);
                }
                
                ParsedTokensList.Add(parserResult.Value);
            }

            return Helpers.Ok();
        } 

        public List<ParserToken> GetLine(Context context)
        {
            return ParsedTokensList[context.LineNumber++];
        }

        public bool IsEOF(Context context)
        {
            if (!(context.LineNumber >= ParsedTokensList.Count))
            {
                if (ParsedTokensList[context.LineNumber] == null)
                {
                    context.LineNumber++;
                    return IsEOF(context);
                }
                
                return false;
            }

            return true;
        }
    }
}