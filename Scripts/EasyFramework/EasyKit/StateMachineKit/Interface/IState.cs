using System;

namespace EasyFramework
{
    public interface IStateUpdate: IState
    {
        void Update(IStateMachineBase machine)
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
        void FixedUpdate(IStateMachineBase machine)
        {
            SetMachine(machine);
            OnFixedUpdate();
            FixedUpdateAction?.Invoke();
        }
        Action FixedUpdateAction{ get; set; }
        protected void OnFixedUpdate();
    }

    public interface IStateBase : IGetMachineAble
    { 
        bool BeforeAdd(IStateMachineBase machine)
        {
            SetMachine(machine);
            return OnBeforeAdd();
        }
        bool AfterAdd(IStateMachineBase machine)
        {
            SetMachine(machine);
            return OnAfterAdd();
        }
        bool BeforeRemove(IStateMachineBase machine)
        {
            SetMachine(machine);
            return OnBeforeRemove();
        }
        bool AfterRemove(IStateMachineBase machine)
        {
            SetMachine(machine);
            return OnAfterRemove();
        }
        protected bool OnBeforeAdd();
        protected bool OnAfterAdd();
        protected bool OnBeforeRemove();
        protected bool OnAfterRemove();
        void Reset(){}
    }
    public interface IState: IStateBase
    {
        bool EnterCondition(IStateMachineBase machine)
        {
            SetMachine(machine);
            if (EnterConditionFunc != null)
                return OnEnterCondition() && EnterConditionFunc();
            return OnEnterCondition();
        }
        bool ExitCondition(IStateMachineBase machine)
        {
            SetMachine(machine);
            if (ExitConditionFunc != null)
                return OnExitCondition() && ExitConditionFunc();
            return OnExitCondition();
        }
        void Enter(IStateMachineBase machine)
        {
            SetMachine(machine);
            EnterAction?.Invoke();
            OnEnter();
        }
        void Exit(IStateMachineBase machine)
        {
            SetMachine(machine);
            ExitAction?.Invoke();
            OnExit();
        }
        Func<bool> EnterConditionFunc{ get; set; }
        Func<bool> ExitConditionFunc{ get; set; }
        Action EnterAction{ get; set; }
        Action ExitAction{ get; set; }
        protected void OnEnter();
        protected void OnExit();
        protected bool OnEnterCondition();
        protected bool OnExitCondition();

        void IStateBase.Reset()
        {
            EnterAction = null;
            ExitAction = null;
            EnterConditionFunc = null;
            ExitConditionFunc = null;
        }
    }
    public interface IState<T>:IStateBase
    {
        bool EnterCondition(IStateMachineBase machine,T param)
        {
            SetMachine(machine);
            if (EnterConditionFunc != null)
                return OnEnterCondition(param) && EnterConditionFunc(param);
            return OnEnterCondition(param);
        }
        bool ExitCondition(IStateMachineBase machine,T param)
        {
            SetMachine(machine);
            if (ExitConditionFunc != null)
                return OnExitCondition(param) && ExitConditionFunc(param);
            return OnExitCondition(param);
        }
        void Enter(IStateMachineBase machine,T param)
        {  
            SetMachine(machine);
            EnterAction?.Invoke(param);
            OnEnter(param);
        }
        void Exit(IStateMachineBase machine, T param)
        {
            SetMachine(machine);
            ExitAction?.Invoke(param);
            OnExit(param);
        }
        Func<T,bool> EnterConditionFunc{ get; set; }
        Func<T,bool> ExitConditionFunc{ get; set; }
        Action<T> EnterAction{ get; set; }
        Action<T> ExitAction{ get; set; }
        protected void OnEnter(T param);
        protected void OnExit(T param);
        protected bool OnEnterCondition(T param);
        protected bool OnExitCondition(T param);
        void IStateBase.Reset()
        {
            EnterAction = null;
            ExitAction = null;
            EnterConditionFunc = null;
            ExitConditionFunc = null;
        }
    }

    public interface IProcedure:IStateBase
    { 
        bool IsEnable { get;}
        void SetEnable(bool isEnable);
    }

    public static class IStateExtension
    {
        public static IState OnEnterCondition(this IState state, Func<bool> condition)
        {
            state.EnterConditionFunc+=condition;
            return state;
        }
        public static IState<T> OnEnterCondition<T>(this IState<T> state, Func<T,bool> condition)
        {
            state.EnterConditionFunc+=condition;
            return state;
        }
        public static IState OnExitCondition(this IState state, Func<bool> condition)
        {
            state.ExitConditionFunc+=condition;
            return state;
        }
        public static IState<T> OnExitCondition<T>(this IState<T> state, Func<T,bool> condition)
        {
            state.ExitConditionFunc+=condition;
            return state;
        }
        public static IState RemoveEnterCondition(this IState state, Func<bool> condition)
        {
            state.EnterConditionFunc-=condition;
            return state;
        }
        public static IState<T> RemoveEnterCondition<T>(this IState<T> state, Func<T, bool> condition)
        {
            state.EnterConditionFunc-=condition;
            return state;
        }
        public static IState RemoveExitCondition(this IState state, Func<bool> condition)
        {
            state.ExitConditionFunc-=condition;
            return state;
        }
        public static IState<T> RemoveExitCondition<T>(this IState<T> state, Func<T, bool> condition)
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
        
        public static IState OnEnter(this IState state, Action enter)
        {
            state.EnterAction+=enter;
            return state;
        }
        public static IState<T> OnEnter<T>(this IState<T> state, Action<T> enter)
        {
            state.EnterAction+=enter;
            return state;
        }
        public static IState OnExit(this IState state, Action exit)
        {
            state.ExitAction+=exit;
            return state;
        }
        public static IState<T> OnExit<T>(this IState<T> state, Action<T> exit)
        {
            state.ExitAction+=exit;
            return state;
        }
        public static IState RemoveEnter(this IState state, Action enter)
        {
            state.EnterAction-=enter;
            return state;
        }
        public static IState<T> RemoveEnter<T>(this IState<T> state, Action<T> enter)
        {
            state.EnterAction-=enter;
            return state;
        }
        public static IState RemoveExit(this IState state, Action exit)
        {
            state.ExitAction -= exit;
            return state;
        }
        public static IState<T> RemoveExit<T>(this IState<T> state, Action<T> exit)
        {
            state.ExitAction -= exit;
            return state;
        }
    }
}