using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Kasic;
using kasic.Logging;
using OperationResult;

namespace kasic
{
    public class Runtime
    {

        public List<Command> Commands { get; private set; }
        
        public Runtime(List<Command> commands)
        {
            Commands = commands;
        }

        public Result<string, KasicError> Run()
        {
            string lastOut = null;
            foreach (var command in Commands)
            {
                if (lastOut != null)
                {
                    command.Args.Add(lastOut);
                }

                var result = command.Run();
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                lastOut = result.Value;
            }
            return Helpers.Ok(lastOut);
        }
        
        
    }
}