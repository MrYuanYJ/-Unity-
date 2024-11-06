using System;

namespace EasyFramework
{
    public abstract class AStateBase : IStateBase
    {
        public IStateMachineBase StateMachineBase { get; set; }
        public T Machine<T>() where T : IStateMachineBase => (T) StateMachineBase;
        bool IStateBase.OnBeforeAdd() => OnBeforeAdd();
        bool IStateBase.OnAfterAdd() => OnAfterAdd();
        bool IStateBase.OnBeforeRemove() => OnBeforeRemove();
        bool IStateBase.OnAfterRemove() => OnAfterRemove();
        protected virtual bool OnBeforeAdd() => true;
        protected virtual bool OnAfterAdd() => true;
        protected virtual bool OnBeforeRemove() => true;
        protected virtual bool OnAfterRemove() => true;
    }
    public abstract class AState: AStateBase,IState
    {
        public Func<bool> EnterConditionFunc { get; set; }
        public Func<bool> ExitConditionFunc { get; set; }
        public Action EnterAction { get; set; }
        public Action ExitAction { get; set; }


        void IState.OnEnter() => OnEnter();
        void IState.OnExit()=> OnExit();
        bool IState.OnEnterCondition()=> OnEnterCondition();

        bool IState.OnExitCondition()=> OnExitCondition();

        protected virtual bool OnEnterCondition() => true;
        protected virtual bool OnExitCondition() => true;
        protected abstract void OnEnter();
        protected abstract void OnExit();
    }

    public abstract class AState<TValue> : AStateBase,IState<TValue>
    {
        public Func<TValue, bool> EnterConditionFunc { get; set; }
        public Func<TValue, bool> ExitConditionFunc { get; set; }
        public Action<TValue> EnterAction { get; set; }
        public Action<TValue> ExitAction { get; set; }
        void IState<TValue>.OnEnter(TValue param) => OnEnter(param);
        void IState<TValue>.OnExit(TValue param) => OnExit(param);
        bool IState<TValue>.OnEnterCondition(TValue param) => OnEnterCondition(param);
        bool IState<TValue>.OnExitCondition(TValue param) => OnExitCondition(param);

        protected virtual bool OnEnterCondition(TValue param) => true;
        protected virtual bool OnExitCondition(TValue param) => true;


        protected abstract void OnEnter(TValue param);
        protected abstract void OnExit(TValue param);
    }

    public abstract class AProcedure : AState, IProcedure
    {
        private bool _isEnable = true;
        public bool IsEnable => _isEnable;
        public void SetEnable(bool isEnable) => _isEnable = isEnable;
    }
    public abstract class AProcedure<TValue> : AState<TValue>, IProcedure
    {
        private bool _isEnable = true;
        public bool IsEnable => _isEnable;
        public void SetEnable(bool isEnable) => _isEnable = isEnable;
    }
    public abstract class AEasyState<TMachine>: AState where TMachine : IStateMachineBase
    {
        public TMachine Machine() => (TMachine)StateMachineBase;
    }
    public abstract class AEasyState<TMachine,TValue>: AState<TValue> where TMachine : IStateMachineBase
    {
        public TMachine Machine() => (TMachine)StateMachineBase;
    }
    public abstract class AEasyProcedure<TMachine> : AEasyState<TMachine>, IProcedure where TMachine : IStateMachineBase
    {
        private bool _isEnable = true;
        public bool IsEnable => _isEnable;
        public void SetEnable(bool isEnable) => _isEnable = isEnable;
    }
    public abstract class AEasyProcedure<TMachine,TValue> : AEasyState<TMachine,TValue>, IProcedure where TMachine : IStateMachineBase
    {
        private bool _isEnable = true;
        public bool IsEnable => _isEnable;
        public void SetEnable(bool isEnable) => _isEnable = isEnable;
    }
}