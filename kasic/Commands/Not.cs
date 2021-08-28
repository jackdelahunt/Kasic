using System.Collections.Generic;
using kasic.Kasic;
using OperationResult;

namespace kasic.Commands
{
    public class Not : Command
    {
        public Not() : base("not")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 1,
                MaxArgs = 1,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.BOOL,
                }),
                ReturnType = KasicType.BOOL,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var value = arguments.AsBool(context, 0);
            if (value.IsError)
            {
                return Helpers.Error(value.Error);
            }
            
            return new ReturnObject(this, !value.Value);
        }
    }
}