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
            IReturnObject lastOut = null;
            foreach (var command in Commands)
            {
                context.Command = command;
                
                if (lastOut != null)
                {
                    command.ArgObject.PipeReturn(context, lastOut);
                }

                var result = command.Run(context);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                lastOut = result.Value;
            }
            
            if(context.RuntimeMode == RuntimeMode.COMMANDLINE && lastOut != null)
                Logger.Logln(lastOut.ToString());
            
            return Helpers.Ok(lastOut.ToString());
        }
        
        
    }
}