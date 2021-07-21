using System.Text;
using kasic.Commands;

namespace kasic.Kasic
{
    public class Context
    {
        public static Context HeadlessContext = new Context
        {
            RuntimeMode = RuntimeMode.HEADLESS,
            LineNumber = 0,
            Command = null
        };
        
        public static Context CommandLineContext = new Context
        {
            RuntimeMode = RuntimeMode.COMMANDLINE,
            LineNumber = 0,
            Command = null
        };
        
        public RuntimeMode RuntimeMode;
        public int LineNumber;
        public Command Command;
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
        HEAP,
    }
    
    public class KasicError
    {
        public string Message;
        public Command Command;
        public KasicRegion Region;
        public int Line;

        public override string ToString()
        {
            string region = this.Region switch
            {
                KasicRegion.LEXER => "LEXER",
                KasicRegion.PARSER => "PARSER",
                KasicRegion.RUNTIME => "RUNTIME",
                KasicRegion.HEAP => "HEAP",
                _ => ""
            };

            StringBuilder builder = new StringBuilder();
            
            builder.Append($"[{this.Line}] ");
            if (region != "")
            {
                builder.Append($"{region} ");
            }
            
            if (this.Message != null)
            {
                builder.Append($"ERROR: {this.Message}; ");
            }
            
            if (this.Command != null)
            {
                builder.Append($"{this.Command.ToString()}");
            }

            return builder.ToString();
        }
    }
}