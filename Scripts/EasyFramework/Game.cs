using EasyFramework.EasyResKit;
using EasyFramework.EasySystem;
using EasyFramework.EasySystem.EasyAttributeSystem;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework
{
    public class Game: AStructure<Game>
    {
        protected override void OnInit()
        {
            Debug.Log("Game Init");

            #if DEBUG
            this.System<EasyLogSystem>();
            #endif
            this.Member(EasyMono.TryRegister());
            this.Member(AssetPool.TryRegister());
            this.Member(GameObjectPool.TryRegister());
            this.System<EasyIDSystem>();
            this.System<EasyResSystem>();
            this.System<EasyEventSystem>();
            this.System<EasyLifeCycleEventSystem>();
            this.System<EasyMono2EntityRelationalMappingSystem>();
            this.System<EasyCodeLoaderSystem>();
            this.System<EasyInputSystem>();
            
            GlobalEvent.MainStructure.RegisterFunc(GetInstance);
            GlobalEvent.ApplicationInit.InvokeEvent(this);
        }

        protected override void OnDispose(bool usePool)
        {
            GlobalEvent.ApplicationQuit.InvokeEvent(this);
            EasyEventDic.Global.ClearAll();
            EasyFuncDic.Global.ClearAll();
        }
    }
}