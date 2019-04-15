using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VMFParser
{
    public abstract class AVBlock
    {
        /// <summary> Contains all sub IVNodes. </summary>
        public IList<IVNode> Body { get; protected set; }

        /// <summary> Returns all nodes matching given names. </summary>
        /// <param name="name"> Only get values with this Name. </param>
        public IEnumerable<IVNode> GetNodesWithName(string name)
        {
            return GetNodesWithName<IVNode>(name);
        }

        /// <summary> Returns all nodes matching given name, and <see cref="VMFParser.IVNode"/> type. </summary>
        /// <typeparam name="T"> Only get values with this type. </typeparam>
        /// <param name="name"> Only get values with this Name. </param>
        /// <returns></returns>
        public IEnumerable<T> GetNodesWithName<T>(string name) where T : class, IVNode
        {
            return Body.WhereClass<T>().Where(node => node.Name == name);
        }

        /// <summary> Returns <see cref="VMFParser.VProperty"/> objects with given name and value. </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<VProperty> GetPropertiesWithNameValue(string name, string value)
        {
            return Body.WhereClass<VProperty>().Where(node => node.Name == name && node.Value == value);
        }

        /// <summary> Checks if <see cref="VMFParser.AVBlock"/> has any <see cref="VMFParser.VProperty"/> with the given name and value. </summary>
        public bool PropertyMatch(string name, string value)
        {
            return GetPropertiesWithNameValue(name, value).Count() > 0;
        }

        /// <summary> Returns <see cref="VMFParser.AVBlock"/> objects with a property matching given name and value.</summary>
        /// <param name="name">Block Name</param>
        /// <param name="propertyName">Sub-Property Name</param>
        /// <param name="propertyValue">Sub-Property Value</param>
        /// <returns></returns>
        public IEnumerable<VBlock> GetBlocksWithNameAndPropertyValue(string name, string propertyName, string propertyValue)
        {
            return Body.WhereClass<VBlock>().Where(block => block.Name == name && block.PropertyMatch(propertyName, propertyValue));
        }
        
        /// <summary> Shortcut for <see cref="GetNodesWithName(string)"/>(name)
        /// Returns all nodes matching given names. </summary>
        /// <param name="name"> Only get values with this Name. </param>
        public IEnumerable<IVNode> this[string name]
        {
            get { return GetNodesWithName(name); }
        }

        /// <summary> Shortcut for <see cref="GetBlocksWithNameAndPropertyValue(string, string, string)"/>(name)
        ///  Returns <see cref="VMFParser.AVBlock"/> objects with a property matching given name and value. </summary>
        /// <param name="name">Block Name</param>
        /// <param name="propertyName">Sub-Property Name</param>
        /// <param name="propertyValue">Sub-Property Value</param>
        /// <returns></returns>
        public IEnumerable<VBlock> this[string name, string propertyName, string propertyValue]
        {
            get { return GetBlocksWithNameAndPropertyValue(name, propertyName, propertyValue); }
        }
    }
}