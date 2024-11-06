/****************************************************
    文件:      StateMachine (1).cs
    作者:      YYJYYDS
    日期:      2024/02/05 14:27:54
    版本:      2021.3.21f1c1
    功能:      
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

/*namespace EasyFramework.StateMachineKit2
{
    public class StateEvent
    {
        public EasyEvent OnInit=new();
        public EasyEvent OnEnter=new();
        public EasyEvent OnExit=new();
        public EasyEvent OnUpdate=new();

        public void Clear()
        {
            OnInit = null;
            OnEnter = null;
            OnExit = null;
            OnUpdate = null;
        }
        public void InvokeOnInit() => OnInit.Invoke();
        public void InvokeOnEnter() => OnEnter.Invoke();
        public void InvokeOnExit() => OnExit.Invoke();
    }

    public interface IState
    {
        Dictionary<AbstractStateMachine, StateEvent> StateEventDic { get; set; }
        
        void Init(AbstractStateMachine machine);
        bool Condition(AbstractStateMachine machine);
        public virtual void Update(AbstractStateMachine machine) => StateEventDic[machine].OnUpdate.Invoke();
    }

    /// <summary>
    /// 状态抽象类，需自行继承此类后使用
    /// </summary>
    public abstract class AState : IState
    {
        public Dictionary<AbstractStateMachine, StateEvent> StateEventDic { get; set; } = new();

        public virtual void Init(AbstractStateMachine machine)
        {
            StateEventDic.TryAdd(machine, new StateEvent());
            StateEventDic[machine].InvokeOnInit();
            this.OnInit(machine);
        }

        public virtual bool Condition(AbstractStateMachine machine) => true;

        public virtual void Enter(AbstractStateMachine machine, params object[] data)
        {
            StateEventDic[machine].InvokeOnEnter();
            this.OnEnter(machine,data);
        }
        public virtual void Exit(AbstractStateMachine machine, params object[] data)
        {
            StateEventDic[machine].InvokeOnExit();
            this.OnExit(machine,data);
        }

        protected abstract void OnInit(AbstractStateMachine machine);
        protected abstract void OnEnter(AbstractStateMachine machine,params object[] data);
        protected abstract void OnExit(AbstractStateMachine machine,params object[] data);
    }

    /// <summary>
    /// 状态抽象类，需自行继承此类后使用
    /// </summary>
    public abstract class AState<A> : IState
    {
        public Dictionary<AbstractStateMachine, StateEvent> StateEventDic { get; set; } = new();

        public virtual void Init(AbstractStateMachine machine)
        {
            StateEventDic.TryAdd(machine, new StateEvent());
            StateEventDic[machine].InvokeOnInit();
            this.OnInit(machine);
        }
        public virtual bool Condition(AbstractStateMachine machine) => true;
        public virtual void Enter(AbstractStateMachine machine,A a, params object[] data)
        {
            StateEventDic[machine].InvokeOnEnter();
            this.OnEnter(machine,a,data);
        }
        public virtual void Exit(AbstractStateMachine machine,A a, params object[] data)
        {
            StateEventDic[machine].InvokeOnExit();
            this.OnExit(machine,a,data);
        }
        protected abstract void OnInit(AbstractStateMachine machine);
        protected abstract void OnEnter(AbstractStateMachine machine,A a, params object[] data);
        protected abstract void OnExit(AbstractStateMachine machine,A a, params object[] data);
    }

    /// <summary>
    /// 状态抽象类，需自行继承此类后使用
    /// </summary>
    public abstract class AState<A, B> : IState
    {
        public Dictionary<AbstractStateMachine, StateEvent> StateEventDic { get; set; } = new();

        public virtual void Init(AbstractStateMachine machine)
        {
            StateEventDic.TryAdd(machine, new StateEvent());
            StateEventDic[machine].InvokeOnInit();
            this.OnInit(machine);
        }
        public virtual bool Condition(AbstractStateMachine machine) => true;
        public virtual void Enter(AbstractStateMachine machine,A a, B b, params object[] data)
        {
            StateEventDic[machine].InvokeOnEnter();
            this.OnEnter(machine,a,b,data);
        }
        public virtual void Exit(AbstractStateMachine machine,A a, B b, params object[] data)
        {
            StateEventDic[machine].InvokeOnExit();
            this.OnExit(machine,a,b,data);
        }
        protected abstract void OnInit(AbstractStateMachine machine);
        protected abstract void OnEnter(AbstractStateMachine machine,A a, B b, params object[] data);
        protected abstract void OnExit(AbstractStateMachine machine,A a, B b, params object[] data);
    }

    public abstract class AbstractStateMachine
    {
        protected bool _pauseUpdate;
        protected bool _pauseMachine;
        protected event Action<AbstractStateMachine> OnUpdate;

        /// <summary>
        /// 暂停状态机，使状态机不能进行状态转换
        /// </summary>
        /// <param name="pauseUpdate">是否暂停状态机的Update</param>
        public virtual void PauseMachine(bool pauseUpdate)
        {
            _pauseMachine = true;
            if (pauseUpdate)
                _pauseUpdate = true;
        }

        /// <summary>
        /// 恢复状态机，使状态机能正常运行
        /// </summary>
        public virtual void ResumeMachine()
        {
            _pauseMachine = false;
            _pauseUpdate = false;
        }

        /// <summary>
        /// 若有状态需要Update，自行在合适位置调用此方法
        /// </summary>
        public virtual void Update()
        {
            if (!_pauseUpdate)
                OnUpdate?.Invoke(this);
        }

        public virtual void AddUpdateEvent<T>(T state, Action action,bool removeOnExit=true) where T : IState
        {
            state.StateEventDic[this].OnUpdate.Register(action);
            if (removeOnExit)
                state.StateEventDic[this].OnExit.Register(
                    () => state.StateEventDic[this].OnUpdate.UnRegister(action));
        }
    }

    public abstract class AbstractStateMachine<V> : AbstractStateMachine where V : class, IState
    {
        protected readonly Dictionary<Type, V> _listeners = new();
        public Type CurrentState;
        public Type PreviousState;
        public virtual bool CanSetStateMachine(Type key) => true;

        public virtual V GetListener(Type key)
        {
            return _listeners[key];
        }

        public virtual T GetListener<T>() where T : class
        {
            return _listeners[typeof(T)] as T;
        }
        
        /// <summary>
        /// 添加新状态监听
        /// </summary>
        /// <param name="state">添加的状态</param>
        /// <returns>是否添加成功</returns>
        public virtual bool AddListener(V state)
        {
            var type = state.GetType();
            if (CanSetStateMachine(type))
                if (_listeners.TryAdd(type, state))
                {
                    _listeners[type].Init(this);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 添加新状态监听
        /// </summary>
        /// <param name="state">添加的状态类</param>
        /// <returns>是否添加成功</returns>
        public virtual bool AddListener(Type state)
        {
            if (CanSetStateMachine(state) && typeof(V).IsAssignableFrom(state))
                if (_listeners.TryAdd(state, Activator.CreateInstance(state) as V))
                {
                    _listeners[state].Init(this);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 添加新状态监听
        /// </summary>
        /// <typeparam name="T">添加的状态类</typeparam>
        /// <returns></returns>
        public virtual bool AddListener<T>() where T : V, new()
        {
            if (CanSetStateMachine(typeof(T)) && _listeners.TryAdd(typeof(T), new T()))
            {
                _listeners[typeof(T)].Init(this);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 去除已有状态监听
        /// </summary>
        /// <param name="state">去除的状态类</param>
        /// <returns>是否去除成功</returns>
        public virtual bool RemoveListener(Type state)
        {
            if (CanSetStateMachine(state) && _listeners.TryGetValue(state, out var value))
            {
                if (CurrentState == state)
                {
                    OnUpdate -= value.Update;
                    PreviousState = CurrentState;
                    CurrentState = null;
                    value.StateEventDic[this].InvokeOnExit();
                }

                return _listeners.Remove(state);
            }

            return false;
        }

        /// <summary>
        /// 去除已有状态监听
        /// </summary>
        /// <typeparam name="T">去除的状态类</typeparam>
        /// <returns>是否去除成功</returns>
        public virtual bool RemoveListener<T>() where T : V
        {
            return RemoveListener(typeof(T));
        }

        protected virtual bool StateChange(Type state)
        {
            if (!_pauseMachine && CanSetStateMachine(state) && state != CurrentState &&
                _listeners.TryGetValue(state, out var value))
            {
                if (!value.Condition(this)) return false;
                
                if (CurrentState != null)
                {
                    OnUpdate -= _listeners[CurrentState].Update;
                }

                PreviousState = CurrentState;
                CurrentState = state;
                OnUpdate += value.Update;
                return true;
            }

            return false;
        }

        protected virtual bool CurrentStateExit()
        {
            if (!_pauseMachine && CurrentState != null)
            {
                OnUpdate -= _listeners[CurrentState].Update;
                PreviousState = CurrentState;
                CurrentState = null;
                return true;
            }

            return false;
        }
    }

    public abstract class AbstractEnumStateMachine<K, V> : AbstractStateMachine where K : Enum where V : class, IState
    {
        protected readonly Dictionary<int, V> _listeners = new();
        public int CurrentState;
        public int PreviousState;
        public virtual bool CanSetStateMachine(int key) => true;

        public virtual V GetListener(int key)
        {
            return _listeners[key];
        }

        public virtual V GetListener(K key) 
        {
            return _listeners[key.GetHashCode()];
        }
        /// <summary>
        /// 添加新状态监听
        /// </summary>
        /// <param name="key">添加的状态类标识</param>
        /// <param name="state">添加的状态</param>
        /// <returns>是否添加成功</returns>
        public virtual bool AddListener(K key,V state)
        {
            if (CanSetStateMachine(key.GetHashCode()))
                if (_listeners.TryAdd(key.GetHashCode(), state))
                {
                    state.Init(this);
                    return true;
                }

            return false;
        }
        /// <summary>
        /// 添加新状态监听
        /// </summary>
        /// <param name="key">添加的(HashCode)状态类标识</param>
        /// <param name="type">添加的状态类类型</param>
        /// <returns>是否添加成功</returns>
        public virtual bool AddListener(int key, Type type)
        {

            if (CanSetStateMachine(key) && typeof(V).IsAssignableFrom(type))
                if (_listeners.TryAdd(key, Activator.CreateInstance(type) as V))
                {
                    _listeners[key].Init(this);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 添加新状态监听
        /// </summary>
        /// <param name="key">添加的状态类标识</param>
        /// <typeparam name="T">添加的状态类</typeparam>
        /// <returns></returns>
        public virtual bool AddListener<T>(K key) where T : V, new()
        {
            if (CanSetStateMachine(key.GetHashCode()) && _listeners.TryAdd(key.GetHashCode(), new T()))
            {
                _listeners[key.GetHashCode()].Init(this);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 去除已有状态监听
        /// </summary>
        /// <param name="key">去除的(HashCode)状态类标识</param>
        /// <returns>是否去除成功</returns>
        public virtual bool RemoveListener(int key)
        {
            if (CanSetStateMachine(key) && _listeners.TryGetValue(key, out var value))
            {
                if (CurrentState.Equals(key))
                {
                    OnUpdate -= value.Update;
                    PreviousState = CurrentState;
                    CurrentState = default;
                    value.StateEventDic[this].InvokeOnExit();
                }

                return _listeners.Remove(key);
            }

            return false;
        }

        /// <summary>
        /// 去除已有状态监听
        /// </summary>
        /// <param name="key">去除的状态类标识</param>
        /// <returns>是否去除成功</returns>
        public virtual bool RemoveListener(K key)
        {
            return RemoveListener(key.GetHashCode());
        }

        protected virtual bool StateChange(int key)
        {
            if (!_pauseMachine && CanSetStateMachine(key) && key != CurrentState &&
                _listeners.TryGetValue(key, out var value))
            {
                if (!value.Condition(this)) return false;
                
                if (CurrentState != 0)
                {
                    OnUpdate -= _listeners[CurrentState].Update;
                }

                PreviousState = CurrentState;
                CurrentState = key;
                OnUpdate += value.Update;
                return true;
            }

            return false;
        }

        protected virtual bool CurrentStateExit()
        {
            if (!_pauseMachine && CurrentState != 0)
            {
                OnUpdate -= _listeners[CurrentState].Update;
                PreviousState = CurrentState;
                CurrentState = 0;
                return true;
            }

            return false;
        }

        protected virtual bool StateReplace(int key, Type state)
        {
            if (CanSetStateMachine(key) && _listeners.ContainsKey(key))
            {
                if (CurrentState == key)
                {
                    OnUpdate -= _listeners[key].Update;
                    _listeners[key].StateEventDic[this].InvokeOnExit();
                }

                _listeners[key] = Activator.CreateInstance(state) as V;
                if (CurrentState == key)
                {
                    OnUpdate += _listeners[key].Update;
                }

                _listeners[key].Init(this);
                return true;
            }

            return false;
        }

        protected virtual bool StateReplace<T>(int key) where T : V, new()
        {
            if (CanSetStateMachine(key) && _listeners.ContainsKey(key))
            {
                if (CurrentState == key)
                {
                    OnUpdate -= _listeners[key].Update;
                    _listeners[key].StateEventDic[this].InvokeOnExit();
                }

                _listeners[key] = new T();
                if (CurrentState == key)
                {
                    OnUpdate += _listeners[key].Update;
                }

                _listeners[key].Init(this);
                return true;
            }

            return false;
        }
    }

    #region 基础状态机

    /// <summary>
    /// 状态机
    /// </summary>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AStateMachine<State> : AbstractStateMachine<State> where State : AState
    {
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="state">目标状态类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(Type state, params object[] data)
        {
            if (StateChange(state))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,data);
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="T">目标状态类</typeparam>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState<T>(params object[] data) where T : State
        {
            return ChangeState(typeof(T), data);
        }

        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,data);
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 状态机
    /// </summary>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AStateMachine<State, A> : AbstractStateMachine<State> where State : AState<A>
    {
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="state">目标状态类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(Type state, A a, params object[] data)
        {
            if (StateChange(state))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,a, data);
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="T">目标状态类</typeparam>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState<T>(A a, params object[] data) where T : State
        {
            return ChangeState(typeof(T), a, data);
        }

        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(A a, params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,a, data);
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 状态机
    /// </summary>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AStateMachine<State, A, B> : AbstractStateMachine<State> where State : AState<A,B>
    {
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="state">目标状态类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(Type state, A a, B b, params object[] data)
        {
            if (StateChange(state))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,a, b, data);
                _listeners[CurrentState].Enter(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <typeparam name="T">目标状态类</typeparam>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState<T>(A a, B b, params object[] data) where T : State
        {
            return ChangeState(typeof(T), a, b, data);
        }

        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(A a, B b, params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,a, b, data);
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 枚举状态机
    /// </summary>
    /// <typeparam name="EState">装载的状态枚举类</typeparam>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AEnumStateMachine<EState, State> : AbstractEnumStateMachine<EState, State>
        where EState : Enum where State : AState
    {
        /// <summary>
        /// 替换已有状态监听
        /// </summary>
        /// <param name="key">原状态的(HashCode)状态类标识</param>
        /// <param name="state">新状态类类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener(int key, Type state, params object[] data)
        {
            if (StateReplace(key, state))
            {
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 替换已有状态监听
        /// </summary>
        /// <typeparam name="T">目标状态类</typeparam>
        /// <param name="key">原状态的状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener<T>(EState key, params object[] data) where T : State, new()
        {
            if (StateReplace<T>(key.GetHashCode()))
            {
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="key">目标(HashCode)状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(int key, params object[] data)
        {
            if (StateChange(key))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,data);
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="key">目标状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(EState key, params object[] data)
        {
            return ChangeState(key.GetHashCode(), data);
        }

        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,data);
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 枚举状态机
    /// </summary>
    /// <typeparam name="EState">装载的状态枚举类</typeparam>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AEnumStateMachine<EState, State, A> : AbstractEnumStateMachine<EState, State>
        where EState : Enum where State : AState<A>
    {
        /// <summary>
        /// 替换已有状态监听
        /// </summary>
        /// <param name="key">原状态的(HashCode)状态类标识</param>
        /// <param name="state">新状态类类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener(int key, Type state, A a, params object[] data)
        {
            if (StateReplace(key, state))
            {
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 替换已有状态监听
        /// </summary>
        /// <typeparam name="T">目标状态类</typeparam>
        /// <param name="key">原状态的状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener<T>(EState key, A a, params object[] data) where T : State, new()
        {
            if (StateReplace<T>(key.GetHashCode()))
            {
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="key">目标(HashCode)状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(int key, A a, params object[] data)
        {
            if (StateChange(key))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,a, data);
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="key">目标状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(EState key, A a, params object[] data)
        {
            return ChangeState(key.GetHashCode(), a, data);
        }

        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(A a, params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,a, data);
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 枚举状态机
    /// </summary>
    /// <typeparam name="EState">装载的状态枚举类</typeparam>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AEnumStateMachine<EState, State, A, B> : AbstractEnumStateMachine<EState, State>
        where EState : Enum where State : AState<A,B>
    {
        /// <summary>
        /// 替换已有状态监听
        /// </summary>
        /// <param name="key">原状态的(HashCode)状态类标识</param>
        /// <param name="state">新状态类类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener(int key, Type state, A a, B b, params object[] data)
        {
            if (StateReplace(key, state))
            {
                _listeners[CurrentState].Enter(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 替换已有状态监听
        /// </summary>
        /// <typeparam name="T">目标状态类</typeparam>
        /// <param name="key">原状态的状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener<T>(EState key, A a, B b, params object[] data) where T : State, new()
        {
            if (StateReplace<T>(key.GetHashCode()))
            {
                _listeners[CurrentState].Enter(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="key">目标(HashCode)状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(int key, A a, B b, params object[] data)
        {
            if (StateChange(key))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,a, b, data);
                _listeners[CurrentState].Enter(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="key">目标状态类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeState(EState key, A a, B b, params object[] data)
        {
            return ChangeState(key.GetHashCode(), a, b, data);
        }

        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(A a, B b, params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,a, b, data);
                return true;
            }

            return false;
        }
    }

    #endregion

    public abstract class AbstractProcedureMachine<V> : AbstractStateMachine<V> where V : class, IState
    {
        protected readonly LinkedList<Type> _procedures = new();
        
        /// <summary>
        /// 添加新流程监听，流程状态机将按添加顺序切换流程
        /// </summary>
        /// <param name="procedure">添加的流程</param>
        /// <returns>是否添加成功</returns>
        public override bool AddListener(V procedure)
        {
            if (base.AddListener(procedure))
            {
                _procedures.AddLast(procedure.GetType());
                return true;
            }

            return false;
        }

        /// <summary>
        /// 添加新流程监听，流程状态机将按添加顺序切换流程
        /// </summary>
        /// <param name="procedure">添加的流程类型</param>
        /// <returns>是否添加成功</returns>
        public override bool AddListener(Type procedure)
        {
            if (base.AddListener(procedure))
            {
                _procedures.AddLast(procedure);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 添加新流程监听，流程状态机将按添加顺序切换流程
        /// </summary>
        /// <typeparam name="T">添加的流程类</typeparam>
        /// <returns>是否添加成功</returns>
        public override bool AddListener<T>()
        {
            if (base.AddListener<T>())
            {
                _procedures.AddLast(typeof(T));
                return true;
            }

            return false;
        }

        /// <summary>
        /// 去除已有流程监听
        /// </summary>
        /// <param name="procedure">去除的流程类型</param>
        /// <returns>是否去除成功</returns>
        public override bool RemoveListener(Type procedure)
        {
            if (base.RemoveListener(procedure))
            {
                _procedures.Remove(procedure);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 去除已有流程监听
        /// </summary>
        /// <typeparam name="T">去除的流程类</typeparam>
        /// <returns>是否去除成功</returns>
        public override bool RemoveListener<T>()
        {
            if (base.RemoveListener<T>())
            {
                _procedures.Remove(typeof(T));
                return true;
            }

            return false;
        }

        /// <summary>
        /// 在指定流程之前添加新流程监听，使添加的新流程执行在指定流程之前
        /// </summary>
        /// <param name="newProcedure">添加的流程类型</param>
        /// <param name="procedure">指定的流程类型</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerBefore(Type procedure, Type newProcedure)
        {
            var node = _procedures.Find(procedure);
            if (node != null)
                if (base.AddListener(newProcedure))
                {
                    _procedures.AddBefore(node, newProcedure);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 在指定流程之前添加新流程监听，使添加的新流程执行在指定流程之前
        /// </summary>
        /// <typeparam name="T">添加的流程类</typeparam>
        /// <param name="procedure">指定的流程类型</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerBefore<T>(Type procedure) where T : V, new()
        {
            var node = _procedures.Find(procedure);
            if (node != null)
                if (base.AddListener<T>())
                {
                    _procedures.AddBefore(node, typeof(T));
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 在指定流程之后添加新流程监听，使添加的新流程执行在指定流程之后
        /// </summary>
        /// <param name="newProcedure">指定的流程类型</param>
        /// <param name="procedure">指定的流程类型</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerAfter(Type procedure, Type newProcedure)
        {
            var node = _procedures.Find(procedure);
            if (node != null)
                if (base.AddListener(newProcedure))
                {
                    _procedures.AddAfter(node, newProcedure);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 在指定流程之后添加新流程监听，使添加的新流程执行在指定流程之后
        /// </summary>
        /// <typeparam name="T">添加的流程类</typeparam>
        /// <param name="procedure">指定的流程类型</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerAfter<T>(Type procedure) where T : V, new()
        {
            var node = _procedures.Find(procedure);
            if (node != null)
                if (base.AddListener<T>())
                {
                    _procedures.AddAfter(node, typeof(T));
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之前执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <param name="moveProcedure">修改执行顺序的流程</param>
        /// <returns>是否修改成功</returns>
        public bool SetListenerBefore(Type procedure, Type moveProcedure)
        {
            var node = _procedures.Find(procedure);
            var moveNode = _procedures.Find(moveProcedure);
            if (node != null && moveNode != null)
            {
                _procedures.AddBefore(node, moveProcedure);
                _procedures.Remove(moveNode);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之前执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <typeparam name="T">修改执行顺序的流程</typeparam>
        /// <returns></returns>
        public bool SetListenerBefore<T>(Type procedure)
        {
            return SetListenerBefore(procedure, typeof(T));
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之后执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <param name="moveProcedure">修改执行顺序的流程</param>
        /// <returns>是否修改成功</returns>
        public bool SetListenerAfter(Type procedure, Type moveProcedure)
        {
            var node = _procedures.Find(procedure);
            var moveNode = _procedures.Find(moveProcedure);
            if (node != null && moveNode != null)
            {
                _procedures.AddAfter(node, moveProcedure);
                _procedures.Remove(moveNode);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之后执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <typeparam name="T">修改执行顺序的流程</typeparam>
        /// <returns>是否修改成功</returns>
        public bool SetListenerAfter<T>(Type procedure)
        {
            return SetListenerAfter(procedure, typeof(T));
        }
    }

    public abstract class AbstractEnumProcedureMachine<K, V> : AbstractEnumStateMachine<K, V>
        where K : Enum where V : class, IState
    {
        protected readonly LinkedList<int> _procedures = new();
        /// <summary>
        /// 添加新流程监听，流程状态机将按添加顺序切换流程
        /// </summary>
        /// <param name="key">添加流程的流程枚举</param>
        /// <param name="procedure">添加的流程</param>
        /// <returns>是否添加成功</returns>
        public override bool AddListener(K key, V procedure)
        {
            if (base.AddListener(key, procedure))
            {
                _procedures.AddLast(key.GetHashCode());
                return true;
            }

            return false;
        }

        /// <summary>
        /// 添加新流程监听，流程状态机将按添加顺序切换流程
        /// </summary>
        /// <param name="procedure">添加流程的(HasCode)流程枚举</param>
        /// <param name="type">添加流程的流程类类型</param>
        /// <returns>是否添加成功</returns>
        public override bool AddListener(int procedure, Type type)
        {
            if (base.AddListener(procedure, type))
            {
                _procedures.AddLast(procedure);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 添加新流程监听，流程状态机将按添加顺序切换流程
        /// </summary>
        /// <typeparam name="T">添加的流程类</typeparam>
        /// <param name="procedure">添加流程的流程枚举</param>
        /// <returns>是否添加成功</returns>
        public override bool AddListener<T>(K procedure)
        {
            if (base.AddListener<T>(procedure))
            {
                _procedures.AddLast(procedure.GetHashCode());
                return true;
            }

            return false;
        }

        /// <summary>
        /// 去除已有流程监听
        /// </summary>
        /// <param name="procedure">被去除流程的(int)流程枚举</param>
        /// <returns>是否去除成功</returns>
        public override bool RemoveListener(int procedure)
        {
            if (base.RemoveListener(procedure))
            {
                _procedures.Remove(procedure);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 去除已有流程监听
        /// </summary>
        /// <param name="procedure">被去除流程的流程枚举</param>
        /// <returns>是否去除成功</returns>
        public override bool RemoveListener(K procedure)
        {
            return RemoveListener(procedure.GetHashCode());
        }

        /// <summary>
        /// 在指定流程之前添加新流程监听，使添加的新流程执行在指定流程之前
        /// </summary>
        /// <param name="type">添加的流程类</param>
        /// <param name="procedure">指定流程的(HashCode)流程枚举</param>
        /// <param name="newProcedure">添加流程的(HashCode)流程枚举</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerBefore(int procedure, int newProcedure, Type type)
        {
            var node = _procedures.Find(procedure);
            if (node != null)
                if (base.AddListener(newProcedure, type))
                {
                    _procedures.AddBefore(node, newProcedure);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 在指定流程之前添加新流程监听，使添加的新流程执行在指定流程之前
        /// </summary>
        /// <typeparam name="T">添加的流程类</typeparam>
        /// <param name="procedure">指定流程的流程枚举</param>
        /// <param name="newProcedure">添加流程的流程枚举</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerBefore<T>(K procedure, K newProcedure) where T : V, new()
        {
            var node = _procedures.Find(procedure.GetHashCode());
            if (node != null)
                if (base.AddListener<T>(newProcedure))
                {
                    _procedures.AddBefore(node, newProcedure.GetHashCode());
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 在指定流程之后添加新流程监听，使添加的新流程执行在指定流程之后
        /// </summary>
        /// <param name="type">添加的流程类</param>
        /// <param name="procedure">指定流程的(int)流程枚举</param>
        /// <param name="newProcedure">添加流程的(int)流程枚举</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerAfter(int procedure, int newProcedure, Type type)
        {
            var node = _procedures.Find(procedure);
            if (node != null)
                if (base.AddListener(newProcedure, type))
                {
                    _procedures.AddAfter(node, newProcedure);
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 在指定流程之后添加新流程监听，使添加的新流程执行在指定流程之后
        /// </summary>
        /// <typeparam name="T">添加的流程类</typeparam>
        /// <param name="procedure">指定流程的流程枚举</param>
        /// <param name="newProcedure">添加流程的流程枚举</param>
        /// <returns>是否添加成功</returns>
        public bool AddListenerAfter<T>(K procedure, K newProcedure) where T : V, new()
        {
            var node = _procedures.Find(procedure.GetHashCode());
            if (node != null)
                if (base.AddListener<T>(newProcedure))
                {
                    _procedures.AddAfter(node, newProcedure.GetHashCode());
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之前执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <param name="moveProcedure">修改执行顺序的流程</param>
        /// <returns>是否修改成功</returns>
        public bool SetListenerBefore(int procedure, int moveProcedure)
        {
            var node = _procedures.Find(procedure);
            var moveNode = _procedures.Find(moveProcedure);
            if (node != null && moveNode != null)
            {
                _procedures.AddBefore(node, moveProcedure);
                _procedures.Remove(moveNode);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之前执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <param name="moveProcedure">修改执行顺序的流程</param>
        /// <returns></returns>
        public bool SetListenerBefore(K procedure,K moveProcedure)
        {
            return SetListenerBefore(procedure.GetHashCode(), moveProcedure.GetHashCode());
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之后执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <param name="moveProcedure">修改执行顺序的流程</param>
        /// <returns>是否修改成功</returns>
        public bool SetListenerAfter(int procedure, int moveProcedure)
        {
            var node = _procedures.Find(procedure);
            var moveNode = _procedures.Find(moveProcedure);
            if (node != null && moveNode != null)
            {
                _procedures.AddAfter(node, moveProcedure);
                _procedures.Remove(moveNode);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 修改已有流程的执行顺序，使其在另一个流程之后执行
        /// </summary>
        /// <param name="procedure">已有流程</param>
        /// <param name="moveProcedure">修改执行顺序的流程</param>
        /// <returns>是否修改成功</returns>
        public bool SetListenerAfter(K procedure,K moveProcedure)
        {
            return SetListenerAfter(procedure.GetHashCode(), moveProcedure.GetHashCode());
        }
    }

    #region 流程状态机，将根据链表的顺序进行流程切换

    /// <summary>
    /// 流程状态机，调用NextProcedure时，将根据链表的顺序进行流程切换
    /// </summary>
    /// <typeparam name="Procedure">流程类</typeparam>
    public abstract class AProcedureMachine<Procedure> : AbstractProcedureMachine<Procedure>
        where Procedure : AState
    {
        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="procedure">目标流程类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(Type procedure, params object[] data)
        {
            if (StateChange(procedure))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,data);
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <typeparam name="T">目标流程类</typeparam>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure<T>(params object[] data)
        {
            return ChangeProcedure(typeof(T), data);
        }

        /// <summary>
        /// 退出当前流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentProcedure(params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换至上一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool PreviousProcedure(params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Previous != null)
                return ChangeProcedure(node.Previous.Value, data);
            else
                return ExitCurrentProcedure(data);
        }

        /// <summary>
        /// 切换至下一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool NextProcedure(params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Next != null)
                return ChangeProcedure(node.Next.Value, data);
            else
                return ExitCurrentProcedure(data);
        }

        /// <summary>
        /// 切换至第一个流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ReStartProcedure(params object[] data)
        {
            var node = _procedures.First;
            if (node != null)
                return ChangeProcedure(node.Value, data);
            else
                return ExitCurrentProcedure(data);
        }
    }

    /// <summary>
    /// 流程状态机，调用NextProcedure时，将根据链表的顺序进行流程切换
    /// </summary>
    /// <typeparam name="Procedure">流程类</typeparam>
    public abstract class AProcedureMachine<Procedure, A> : AbstractProcedureMachine<Procedure>
        where Procedure : AState<A>
    {
        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="procedure">目标流程类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(Type procedure, A a, params object[] data)
        {
            if (StateChange(procedure))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,a, data);
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <typeparam name="T">目标流程类</typeparam>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure<T>(A a, params object[] data) where T : Procedure
        {
            return ChangeProcedure(typeof(T), a, data);
        }

        /// <summary>
        /// 退出当前流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentProcedure(A a, params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,a, data);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// 切换至上一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool PreviousProcedure(A a,params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Previous != null)
                return ChangeProcedure(node.Previous.Value,a, data);
            else
                return ExitCurrentProcedure(a,data);
        }

        /// <summary>
        /// 切换至下一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool NextProcedure(A a, params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Next != null)
                return ChangeProcedure(node.Next.Value, a, data);
            else
                return ExitCurrentProcedure(a, data);
        }
        
        /// <summary>
        /// 切换至第一个流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ReStartProcedure(A a,params object[] data)
        {
            var node = _procedures.First;
            if (node != null)
                return ChangeProcedure(node.Value,a, data);
            else
                return ExitCurrentProcedure(a,data);
        }
    }

    /// <summary>
    /// 枚举流程状态机，调用NextProcedure时，将根据链表的顺序进行流程切换
    /// </summary>
    /// <typeparam name="EProcedure">流程枚举类</typeparam>
    /// <typeparam name="Procedure">流程类</typeparam>
    public abstract class
        AEnumProcedureMachine<EProcedure, Procedure> : AbstractEnumProcedureMachine<EProcedure, Procedure>
        where EProcedure : Enum where Procedure : AState
    {
        /// <summary>
        /// 替换已有流程监听
        /// </summary>
        /// <param name="key">原流程的(HashCode)状态类标识</param>
        /// <param name="state">新流程类类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener(int key, Type state, params object[] data)
        {
            if (StateReplace(key, state))
            {
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 替换已有流程监听
        /// </summary>
        /// <typeparam name="T">目标流程类</typeparam>
        /// <param name="key">原状态的流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener<T>(EProcedure key, params object[] data) where T : Procedure, new()
        {
            if (StateReplace<T>(key.GetHashCode()))
            {
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="key">目标(HashCode)流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(int key, params object[] data)
        {
            if (StateChange(key))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,data);
                _listeners[CurrentState].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="key">目标流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(EProcedure key, params object[] data)
        {
            return ChangeProcedure(key.GetHashCode(), data);
        }

        /// <summary>
        /// 退出当前流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentProcedure(params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,data);
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// 切换至上一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool PreviousProcedure(params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Previous != null)
                return ChangeProcedure(node.Previous.Value, data);
            else
                return ExitCurrentProcedure(data);
        }

        /// <summary>
        /// 切换至下一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public bool NextProcedure(params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Next != null)
                return ChangeProcedure(node.Next.Value, data);
            else
                return ExitCurrentProcedure(data);
        }
        
        /// <summary>
        /// 切换至第一个流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ReStartProcedure(params object[] data)
        {
            var node = _procedures.First;
            if (node != null)
                return ChangeProcedure(node.Value, data);
            else
                return ExitCurrentProcedure(data);
        }
    }

    /// <summary>
    /// 枚举流程状态机，调用NextProcedure时，将根据链表的顺序进行流程切换
    /// </summary>
    /// <typeparam name="EProcedure">流程枚举类</typeparam>
    /// <typeparam name="Procedure">流程类</typeparam>
    public abstract class
        AEnumProcedureMachine<EProcedure, Procedure, A> : AbstractEnumProcedureMachine<EProcedure, Procedure>
        where EProcedure : Enum where Procedure : AState<A>
    {
        /// <summary>
        /// 替换已有流程监听
        /// </summary>
        /// <param name="key">原流程的(HashCode)状态类标识</param>
        /// <param name="state">新流程类类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener(int key, Type state, A a, params object[] data)
        {
            if (StateReplace(key, state))
            {
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 替换已有流程监听
        /// </summary>
        /// <typeparam name="T">目标流程类</typeparam>
        /// <param name="key">原状态的流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener<T>(EProcedure key, A a, params object[] data) where T : Procedure, new()
        {
            if (StateReplace<T>(key.GetHashCode()))
            {
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="key">目标(HashCode)流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(int key, A a, params object[] data)
        {
            if (StateChange(key))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,a, data);
                _listeners[CurrentState].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="key">目标流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(EProcedure key, A a, params object[] data)
        {
            return ChangeProcedure(key.GetHashCode(), a, data);
        }

        /// <summary>
        /// 退出当前流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentProcedure(A a, params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,a, data);
                return true;
            }

            return false;
        }
                
        /// <summary>
        /// 切换至上一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool PreviousProcedure(A a,params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Previous != null)
                return ChangeProcedure(node.Previous.Value,a, data);
            else
                return ExitCurrentProcedure(a,data);
        }

        /// <summary>
        /// 切换至下一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public bool NextProcedure(A a, params object[] data)
        {
            var eProcedure = _procedures.FirstOrDefault(eProcedure => eProcedure.GetHashCode() == CurrentState);
            var node = _procedures.Find(eProcedure);
            if (node != null && node.Next != null)
                return ChangeProcedure(node.Next.Value, a, data);
            else
                return ExitCurrentProcedure(a, data);
        }
                
        /// <summary>
        /// 切换至第一个流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ReStartProcedure(A a,params object[] data)
        {
            var node = _procedures.First;
            if (node != null)
                return ChangeProcedure(node.Value,a, data);
            else
                return ExitCurrentProcedure(a,data);
        }
    }

    /// <summary>
    /// 枚举流程状态机，调用NextProcedure时，将根据链表的顺序进行流程切换
    /// </summary>
    /// <typeparam name="EProcedure">流程枚举类</typeparam>
    /// <typeparam name="Procedure">流程类</typeparam>
    public abstract class
        AEnumProcedureMachine<EProcedure, Procedure, A, B> : AbstractEnumProcedureMachine<EProcedure, Procedure>
        where EProcedure : Enum where Procedure : AState<A,B>
    {
        /// <summary>
        /// 替换已有流程监听
        /// </summary>
        /// <param name="key">原流程的(HashCode)状态类标识</param>
        /// <param name="state">新流程类类型</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener(int key, Type state, A a, B b, params object[] data)
        {
            if (StateReplace(key, state))
            {
                _listeners[CurrentState].Enter(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 替换已有流程监听
        /// </summary>
        /// <typeparam name="T">目标流程类</typeparam>
        /// <param name="key">原状态的流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否替换成功</returns>
        public virtual bool ReplaceListener<T>(EProcedure key, A a, B b, params object[] data)
            where T : Procedure, new()
        {
            if (StateReplace<T>(key.GetHashCode()))
            {
                _listeners[CurrentState].Enter(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="key">目标(HashCode)流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(int key, A a, B b, params object[] data)
        {
            if (StateChange(key))
            {
                if (_listeners.ContainsKey(PreviousState))
                    _listeners[PreviousState].Exit(this,a, b, data);
                _listeners[CurrentState].Enter(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换流程
        /// </summary>
        /// <param name="key">目标流程类标识</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ChangeProcedure(EProcedure key, A a, B b, params object[] data)
        {
            return ChangeProcedure(key.GetHashCode(), a, b, data);
        }

        /// <summary>
        /// 退出当前流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentProcedure(A a, B b, params object[] data)
        {
            if (CurrentStateExit())
            {
                _listeners[PreviousState].Exit(this,a, b, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 切换至上一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool PreviousProcedure(A a,B b,params object[] data)
        {
            var node = _procedures.Find(CurrentState);
            if (node != null && node.Previous != null)
                return ChangeProcedure(node.Previous.Value,a,b, data);
            else
                return ExitCurrentProcedure(a,b,data);
        }

        /// <summary>
        /// 切换至下一个流程，流程切换顺序为链表顺序
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public bool NextProcedure(A a, B b, params object[] data)
        {
            var eProcedure = _procedures.FirstOrDefault(eProcedure => eProcedure.GetHashCode() == CurrentState);
            var node = _procedures.Find(eProcedure);
            if (node != null && node.Next != null)
                return ChangeProcedure(node.Next.Value, a, b, data);
            else
                return ExitCurrentProcedure(a, b, data);
        }
                        
        /// <summary>
        /// 切换至第一个流程
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否切换成功</returns>
        public virtual bool ReStartProcedure(A a,B b,params object[] data)
        {
            var node = _procedures.First;
            if (node != null)
                return ChangeProcedure(node.Value,a,b, data);
            else
                return ExitCurrentProcedure(a,b,data);
        }
    }

    #endregion

    public abstract class AbstractFlagsEnumStateMachine<K, V> : AbstractEnumStateMachine<K, V>
        where K : Enum where V : class, IState
    {
        public override bool CanSetStateMachine(int key) => ((key - 1) & key) == 0;

        public virtual bool SingleStateEnter(int key)
        {
            if (!_pauseMachine && CanSetStateMachine(key) && _listeners.ContainsKey(key) && (CurrentState & key) == 0)
            {
                if (!_listeners[key].Condition(this)) return false;
                
                CurrentState |= key;
                OnUpdate += _listeners[key].Update;
                return true;
            }

            return false;
        }

        public virtual bool SingleStateExit(int key)
        {
            if (!_pauseMachine && CanSetStateMachine(key) && _listeners.ContainsKey(key) && (CurrentState & key) != 0)
            {
                CurrentState &= ~key;
                OnUpdate -= _listeners[key].Update;
                return true;
            }

            return false;
        }
    }

    #region [Flags]枚举状态机，支持多状态混合

    /// <summary>
    /// [Flags]枚举状态机，支持多状态混合，需传入带有[Flags]标签的枚举
    /// </summary>
    /// <typeparam name="EState">装载的[Flags]状态枚举类</typeparam>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AFlagsEnumStateMachine<EState, State> : AbstractFlagsEnumStateMachine<EState, State>
        where EState : Enum where State : AState
    {
        /// <summary>
        /// 叠加单个状态，即在当前状态下，叠加一个新状态，新状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="key">叠加的新状态的(int)[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否叠加成功</returns>
        public virtual bool EnterSingleState(int key, params object[] data)
        {
            if (SingleStateEnter(key))
            {
                _listeners[key].Enter(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 叠加单个状态，即在当前状态下，叠加一个新状态，新状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="state">叠加的新状态的[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否叠加成功</returns>
        public virtual bool EnterSingleState(EState state, params object[] data)
        {
            return EnterSingleState(state.GetHashCode(), data);
        }

        /// <summary>
        /// 退出单个状态，即在当前状态下，退出一个原有状态，退出状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="key">退出状态的(HashCode)[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitSingleState(int key, params object[] data)
        {
            if (SingleStateExit(key))
            {
                _listeners[key].Exit(this,data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 退出单个状态，即在当前状态下，退出一个原有状态，退出状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="state">退出状态的[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitSingleState(EState state, params object[] data)
        {
            return ExitSingleState(state.GetHashCode(), data);
        }

        /// <summary>
        /// 改变状态，可以单次叠加或退出多个状态
        /// </summary>
        /// <param name="state">新状态的(HashCode)[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否改变成功</returns>
        public virtual bool ChangeState(int state, params object[] data)
        {
            bool success = true;
            if (!_pauseMachine && state != CurrentState)
            {
                var add = state & (~CurrentState);
                var remove = CurrentState & (~state);
                var i = 0;
                while (add != 0 && i < 32)
                {
                    if ((add & (1 << i)) != 0)
                    {
                        success = success && EnterSingleState(1 << i, data);
                        add -= 1 << i;
                    }

                    i++;
                }

                i = 0;
                while (remove != 0 && i < 32)
                {
                    if ((remove & (1 << i)) != 0)
                    {
                        success = success && ExitSingleState(1 << i, data);
                        remove -= 1 << i;
                    }

                    i++;
                }

                return success;
            }

            return false;
        }

        /// <summary>
        /// 改变状态，可以单次叠加或退出多个状态
        /// </summary>
        /// <param name="key">新状态的[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否改变成功</returns>
        public virtual bool ChangeState(EState key, params object[] data)
        {
            return ChangeState(key.GetHashCode(), data);
        }
        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(params object[] data)
        {
            return ChangeState(0, data);
        }
    }

    /// <summary>
    /// [Flags]枚举状态机，支持多状态混合，需传入带有[Flags]标签的枚举
    /// </summary>
    /// <typeparam name="EState">装载的[Flags]状态枚举类</typeparam>
    /// <typeparam name="State">装载的状态类</typeparam>
    public abstract class AFlagsEnumStateMachine<EState, State, A> : AbstractFlagsEnumStateMachine<EState, State>
        where EState : Enum where State : AState<A>
    {
        /// <summary>
        /// 叠加单个状态，即在当前状态下，叠加一个新状态，新状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="key">叠加的新状态的(int)[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否叠加成功</returns>
        public virtual bool EnterSingleState(int key, A a, params object[] data)
        {
            if (SingleStateEnter(key))
            {
                _listeners[key].Enter(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 叠加单个状态，即在当前状态下，叠加一个新状态，新状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="state">叠加的新状态的[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否叠加成功</returns>
        public virtual bool EnterSingleState(EState state, A a, params object[] data)
        {
            return EnterSingleState(state.GetHashCode(), a, data);
        }

        /// <summary>
        /// 退出单个状态，即在当前状态下，退出一个原有状态，退出状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="key">退出状态的(HashCode)[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitSingleState(int key, A a, params object[] data)
        {
            if (SingleStateExit(key))
            {
                _listeners[key].Exit(this,a, data);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 退出单个状态，即在当前状态下，退出一个原有状态，退出状态的状态枚举值必须为2的次幂
        /// </summary>
        /// <param name="state">退出状态的[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitSingleState(EState state, A a, params object[] data)
        {
            return ExitSingleState(state.GetHashCode(), a, data);
        }

        /// <summary>
        /// 改变状态，可以单次叠加或退出多个状态
        /// </summary>
        /// <param name="state">新状态的(HashCode)[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否改变成功</returns>
        public virtual bool ChangeState(int state, A a, params object[] data)
        {
            bool success = true;
            if (!_pauseMachine && state != CurrentState)
            {
                var add = state & (~CurrentState);
                var remove = CurrentState & (~state);
                var i = 0;
                while (add != 0 && i < 32)
                {
                    if ((add & (1 << i)) != 0)
                    {
                        success = success && EnterSingleState(1 << i, a, data);
                        add -= 1 << i;
                    }

                    i++;
                }

                i = 0;
                while (remove != 0 && i < 32)
                {
                    if ((remove & (1 << i)) != 0)
                    {
                        success = success && ExitSingleState(1 << i, a, data);
                        remove -= 1 << i;
                    }

                    i++;
                }

                return success;
            }

            return false;
        }

        /// <summary>
        /// 改变状态，可以单次叠加或退出多个状态
        /// </summary>
        /// <param name="key">新状态的[Flags]状态枚举</param>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否改变成功</returns>
        public virtual bool ChangeState(EState key, A a, params object[] data)
        {
            return ChangeState(key.GetHashCode(), a, data);
        }
        /// <summary>
        /// 退出当前状态
        /// </summary>
        /// <param name="data">自行添加的参数</param>
        /// <returns>是否退出成功</returns>
        public virtual bool ExitCurrentState(A a, params object[] data)
        {
            return ChangeState(0, a, data);
        }
    }

    #endregion
}*/