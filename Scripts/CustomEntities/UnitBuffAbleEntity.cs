namespace EasyFramework
{
    public class UnitBuffAbleEntity: AMonoEntity<UnitBuffAble>
    {
        public override IStructure Structure => UnitStructure.TryRegister();
    }
}