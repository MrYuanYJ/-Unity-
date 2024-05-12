using System.Collections.Generic;
using System.Linq;

namespace EasyFramework.EasyTagKit
{
    public interface ITagAble
    {
        bool HasTag<TTag>(TTag tag);
        bool HasTags<TTag>(params TTag[] tags);
    }
    public interface ITagAble<T>: ITagAble
    {
        HashSet<T> Tags { get; set; }

        bool ITagAble.HasTag<TTag>(TTag tag)
        {
            if (tag is T t)
                return HasTag(t);
            return false;
        }
        bool HasTag(T tag)=> Tags.Contains(tag);

        bool ITagAble.HasTags<TTag>(params TTag[] tags)
        {
            if (tags is T[] t)
                return HasTags(t);
            return false;
        }
        bool HasTags(params T[] tags)=> tags.ToHashSet().IsSubsetOf(Tags);
    }
}