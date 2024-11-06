using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework
{
    [System.Serializable]
    public abstract class ASM<TKey, TState>: ISMachine<TKey,TState> where TState : IStateBase
    {
        public bool IsPause { get; set; }

        Dictionary<TKey, TState> ISMachine<TKey, TState>.States { get; } = new();

        [SerializeField]private TKey currentState;
        [SerializeField]private TKey previousState;
        public TKey CurrentState{get=> currentState; set=> currentState = value;}
        public TKey PreviousState { get=> previousState; set=> previousState = value; }
        public bool IsInit { get; set; }
        public bool InitDone { get; set; }
        public IEasyEvent InitEvent { get; }=new EasyEvent();
        public IEasyEvent DisposeEvent { get; }=new EasyEvent();
        public IEasyEvent UpdateEvent { get; } = new EasyEvent();
        public IEasyEvent FixedUpdateEvent { get; } = new EasyEvent();
        public Action<IStateMachineBase> MUpdateAction { get; set; }
        public Action<IStateMachineBase> MFixedUpdateAction { get; set; }

        bool ISMachine<TKey, TState>.OnBeforeAdd(TKey key, TState state) => OnBeforeAdd(key, state);
        bool ISMachine<TKey, TState>.OnAfterAdd(TKey key, TState state) => OnAfterAdd(key, state);
        bool ISMachine<TKey, TState>.OnBeforeRemove(TKey key, TState state) => OnBeforeRemove(key, state);
        bool ISMachine<TKey, TState>.OnAfterRemove(TKey key, TState state) => OnAfterRemove(key, state);
        bool ISMachine<TKey, TState>.BeforeStateChange(TKey key, TState state) => OnBeforeStateChange(key, state);
        void IStateMachineBase.StateChange() => OnStateChange();


        protected virtual bool OnBeforeAdd(TKey key, TState state) => true;
        protected virtual bool OnAfterAdd(TKey key, TState state) => true;
        protected virtual bool OnBeforeRemove(TKey key, TState state) => true;
        protected virtual bool OnAfterRemove(TKey key, TState state) => true;
        protected virtual bool OnBeforeStateChange(TKey key, TState state) => true;
        protected virtual void OnStateChange() {}
        
        void IInitAble.OnInit()
        {
            UpdateEvent.Register(() =>
            {
                if(!IsPause)
                    MUpdateAction?.Invoke(this);
            }).UnRegisterOnDispose(this);
            FixedUpdateEvent.Register(() =>
            {
                if (!IsPause)
                    MFixedUpdateAction?.Invoke(this);
            }).UnRegisterOnDispose(this);
        }
        void IDisposeAble.OnDispose(bool usePool) {}
    }
    public abstract class AStateMachine<TKey,TState>: ASM<TKey, TState>,IStateMachine<TKey,TState> where TState : IState 
    {
       
    }
    
    public abstract class AStateMachine<TKey,TState,TValue>: ASM<TKey, TState>,IStateMachine<TKey,TState,TValue> where TState :  IState<TValue>
    {
        
    }
}