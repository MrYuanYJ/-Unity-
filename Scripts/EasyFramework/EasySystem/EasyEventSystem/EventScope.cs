namespace EasyFramework
{
    [System.Flags]
    public enum EventScope: long
    {
        Global=1<<0,
        Framework=1<<1,
        Scene=1<<2,
        GameObject=1<<3,
        Component=1<<4,
        
        All=long.MaxValue,
    }
}