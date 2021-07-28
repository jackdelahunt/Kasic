using System.Text;
using kasic.Commands;

namespace kasic.Kasic
{
    public class Context
    {
        public RuntimeMode RuntimeMode;
        public Command Command;
        public KasicRegion Region;
        public int LineNumber;
    }

    public enum RuntimeMode
    {
        COMMANDLINE,
        HEADLESS
    }
    
    public enum KasicRegion
    {
        UNKNOWN = -1,
        LEXER,
        PARSER,
        RUNTIME,
    }
    
    public enum KasicType
    {
        VOID,
        NUMBER,
        STRING,
        BOOL,
        ANY
    }
    
    public class KasicError
    {
        public Context Context;
        public string Message;

        public override string ToString()
        {
            string region = this.Context.Region switch
            {
                KasicRegion.LEXER => "LEXER",
                KasicRegion.PARSER => "PARSER",
                KasicRegion.RUNTIME => "RUNTIME",
                _ => ""
            };

            StringBuilder builder = new StringBuilder();
            
            builder.Append($"[{this.Context.LineNumber}] ");
            if (region != "")
            {
                builder.Append($"{region} ");
            }
            
            if (this.Message != null)
            {
                builder.Append($"ERROR: {this.Message}; ");
            }
            
            if (this.Context.Command != null)
            {
                builder.Append($"{this.Context.Command.ToString()}");
            }

            return builder.ToString();
        }
    }
}