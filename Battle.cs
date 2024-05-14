using UnityEngine;

namespace EasyFramework
{
    public class Battle: AStructure<Battle>
    {
        private Battle(){}
        public override void OnInit()
        {
            Debug.Log("Battle Init");
            this.System<BuffSystem>();
            this.DisposeWith(Game.Get());
        }
    }
}