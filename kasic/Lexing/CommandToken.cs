using System;
using System.Collections.Generic;
using System.Text;

namespace kasic.Lexing
{
    public struct CommandToken
    {
        public string Name;
        public List<string> Args;
        public List<string> Flags;

        public CommandToken(string name)
        {
            Name = name;
            Args = new List<string>();
            Flags = new List<string>();
        }

        public override string ToString()
        {
            StringBuilder args = new StringBuilder();
            foreach (var arg in Args)
            {
                args.Append(arg);
                args.Append(' ');
            }
            
            StringBuilder flags = new StringBuilder();
            foreach (var flag in Flags)
            {
                flags.Append(flag);
                flags.Append(' ');
            }
            return $"Name:{Name}: Args:{args},  Flags:{flags}";
        }
    }
}