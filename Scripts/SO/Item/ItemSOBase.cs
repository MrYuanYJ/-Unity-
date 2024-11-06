using UnityEngine;

namespace EasyFramework
{
    [CreateAssetMenu(fileName = "New ItemSOBase", menuName = "EasyFramework/ItemSOBase")]
    public class ItemSOBase: ScriptableObject
    {
        public int ItemID;
        public EItem ItemType;
        public string ItemName;
        public string Description;
        public Sprite Icon;
        public int MaxCount;
    }
}