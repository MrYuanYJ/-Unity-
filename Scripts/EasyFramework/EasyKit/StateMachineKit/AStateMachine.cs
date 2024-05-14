using System;
using System.Collections.Generic;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework.StateMachineKit
{
    public abstract class ASM<TKey, TState>: ISMachine<TKey,TState> where TState : IState
    {
        public bool IsPause { get; set; }

        Dictionary<TKey, TState> ISMachine<TKey, TState>.States { get; } = new();

        public TKey CurrentState { get; set; }
        public TKey PreviousState { get; set; }

        Func<TKey, TState, bool> ISMachine<TKey, TState>.BeforeStateChange => (key, state) => !IsPause;

        Action ISMachine<TKey, TState>.AfterStateChange => null;   
        public bool IsInit { get; set; }
        public IEasyEvent InitEvent { get; }=new EasyEvent();
        public IEasyEvent DisposeEvent { get; }=new EasyEvent();
        public IEasyEvent UpdateEvent { get; } = new EasyEvent();
        public IEasyEvent FixedUpdateEvent { get; } = new EasyEvent();
        public EasyEvent<IStateMachine> MUpdateEvent { get; set; } = new();
        public EasyEvent<IStateMachine> MFixedUpdateEvent { get; set; } = new();

        public void OnUpdate() { }
        public void OnFixedUpdate() { }


        public void OnInit()
        {
            UpdateEvent.Register(() =>
            {
                if(!IsPause)
                    MUpdateEvent?.Invoke(this);
                Debug.Log(this.GetType().Name + " Update");
            }).UnRegisterOnDispose(this);
            FixedUpdateEvent.Register(() =>
            {
                if (!IsPause)
                    MFixedUpdateEvent?.Invoke(this);
            }).UnRegisterOnDispose(this);
        }
        public void OnDispose(){}
    }
    public abstract class AStateMachine<TKey,TState>: ASM<TKey, TState>,IStateMachine<TKey,TState> where TState : IEasyState 
    {
       
    }
    
    public abstract class AStateMachine<TKey,TState,TValue>: ASM<TKey, TState>,IStateMachine<TKey,TState,TValue> where TState :  IEasyState<TValue>
    {
        
    }
}