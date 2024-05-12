using System.Collections.Generic;
using EasyFramework.EasyTagKit;

namespace EasyFramework
{
    public abstract class ABuff: IBuff
    {
        IBuffableUnit IBuff.Unit { get; set; }
        float IBuff.ExistingTime { get; set; }
        float IBuff.Magnification { get; set; } = 1;
        bool IBuff.IsDeInit { get; set; }
        EBuff IBuff.EBuff { get; set; }
        HashSet<BuffTag> ITagAble<BuffTag>.Tags { get; set; }


        public IBuffableUnit Unit => ((IBuff)this).Unit;
        public float Magnification => ((IBuff)this).Magnification;
        public EBuff EBuff => ((IBuff)this).EBuff;
        
        
        public abstract float Duration { get; }
        public abstract float MaxMagnification { get; }
        
        
        public virtual void OnInit(IBuffAddData data){}
        public virtual void OnDeInit(){}
        public virtual void OnRunning(){}
        public virtual void OnExecute(){}
        public virtual void OnStack(){}
        public virtual void OnUnstack(){}
    }
}