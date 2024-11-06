using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public interface IGetMachineAble
    { 
        IStateMachineBase StateMachineBase { get; set; }
        T Machine<T>() where T: IStateMachineBase;
        public void SetMachine(IStateMachineBase machine) => StateMachineBase = machine;
    }

    public interface IStateMachineBase:IEasyLife
    {
        bool IsPause { get; set; }
        /// <summary>
        /// 状态切换前调用，不建议在这里进行状态切换
        /// </summary>
        void StateChange();
    }
    

    public interface ISMachine<TKey, TState>:IStateMachineBase where TState: IStateBase
    {
        public Dictionary<TKey, TState> States { get; }
        TKey CurrentState { get; set; }
        TKey PreviousState{ get; set; }
        Action<IStateMachineBase> MUpdateAction { get; set; }
        Action<IStateMachineBase> MFixedUpdateAction { get; set; }

        bool BeforeAdd(TKey key, TState state)=>OnBeforeAdd(key, state) && state.BeforeAdd(this);
        bool AfterAdd(TKey key, TState state)=>OnAfterAdd(key, state) && state.AfterAdd(this);
        bool BeforeRemove(TKey key, TState state)=>OnBeforeRemove(key, state) && state.BeforeRemove(this);
        bool AfterRemove(TKey key, TState state)=>OnAfterRemove(key, state) && state.AfterRemove(this);

        protected bool BeforeStateChange(TKey key, TState state);
        protected bool OnBeforeAdd(TKey key, TState state);
        protected bool OnAfterAdd(TKey key, TState state);
        protected bool OnBeforeRemove(TKey key, TState state);
        protected bool OnAfterRemove(TKey key, TState state);
        protected static void RemoveCurrentUpdateEvent(ISMachine<TKey, TState> self)
        {
            if (self.States.TryGetValue(self.CurrentState, out var currentState))
            {
                if (currentState is IStateUpdate update)
                    self.MUpdateAction-=update.Update;
                if (currentState is IStateFixedUpdate fixedUpdate)
                    self.MFixedUpdateAction-=fixedUpdate.FixedUpdate;
            }
        }
        protected static void AddCurrentUpdateEvent(ISMachine<TKey, TState> self)
        {
            if (self.States.TryGetValue(self.CurrentState, out var currentState))
            {
                if (currentState is IStateUpdate update)
                    self.MUpdateAction+=update.Update;
                if (currentState is IStateFixedUpdate fixedUpdate)
                    self.MFixedUpdateAction+=fixedUpdate.FixedUpdate;
            }
        }

        public static bool StateChange(ISMachine<TKey, TState> self, TKey key, TState state)
        {
            if (!self.BeforeStateChange(key, state)) return false;

            RemoveCurrentUpdateEvent(self);
            self.PreviousState = self.CurrentState;
            self.CurrentState = key;
            AddCurrentUpdateEvent(self);

            self.StateChange();
            return true;
        }

        public static bool CurrentExit(ISMachine<TKey, TState> self)
        {
            RemoveCurrentUpdateEvent(self);
            self.PreviousState = self.CurrentState;
            self.CurrentState = default;
            self.StateChange();
            return true;
        }

        public static bool StateReplace(ISMachine<TKey, TState> self,TKey key,TState state,out TState oldState)
        {
            if (self.States.ContainsKey(key)
                && self.BeforeAdd(key, state))
            {
                var isEquals = EqualityComparer<TKey>.Default.Equals(key, self.CurrentState);
                if (isEquals)
                    RemoveCurrentUpdateEvent(self);

                oldState = self.States[key];
                self.States[key] = state;

                if (isEquals)
                    AddCurrentUpdateEvent(self);
                
                return true;
            }

            oldState = default;
            return false;
        }
    }

    public static class StateMachineBaseExtension
    {
        public static TState GetState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key)
            where TState : IStateBase
            => self.States[key];
        public static bool AddState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key, TState state)
            where TState : IStateBase
            => self.BeforeAdd(key, state) && self.States.TryAdd(key, state) && self.AfterAdd(key, state);
        public static bool AddState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key, Type state)
            where TState : IStateBase
        {
            var stateInstance=(TState) Activator.CreateInstance(state);
            return self.BeforeAdd(key,stateInstance) && self.States.TryAdd(key,stateInstance) && self.AfterAdd(key,stateInstance);
        }

        public static bool RemoveState<TKey, TState>(this ISMachine<TKey, TState> self, TKey key)
            where TState : IStateBase
        {
            if (EqualityComparer<TKey>.Default.Equals(key, self.CurrentState)
                || !self.States.TryGetValue(key, out var state))
                return false;
            ;
            return self.BeforeRemove(key,state) &&self.States.Remove(key) && self.AfterRemove(key,state);
        }
    }
    public interface IStateMachine<TKey, TState, TValue> : ISMachine<TKey, TState> where TState : IState<TValue>
    {
    }

    public static class StateMachineValueExtension
    {
        public static void ChangeState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TKey key, TValue param,Action<TKey,TValue> onFailed=null)
            where TState : IState<TValue>
        {
            if (!self.IsPause
                && !EqualityComparer<TKey>.Default.Equals(key, self.CurrentState))
            {
                var canExit = true;
                if (self.States.TryGetValue(self.CurrentState, out var currentState))
                    canExit = currentState.ExitCondition(self, param);
                if (canExit
                    && self.States.TryGetValue(key, out var nextState)
                    && nextState.EnterCondition(self, param)
                    && ISMachine<TKey, TState>.StateChange(self, key, nextState))
                {
                    currentState?.Exit(self, param);
                    nextState.Enter(self, param);
                    return;
                }
            }

            onFailed?.Invoke(key,param);
        }
        public static bool ReplaceState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TKey key, TState state, TValue param)
            where TState : IState<TValue>
        {
            if (ISMachine<TKey, TState>.StateReplace(self, key, state, out var oldState)
                && !EqualityComparer<TKey>.Default.Equals(key, self.CurrentState))
            {
                oldState.Exit(self,param);
                self.States[self.CurrentState].Enter(self,param);
                return true;
            }

            return false;
        }
        public static bool ReplaceState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TKey key, TValue param)
            where TState : IState<TValue>, new()
        {
            return ReplaceState(self, key, new TState(), param);
        }
        public static void ExitCurrentState<TKey, TState, TValue>(this IStateMachine<TKey, TState, TValue> self, TValue param,Action<TKey,TValue> onFailed=null)
            where TState : IState<TValue>
        {
            if (!self.IsPause)
            {
                var canExit = false;
                if(self.States.TryGetValue(self.CurrentState, out var currentState))
                    canExit=currentState.ExitCondition(self,param);
                if (canExit
                    && ISMachine<TKey, TState>.CurrentExit(self))
                {
                    currentState.Exit(self,param);
                    return;
                }
                    
            }

            onFailed?.Invoke(self.CurrentState,param);
        }
    }
    
    public interface IStateMachine<TKey, TState> : ISMachine<TKey, TState> where TState : IState
    {
    }

    public static class StateMachineExtension
    {
        public static bool AddState<TKey>(this IStateMachine<TKey, UniversalState> self, TKey key)
        {
            return self.AddState(key, UniversalState.Pool.Fetch());
        }
        public static bool AddState<TKey, TValue>(this IStateMachine<TKey, UniversalState<TValue>, TValue> self, TKey key)
        {
            return self.AddState(key, UniversalState<TValue>.Pool.Fetch());
        }
        public static bool AddState<TKey, TMachine>(this IStateMachine<TKey, UniversalEasyState<TMachine>> self, TKey key)
            where TMachine : IStateMachineBase
        {
            return self.AddState(key, UniversalEasyState<TMachine>.Pool.Fetch());
        }
        public static bool AddState<TKey, TMachine, TValue>(this IStateMachine<TKey, UniversalEasyState<TMachine, TValue>, TValue> self, TKey key)
            where TMachine : IStateMachineBase
        {
            return self.AddState(key, UniversalEasyState<TMachine, TValue>.Pool.Fetch());
        }
        public static bool AddState<TKey>(this IStateMachine<TKey, UniversalProcedure> self, TKey key)
        {
            return self.AddState(key, UniversalProcedure.Pool.Fetch());
        }
        public static bool AddState<TKey, TValue>(this IStateMachine<TKey, UniversalProcedure<TValue>, TValue> self, TKey key)
        {
            return self.AddState(key, UniversalProcedure<TValue>.Pool.Fetch());
        }
        public static bool AddState<TKey, TMachine>(this IStateMachine<TKey, UniversalEasyProcedure<TMachine>> self, TKey key)
            where TMachine : IStateMachineBase
        {
            return self.AddState(key, UniversalEasyProcedure<TMachine>.Pool.Fetch());
        }
        public static bool AddState<TKey, TMachine, TValue>(this IStateMachine<TKey, UniversalEasyProcedure<TMachine, TValue>, TValue> self, TKey key)
            where TMachine : IStateMachineBase
        {
            return self.AddState(key, UniversalEasyProcedure<TMachine, TValue>.Pool.Fetch());
        }


        public static void ChangeState<TKey, TState>(this IStateMachine<TKey, TState> self, TKey key,Action<TKey> onFailed=null)
            where TState : IState
        {
            if (!self.IsPause
                && !EqualityComparer<TKey>.Default.Equals(key, self.CurrentState)
                && self.States.TryGetValue(self.CurrentState, out var currentState)
                && currentState.ExitCondition(self)
                && self.States.TryGetValue(key, out var nextState)
                && nextState.EnterCondition(self)
                && ISMachine<TKey, TState>.StateChange(self, key, nextState))
            {
                self.States[self.PreviousState].Exit(self);
                self.States[self.CurrentState].Enter(self);
                return;
            }
            onFailed?.Invoke(key);
        }
        public static bool ReplaceState<TKey, TState>(this IStateMachine<TKey, TState> self, TKey key, TState state)
            where TState : IState
        {
            if (IStateMachine<TKey, TState>.StateReplace(self, key, state, out var oldState)
                && !EqualityComparer<TKey>.Default.Equals(key, self.CurrentState))
            {
                oldState.Exit(self);
                self.States[self.CurrentState].Enter(self);
                return true;
            }

            return false;
        }
        public static bool ReplaceState<TKey, TState>(this IStateMachine<TKey, TState> self, TKey key)
            where TState : IState, new()
        {
            return ReplaceState(self, key, new TState());
        }
        public static void ExitCurrentState<TKey, TState>(this IStateMachine<TKey, TState> self,Action<TKey> onFailed=null)
            where TState : IState
        {
            if (!self.IsPause
                && self.States.TryGetValue(self.CurrentState, out var currentState)
                && currentState.ExitCondition(self)
                && ISMachine<TKey, TState>.CurrentExit(self))
            {
                currentState.Exit(self);
                return;
            }

            onFailed?.Invoke(self.CurrentState);
        }
        public static void ExitState<TKey, TState>(this IStateMachine<TKey, TState> self,TKey key, Action<TKey> onFailed = null)
            where TState : IState
        {
            if(self.CurrentState.Equals(key))
                self.ExitCurrentState(onFailed);
        }
    }
}