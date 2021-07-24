using System.Collections.Generic;

namespace kasic.Kasic
{
    public class ArgumentList
    {
        public List<KasicType> argumentTypes { get; private set; }

        public ArgumentList(List<KasicType> argumentTypes)
        {
            this.argumentTypes = argumentTypes;
        }
    }
}