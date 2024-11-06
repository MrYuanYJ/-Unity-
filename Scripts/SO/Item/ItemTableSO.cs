
using UnityEngine;

namespace EasyFramework
{
    public class ItemTableSO: AScriptableObjectSingleton<ItemTableSO>
    {
        public ItemSOBase[] items;
    }
    
    #if UNITY_EDITOR

    public class ItemTableSOModule : AFrameworkModule
    {
        public override ScriptableObject Data => ItemTableSO.Instance;
        public override string ModuleName => "ItemTable";
        public override int Priority { get; } = -1;
    }
#endif
}