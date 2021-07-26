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
        public CommandSettings CommandSettings;

        public Command(string name)
        {
            Name = name;
        }
        
        public virtual Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            throw new NotImplementedException("Command command is not valid");
        }
    }

    public struct CommandSettings
    {
        public uint MinArgs;
        public uint MaxArgs;
        public ArgumentList ArgumentList;
        public KasicType ReturnType;
    }
}