using UnityEngine;

namespace EasyFramework
{
    [AddComponentMenu("Easy Framework/Item/Equipment/Weapon Part/Shoot")]
    public class Shoot: AMonoEntityCarrier
    {
        public float shootWindUp;
        public float shootWindDown;
        public float shootCooldown;
    }
}