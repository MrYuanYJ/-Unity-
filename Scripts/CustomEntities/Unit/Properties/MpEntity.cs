namespace EasyFramework
{
    public class MpEntity: APropertyEntity<Mp>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public override EUnitProperty PropertyType => EUnitProperty.Mp;

        protected override void OnActive()
        {
            Property.CurrentPct.InvokeAndRegister(OnMpChanged);
        }

        protected override void OnUnActive()
        {
            Property.CurrentPct.UnRegister(OnMpChanged);
        }
        
        private void OnMpChanged(float percent)
        {
            var entity = (IUnitEntity) Root;
            RoleEvent.MpChanged.InvokeEvent(entity, percent);
        }
    }
}