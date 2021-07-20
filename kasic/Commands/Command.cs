using System.Collections.Generic;
using kasic.Logging;
using OperationResult;

namespace kasic.Commands
{
    public interface Command
    { 
        Result<string, KasicError> Run();
        public void PassData(List<string> args, List<string> flags);
        public void AddArg(string arg);
        string Name();
        List<string> Args();
        List<string> Flags();
        public CommandSettings CommandSettings();
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