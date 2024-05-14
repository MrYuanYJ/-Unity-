using System.Collections.Generic;
using EasyFramework.EasyTaskKit;
using EasyFramework.EventKit;
using EXFunctionKit;
using StateMachineKit.Test;
using UnityEngine;

namespace EasyFramework
{
    public class Test: AMonoEntity<ECBuffAble>,IUpdateAble
    {
        public ESProperty<List<int>> test = new();
        public override void OnInit()
        {
            base.OnInit();
            Debug.Log("Test Init");
        }

        public override void OnStart()
        {
            Debug.Log("Test Start");
            var handle = EasyTask.Seconds(0.01f, 5, _ =>
            {
                BuffEvent.AddBuffByMeans.InvokeEvent(EMeans.Attack, new NormalBuffAddData(MEntity, EBuff.Water, 10));
                BuffEvent.AddBuffByMeans.InvokeEvent(EMeans.Attack, new NormalBuffAddData(MEntity, EBuff.Ice, 10, 2));
                BuffEvent.AddBuffByMeans.InvokeEvent(EMeans.Skill, new NormalBuffAddData(MEntity, EBuff.Fire, 5));
            });
            EasyTask.Delay(1).OnCompleted(_ => MEntity.GetStructure().Dispose());
        }

        public IEasyEvent UpdateEvent { get; } = new EasyEvent();
        public void OnUpdate()
        {
            
        }
    }
    
    public class GameStart: StartAutoEvent<GameInitComponent>
    {
        protected override void OnStart(GameInitComponent self)
        {
            self.Bind.BindObj.GameObject().AddComponent<Test>();
        }
    }
}