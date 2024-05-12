namespace EasyFramework.StateMachineKit
{
    public abstract class AState: IEasyState
    {
        private IEasyState Self => this;
        IStateMachine IGetMachineAble.StateMachine { get; set; }

        public IStateMachine Machine() => Self.StateMachine;
        public T Machine<T>() where T : IStateMachine => (T) Self.StateMachine;

        public virtual bool OnEnterCondition() => true;
        public virtual bool OnExitCondition() => true;
        public abstract void OnEnter(object[] objects);
        public abstract void OnExit(object[] objects);
    }

    public abstract class AState<TValue> : IEasyState<TValue>
    {
        private IEasyState<TValue> Self => this;
        IStateMachine IGetMachineAble.StateMachine { get; set; }

        public IStateMachine Machine() => Self.StateMachine;
        public T Machine<T>() where T : IStateMachine => (T) Self.StateMachine;

        public virtual bool OnEnterCondition() => true;
        public virtual bool OnExitCondition() => true;
        public abstract void OnEnter(TValue t, object[] objects);
        public abstract void OnExit(TValue t, object[] objects);
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