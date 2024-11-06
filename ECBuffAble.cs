using System.Collections.Generic;
using EXFunctionKit;

namespace EasyFramework
{
    public class ECBuffAble: AEntity,IBuffableUnit
    {
        Dictionary<BuffTag, HashSet<ITagAble<BuffTag>>> ITagAbleContainer<BuffTag>.TagMap { get; set; }
        Dictionary<int, HashSet<IBuff>> IBuffableUnit.PollOrder { get; set; }
        Dictionary<EBuff, IBuff> IBuffableUnit.Buffs { get; set; }
        Dictionary<EMeans, Dictionary<EBuff, CoroutineHandle>> IBuffableUnit.Timers { get; set; }
        public override IStructure Structure => BattleStructure.TryRegister();

        protected override void OnInit()
        {
            base.OnInit();
            this.As<IBuffableUnit>().InitBuffableUnit();
        }

        protected override void OnDispose(bool usePool)
        {
            this.As<IBuffableUnit>().Set(unit =>
            {
                unit.TagMap?.Clear();
                unit.PollOrder?.ForEachValue(set => set.Clear());
                unit.Buffs?.Clear();
                unit.Timers?.ForEachValue(dic => dic.ForEach(keyValuePair =>
                {
                    if (keyValuePair.Value != null)
                    {
                        keyValuePair.Value.Cancel();
                        dic[keyValuePair.Key] = null;
                    }
                }));
            });
        }

        public IEasyEvent UpdateEvent { get; }= new EasyEvent();
    }
}