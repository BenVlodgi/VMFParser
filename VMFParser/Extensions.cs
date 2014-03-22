using System;

namespace VMFParser
{
    public static class Extensions
    {
        /// <summary>Returns a new array from given index with the specified length. </summary>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
