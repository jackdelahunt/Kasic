using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using OperationResult;

namespace kasic.Commands
{
    public abstract class Command
    {
        public string Name;
        public ArgObject ArgObject;
        public List<string> Flags;
        public CommandSettings CommandSettings;

        public Command(string name)
        {
            Name = name;
            Flags = new List<string>();
        }
        
        public virtual Result<string, KasicError> Run(Context context)
        {
            throw new NotImplementedException("Command command is not valid");
        }
    }

    public struct CommandSettings
    {
        public uint MinArgs;
        public uint MaxArgs;
        public KasicType FieldType;
        public KasicType ReturnType;
    }
}