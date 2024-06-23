using System;

namespace EasyFramework.StateMachineKit
{
    public interface IState:IGetMachineAble
    {
        bool EnterCondition(IStateMachine machine)
        {
            SetMachine(machine);
            if (EnterConditionFunc != null)
                return OnEnterCondition() && EnterConditionFunc();
            return OnEnterCondition();
        }

        bool ExitCondition(IStateMachine machine)
        {
            SetMachine(machine);
            if (ExitConditionFunc != null)
                return OnExitCondition() && ExitConditionFunc();
            return OnExitCondition();
        }

        Func<bool> EnterConditionFunc{ get; set; }
        Func<bool> ExitConditionFunc{ get; set; }
        
        protected bool OnEnterCondition();
        protected bool OnExitCondition();
    }

    public interface IStateUpdate: IState
    {
        void Update(IStateMachine machine)
        {
            SetMachine(machine);
            OnUpdate();
            UpdateAction?.Invoke();
        }
        Action UpdateAction{ get; set; }
        protected void OnUpdate();
    }
    public interface IStateFixedUpdate: IState
    {
        void FixedUpdate(IStateMachine machine)
        {
            SetMachine(machine);
            OnFixedUpdate();
            FixedUpdateAction?.Invoke();
        }
        Action FixedUpdateAction{ get; set; }
        protected void OnFixedUpdate();
    }
    
    public interface IEasyState : IState
    {
        void Enter(IStateMachine machine, params object[] obj)
        {
            SetMachine(machine);
            OnEnter(obj);
            EnterAction?.Invoke(obj);
        }
        void Exit(IStateMachine machine, params object[] obj)
        {
            SetMachine(machine);
            OnExit(obj);
            ExitAction?.Invoke(obj);
        }
        Action<object[]> EnterAction{ get; set; }
        Action<object[]> ExitAction{ get; set; }
        protected void OnEnter(object[] objects);
        protected void OnExit(object[] objects);
    }
    public interface IEasyState<T>:IState
    {
        void Enter(IStateMachine machine,T t, params object[] obj)
        {  
            SetMachine(machine);
            OnEnter(t, obj);
            EnterAction?.Invoke(t, obj);
        }
        void Exit(IStateMachine machine, T t, params object[] obj)
        {
            SetMachine(machine);
            OnExit(t, obj);
            ExitAction?.Invoke(t, obj);
        }
        Action<T,object[]> EnterAction{ get; set; }
        Action<T,object[]> ExitAction{ get; set; }
        protected void OnEnter(T t,object[] objects);
        protected void OnExit(T t,object[] objects);
    }

    public interface IProcedure:IState
    { 
        bool IsEnable { get;}
        void SetEnable(bool isEnable);
    }

    public static class IStateExtension
    {
        public static T OnEnterCondition<T>(this T state, Func<bool> condition) where T : IState
        {
            state.EnterConditionFunc+=condition;
            return state;
        }
        public static T RemoveEnterCondition<T>(this T state, Func<bool> condition) where T : IState
        {
            state.EnterConditionFunc-=condition;
            return state;
        }
        public static T OnExitCondition<T>(this T state, Func<bool> condition) where T : IState
        {
            state.ExitConditionFunc+=condition;
            return state;
        }
        public static T RemoveExitCondition<T>(this T state, Func<bool> condition) where T : IState
        {
            state.ExitConditionFunc-=condition;
            return state;
        }
        public static T OnUpdate<T>(this T state, Action update) where T : IStateUpdate
        {
            state.UpdateAction+=update;
            return state;
        }
        public static T RemoveUpdate<T>(this T state, Action update) where T : IStateUpdate
        {
            state.UpdateAction-=update;
            return state;
        }
        public static T OnFixedUpdate<T>(this T state, Action fixedUpdate) where T : IStateFixedUpdate
        {
            state.FixedUpdateAction+=fixedUpdate;
            return state;
        }
        public static T RemoveFixedUpdate<T>(this T state, Action fixedUpdate) where T : IStateFixedUpdate
        {
            state.FixedUpdateAction-=fixedUpdate;
            return state;
        }
        public static T OnEnter<T>(this T state, Action<object[]> enter) where T : IEasyState
        {
            state.EnterAction+=enter;
            return state;
        }
        public static T RemoveEnter<T>(this T state, Action<object[]> enter) where T : IEasyState
        {
            state.EnterAction-=enter;
            return state;
        }
        public static T OnExit<T>(this T state, Action<object[]> exit) where T : IEasyState
        {
            state.ExitAction+=exit;
            return state;
        }
        public static T RemoveExit<T>(this T state, Action<object[]> exit) where T : IEasyState
        {
            state.ExitAction-=exit;
            return state;
        }
        public static IEasyState<T> OnEnter<T>(this IEasyState<T> state, Action<T,object[]> enter)
        {
            state.EnterAction+=enter;
            return state;
        }
        public static IEasyState<T> RemoveEnter<T>(this IEasyState<T> state, Action<T,object[]> enter)
        {
            state.EnterAction-=enter;
            return state;
        }
        public static IEasyState<T> OnExit<T>(this IEasyState<T> state, Action<T,object[]> exit)
        {
            state.ExitAction+=exit;
            return state;
        }
        public static IEasyState<T> RemoveExit<T>(this IEasyState<T> state, Action<T,object[]> exit)
        {
            state.ExitAction-=exit;
            return state;
        }
    }
}