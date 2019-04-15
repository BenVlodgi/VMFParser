namespace VMFParser
{
    public interface IDeepCloneable<T> where T : IDeepCloneable<T>
    {
        T DeepClone();
    }
}
