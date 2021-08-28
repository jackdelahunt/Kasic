using System.Collections.Generic;
using kasic.Kasic;
using OperationResult;

namespace kasic.Commands
{
    public class Sub : Command
    {
        public Sub() : base("sub")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.NUMBER,
                    KasicType.NUMBER,
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var first = arguments.AsNumber(context, 0);
            if (first.IsError)
            {
                return Helpers.Error(first.Error);
            }
            
            var second = arguments.AsNumber(context, 1);
            if (second.IsError)
            {
                return Helpers.Error(second.Error);
            }

            return new ReturnObject(this, first.Value - second.Value);
        }
    }
}