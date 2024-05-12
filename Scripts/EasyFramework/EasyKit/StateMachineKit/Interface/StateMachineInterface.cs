using System;
using System.Collections.Generic;
using System.Linq;
using EXFunctionKit;
using Unity.VisualScripting;
using UnityEngine;

namespace EasyFramework.StateMachineKit
{
    public interface IGetMachineAble
    { 
        IStateMachine StateMachine { get; protected set; }
        IStateMachine Machine();
        T Machine<T>() where T: IStateMachine;
        public void SetMachine(IStateMachine machine) => StateMachine = machine;
    }

    public interface IStateMachine
    {
        bool IsPause { get; set; }
    }

    public interface ISMachine<TKey, TState>:IStateMachine where TState: IState
    {
        public Dictionary<TKey, TState> States { get; }
        TKey CurrentState { get; set; }
        TKey PreviousState{ get; set; }
        protected Func<TKey,TState,bool> BeforeStateChange { get; }
        protected Action AfterStateChange { get; }
        event Action<IStateMachine> OnUpdate;
        event Action<IStateMachine> OnFixedUpdate;

        bool BeforeAdd(TKey key) => true;
        bool AfterAdd(TKey key) => true;
        bool AfterRemove(TKey key) => true;
        protected static void RemoveCurrentUpdateEvent(ISMachine<TKey, TState> self)
        {
            if (!EqualityComparer<TKey>.Default.Equals(default, self.CurrentState))
            {
                if (self.States[self.CurrentState] is IStateUpdate update)
                    self.OnUpdate -= update.Update;
                if (self.States[self.CurrentState] is IStateFixedUpdate fixedUpdate)
                    self.OnFixedUpdate -= fixedUpdate.FixedUpdate;
            }
        }
        protected static void AddCurrentUpdateEvent(ISMachine<TKey, TState> self)
        {
            if (!EqualityComparer<TKey>.Default.Equals(default, self.CurrentState))
            {
                if (self.States[self.CurrentState] is IStateUpdate update)
                    self.OnUpdate += update.Update;
                if (self.States[self.CurrentState] is IStateFixedUpdate fixedUpdate)
                    self.OnFixedUpdate += fixedUpdate.FixedUpdate;
            }
        }
        public static bool StateChange(ISMachine<TKey, TState> self, TKey key)
        {
            if (!self.IsPause
                && !EqualityComparer<TKey>.Default.Equals(key, self.CurrentState)
                && self.States.TryGetValue(key, out var state))
            {
                if (!state.EnterCondition(self)) return false;
                if (self.BeforeStateChange != null && !self.BeforeStateChange(key, state)) return false;

                RemoveCurrentUpdateEvent(self);
                self.PreviousState = self.CurrentState;
                self.CurrentState = key;
                AddCurrentUpdateEvent(self);

                self.AfterStateChange?.Invoke();
                return true;
            }

            return false;
        }
        public static bool CurrentExit(ISMachine<TKey, TState> self)
        {
            if (!self.IsPause && !EqualityComparer<TKey>.Default.Equals(default,self.CurrentState))
            {
                if (!self.States[self.CurrentState].ExitCondition(self)) return false;
                RemoveCurrentUpdateEvent(self);
                self.PreviousState = self.CurrentState;
                self.CurrentState = default;
                return true;
            }

            return false;
        }
        public static TState StateReplace(ISMachine<TKey, TState> self,TKey key,TState state)
        {
            if (self.States.ContainsKey(key)&& self.BeforeAdd(key))
            {
                var isEquals = EqualityComparer<TKey>.Default.Equals(key, self.CurrentState);
                if (isEquals)
                    RemoveCurrentUpdateEvent(self);

                var oldState = self.States[key];
                self.States[key] = state;

                if (isEquals)
                    AddCurrentUpdateEvent(self);
                
                return oldState;
            }

            return default;
        }
    }

    public static class StateMachineBaseExtension
    {
        public static TState GetState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key)
            where TState : IState
            => self.States[key];
        public static bool AddState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key, TState state)
            where TState : IState
            => self.BeforeAdd(key) && self.States.TryAdd(key, state) && self.AfterAdd(key);
        public static bool AddState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key, Type state)
            where TState : IState
            => self.BeforeAdd(key) && self.States.TryAdd(key, (TState)Activator.CreateInstance(state)) && self.AfterAdd(key);
        public static bool RemoveState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key)
            where TState : IState
        {
            if (EqualityComparer<TKey>.Default.Equals(key, self.CurrentState))
                return false;
            return self.States.Remove(key) && self.AfterRemove(key);
        }
    }

    public interface ITypeSMachine<TState> : ISMachine<Type, TState> where TState: IState
    {
    }

    public static class TypeStateMachineBaseExtension
    {
        public static bool AddState<TState>(this ITypeSMachine<TState> self, TState state)
            where TState : IState
            => self.AddState(state.GetType(), state);
        public static bool AddState<TState>(this ITypeSMachine<TState> self, Type key)
            where TState : IState
            => self.AddState(key, key);
    }


    public interface IStateMachine<TKey, TState, TValue> : ISMachine<TKey, TState> where TState : IEasyState<TValue>
    {
    }

    public static class StateMachineValueExtension
    {
        public static bool ChangeState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TKey key, TValue t, params object[] data)
            where TState : IEasyState<TValue>
        {
            if (IStateMachine<TKey, TState, TValue>.StateChange(self,key))
            {
                if (!EqualityComparer<TKey>.Default.Equals(self.PreviousState, default))
                    self.States[self.PreviousState].Exit(self, t, data);
                self.States[self.CurrentState].Enter(self,t,data);
                return true;
            }

            return false;
        }
        public static bool ReplaceState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TKey key, TState state, TValue t, params object[] data)
            where TState : IEasyState<TValue>
        {
            var oldState = IStateMachine<TKey, TState, TValue>.StateReplace(self, key, state);
            if (!EqualityComparer<TState>.Default.Equals(oldState,default))
            {
                oldState.Exit(self,t,data);
                self.States[self.CurrentState].Enter(self,t,data);
                return true;
            }

            return false;
        }
        public static bool ReplaceState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TKey key, TValue t, params object[] data)
            where TState : IEasyState<TValue>, new()
        {
            return ReplaceState(self, key, new TState(), t, data);
        }
        public static bool ExitCurrentState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TValue t, params object[] data)
            where TState : IEasyState<TValue>
        {
            if (IStateMachine<TKey, TState, TValue>.CurrentExit(self))
            {
                self.States[self.PreviousState].Exit(self, t, data);
                return true;
            }

            return false;
        }
    }
    
    public interface IStateMachine<TKey, TState> : ISMachine<TKey, TState> where TState : IEasyState
    {
    }

    public static class StateMachineExtension
    {
        public static bool ChangeState<TKey, TState>(this IStateMachine<TKey, TState> self, TKey key, params object[] data)
            where TState : IEasyState
        {
            if (IStateMachine<TKey, TState>.StateChange(self,key))
            {
                if (!EqualityComparer<TKey>.Default.Equals(self.PreviousState, default))
                    self.States[self.PreviousState].Exit(self, data);
                self.States[self.CurrentState].Enter(self,data);
                return true;
            }

            return false;
        }
        public static bool ReplaceState<TKey, TState>(this IStateMachine<TKey, TState> self, TKey key, TState state, params object[] data)
            where TState : IEasyState
        {
            var oldState = IStateMachine<TKey, TState>.StateReplace(self, key, state);
            if (!EqualityComparer<TState>.Default.Equals(oldState,default))
            {
                oldState.Exit(self,data);
                self.States[self.CurrentState].Enter(self,data);
                return true;
            }

            return false;
        }
        public static bool ReplaceState<TKey, TState>(this IStateMachine<TKey, TState> self, TKey key, params object[] data)
            where TState : IEasyState, new()
        {
            return ReplaceState(self, key, new TState(), data);
        }

        public static bool ExitCurrentState<TKey, TState>(this IStateMachine<TKey, TState> self, params object[] data)
            where TState : IEasyState
        {
            if (IStateMachine<TKey, TState>.CurrentExit(self))
            {
                self.States[self.PreviousState].Exit(self, data);
                return true;
            }

            return false;
        }
    }

    public interface ITypeStateMachine<TState, TValue> : IStateMachine<Type, TState, TValue>,ITypeSMachine<TState> where TState : IEasyState<TValue>
    {
    }
    public static class TypeStateMachineValueExtension
    {
    }
    public interface ITypeStateMachine<TState> : IStateMachine<Type, TState>,ITypeSMachine<TState> where TState : IEasyState
    {
    }
    public static class TypeStateMachineExtension
    {
    }
}