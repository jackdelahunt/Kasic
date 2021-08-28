using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using kasic.Kasic;
using kasic.Logging;
using kasic.Memory;
using kasic.Utils;
using OperationResult;
using System.Web;

namespace kasic.Commands
{
    public class Dump : Command
    {
        public Dump() : base("dump")
        {
            CommandSettings = new CommandSettings()
            {
                MinArgs = 0,
                MaxArgs = 0,
                ArgumentList = new ArgumentList(new List<KasicType>()),
                ReturnType = KasicType.STRING,
            };
        }

        public override Result<IReturnObject, KasicError> Run(Context context, Arguments arguments, List<string> flags)
        {
            var json =JsonSerializer.Serialize(
                Heap.Dump, typeof(List<HeapObject>), 
                new JsonSerializerOptions() {WriteIndented = flags.Contains("-i")}
                );
            return new ReturnObject(this, json);
        }
    }
}