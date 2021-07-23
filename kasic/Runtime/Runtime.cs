using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Kasic;
using kasic.Logging;
using OperationResult;
using String = System.String;

namespace kasic
{
    public class Runtime
    {
        public List<Command> Commands { get; private set; }
        
        public Runtime(List<Command> commands)
        {
            Commands = commands;
        }

        public Result<string, KasicError> Run(Context context)
        {
            string lastOut = null;
            foreach (var command in Commands)
            {
                context.Command = command;
                
                if (lastOut != null)
                {
                    command.ArgObject.AddArgument(context, lastOut);
                }

                var result = command.Run(context);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                lastOut = result.Value;
            }
            
            if(context.RuntimeMode == RuntimeMode.COMMANDLINE && !String.IsNullOrEmpty(lastOut))
                Logger.Logln(lastOut);
            
            return Helpers.Ok(lastOut);
        }
        
        
    }
}