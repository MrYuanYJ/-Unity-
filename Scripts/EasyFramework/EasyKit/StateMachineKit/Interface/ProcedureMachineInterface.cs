using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public interface IProcedureSMachine<TKey,TState>:ISMachine<TKey, TState> where TState : IProcedure
    {
        public LinkedList<TKey> Procedures { get; }

        bool ISMachine<TKey, TState>.OnAfterAdd(TKey key, TState state)
        {
            if (!OnAfterAdd(key, state) || !state.AfterAdd(this))
                return false;
            Procedures.AddLast(key);
            return true;
        }
        bool ISMachine<TKey, TState>.AfterRemove(TKey key, TState state)
        {
            if (!OnAfterRemove(key, state) || !state.AfterRemove(this))
                return false;
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
        public static bool AddProcedure<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TState addState)
            where TState : IProcedure
        {
            if (self.AddState(key, addState))
            {
                self.Procedures.AddLast(key);
                return true;
            }

            return false;
        }
        public static bool RemoveProcedure<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key)
            where TState : IProcedure
        {
            if (self.RemoveState(key))
            {
                self.Procedures.Remove(key);
                return true;
            }

            return false;
        }
        public static bool AddProcedureBefore<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey newKey, TState addState)
            where TState : IProcedure, new()
        {
            var node = self.Procedures.Find(key);
            if (node != null)
                if (self.AddState(newKey, addState))
                {
                    self.Procedures.AddBefore(node, newKey);
                    return true;
                }

            return false;
        }
        public static bool AddProcedureAfter<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey newKey, TState addState)
            where TState : IProcedure, new()
        {
            var node = self.Procedures.Find(key);
            if (node != null)
                if (self.AddState(newKey, addState))
                {
                    self.Procedures.AddAfter(node, newKey);
                    return true;
                }

            return false;
        }
        public static bool MoveProcedureBefore<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey moveKey)
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
        public static bool MoveProcedureAfter<TKey, TState>(this IProcedureSMachine<TKey, TState> self, TKey key, TKey moveKey)
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
        public static bool AddProcedureAfter<TState>(this ITypeProcedureSMachine<TState> self, Type key, TState addState)
            where TState : IProcedure, new()
            => self.AddProcedureAfter(key, addState.GetType(), addState);
        public static bool AddProcedureBefore<TState>(this ITypeProcedureSMachine<TState> self, Type key, TState addState)
            where TState : IProcedure, new()
            => self.AddProcedureBefore(key, addState.GetType(), addState);
    }



    public interface IProcedureStateMachine<TKey, TState, TValue> : IStateMachine<TKey, TState, TValue>, IProcedureSMachine<TKey, TState> where TState : IState<TValue>,IProcedure
    {
    }

    public static class ProcedureStateMachineValueExtension
    {
        public static void NextProcedure<TKey, TState, TValue>(this IProcedureStateMachine<TKey, TState, TValue> self, TValue param,Action<TKey,TValue> onFailed = null)
            where TState : IState<TValue>, IProcedure
        {
            var next = IProcedureSMachine<TKey, TState>.FindNext(self.Procedures.Find(self.CurrentState),
                next => self.States[next.Value].IsEnable);
            if (next != null)
                self.ChangeState(next.Value,param,onFailed);
            else
                self.ExitCurrentState(param,onFailed);
        }

        public static void PreviousProcedure<TKey, TState, TValue>(this IProcedureStateMachine<TKey, TState, TValue> self, TValue param,Action<TKey,TValue> onFailed = null)
            where TState : IState<TValue>, IProcedure
        {
            var previous = IProcedureSMachine<TKey, TState>.FindPrevious(self.Procedures.Find(self.CurrentState),
                previous => self.States[previous.Value].IsEnable);
            if (previous != null)
                self.ChangeState(previous.Value,param,onFailed);
            else
                self.ExitCurrentState(param,onFailed);
        }

        public static void ReStart<TKey, TState, TValue>(this IProcedureStateMachine<TKey, TState, TValue> self, TValue param,Action<TKey,TValue> onFailed = null)
            where TState : IState<TValue>, IProcedure
        {
            var node = self.Procedures.First;
            if (node != null)
                self.ChangeState(node.Value,param,onFailed);
            else
                self.ExitCurrentState(param,onFailed);
        }
    }

    public interface IProcedureStateMachine<TKey, TState> : IStateMachine<TKey, TState>, IProcedureSMachine<TKey, TState> where TState : IState, IProcedure
    {
    }

    public static class ProcedureStateMachineExtension
    {
        public static bool AddProcedure<TKey>(this IProcedureSMachine<TKey, UniversalProcedure> self, TKey key)
        {
            return self.AddProcedure(key, UniversalProcedure.Pool.Fetch());
        }
        public static bool AddProcedure<TKey, TValue>(this IProcedureSMachine<TKey, UniversalProcedure<TValue>> self, TKey key)
        {
            return self.AddProcedure(key, UniversalProcedure<TValue>.Pool.Fetch());
        }
        public static bool AddProcedure<TKey,TMachine>(this IProcedureSMachine<TKey,UniversalEasyProcedure<TMachine>> self, TKey key)
            where TMachine : IProcedureSMachine<TKey,UniversalEasyProcedure<TMachine>>
        {
            return self.AddProcedure(key, UniversalEasyProcedure<TMachine>.Pool.Fetch());
        }
        public static bool AddProcedure<TKey,TMachine,TValue>(this IProcedureSMachine<TKey, UniversalEasyProcedure<TMachine,TValue>> self, TKey key)
            where TMachine : IProcedureSMachine<TKey, UniversalEasyProcedure<TMachine,TValue>>
        {
            return self.AddProcedure(key, UniversalEasyProcedure<TMachine,TValue>.Pool.Fetch());
        }
        public static void NextProcedure<TKey, TState>(this IProcedureStateMachine<TKey, TState> self,Action<TKey> onFailed = null)
            where TState : IState, IProcedure
        {
            var node = self.Procedures.Find(self.CurrentState);
            if (node != null && node.Next != null)
                self.ChangeState(node.Next.Value,onFailed);
            else
                self.ExitCurrentState(onFailed);
        }

        public static void PreviousProcedure<TKey, TState>(this IProcedureStateMachine<TKey, TState> self,Action<TKey> onFailed = null)
            where TState : IState, IProcedure
        {
            var node = self.Procedures.Find(self.CurrentState);
            if (node != null && node.Previous != null)
                self.ChangeState(node.Previous.Value,onFailed);
            else
                self.ExitCurrentState(onFailed);
        }

        public static void ReStart<TKey, TState>(this IProcedureStateMachine<TKey, TState> self,Action<TKey> onFailed = null)
            where TState : IState, IProcedure
        {
            var node = self.Procedures.First;
            if (node != null)
                self.ChangeState(node.Value,onFailed);
            else
                self.ExitCurrentState(onFailed);
        }
    }}