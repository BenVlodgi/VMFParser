using System.Collections.Generic;
using System.Linq;

namespace VMFParser
{
    /// <summary>Represents a block containing other IVNodes in a VMF</summary>
    public class VBlock : AVBlock, IVNode, IDeepCloneable<VBlock>
    {
        public string Name { get; private set; }
        //public IList<IVNode> Body { get; protected set; }

        /// <summary>Initializes a new instance of the <see cref="VBlock"/> class from its name and a list of IVNodes.</summary>
        public VBlock(string name, IList<IVNode> body = null)
        {
            Name = name;
            if (body == null)
                body = new List<IVNode>();
            Body = body;
        }

        /// <summary>Initializes a new instance of the <see cref="VBlock"/> class from VMF text.</summary>
        public VBlock(string[] text)
        {
            Name = text[0].Trim();
            Body = Utils.ParseToBody(text.SubArray(2, text.Length - 3));
        }

        /// <summary>Generates the VMF text representation of this block.</summary>
        /// <param name="useTabs">if set to <c>true</c> the text will be tabbed accordingly.</param>
        public string[] ToVMFStrings(bool useTabs = true)
        {
            var text = Utils.BodyToString(Body);
            if (useTabs)
                text = text.Select(t => t.Insert(0, "\t")).ToList();
            text.Insert(0, Name);
            text.Insert(1, "{");
            text.Add("}");
            return text.ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + " (" + Name + ")";
        }

        IVNode IDeepCloneable<IVNode>.DeepClone() => DeepClone();
        public VBlock DeepClone()
        {
            return new VBlock(Name, Body == null ? null : Body.Select(node => node.DeepClone()).ToList());
        }
    }
}
