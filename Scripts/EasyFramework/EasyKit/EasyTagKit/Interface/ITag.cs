namespace EasyFramework.EasyTagKit
{
    public interface ITag
    {
        ITag Parent { get; }
        
        
    }

    public interface ITag<T> : ITag
    {
        ITag ITag.Parent=> Parent;
        public new ITag<T> Parent { get; }
    }
}