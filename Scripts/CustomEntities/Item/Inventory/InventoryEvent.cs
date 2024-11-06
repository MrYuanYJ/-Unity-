namespace EasyFramework
{
    public struct InventoryEvent
    {
        public sealed class LockInventory : AEventIndex<LockInventory,LockInventoryParams>{}
    }

    public struct LockInventoryParams
    {
        public IUnitEntity Unit;
        public bool IsLocked;
        public bool IsPlayAni;
    }
}