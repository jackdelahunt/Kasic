using System.Collections.Generic;
using kasic.Kasic;
using OperationResult;

namespace kasic.Commands.BoolOperators
{
    public class And : Command
    {
        public And() : base("and")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.BOOL,
                    KasicType.BOOL
                }),
                ReturnType = KasicType.BOOL,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var valueOne = arguments.AsBool(context, 0);
            if (valueOne.IsError)
            {
                return Helpers.Error(valueOne.Error);
            }
            
            var valueTwo = arguments.AsBool(context, 1);
            if (valueTwo.IsError)
            {
                return Helpers.Error(valueTwo.Error);
            }
            
            return new ReturnObject(this, valueOne.Value && valueTwo.Value);
        }
    }
}