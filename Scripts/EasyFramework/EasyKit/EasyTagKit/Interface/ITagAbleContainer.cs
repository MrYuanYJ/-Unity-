using System.Collections.Generic;

namespace EasyFramework.EasyTagKit
{
    public interface ITagAbleContainer<TTag>
    {
        Dictionary<TTag,HashSet<ITagAble<TTag>>> TagMap { get; protected set; }

        void Add(ITagAble<TTag> tagAble)
        {
            TagMap??= new Dictionary<TTag, HashSet<ITagAble<TTag>>>();
            foreach (var tag in tagAble.Tags)
            {
                if (!TagMap.TryGetValue(tag, out var hashSet))
                {
                    TagMap[tag] = new HashSet<ITagAble<TTag>>();
                }
                TagMap[tag].Add(tagAble);
            }
        }

        void Remove(ITagAble<TTag> tagAble)
        {
            foreach (var tag in tagAble.Tags)
            {
                if (TagMap.TryGetValue(tag, out var hashSet))
                {
                    hashSet.Remove(tagAble);
                }
            }
        }

        HashSet<ITagAble<TTag>> GetIntersect(TTag tag ,params TTag[] tags)
        {
            if (TagMap.TryGetValue(tag, out var result))
            {
                foreach (var tempTag in tags)
                {
                    if (TagMap.TryGetValue(tempTag, out var hashSet))
                    {
                        result.IntersectWith(hashSet);
                    }
                }
            }

            return result;
        }

        HashSet<ITagAble<TTag>> GetUnion(TTag tag, params TTag[] tags)
        {
            TagMap.TryGetValue(tag, out var result);
            foreach (var tempTag in tags)
            {
                if (TagMap.TryGetValue(tempTag, out var hashSet))
                {
                    result ??= new HashSet<ITagAble<TTag>>();
                    result.UnionWith(hashSet);
                }
            }

            return result;
        }
    }
}