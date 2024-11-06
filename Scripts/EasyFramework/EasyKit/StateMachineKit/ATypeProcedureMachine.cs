using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public class ATypeProcedureMachineBase<TState>: ASM<Type,TState>,IProcedureStateMachine<Type,TState> where TState :  IProcedure, IState
    {
        LinkedList<Type> IProcedureSMachine<Type, TState>.Procedures { get; } = new();
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T),typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public void ChangeState<T>(Action<Type> onFailed = null) where T : TState => this.ChangeState(typeof(T),onFailed);
    }

    public class ATypeProcedureMachineBase<TState, TValue> : ASM<Type, TState>, IProcedureStateMachine<Type,TState, TValue> where TState : IState<TValue>, IProcedure
    {
        LinkedList<Type> IProcedureSMachine<Type, TState>.Procedures { get; } = new();
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T), typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public void ChangeState<T>(TValue param, Action<Type, TValue> onFailed = null) where T : TState => this.ChangeState(typeof(T), param,onFailed);
    }
}