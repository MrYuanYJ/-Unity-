using System;
using System.Collections.Generic;
using EasyFramework.EasyTagKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public interface IBuffableUnit: ITagAbleContainer<BuffTag>
    {
        Dictionary<int,HashSet<IBuff>> PollOrder { get; protected set; }
        Dictionary<EBuff,IBuff> Buffs{ get; protected set; }
        Dictionary<EMeans, Dictionary<EBuff,CoroutineHandle>> Timers { get; protected set; }
        
        void InitBuffableUnit()
        {
            if (PollOrder==null||PollOrder.Count == 0)
            {
                var enumerable = Enum.GetValues(typeof(EBuff)).AsEnumerable(o =>o.GetHashCode()/100);
                Buffs = new();
                PollOrder = new();
                Timers = new();
                TagMap = new();
                PollOrder.Fill(enumerable,_=>new HashSet<IBuff>());
                Timers.Fill(_ => new Dictionary<EBuff, CoroutineHandle>().Fill(_ => null));
            }
        }

        void PollBuffs()
        {
            foreach (var buffs in PollOrder.Values)
            {
                foreach (var buff in buffs)
                {
                    buff.Running(Time.deltaTime);
                }
            }
        }

        HashSet<ITagAble<BuffTag>> GetBuffByTag(BuffTag tag,params BuffTag[] tags)
        {
            return this.GetIntersect(tag, tags);
        }
    }
}