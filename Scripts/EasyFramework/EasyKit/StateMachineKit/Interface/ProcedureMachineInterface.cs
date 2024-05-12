using System;
using System.Collections.Generic;
using UnityEngine;

namespace EasyFramework.StateMachineKit
{
    public interface IProcedureSMachine<TKey,TState>:ISMachine<TKey, TState> where TState : IProcedure
    {
        public LinkedList<TKey> Procedures { get; }

        bool ISMachine<TKey, TState>.AfterAdd(TKey key)
        {
            Procedures.AddLast(key);
            return true;
        }
        bool ISMachine<TKey, TState>.AfterRemove(TKey key)
        {
            Procedures.Remove(key);
            return true;
        }

        public static LinkedListNode<TKey> FindNext(LinkedListNode<TKey> self, Func<LinkedListNode<TKey>,bool> judge)
        {
            if (self != null && self.Next != null)
                if (judge(self.Next))
                    return self.Next;
                else
                    return FindNext(self.Next,judge);
            return null;
        }
        public static LinkedListNode<TKey> FindPrevious(LinkedListNode<TKey> self, Func<LinkedListNode<TKey>,bool> judge)
        {
            if (self != null && self.Previous != null)
                if (judge(self.Previous))
                    return self.Previous;
                else
                    return FindPrevious(self.Previous,judge);
            return null;
        }
    }

    public static class ProcedureSMachineExtension
    {
        public static bool AddStateBefore<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey newKey, TState addState)
            where TState : IProcedure, new()
        {
            var node = self.Procedures.Find(key);
            if (node != null)
                if (self.BeforeAdd(newKey) && self.States.TryAdd(newKey, addState))
                {
                    self.Procedures.AddBefore(node, newKey);
                    return true;
                }

            return false;
        }
        public static bool AddStateAfter<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey newKey, TState addState)
            where TState : IProcedure, new()
        {
            var node = self.Procedures.Find(key);
            if (node != null)
                if (self.BeforeAdd(newKey) && self.States.TryAdd(newKey, addState))
                {
                    self.Procedures.AddAfter(node, newKey);
                    return true;
                }

            return false;
        }
        public static bool MoveStateBefore<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey moveKey)
            where TState : IProcedure
        {
            var node = self.Procedures.Find(key);
            var moveNode = self.Procedures.Find(moveKey);
            if (node != null && moveNode != null)
            {
                self.Procedures.AddBefore(node, moveKey);
                self.Procedures.Remove(moveNode);
                return true;
            }

            return false;
        }
        public static bool MoveStateAfter<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey moveKey)
            where TState : IProcedure
        {
            var node = self.Procedures.Find(key);
            var moveNode = self.Procedures.Find(moveKey);
            if (node != null && moveNode != null)
            {
                self.Procedures.AddAfter(node, moveKey);
                self.Procedures.Remove(moveNode);
                return true;
            }

            return false;
        }
    }

    public interface ITypeProcedureSMachine<TState> : IProcedureSMachine<Type, TState> where TState : IProcedure
    {
    }

    public static class TypeProcedureSMachineExtension
    {
        public static bool AddStateAfter<TState>(this ITypeProcedureSMachine<TState> self, Type key, TState addState)
            where TState : IProcedure, new()
            => self.AddStateAfter(key, addState.GetType(), addState);
        public static bool AddStateBefore<TState>(this ITypeProcedureSMachine<TState> self, Type key, TState addState)
            where TState : IProcedure, new()
            => self.AddStateBefore(key, addState.GetType(), addState);
    }



    public interface IProcedureStateMachine<TKey, TState, TValue> : IStateMachine<TKey, TState, TValue>, IProcedureSMachine<TKey, TState> where TState : IEasyState<TValue>,IProcedure
    {
    }

    public static class ProcedureStateMachineValueExtension
    {
        public static bool NextState<TKey, TState, TValue>(this IProcedureStateMachine<TKey, TState, TValue> self,
            TValue t, params object[] data)
            where TState : IEasyState<TValue>, IProcedure
        {
            var next = IProcedureSMachine<TKey, TState>.FindNext(self.Procedures.Find(self.CurrentState),
                next => self.States[next.Value].IsEnable);
            if (next != null)
                return self.ChangeState(next.Value, t, data);
            else
                return self.ExitCurrentState(t, data);
        }

        public static bool PreviousState<TKey, TState, TValue>(this IProcedureStateMachine<TKey, TState, TValue> self,
            TValue t, params object[] data)
            where TState : IEasyState<TValue>, IProcedure
        {
            var previous = IProcedureSMachine<TKey, TState>.FindPrevious(self.Procedures.Find(self.CurrentState),
                previous => self.States[previous.Value].IsEnable);
            if (previous != null)
                return self.ChangeState(previous.Value, t, data);
            else
                return self.ExitCurrentState(t, data);
        }

        public static bool ReStart<TKey, TState, TValue>(this IProcedureStateMachine<TKey, TState, TValue> self,
            TValue t, params object[] data)
            where TState : IEasyState<TValue>, IProcedure
        {
            var node = self.Procedures.First;
            if (node != null)
                return self.ChangeState(node.Value, t, data);
            else
                return self.ExitCurrentState(t, data);
        }
    }

    public interface IProcedureStateMachine<TKey, TState> : IStateMachine<TKey, TState>, IProcedureSMachine<TKey, TState> where TState : IEasyState, IProcedure
    {
    }

    public static class ProcedureStateMachineExtension
    {
        public static bool NextState<TKey, TState>(this IProcedureStateMachine<TKey, TState> self, params object[] data)
            where TState : IEasyState, IProcedure
        {
            var node = self.Procedures.Find(self.CurrentState);
            if (node != null && node.Next != null)
                return self.ChangeState(node.Next.Value, data);
            else
                return self.ExitCurrentState(data);
        }

        public static bool PreviousState<TKey, TState>(this IProcedureStateMachine<TKey, TState> self, params object[] data)
            where TState : IEasyState, IProcedure
        {
            var node = self.Procedures.Find(self.CurrentState);
            if (node != null && node.Previous != null)
                return self.ChangeState(node.Previous.Value, data);
            else
                return self.ExitCurrentState(data);
        }

        public static bool ReStart<TKey, TState>(this IProcedureStateMachine<TKey, TState> self, params object[] data)
            where TState : IEasyState, IProcedure
        {
            var node = self.Procedures.First;
            if (node != null)
                return self.ChangeState(node.Value, data);
            else
                return self.ExitCurrentState(data);
        }
    }
    public interface ITypeProcedureStateMachine<TState,TValue>: ITypeStateMachine<TState,TValue>,ITypeProcedureSMachine<TState>,IProcedureStateMachine<Type, TState,TValue> where TState : IEasyState<TValue>, IProcedure {}
    public interface ITypeProcedureStateMachine<TState>: ITypeStateMachine<TState>,ITypeProcedureSMachine<TState>,IProcedureStateMachine<Type, TState> where TState : IEasyState, IProcedure {}
}