namespace VMFParser
{
    /// <summary>Represents an entry in a VMF.</summary>
    public interface IVNode : IDeepCloneable<IVNode>
    {
        string Name { get; }
    }
}
