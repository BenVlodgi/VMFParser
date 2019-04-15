using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary> Returns first <see cref="VMFParser.VBlock"/> in list. </summary>
        public static VBlock VBlock(this IEnumerable<IVNode> body)
        {
            return body.WhereClass<VBlock>().FirstOrDefault();
        }

        /// <summary> Returns first <see cref="VMFParser.VBlock"/> in list that matches the predicate. </summary>
        public static VBlock VBlock(this IEnumerable<IVNode> body, Func<VBlock, bool> predicate)
        {
            return body.WhereClass<VBlock>().FirstOrDefault(predicate);
        }

        /// <summary> Returns first <see cref="VMFParser.VProperty"/> in list. </summary>
        public static VProperty VProperty(this IEnumerable<IVNode> body)
        {
            return body.WhereClass<VProperty>().FirstOrDefault();
        }

        /// <summary> Returns first <see cref="VMFParser.VProperty"/> in list that matches the predicate. </summary>
        public static VProperty VProperty(this IEnumerable<IVNode> body, Func<VProperty, bool> predicate)
        {
            return body.WhereClass<VProperty>().FirstOrDefault(predicate);
        }

        /// <summary> Returns given list, with values cast to given type, nulls are removed. </summary>
        /// <typeparam name="AsType">Type all values will be cast to. </typeparam>
        public static IEnumerable<AsType> WhereClass<AsType>(this IEnumerable<object> data) where AsType : class
        {
            return data.Where(d => d as AsType != null).Cast<AsType>();
        }

        /// <summary> Adds key value to dictionary unless already exists. </summary>
        /// <param name="update">When true, value will be updated. </param>
        /// <returns> True if given value was set. </returns>
        public static bool AddSafe<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value, bool update = false)
        {
            if(dict.ContainsKey(key))
            {
                if (update)
                {
                    dict[key] = value;
                    return true;
                }
                else return false;
            }
            else
            {
                dict.Add(key, value);
                return true;
            }
        }
    }
}
