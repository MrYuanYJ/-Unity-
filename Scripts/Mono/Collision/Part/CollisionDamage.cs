using System.Collections.Generic;

namespace EasyFramework
{
    public enum ECollisionDamage
    {
        OnlyDamageToFirst,
        DamageToAll
    }
    [System.Serializable]
    public struct CollisionDamageData
    {
        public string CollisionGroup;
        public ECollisionDamage CollisionDamageType;
        public EUnitProperty DamageBase;
        public EPropertyValue DamageBaseValue;
        public float DamageMultiplier;
        public int Damage;
        public EDamage DamageType;
        public EDamageAct DamageAct;
    }

    public class CollisionDamage: AMonoEntityCarrier
    {
        public List<CollisionDamageData> CollisionDamageDatas;
    }
}