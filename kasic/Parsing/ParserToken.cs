using System.Collections.Generic;
using kasic.Commands;
using kasic.Kasic;

namespace kasic.Parsing
{
    public struct ParserToken
    {
        public Command Command;
        public ArgObject ArgObject;
        public List<string> Flags;
    }
}