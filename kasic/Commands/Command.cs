using System;
using System.Collections.Generic;
using kasic.Logging;
using OperationResult;

namespace kasic.Commands
{
    public abstract class Command
    {
        public string Name;
        public List<string> Args;
        public List<string> Flags;
        public CommandSettings CommandSettings;

        public Command(string name)
        {
            this.Name = name;
            Args = new List<string>();
            Flags = new List<string>();
        }
        
        public virtual Result<string, KasicError> Run()
        {
            throw new NotImplementedException("Command command is not valid");
        }
    }

    public enum KasicType
    {
        VOID,
        NUMBER,
        STRING,
        ANY
    }

    public struct CommandSettings
    {
        public uint MinArgs;
        public uint MaxArgs;
        public KasicType FieldType;
        public KasicType ReturnType;
        public List<KasicType> DefinedFields;
    }
}