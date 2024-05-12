namespace EasyFramework.StateMachineKit
{
    public interface IState:IGetMachineAble
    {
        bool EnterCondition(IStateMachine machine)
        {
            SetMachine(machine);
            return OnEnterCondition();
        }

        bool ExitCondition(IStateMachine machine)
        {
            SetMachine(machine);
            return OnExitCondition();
        }

        bool OnEnterCondition();
        bool OnExitCondition();
    }

    public interface IStateUpdate: IState
    {
        void Update(IStateMachine machine)
        {
            SetMachine(machine);
            OnUpdate();
        }
        void OnUpdate();
    }
    public interface IStateFixedUpdate: IState
    {
        void FixedUpdate(IStateMachine machine)
        {
            SetMachine(machine);
            OnFixedUpdate();
        }
        void OnFixedUpdate();
    }
    
    public interface IEasyState : IState
    {
        void Enter(IStateMachine machine, params object[] obj)
        {
            SetMachine(machine);
            OnEnter(obj);
        }
        void Exit(IStateMachine machine, params object[] obj)
        {
            SetMachine(machine);
            OnExit(obj);
        }
        void OnEnter(object[] objects);
        void OnExit(object[] objects);
    }
    public interface IEasyState<T>:IState
    {
        void Enter(IStateMachine machine,T t, params object[] obj)
        {  
            SetMachine(machine);
            OnEnter(t, obj);
        }
        void Exit(IStateMachine machine, T t, params object[] obj)
        {
            SetMachine(machine);
            OnExit(t, obj);
        }
        void OnEnter(T t,object[] objects);
        void OnExit(T t,object[] objects);
    }

    public interface IProcedure:IState
    { 
        bool IsEnable { get;}
        void SetEnable(bool isEnable);
    }
}