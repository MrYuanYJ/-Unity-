using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFramework
{
    public interface IEasyFSM<out TKey>
    {
        TKey CurrentState { get; }
        TKey PreviousState { get; }
    }

    public enum StateStatus
    {
        None,
        Enter,
        Exit,
    }
    public interface IEasyFSMState
    {
        
    }

    public class EasyFSMState
    {
        public EasyEvent Enter;
        public EasyEvent Exit;
        public Func<bool> EnterCondition;
        public Func<bool> ExitCondition;
        public EasyFSMState OnEnter(Action onEnter)
        {
            Enter ??= new EasyEvent();
            Enter.Register(onEnter);
            return this;
        }
        public EasyFSMState OnExit(Action onExit)
        {
            Exit ??= new EasyEvent();
            Exit.Register(onExit);
            return this;
        }
        public EasyFSMState OnEnterCondition(Func<bool> condition)
        {
            EnterCondition=condition;
            return this;
        }
        public EasyFSMState OnExitCondition(Func<bool> condition)
        {
            ExitCondition=condition;
            return this;
        }
    }
    public class EasyFSMState<T>
    {
        public EasyEvent<T> Enter;
        public EasyEvent<T> Exit;
        public Func<T,bool> EnterCondition;
        public Func<T,bool> ExitCondition;
        public EasyFSMState<T> OnEnter(Action onEnter)
        {
            Enter ??= new EasyEvent<T>();
            Enter.Register(onEnter);
            return this;
        }
        public EasyFSMState<T> OnExit(Action onExit)
        {
            Exit ??= new EasyEvent<T>();
            Exit.Register(onExit);
            return this;
        }
        public EasyFSMState<T> OnEnter(Action<T> onEnter)
        {
            Enter ??= new EasyEvent<T>();
            Enter.Register(onEnter);
            return this;
        }
        public EasyFSMState<T> OnExit(Action<T> onExit)
        {
            Exit ??= new EasyEvent<T>();
            Exit.Register(onExit);
            return this;
        }
        public EasyFSMState<T> OnEnterCondition(Func<T,bool> condition)
        {
            EnterCondition=condition;
            return this;
        }
        public EasyFSMState<T> OnExitCondition(Func<T,bool> condition)
        {
            ExitCondition=condition;
            return this;
        }
    }
    public class EasyFSM<TKey>:IEasyFSM<TKey>
    {
        private Dictionary<TKey, EasyFSMState> _states = new();
        public TKey CurrentState { get; private set; }
        public TKey PreviousState { get; private set; }
        public bool ChangeState(TKey state) 
        {
            if(EqualityComparer<TKey>.Default.Equals(CurrentState, state))
                return false;
            EasyFSMState stateExit = null;
            EasyFSMState stateEnter = null;
            if (CurrentState!= null && _states.TryGetValue(CurrentState, out stateExit))
                if(stateExit.ExitCondition!= null && !stateExit.ExitCondition.Invoke())
                    return false;
            if (state != null && _states.TryGetValue(state, out stateEnter))
                if (stateEnter.EnterCondition!= null && !stateEnter.EnterCondition.Invoke())
                    return false;
            
            PreviousState = CurrentState;
            CurrentState = state;
            stateExit?.Exit?.BaseInvoke();
            stateEnter?.Enter?.BaseInvoke();
            return true;
        }
        public EasyFSMState this[TKey key]
        {
            get
            {
                if (!_states.TryGetValue(key, out var state))
                {
                    state = new EasyFSMState();
                    _states[key] = state;
                }
                return state;
            }
        }
        public EasyFSM<TKey> OnEnter(TKey key, Action onEnter)
        {
            this[key].OnEnter(onEnter);
            return this;
        }
        public EasyFSM<TKey> OnExit(TKey key, Action onExit)
        {
            this[key].OnExit(onExit);
            return this;
        }
        public EasyFSM<TKey> OnEnterCondition(TKey key, Func<bool> condition)
        {
            this[key].OnEnterCondition(condition);
            return this;
        }
        public EasyFSM<TKey> OnExitCondition(TKey key, Func<bool> condition)
        {
            this[key].OnExitCondition(condition);
            return this;
        }
    }

    public class EasyFSM<TKey, T> : IEasyFSM<TKey>
    {
        private Dictionary<TKey, EasyFSMState<T>> _states;
        public TKey CurrentState { get; private set; }
        public TKey PreviousState { get; private set; }

        public bool ChangeState(TKey state, T arg)
        {
            if (EqualityComparer<TKey>.Default.Equals(CurrentState, state))
                return false;
            EasyFSMState<T> stateExit = null;
            EasyFSMState<T> stateEnter = null;
            if (CurrentState!= null && _states.TryGetValue(CurrentState, out stateExit))
                if(stateExit.ExitCondition!= null && !stateExit.ExitCondition.Invoke(arg))
                    return false;
            if (state != null && _states.TryGetValue(state, out stateEnter))
                if (stateEnter.EnterCondition!= null && !stateEnter.EnterCondition.Invoke(arg))
                    return false;
            
            PreviousState = CurrentState;
            CurrentState = state;
            stateExit?.Exit?.BaseInvoke();
            stateEnter?.Enter?.BaseInvoke();
            return true;
        }

        public EasyFSMState<T> this[TKey key]
        {
            get
            {
                if (!_states.TryGetValue(key, out var state))
                {
                    state = new EasyFSMState<T>();
                    _states[key] = state;
                }

                return state;
            }
        }
        public EasyFSM<TKey, T> OnEnter(TKey key, Action onEnter)
        { 
            this[key].OnEnter(onEnter);
            return this;
        }
        public EasyFSM<TKey, T> OnExit(TKey key, Action onExit)
        {
            this[key].OnExit(onExit);
            return this;
        }
        public EasyFSM<TKey, T> OnEnterCondition(TKey key, Func<T, bool> condition)
        {
            this[key].OnEnterCondition(condition);
            return this;
        }
        public EasyFSM<TKey, T> OnExitCondition(TKey key, Func<T, bool> condition)
        {
            this[key].OnExitCondition(condition);
            return this;
        }
        
        public EasyFSM<TKey, T> OnEnter(TKey key, Action<T> onEnter)
        {
            this[key].OnEnter(onEnter);
            return this;
        }
        public EasyFSM<TKey, T> OnExit(TKey key, Action<T> onExit)
        {
            this[key].OnExit(onExit);
            return this;
        }
    }
}