namespace EasyFramework
{
    public class ShieldedEntity: APropertyEntity<Shield>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public override EUnitProperty PropertyType => EUnitProperty.Shield;

        protected override void OnActive()
        {
            Property.CurrentPct.InvokeAndRegister(OnShieldChanged);
        }

        protected override void OnUnActive()
        {
            Property.CurrentPct.UnRegister(OnShieldChanged);
        }
        
        private void OnShieldChanged(float percent)
        {
            var entity = (IUnitEntity) Root;
            RoleEvent.ShieldChanged.InvokeEvent(entity, percent);
        }
    }
}