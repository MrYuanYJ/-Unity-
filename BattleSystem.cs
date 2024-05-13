using UnityEngine;

namespace EasyFramework
{
    public class BattleSystem: AStructure<BattleSystem>
    {
        private BattleSystem(){}
        public override void OnInit()
        {
            Debug.Log("BattleSystem Init");
            this.RegisterSystem<EasyBuffSystem>();
        }
    }
}