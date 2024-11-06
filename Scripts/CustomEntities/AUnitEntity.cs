using System;
namespace EasyFramework
{
    public abstract class AUnitEntity<T,TStructure>: AMonoEntity<T>,IUnitEntity where T : AMonoEntityCarrier where TStructure : Singleton<TStructure>,IStructure
    {
        public long UnitId { get; private set; }
        public abstract Enum UnitType { get; }
        public EasyEventDic EventDic { get; } = new();
        public EasyFuncDic FuncDic { get; } = new();
        public override IStructure Structure => Singleton<TStructure>.TryRegister();

        protected override void OnInit()
        {
            UnitId = EasyID.GetNewID.InvokeFunc();
            UnitEvent.Register.InvokeEvent(this);
            Mono.GetOrAddMonoEntity<BBProperty>();
            UnitFunc.GetBelongUnit.RegisterFunc(this, GetBelongUnitEntity);
        }

        protected override void OnDispose(bool usePool)
        {
            UnitEvent.UnRegister.InvokeEvent(this);
            UnitFunc.GetBelongUnit.UnRegisterFunc(this, GetBelongUnitEntity);
        }
        protected abstract IUnitEntity GetBelongUnitEntity();
    }
}