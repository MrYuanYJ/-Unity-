using System;
using System.Collections.Generic;
using EasyFramework.EventKit;
using UnityEngine;

namespace EasyFramework.StateMachineKit
{
    [System.Serializable]
    public abstract class ASM<TKey, TState>: ISMachine<TKey,TState> where TState : IState
    {
        public bool IsPause { get; set; }

        Dictionary<TKey, TState> ISMachine<TKey, TState>.States { get; } = new();

        [SerializeField]private TKey currentState;
        [SerializeField]private TKey previousState;
        public TKey CurrentState{get=> currentState; set=> currentState = value;}
        public TKey PreviousState { get=> previousState; set=> previousState = value; }

        Func<TKey, TState, bool> ISMachine<TKey, TState>.BeforeStateChange => (key, state) => !IsPause;

        Action ISMachine<TKey, TState>.AfterStateChange => null;   
        public bool IsInit { get; set; }
        public IEasyEvent InitEvent { get; }=new EasyEvent();
        public IEasyEvent DisposeEvent { get; }=new EasyEvent();

        public IEasyEvent UpdateEvent { get; } = new EasyEvent();
        public IEasyEvent FixedUpdateEvent { get; } = new EasyEvent();
        public Action<IStateMachine> MUpdateAction { get; set; }
        public Action<IStateMachine> MFixedUpdateAction { get; set; }
        

        void IInitAble.OnInit()
        {
            UpdateEvent.Register(() =>
            {
                if(!IsPause)
                    MUpdateAction?.Invoke(this);
                Debug.Log(this.GetType().Name + " Update");
            }).UnRegisterOnDispose(this);
            FixedUpdateEvent.Register(() =>
            {
                if (!IsPause)
                    MFixedUpdateAction?.Invoke(this);
            }).UnRegisterOnDispose(this);
        }
        void IDisposeAble.OnDispose(bool usePool) {}
    }
    public abstract class AStateMachine<TKey,TState>: ASM<TKey, TState>,IStateMachine<TKey,TState> where TState : IEasyState 
    {
       
    }
    
    public abstract class AStateMachine<TKey,TState,TValue>: ASM<TKey, TState>,IStateMachine<TKey,TState,TValue> where TState :  IEasyState<TValue>
    {
        
    }
}