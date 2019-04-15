using System;
using System.Collections.Generic;
using System.Linq;

namespace VMFParser
{
    /// <summary>Represents a VMF</summary>
    public class VMF : AVBlock, IDeepCloneable<VMF>
    {
        protected const string STR_id = "id";

        /// <summary> Hold pointers to each Block with a property for id and the int lookup value. </summary>
        protected Dictionary<int, VBlock> BlockIDTable = null;

        protected int HighestID = 0;


        //public IList<IVNode> Body { get; protected set; }

        #region Normal VMF Nodes

        protected VBlock versionInfo;
        public VBlock VersionInfo { get { return versionInfo ?? (versionInfo = this["versioninfo"].VBlock()); } }
        
        protected VBlock visGroups;
        public VBlock VisGroups { get { return visGroups ?? (visGroups = this["visgroups"].VBlock()); } }

        protected VBlock viewSettings;
        public VBlock ViewSettings { get { return viewSettings ?? (viewSettings = this["viewsettings"].VBlock()); } }

        protected VBlock world;
        public VBlock World { get { return world ?? (world = this["world"].VBlock()); } }

        protected VBlock cameras;
        public VBlock Cameras { get { return cameras ?? (cameras = this["cameras"].VBlock()); } }

        protected VBlock cordons;
        public VBlock Cordons { get { return cordons ?? (cordons = this["cordons"].VBlock()); } }

        #endregion


        /// <summary>Initializes a new instance of the <see cref="VMF"/> class.</summary>
        public VMF()
        {
            Body = new List<IVNode>();
        }

        /// <summary>Initializes a new instance of the <see cref="VMF"/> class from a list of IVNodes.</summary>
        public VMF(IList<IVNode> body)
        {
            Body = body;
        }

        /// <summary>Initializes a new instance of the <see cref="VMF"/> class from the VMF text.</summary>
        public VMF(string[] text)
        {
            Body = Utils.ParseToBody(text);
        }

        public string ToVMFString()
        {
            return string.Join(Environment.NewLine, ToVMFStrings());
        }

        /// <summary>Generates the VMF text from the body of IVNodes.</summary>
        public string[] ToVMFStrings()
        {
            return Utils.BodyToString(Body).ToArray();
        }
        
        public void RebuildBlockIDTable()
        {
            BlockIDTable = new Dictionary<int, VBlock>();

            // If there are any root vmf level properties called ID, we'll be ignoring that.
            // This has never been seen
            foreach (var block in Body.WhereClass<VBlock>())
            {
                RebuildBlockIDTableRecursive(block);
            }
        }

        protected void RebuildBlockIDTableRecursive(VBlock container)
        {
            VProperty idProperty = container["id"].VProperty();
            if (idProperty != null)
            {
                RegisterID(idProperty.GetValueAsInt(), container);
            }

            foreach (var node in container.Body.WhereClass<VBlock>())
            {
                RebuildBlockIDTableRecursive(node);
            }
        }

        public VBlock GetIDValue(int id)
        {
            if (BlockIDTable == null)
            {
                RebuildBlockIDTable();
            }

            return BlockIDTable[id];
        }

        public void RegisterID(int id, VBlock block)
        {
            BlockIDTable.AddSafe(id, block);

            if (id > HighestID)
                HighestID = id;
        }

        
        public int GetUniqueID(bool consume = false, VBlock consumingBlock = null)
        {
            if (BlockIDTable == null)
            {
                RebuildBlockIDTable();
            }

            int newID = HighestID + 1;

            // We only search 10 times to find a random ID, lets not get stuck here
            for (int counter = 0; BlockIDTable.ContainsKey(newID) && counter < 10; counter++)
            {
                int minRand = newID > int.MaxValue / 2 ? 0 : newID;
                newID = new Random().Next(minRand, int.MaxValue);
            }

            if (consume)
            {
                RegisterID(newID, consumingBlock);
            }
            return newID;
        }

        //private static bool ContainsID(IList<IVNode> nodes, string id)
        //{
        //    foreach (var node in nodes)
        //    {
        //        if ((node.GetType() == typeof(VBlock) && ContainsID((node as VBlock).Body, id)) ||
        //            (node.GetType() == typeof(VProperty) && node.Name == "id" && (node as VProperty).Value == id))
        //            return true;
        //    }
        //    return false;
        //}

        public VMF DeepClone()
        {
            return new VMF(Body == null ? null : Body.Select(node => node.DeepClone()).ToList());
        }
    }
}
