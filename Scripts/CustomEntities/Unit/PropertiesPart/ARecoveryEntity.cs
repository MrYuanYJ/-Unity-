namespace EasyFramework
{
    public abstract class ARecoveryEntity<T>: AMonoEntity<T> where T : AMonoEntityCarrier,IRecovery
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        private IPropertyEntity _propertyEntity;
        private RoleProperties _recoveryProperty;

        protected override void OnInit()
        {
            _propertyEntity = (IPropertyEntity) Parent;
            var entity = (IUnitEntity) Root;
            _recoveryProperty=BBPropertyFunc.SetRoleProperty(entity,
                BBPropertyFunc.GetRecoveryStr(_propertyEntity.PropertyType), new RoleProperties(0));
            _recoveryProperty.BaseAdd.Value = Mono.RecoveryData.RecoveryValue;
            _recoveryProperty.BasePct.Value = (int) (Mono.RecoveryData.RecoveryPercent * 10000);
            _propertyEntity.Property.CurrentPct.InvokeAndRegister(OnPropertyChange);
        }

        protected override void OnDispose(bool usePool)
        {
            _propertyEntity.Property.CurrentPct.UnRegister(OnPropertyChange);
        }

        private void OnPropertyChange(float percent)
        {
            var entity = (IUnitEntity) Root;
            if (RoleFunc.GetCurrentRoleState.InvokeFunc(entity) != ERoleState.Die)
            {
                if (percent < 1)
                    this.SetActive(true);
                else if (percent >= 1)
                    this.SetActive(false);
            }
            else
            {
                this.SetActive(false);
            }
        }
        protected void Recovery()
        {
            var maxValue = _propertyEntity.Property.MaxValue;
            var recoveryValue = (_recoveryProperty.FinalAdd.Value +
                                 (_recoveryProperty.BaseAdd.Value + maxValue * (_recoveryProperty.GetBasePct()-1)) *
                                 _recoveryProperty.GetFinalPct()) *
                                _recoveryProperty.CurrentPct.Value;
            _propertyEntity.Property.AddValuePercent(recoveryValue/maxValue);
        }
        
    }
}