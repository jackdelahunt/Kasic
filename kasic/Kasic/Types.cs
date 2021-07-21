using kasic.Commands;

namespace kasic.Kasic
{
    public class Context
    {
        public RuntimeMode RuntimeMode;
        public int LineNumber;
    }

    public enum RuntimeMode
    {
        COMMANDLINE,
        HEADLESS
    }
    
    public class KasicError
    {
        public string Message;
        public Command Command;
        public KasicRegion Region;
        public int Line;
    }

    public enum KasicRegion
    {
        UNKNOWN = -1,
        LEXER,
        PARSER,
        RUNTIME,
        HEAP,
    }
}