using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public class Melt: ABuff
    {
        public override float Duration { get; } = 0;
        public override float MaxMagnification { get; } = 3;

        private float _damage;

        public override void OnInit(IBuffAddData data)
        {
            _damage = data.As<NormalBuffAddData>().Damage;
            Debug.Log($"融化！{_damage*Magnification},倍率:{Magnification}");
        }

        public override void OnExecute()
        {
            
        }
    }
}