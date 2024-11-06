namespace EasyFramework
{
    public abstract class APropertyEntity<T>: AMonoEntity<T>,IPropertyEntity where T : AMonoEntityCarrier,IProperty
    {
        public override IStructure Structure => UnitStructure.TryRegister();
        public abstract EUnitProperty PropertyType { get; }
        public RoleProperties Property { get; private set; }
        
        protected override void OnInit()
        {
            if (Mono.BasedOnThis)
                Property = BBPropertyFunc.SetRoleProperty((IUnitEntity) Root, PropertyType.ToString(), new RoleProperties(Mono.BaseValue,Mono.CurrentPercent));
            else
                Property = BBPropertyFunc.GetOrAddRoleProperty((IUnitEntity) Root, PropertyType.ToString());
        }
    }
}