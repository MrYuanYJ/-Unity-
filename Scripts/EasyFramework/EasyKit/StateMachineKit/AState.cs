using System;

namespace EasyFramework.StateMachineKit
{
    public abstract class AState: IEasyState
    {
        private IEasyState Self => this;
        IStateMachine IGetMachineAble.StateMachine { get; set; }

        public IStateMachine Machine() => Self.StateMachine;
        public T Machine<T>() where T : IStateMachine => (T) Self.StateMachine;

        Func<bool> IState.EnterConditionFunc { get; set; }

        Func<bool> IState.ExitConditionFunc { get; set; }

        bool IState.OnEnterCondition()=> OnEnterCondition();

        bool IState.OnExitCondition()=> OnExitCondition();

        protected virtual bool OnEnterCondition() => true;
        protected virtual bool OnExitCondition() => true;

        Action<object[]> IEasyState.EnterAction { get; set; }

        Action<object[]> IEasyState.ExitAction { get; set; }

        void IEasyState.OnEnter(object[] objects)=> OnEnter(objects);

        void IEasyState.OnExit(object[] objects)=> OnExit(objects);
        protected abstract void OnEnter(object[] objects);
        protected abstract void OnExit(object[] objects);
    }

    public abstract class AState<TValue> : IEasyState<TValue>
    {
        private IEasyState<TValue> Self => this;
        IStateMachine IGetMachineAble.StateMachine { get; set; }

        public IStateMachine Machine() => Self.StateMachine;
        public T Machine<T>() where T : IStateMachine => (T) Self.StateMachine;

        Func<bool> IState.EnterConditionFunc { get; set; }

        Func<bool> IState.ExitConditionFunc { get; set; }

        protected virtual bool OnEnterCondition() => true;
        bool IState.OnExitCondition()=> OnExitCondition();
        bool IState.OnEnterCondition()=>OnEnterCondition();

        protected virtual bool OnExitCondition() => true;

        Action<TValue, object[]> IEasyState<TValue>.EnterAction { get; set; }

        Action<TValue, object[]> IEasyState<TValue>.ExitAction { get; set; }

        void IEasyState<TValue>.OnEnter(TValue t, object[] objects)=>OnEnter(t, objects);
        void IEasyState<TValue>.OnExit(TValue t, object[] objects)=> OnExit(t, objects);
        protected abstract void OnEnter(TValue t, object[] objects);
        protected abstract void OnExit(TValue t, object[] objects);
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
    public abstract class AEasyState<TMachine>: AState where TMachine : IStateMachine
    {
        private IEasyState Self => this;
        public new TMachine Machine() => (TMachine) Self.StateMachine;
    }
    public abstract class AEasyState<TMachine,TValue>: AState<TValue> where TMachine : IStateMachine
    {
        private IEasyState<TValue> Self => this;
        public new TMachine Machine() => (TMachine) Self.StateMachine;
    }
    public abstract class AEasyProcedure<TMachine> : AEasyState<TMachine>, IProcedure where TMachine : IStateMachine
    {
        private bool _isEnable = true;
        public bool IsEnable => _isEnable;
        public void SetEnable(bool isEnable) => _isEnable = isEnable;
    }
    public abstract class AEasyProcedure<TMachine,TValue> : AEasyState<TMachine,TValue>, IProcedure where TMachine : IStateMachine
    {
        private bool _isEnable = true;
        public bool IsEnable => _isEnable;
        public void SetEnable(bool isEnable) => _isEnable = isEnable;
    }
}