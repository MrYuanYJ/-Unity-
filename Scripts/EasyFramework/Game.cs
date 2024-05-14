using EasyFramework.EasySystem;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework
{
    public class Game: AStructure<Game>
    {
        private Game(){}
        public override void OnInit()
        {
            Debug.Log("Game Init");
            this.System<EasyEventSystem>();
            this.System<EasyLifeCycleEventSystem>();
            this.System<EasyCodeLoaderSystem>();
            GlobalEvent.ApplicationInit.InvokeEvent(this);
        }

        public override void OnDispose()
        {
            EasyEventDic.Global.ClearAll();
            EasyFuncDic.Global.ClearAll();
            ClassEvent.Global.ClearAll();
            ClassFunc.Global.ClearAll();
            GlobalEvent.ApplicationQuit.InvokeEvent(this);
        }
    }
}