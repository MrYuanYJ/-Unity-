using System;
using System.Collections.Generic;

namespace EasyFramework
{
    /// <summary>
    /// buff类型，(int)(枚举值/100)表示buff的优先级,越小表示优先级越高，必如200比300优先级高，200与201的优先级相同
    /// </summary>
    public enum EBuff
    {
        None = 0,
        [Bind(typeof(Fire))]  Fire = 100,
        Earth = 200,
        [Bind(typeof(Water))] Water = 300,
        Lightning,
        [Bind(typeof(Ice))]   Ice = 400,
        Grass = 500,
        Air = 600,

        Bleed = 700,
        Stun = 800,
        Burn = 900,
        Freeze = 1000,
        [Bind(typeof(Evaporation))] Evaporation = 1100,
        [Bind(typeof(Melt))] Melt = 1200,
    }

    public enum BuffTag
    {
        Normal,
        Element,
        Fire,
        Reaction
    }

    public class BuffTagAttribute : Attribute
    {
        public readonly HashSet<BuffTag> Tags = new();
        public BuffTagAttribute(BuffTag tag,params BuffTag[] tags)
        {
            Tags.Add(tag);
            foreach (var t in tags)
                Tags.Add(t);
        }
    }
}