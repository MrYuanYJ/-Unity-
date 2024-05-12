using System;
using System.Collections.Generic;

namespace EasyFramework.StateMachineKit
{
    public class ATypeProcedureMachine<TState>: ASM<Type,TState>,ITypeProcedureStateMachine<TState> where TState :  IProcedure, IEasyState
    {
        LinkedList<Type> IProcedureSMachine<Type, TState>.Procedures { get; } = new();
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public bool ChangeState<T>(params object[] args) where T : TState => this.ChangeState(typeof(T), args);
    }

    public class ATypeProcedureMachine<TState, TValue> : ASM<Type, TState>, ITypeProcedureStateMachine<TState, TValue> where TState : IEasyState<TValue>, IProcedure
    {
        LinkedList<Type> IProcedureSMachine<Type, TState>.Procedures { get; } = new();
        public T GetState<T>() where T : TState => (T)this.GetState(typeof(T));
        public bool AddState<T>() where T : TState => this.AddState(typeof(T));
        public bool RemoveState<T>() where T : TState => this.RemoveState(typeof(T));
        public bool ChangeState<T>(TValue t,params object[] args) where T : TState => this.ChangeState(typeof(T), t, args);
    }
}