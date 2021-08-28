using System.Collections.Generic;
using kasic.Kasic;
using OperationResult;

namespace kasic.Commands
{
    public class Greater : Command
    {
        public Greater() : base("great")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.NUMBER,
                    KasicType.NUMBER
                }),
                ReturnType = KasicType.BOOL,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var num1 = arguments.AsNumber(context, 0);
            if (num1.IsError)
            {
                return Helpers.Error(num1.Error);
            }
            
            var num2 = arguments.AsNumber(context, 1);
            if (num2.IsError)
            {
                return Helpers.Error(num2.Error);
            }
            
            return new ReturnObject(this, num1.Value < num2.Value);
        }
    }
}