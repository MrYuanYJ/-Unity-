using UnityEngine;

namespace EasyFramework
{
    [AddComponentMenu("Easy Framework/Item/Weapon")]
    public class Weapon: Item
    {
        public EEquipment UnitType=> EEquipment.Weapon;
        public WeaponProcedureMachine WeaponMachine;
        public float windUp;
        public float attack;
        public float windDown;
        public float cooldown;
    }
}