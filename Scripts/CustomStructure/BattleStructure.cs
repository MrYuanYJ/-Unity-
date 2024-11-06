using EasyFramework.EasySystem;
using Sirenix.Utilities;
using UnityEngine;

namespace EasyFramework
{
    public class BattleStructure: AStructure<BattleStructure>
    {
        protected override void OnInit()
        {
            this.System<BuffSystem>();
            this.System<DamageSystem>();
            this.DisposeWith(Game.TryRegister());
        }
    }
}