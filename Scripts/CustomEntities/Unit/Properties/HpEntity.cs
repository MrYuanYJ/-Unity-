namespace EasyFramework
{
    public enum EHpState
    {
        Low,
        Half,
        Normal,
        Overflow
    }
    public class HpEntity: APropertyEntity<Hp>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public override EUnitProperty PropertyType => EUnitProperty.Hp;
        
        private readonly ESProperty<EHpState> _hpStateProperty = new();
        

        protected override void OnActive()
        {
            Property.CurrentPct.InvokeAndRegister(OnHpChanged);
            var entity = (IUnitEntity) Root;
            RoleEvent.AddRoleState.InvokeEvent(entity,ERoleState.Die);
            RoleFunc.GetRoleState.InvokeFunc(entity, ERoleState.Die).OnEnter(OnDie);
        }

        protected override void OnUnActive()
        {
            Property.CurrentPct.UnRegister(OnHpChanged);
            RoleEvent.RemoveRoleState.InvokeEvent((IUnitEntity)Root,ERoleState.Die);
        }

        private void OnHpChanged(float percent)
        {
            var entity = (IUnitEntity) Root;
            RoleEvent.HpChanged.InvokeEvent(entity, percent);
            if (percent <= 0)
            {
                RoleEvent.ChangeRoleState.InvokeEvent(entity, ERoleState.Die);
            }

            RefreshHpState();
        }

        private void OnDie()
        {
            RoleEvent.Death.InvokeEvent((IUnitEntity) Root);
        }

        private void RefreshHpState()
        {
            var hpPct = Property.CurrentPct.Value;
            _hpStateProperty.Value = hpPct switch
            {
                > 1 => EHpState.Overflow,
                >= 0.75f => EHpState.Normal,
                >= 0.25f => EHpState.Half,
                _ => EHpState.Low,
            };
        }

    }
}