using System.Collections.Generic;
using System.Linq;

namespace VMFParser
{
    public class VBlock : IVNode
    {
        public string Name { get; private set; }
        public IList<IVNode> Body { get; private set; }

        public VBlock(string name, IList<IVNode> body = null)
        {
            Name = name;
            if (body == null)
                body = new List<IVNode>();
        }

        public VBlock(string[] text)
        {
            Name = text[0].Trim();
            Body = Utils.ParseToBody(text.SubArray(2, text.Length - 3));
        }

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
    }
}
