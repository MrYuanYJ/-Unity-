using System.Collections.Generic;

namespace EasyFramework
{
    public abstract class AProcedureMachine<TKey, TState> : ASM<TKey, TState>,
        IProcedureStateMachine<TKey, TState> where TState : IState, IProcedure
    {
        private IProcedureStateMachine<TKey, TState> Self => this;
        LinkedList<TKey> IProcedureSMachine<TKey, TState>.Procedures { get; } = new();
    }

    public abstract class AProcedureMachine<TKey, TState, TValue> : ASM<TKey, TState>,
        IProcedureStateMachine<TKey, TState, TValue> where TState : IState<TValue>, IProcedure
    {
        private IProcedureStateMachine<TKey, TState, TValue> Self => this;
        LinkedList<TKey> IProcedureSMachine<TKey, TState>.Procedures { get; } = new();
    }
}