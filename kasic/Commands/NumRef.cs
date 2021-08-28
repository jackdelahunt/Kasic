using System;
using System.Collections.Generic;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;

namespace kasic.Commands
{
    public class NumRef : Command
    {
        public NumRef() : base("&num")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 2,
                MaxArgs = 2,
                ArgumentList = new ArgumentList(new List<KasicType>()
                {
                    KasicType.STRING,
                    KasicType.NUMBER,
                }),
                ReturnType = KasicType.NUMBER,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var name = arguments.AsString(context, 0);
            if (name.IsError)
            {
                return Helpers.Error(name.Error);
            }
            
            var value = arguments.AsNumber(context, 1);
            if (value.IsError)
            {
                return Helpers.Error(value.Error);
            }

            var nameArgKasicObject = arguments.GetKasicObject(0);
            
            // if this arg object has not been linked to a value on the heap yet
            if (nameArgKasicObject.ObjectId < 0)
            {
                // look for value based on name
                var getByNameResult = Heap.GetByName(context, name.Value);
                if (getByNameResult.IsSuccess)
                {
                    // link object to heap, then continue to update
                    nameArgKasicObject.ObjectId = getByNameResult.Value.ObjectId;
                }
                else
                {
                    // if name is not on the heap then push to the heap
                    var pushResult = Heap.Push(context, name.Value, value.Value, KasicType.NUMBER, flags.Contains("-c"));
                    if (pushResult.IsError)
                    {
                        return Helpers.Error(pushResult.Error);
                    }

                    // update id to reflect we have data on the heap
                    nameArgKasicObject.ObjectId = pushResult.Value;
                    return new ReturnObject(this, value.Value);
                }
            }

            // object is linked to the heap so update the value stored
            var heapResult = Heap.Update(context, nameArgKasicObject.ObjectId, value.Value, KasicType.NUMBER);
            if (heapResult.IsError)
            {
                return Helpers.Error(heapResult.Error);
            }
            return new ReturnObject(this, value.Value);
        }
    }
}