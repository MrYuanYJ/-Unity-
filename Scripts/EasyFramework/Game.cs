using EasyFramework.EasyResKit;
using EasyFramework.EasySystem;
using EasyFramework.EasySystem.EasyAttributeSystem;
using UnityEngine;

namespace EasyFramework
{
    public class Game: AStructure<Game>
    {
        [RuntimeInitializeOnLoadMethod]
        public static void GameStart()
        {
            TryRegister();
        }
        protected override void OnInit()
        {
            Debug.Log("Game Init");

            #if DEBUG
            this.System<EasyLogSystem>();
            #endif
            this.Member(EasyMono.TryRegister());
            this.Member(AssetPool.TryRegister());
            this.Member(GameObjectPool.TryRegister());
            this.Member(ComponentPool.TryRegister());
            this.System<EasyIDSystem>();
            this.System<EasyResSystem>();
            this.System<EasyEventSystem>();
            this.System<EasyLifeCycleEventSystem>();
            this.System<EasyMono2EntityRelationalMappingSystem>();
            this.System<EasyCodeLoaderSystem>();
            this.System<EasyInputSystem>();
            this.System<EasyBufferSystem>();
            this.System<EasyTimeSystem>();
            
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

    public struct GameEvent
    {
        public sealed class InputDown: AEventIndex<InputDown,EInput>{}
        public sealed class InputUp: AEventIndex<InputUp, EInput>{}
        public sealed class InputHold: AEventIndex<InputHold, EInput, float>{}
    }
}