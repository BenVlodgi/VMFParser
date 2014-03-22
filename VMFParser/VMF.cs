using System.Collections.Generic;
using System.Linq;

namespace VMFParser
{
    public class VMF
    {
        public IList<IVNode> Body { get; private set; }

        public VMF(string[] text)
        {
            Body = Utils.ParseToBody(text);
        }

        public string[] ToVMFStrings()
        {
            return Utils.BodyToString(Body).ToArray();
        }
    }
}
