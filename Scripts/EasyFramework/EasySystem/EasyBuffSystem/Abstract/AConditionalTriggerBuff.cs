using System.Collections.Generic;
using EasyFramework.EasyTagKit;

namespace EasyFramework
{
    public abstract class AConditionalTriggerBuff: IConditionalTriggerBuff
    {
        IBuffableUnit IBuff.Unit { get; set; }
        float IBuff.ExistingTime { get; set; }
        float IBuff.Magnification { get; set; }
        EBuff IBuff.EBuff { get; set; }
        bool IBuff.IsDeInit { get; set; }
        int IConditionalTriggerBuff.TriggerCount { get; set; }
        HashSet<BuffTag> ITagAble<BuffTag>.Tags { get; set; }
        
        public abstract float Duration { get; }
        public abstract float MaxMagnification { get; }
        public abstract int MaxTriggerCount { get; }
        
        
        public virtual void OnInit(IBuffAddData data){}
        public virtual void OnDeInit(){}
        public virtual void OnRunning(){}
        public virtual void OnExecute(){}
        public virtual void OnStack(){}
        public virtual void OnUnstack(){}
    }
}