using System;

namespace EasyFramework
{
    public class ATypeStateMachine<TState>: ASM<Type, TState>,IStateMachine<Type,TState> where TState : IState
    {
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T), typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public void ChangeState<T>(Action<Type> onFailed = null) where T : TState => this.ChangeState(typeof(T),onFailed);
    }
    public class ATypeStateMachine<TState,TValue>: ASM<Type, TState>,IStateMachine<Type,TState,TValue> where TState : IState<TValue>
    {
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T), typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public void ChangeState<T>(TValue param, Action<Type,TValue> onFailed = null) where T : TState => this.ChangeState(typeof(T), param, onFailed);
    }
}