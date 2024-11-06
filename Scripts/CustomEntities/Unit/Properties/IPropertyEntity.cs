namespace EasyFramework
{
    public interface IPropertyEntity: IMonoEntity
    {
        public EUnitProperty PropertyType { get; }
        public RoleProperties Property { get; }
    }
}