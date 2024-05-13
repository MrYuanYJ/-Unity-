namespace EasyFramework
{
    public class GameInitComponent: AEntity
    {
        public override IStructure GetStructure() => Game.Instance;
    }
}