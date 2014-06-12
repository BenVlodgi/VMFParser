using System;
using System.Collections.Generic;
using System.Linq;

namespace VMFParser
{
    internal static class Utils
    {
        internal static Type GetNodeType(string line)
        {
            return line.Trim().StartsWith("\"") ? typeof(VProperty) : typeof(VBlock);
        }

        internal static IList<IVNode> ParseToBody(string[] body)
        {
            IList<IVNode> newBody = new List<IVNode>();
            int depth = 0;
            var wasDeep = false;
            IList<string> nextBlock = null;
            for (int i = 0; i < body.Length; i++)
            {
                var line = body[i].Trim();

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                    continue;

                var readable = line.FirstOrDefault() != default(char);

                if (readable && line.First() == '{')
                    depth++;

                if (depth == 0)
                    if (Utils.GetNodeType(line) == typeof(VProperty))
                        newBody.Add(new VProperty(line));
                    else
                    {
                        nextBlock = new List<string>();
                        nextBlock.Add(line);
                    }
                else
                    nextBlock.Add(line);

                wasDeep = depth > 0;

                if (readable && line.First() == '}')
                    depth--;

                if (wasDeep && depth == 0)
                {
                    newBody.Add(new VBlock(nextBlock.ToArray()));
                    nextBlock = null;
                }
            }
            return newBody;
        }

        internal static IList<string> BodyToString(IList<IVNode> body)
        {
            IList<string> text = new List<string>();
            if (body != null)
                foreach (var node in body)
                    if (node.GetType() == typeof(VProperty))
                        text.Add(((VProperty)node).ToVMFString());
                    else
                        foreach (string s in ((VBlock)node).ToVMFStrings())
                            text.Add(s);
            return text;
        }
    }
}
