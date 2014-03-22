using System.Collections.Generic;
using System.Linq;

namespace VMFParser
{
    /// <summary>Represents a VMF</summary>
    public class VMF
    {
        public IList<IVNode> Body { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="VMF"/> class.</summary>
        public VMF()
        {
            Body = new List<IVNode>();
        }

        /// <summary>Initializes a new instance of the <see cref="VMF"/> class from a list of IVNodes.</summary>
        public VMF(IList<IVNode> body)
        {
            Body = body;
        }

        /// <summary>Initializes a new instance of the <see cref="VMF"/> class from the VMF text.</summary>
        public VMF(string[] text)
        {
            Body = Utils.ParseToBody(text);
        }
        /// <summary>Generates the VMF text from the body of IVNodes.</summary>
        public string[] ToVMFStrings()
        {
            return Utils.BodyToString(Body).ToArray();
        }
    }
}
