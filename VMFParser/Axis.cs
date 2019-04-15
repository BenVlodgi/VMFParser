namespace VMFParser
{
    /// <summary>
    /// Example:
    /// "uaxis" "[1 0 0 0] 0.25"
    /// "Name" "[X Y Z Translation] Scale"
    /// </summary>
    public struct Axis
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
        public decimal Translation { get; set; }
        public decimal Scale { get; set; }

        public Axis(string s)
        {
            decimal x, y, z, translation, scale;

            if (ParseValues(s, out x, out y, out z, out translation, out scale))
            {
                X = x;
                Y = y;
                Z = z;
                Translation = translation;
                Scale = scale;
            }
            else
            {
                X = 0;
                Y = 0;
                Z = 0;
                Translation = 0;
                Scale = 0;
            }
        }

        public override string ToString()
        {
            return ToVMFString();
        }

        public string ToVMFString()
        {
            return string.Format("[{0} {1} {2} {3}] {4}", X, Y, Z, Translation, Scale);
        }


        public static bool TryParse(string s, out Axis axis)
        {
            decimal x, y, z, translation, scale;

            if (ParseValues(s, out x, out y, out z, out translation, out scale))
            {
                axis = new Axis()
                {
                    X = x,
                    Y = y,
                    Z = z,
                    Translation = translation,
                    Scale = scale,
                };
                return true;
            }
            else
            {
                axis = new Axis();
                return false;
            }
        }

        private static bool ParseValues(string s, out decimal x, out decimal y, out decimal z, out decimal translation, out decimal scale)
        {
            var array = s.Trim().Split(new char[] { ' ', '[', ']' }, 5);

            x = y = z = translation = scale = 0;

            if (array.Length != 5) return false;
            if (!decimal.TryParse(array[0], out x)) return false;
            if (!decimal.TryParse(array[1], out y)) return false;
            if (!decimal.TryParse(array[2], out z)) return false;
            if (!decimal.TryParse(array[3], out translation)) return false;
            if (!decimal.TryParse(array[4], out scale)) return false;

            return true;
        }
    }
}
