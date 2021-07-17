using System;
using System.Collections.Generic;
using kasic.Commands;

namespace kasic
{
    public class Runtime
    {

        public List<Command> Commands { get; private set; }
        
        public Runtime(List<Command> commands)
        {
            Commands = commands;
        }

        public void Run()
        {
            string lastOut = null;
            foreach (var command in Commands)
            {
                if (lastOut != null)
                {
                    command.AddArg(lastOut);
                }

                lastOut = command.Run();
            }
            Console.WriteLine(lastOut);
        }
        
        
    }
}