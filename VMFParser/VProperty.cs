namespace VMFParser
{
    public class VProperty : IVNode
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        /// <summary>Initializes a new instance of the <see cref="VProperty"/> class from the name and value of a property.</summary>
        public VProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>Initializes a new instance of the <see cref="VProperty"/> class from text.</summary>
        public VProperty(string text)
        {
            var texts = text.Trim().Split(new char[] { ' ', '\t' }, 2);
            Name = texts[0].Trim('"');
            Value = texts[1].Trim('"');
        }

        /// <summary>Generates the VMF text representation of this Property.</summary>
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
