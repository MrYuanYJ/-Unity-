using EasyFramework.EventKit;
using EasyFramework.StateMachineKit;
using UnityEngine;

namespace StateMachineKit.Test
{
    public class StateMachineTest: MonoBehaviour
    {
        public fsm Fsm = new();

        private void Start()
        {
            Test();
        }

        private void Test()
        {
            gameObject.Register<UpdateListener>(Fsm.Update);
            Fsm.AddState<GameStart>();
            Fsm.AddState<GameEnd>();
            Fsm.ChangeState<GameStart>("66");
            Fsm.ReStart("sda");
        }
    }

    public class fsm : ATypeProcedureMachine<AGamgeState,string>{}

    public abstract class AGamgeState : AEasyProcedure<fsm,string>{}
    public class GameStart : AGamgeState
    {
        public override void OnEnter(string str,object[] objects)
        {
            Debug.Log("Enter "+str);
            Machine().NextState("77");
        }

        public override void OnExit(string str,object[] objects)
        {
            Debug.Log("Exit "+this.GetType().Name);
        }
    }
    public class Gaming : AGamgeState
    {
        public override void OnEnter(string str,object[] objects)
        {
            Debug.Log("Enter "+str);
            Machine().NextState("77");
        }

        public override void OnExit(string str,object[] objects)
        {
            Debug.Log("Exit "+this.GetType().Name);
        }
    }
    public class GameEnd : AGamgeState,IStateUpdate
    {
        public override void OnEnter(string str,object[] objects)
        {
            Debug.Log("Enter "+str);
        }

        public override void OnExit(string str,object[] objects)
        {
            Debug.Log("Exit "+this.GetType().Name);
        }

        public void OnUpdate()
        {
            Debug.Log(Machine().IsPause);
        }
    }
}