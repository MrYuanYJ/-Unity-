using System;
using System.Collections.Generic;

namespace EasyFramework.StateMachineKit
{
    public class ATypeStateMachine<TState>: ASM<Type, TState>,ITypeStateMachine<TState> where TState : IEasyState
    {
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public bool ChangeState<T>(params object[] args) where T : TState => this.ChangeState(typeof(T), args);
    }
    public class ATypeStateMachine<TState,TValue>: ASM<Type, TState>,ITypeStateMachine<TState,TValue> where TState : IEasyState<TValue>
    {
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public bool ChangeState<T>(TValue t,params object[] args) where T : TState => this.ChangeState(typeof(T), t, args);
    }
}