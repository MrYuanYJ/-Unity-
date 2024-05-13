using System;
using System.Collections.Generic;
using EasyFramework.EasyTaskKit;
using EasyFramework.EventKit;
using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public partial struct BuffEvent
    {
        public abstract class AddBuff : AEventIndex<AddBuff, IBuffAddData> { }
        public abstract class AddBuffByMeans : AEventIndex<AddBuffByMeans,EMeans, IBuffAddData> { }
        public abstract class RemoveBuff : AEventIndex<RemoveBuff, IBuffableUnit, EBuff, float> { }
    }
    public class EasyBuffSystem : ASystem,IEasyUpdate
    {
        private readonly List<IBuffableUnit> _pollLst = new List<IBuffableUnit>();
        private readonly Queue<Action> _setQueue = new Queue<Action>();
        private static EasyBuffSetting Setting => EasyBuffSetting.Instance;

        public override void OnInit()
        {
            base.OnInit();
            BuffEvent.AddBuff.RegisterEvent(AddBuff);
            BuffEvent.AddBuffByMeans.RegisterEvent(AddBuffByMeans);
            BuffEvent.RemoveBuff.RegisterEvent(RemoveBuff);
        }

        public override void OnDispose()
        {
            base.OnDispose();
            BuffEvent.AddBuff.UnRegisterEvent(AddBuff);
            BuffEvent.AddBuffByMeans.UnRegisterEvent(AddBuffByMeans);
            BuffEvent.RemoveBuff.UnRegisterEvent(RemoveBuff);
            _pollLst.Clear();
            _setQueue.Clear();
        }

        private IBuff CreateBuff(EBuff eBuff)
        {
            if (!IBuff.BuffPool.TryGetValue(eBuff, out var pool))
            {
                pool = new EasyPool<IBuff>(() => (IBuff) Activator.CreateInstance(Setting.AllBuff[eBuff]), null, null);
                IBuff.BuffPool[eBuff] = pool;
            }
            var buff = pool.Fetch();
            buff.Tags = Setting.GetBuffTags(eBuff);
            return buff;
        }
        public void AddBuff(IBuffAddData data)
        {
            data.Unit.InitBuffableUnit();
            AddBuffToDic(data);
        }

        public void AddBuffByMeans(EMeans means, IBuffAddData data)
        {
            var buffType = data.EBuff;
            var unit = data.Unit;
            unit.InitBuffableUnit();
            if (unit.Timers[means][buffType] == null)
            {
                var cd=Setting.GetAddElementBuffByMeanCd(means);
                unit.Timers[means][buffType]=EasyTask.Delay(cd);
                unit.Timers[means][buffType].Completed += _ =>
                {
                    unit.Timers[means][buffType] = null;
                };
                AddBuffToDic(data);
            }
        }

        public void RemoveBuff(IBuffableUnit unit, EBuff eBuff, float stackCount = float.MaxValue)
        {
            _setQueue.Enqueue(() =>
            {
                if (unit.Buffs.TryGetValue(eBuff, out IBuff existingBuff))
                {
                    if (existingBuff.Magnification > stackCount)
                    {
                        existingBuff.Unstack(stackCount);
                    }
                    else
                    {
                        existingBuff.DeInit();
                        unit.Buffs.Remove(eBuff);
                        unit.PollOrder[eBuff.GetHashCode() / 100].Remove(existingBuff);
                        unit.Remove(existingBuff);
                        IBuff.BuffPool[eBuff].Recycle(existingBuff);
                        if (unit.Buffs.Count == 0)
                            LeavePoll(unit);
                    }
                }
            });
        }
        private void AddBuffToDic(IBuffAddData data)
        {
            _setQueue.Enqueue(() =>
            {
                if(Reaction(data))
                    return;
                var buff= CreateBuff(data.EBuff);
                
                if (data.Unit.Buffs.Count == 0)
                    JoinPoll(data.Unit);
                if (!data.Unit.Buffs.TryGetValue(data.EBuff, out IBuff existingBuff))
                {
                    data.Unit.Buffs[data.EBuff] = buff;
                    data.Unit.PollOrder[data.EBuff.GetHashCode() / 100].Add(buff);
                    data.Unit.Add(buff);
                    buff.Init(data);
                }
                else
                {
                    existingBuff.Stack(data.Magnification);
                }
            });
        }
        public void ClearBuffs(IBuffableUnit unit)
        {
            foreach (var buff in unit.Buffs.Values)
            {
                buff.DeInit();
            }
            unit.Buffs.Clear();
            unit.PollOrder.ForEachValue(h=>h.Clear());
            LeavePoll(unit);
        }

        public void ClearBuffsByTag(string buffTag)
        {

        }
        public EasyEvent<IEasyUpdate> UpdateEvent { get; set; } = new();

        public void OnUpdate()
        {
            foreach (IBuffableUnit unit in _pollLst) { unit.PollBuffs(); }
            
            while (_setQueue.Count > 0) { _setQueue.Dequeue()(); }
        }

        private void JoinPoll(IBuffableUnit unit)
        {
            _setQueue.Enqueue(() => _pollLst.Add(unit));
        }

        private void LeavePoll(IBuffableUnit unit)
        {
            _setQueue.Enqueue(() => _pollLst.Remove(unit));
        }

        public Reactor GetReactor(EBuff eBuff, IBuffableUnit unit)
        {
            if (Setting.ReactorDic.TryGetValue(eBuff, out var reactors))
            {
                foreach (var reactor in reactors)
                {
                    var anotherMaterial = reactor.GetMaterial(eBuff).other;
                    if (unit.Buffs.ContainsKey(anotherMaterial.Buff))
                    {
                        return reactor;
                    }
                }
            }

            return null;
        }

        public bool Reaction(IBuffAddData data)
        {
            var reactor = GetReactor(data.EBuff, data.Unit);
            if (reactor!= null)
            {
                var material = reactor.GetMaterial(data.EBuff);
                var selfProportion = data.Magnification / material.self.Consumption;
                var otherProportion = data.Unit.Buffs[material.other.Buff].Magnification / material.other.Consumption;

                var proportion = Mathf.Min(selfProportion, otherProportion);

                RemoveBuff(data.Unit, material.other.Buff, Mathf.CeilToInt(material.other.Consumption * proportion));
                data.EBuff = reactor.NewBuff;
                data.Magnification = proportion;
                AddBuff(data);
                return true;
            }

            return false;
        }
    }
}