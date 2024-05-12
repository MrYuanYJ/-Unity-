using UnityEngine.PlayerLoop;

namespace EasyECS.New
{
    public abstract class AEasySystem<T> where T : struct, IEasyComponent
    {
        public ref T[] Components => ref EasyECSMgr.GetEasyComponents<T>().Components;
    }

    
}