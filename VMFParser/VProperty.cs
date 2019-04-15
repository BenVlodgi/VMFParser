namespace VMFParser
{
    public class VProperty : IVNode, IDeepCloneable<VProperty>
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

        /// <summary> This is used mostly for debug visualization.</summary>
        public override string ToString()
        {
            return base.ToString() + " (" + Name + ")";
        }

        IVNode IDeepCloneable<IVNode>.DeepClone() => DeepClone();
        public VProperty DeepClone()
        {
            return new VProperty(Name, Value);
        }

        public int GetValueAsInt(int defaultValue = 0)
        {
            return int.TryParse(Value, out int val) ? val : defaultValue;
        }

        public void SetValueAsInt(int value)
        {
            Value = value.ToString();
        }
        
        public decimal GetValueAsDecimal(decimal defaultValue = 0M)
        {
            return decimal.TryParse(Value, out decimal val) ? val : defaultValue;
        }

        public void SetValueAsAxis(decimal value)
        {
            Value = value.ToString();
            // TODO: investigate using next line instead
            //Value = value.ToString("E6", System.Globalization.CultureInfo.CreateSpecificCulture("sv-SE"));
        }

        public Axis GetValueAsAxis()
        {
            return Axis.TryParse(Value, out Axis val) ? val : val;
        }

        public void SetValueAsAxis(Axis value)
        {
            Value = value.ToVMFString();
        }
    }
}
