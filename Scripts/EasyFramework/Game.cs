using EasyFramework.EasySystem;
using EasyFramework.EasyUIKit;
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
            this.RegisterSystem<EasyEventSystem>();
            this.RegisterSystem<EasyLifeCycleEventSystem>();
            this.RegisterSystem<EasyCodeLoaderSystem>();
            this.RegisterSystem<EasyBuffSystem>();
            GlobalEvent.ApplicationInit.InvokeEvent(this);
            Debug.Log(EventSystem.InvokeAll<TestEventStruct>().GetResult().Value<int>());
        }

        public override void OnDispose()
        {
            GlobalEvent.ApplicationQuit.InvokeEvent(this);
        }
    }
    [EventScope(EventScope.GameObject,EventScope.Global)]
    public class TestEvent: AutoEasyFunc<TestEventStruct>
    {
        protected override IResult Run(TestEventStruct self)
        {
            Debug.Log("TestEvent Run");
            return 999.AsResult();
        }
    }
    
    public class TestUpdateSystem: InitAutoEvent<ECBuffAble>
    {
        protected override void OnInit(ECBuffAble self)
        {
            Debug.Log(self.GetType().Name);
        }
    }
    public class TestStartAutoSystem: StartAutoEvent<ECBuffAble>
    {
        protected override void OnStart(ECBuffAble self)
        {
            Debug.Log(self.GetType().Name+" Start");
        }
    }

    public struct TestEventStruct
    {
        
    }
}