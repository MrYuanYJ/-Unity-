using System;
using System.Collections.Generic;
using UnityEngine.SearchService;

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

        public event Action<IStateMachine> OnUpdate;
        public event Action<IStateMachine> OnFixedUpdate;

        public void Update()
        {
            if(!IsPause)
                OnUpdate?.Invoke(this);
        }
        public void FixedUpdate()
        {
            if(!IsPause)
                OnFixedUpdate?.Invoke(this);
        }
    }
    public abstract class AStateMachine<TKey,TState>: ASM<TKey, TState>,IStateMachine<TKey,TState> where TState : IEasyState 
    {
       
    }
    
    public abstract class AStateMachine<TKey,TState,TValue>: ASM<TKey, TState>,IStateMachine<TKey,TState,TValue> where TState :  IEasyState<TValue>
    {
        
    }
}