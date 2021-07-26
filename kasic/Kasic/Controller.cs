using System.Collections.Generic;
using kasic.Lexing;

namespace kasic.Kasic
{
    public class Controller
    {
        public List<List<CommandToken>> LexedInput { get; private set; }

        public Controller(List<List<CommandToken>> lexedInput)
        {
            this.LexedInput = lexedInput;
        }

        public List<CommandToken> GetLine(Context context)
        {
            return LexedInput[context.LineNumber++];
        }

        public bool IsEOF(Context context)
        {
            if (!(context.LineNumber >= LexedInput.Count))
            {
                if (LexedInput[context.LineNumber] == null)
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