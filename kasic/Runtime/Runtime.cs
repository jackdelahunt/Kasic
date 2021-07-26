using System;
using System.Collections.Generic;
using kasic.Commands;
using kasic.Kasic;
using kasic.Logging;
using kasic.Parsing;
using OperationResult;
using String = System.String;

namespace kasic
{
    public class Runtime
    {
        public List<ParserToken> Tokens { get; private set; }
        
        public Runtime(List<ParserToken> tokens)
        {
            this.Tokens = tokens;
        }

        public Result<string, KasicError> Run(Context context)
        {
            IReturnObject lastOut = null;
            foreach (var token in Tokens)
            {
                context.Command = token.Command;
                
                if (lastOut != null)
                {
                    token.ArgObject.PipeReturn(context, lastOut);
                }

                var result = token.Command.Run(context, token.ArgObject, token.Flags);
                if (result.IsError)
                {
                    return Helpers.Error(result.Error);
                }

                lastOut = result.Value;
            }

            if (context.RuntimeMode == RuntimeMode.COMMANDLINE)
            {
                var output = lastOut?.ToString();
                if (!string.IsNullOrEmpty(output))
                {
                    Logger.Logln(output);
                    return Helpers.Ok(output);
                }
            }

            return Helpers.Ok("");
        }
        
        
    }
}