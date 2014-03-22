namespace VMFParser
{
    public class VProperty : IVNode
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        public VProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public VProperty(string text)
        {
            var texts = text.Trim().Split(new char[] { ' ', '\t' }, 2);
            Name = texts[0].Trim('"');
            Value = texts[1].Trim('"');
        }

        public string ToVMFString()
        {
            return string.Format("\"{0}\" \"{1}\"", Name, Value);
        }

        public override string ToString()
        {
            return base.ToString() + " (" + Name + ")";
        }
    }
}
