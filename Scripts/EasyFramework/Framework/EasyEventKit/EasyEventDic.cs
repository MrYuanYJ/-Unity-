using System;
using System.Collections.Generic;

namespace EasyFramework.EventKit
{
    public interface IGetEasyEventDic
    {
        public EasyEventDic EventDic { get; }
    }

    public interface IEasyEventDic
    {
        protected Dictionary<Type, IEasyEvent> Events { get; }

        public bool TryGet<T>(out IEasyEvent easyEvent)
        {
            return Events.TryGetValue(typeof(T), out easyEvent);
        }

        public EasyEvent GetOrAddEvent<T>()
        {
            if (Events.TryGetValue(typeof(T), out var @event)) 
                return (EasyEvent)@event;
            var easyEvent = new EasyEvent();
            Events.Add(typeof(T), easyEvent);

            return easyEvent;
        }
        public EasyEvent<T1> GetOrAddEvent<T, T1>()
        {
            if (Events.TryGetValue(typeof(T), out var @event))
                return (EasyEvent<T1>) @event;
            var easyEvent = new EasyEvent<T1>();
            Events.Add(typeof(T), easyEvent);
            return easyEvent;
        }
        public EasyEvent<T1, T2> GetOrAddEvent<T, T1, T2>()
        {
            if (Events.TryGetValue(typeof(T), out var @event))
                return (EasyEvent<T1, T2>) @event;
            var easyEvent = new EasyEvent<T1, T2>();
            Events.Add(typeof(T), easyEvent);
            return easyEvent;
        }
        public EasyEvent<T1, T2, T3> GetOrAddEvent<T, T1, T2, T3>()
        {
            if (Events.TryGetValue(typeof(T), out var @event))
                return (EasyEvent<T1, T2, T3>) @event;
            var easyEvent = new EasyEvent<T1, T2, T3>();
            Events.Add(typeof(T), easyEvent);
            return easyEvent;
        }
        public EasyEvent<T1, T2, T3, T4> GetOrAddEvent<T, T1, T2, T3, T4>()
        {
            if (Events.TryGetValue(typeof(T), out var @event))
                return (EasyEvent<T1, T2, T3, T4>) @event;
            var easyEvent = new EasyEvent<T1, T2, T3, T4>();
            Events.Add(typeof(T), easyEvent);
            return easyEvent;
        }
        public EasyEvent<T1, T2, T3, T4, T5> GetOrAddEvent<T, T1, T2, T3, T4, T5>()
        {
            if (Events.TryGetValue(typeof(T), out var @event))
                return (EasyEvent<T1, T2, T3, T4, T5>) @event;
            var easyEvent = new EasyEvent<T1, T2, T3, T4, T5>();
            Events.Add(typeof(T), easyEvent);
            return easyEvent;
        }

        public void Clear<T>()
        {
            if (Events.TryGetValue(typeof(T), out var @event))
                @event.Clear();
        }

        public void ClearAll()
        {
            foreach (var value in Events.Values)
            {
                value.Clear();
            }
        }
    }
    public class EasyEventDic: IEasyEventDic,IGetEasyEventDic
    {
        public static EasyEventDic Global => Instance.Value;
        private static readonly Lazy<EasyEventDic> Instance=new Lazy<EasyEventDic>(()=>new EasyEventDic());
        private readonly Dictionary<Type, IEasyEvent> _events = new();
        
        public EasyEvent<T> GetOrAddEasyEvent<T>() where T : struct
        {
            if (_events.TryGetValue(typeof(T), out var @event))
                return (EasyEvent<T>) @event;
            var easyEvent = new EasyEvent<T>();
            _events.Add(typeof(T), easyEvent);
            return easyEvent;
        }

        public IUnRegisterHandle Register<T>(Action action) where T : struct => GetOrAddEasyEvent<T>().Register(action);
        public IUnRegisterHandle Register<T>(Action<T> action) where T : struct=> GetOrAddEasyEvent<T>().Register(action);
        public void UnRegister<T>(Action action) where T : struct=> GetOrAddEasyEvent<T>().UnRegister(action);
        public void UnRegister<T>(Action<T> action) where T : struct=>GetOrAddEasyEvent<T>().UnRegister(action);

        public void Invoke<T>(T t) where T : struct=>  GetOrAddEasyEvent<T>().Invoke(t);

        public EasyEventDic EventDic => this;
        Dictionary<Type, IEasyEvent> IEasyEventDic.Events => _events;
    }
    public interface IGetEasyFuncDic
    {
        public EasyFuncDic FuncDic { get; }
    }

    public interface IEasyFuncDic
    {
        protected Dictionary<Type, IEasyFunc> Funcs { get; }

        public bool TryGet<T>(out IEasyFunc easyFunc)
        {
            return Funcs.TryGetValue(typeof(T), out easyFunc);
        }

        public EasyFunc<TReturn> GetOrAddFunc<T, TReturn>()
        {
            if (Funcs.TryGetValue(typeof(T), out var func))
                try { return (EasyFunc<TReturn>)func; }
                catch (Exception)
                {
                    throw new Exception(
                        $"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                }

            var easyFunc = new EasyFunc<TReturn>();
            Funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        public EasyFunc<T1, TReturn> GetOrAddFunc<T, T1, TReturn>()
        {
            if (Funcs.TryGetValue(typeof(T), out var func))
                try
                {
                    return (EasyFunc<T1, TReturn>) func;
                }
                catch (Exception)
                {
                    throw new Exception(
                        $"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                }

            var easyFunc = new EasyFunc<T1, TReturn>();
            Funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        public EasyFunc<T1, T2, TReturn> GetOrAddFunc<T, T1, T2, TReturn>()
        {
            if (Funcs.TryGetValue(typeof(T), out var func))
                try { return (EasyFunc<T1, T2, TReturn>)func; }
                catch (Exception)
                {
                    throw new Exception(
                        $"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                }

            var easyFunc = new EasyFunc<T1, T2, TReturn>();
            Funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        public EasyFunc<T1, T2, T3, TReturn> GetOrAddFunc<T, T1, T2, T3, TReturn>()
        {
            if (Funcs.TryGetValue(typeof(T), out var func))
                try { return (EasyFunc<T1, T2, T3, TReturn>)func; }
                catch (Exception)
                {
                    throw new Exception(
                        $"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                }

            var easyFunc = new EasyFunc<T1, T2, T3, TReturn>();
            Funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        public EasyFunc<T1, T2, T3, T4, TReturn> GetOrAddFunc<T, T1, T2, T3, T4, TReturn>()
        {
            if (Funcs.TryGetValue(typeof(T), out var func))
                try { return (EasyFunc<T1, T2, T3, T4, TReturn>)func; }
                catch (Exception)
                {
                    throw new Exception(
                        $"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                }

            var easyFunc = new EasyFunc<T1, T2, T3, T4, TReturn>();
            Funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        public EasyFunc<T1, T2, T3, T4, T5, TReturn> GetOrAddFunc<T, T1, T2, T3, T4, T5, TReturn>()
        {
            if (Funcs.TryGetValue(typeof(T), out var func))
                try { return (EasyFunc<T1, T2, T3, T4, T5, TReturn>)func; }
                catch (Exception)
                {
                    throw new Exception(
                        $"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                }

            var easyFunc = new EasyFunc<T1, T2, T3, T4, T5, TReturn>();
            Funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }

        public void Clear<T>()
        {
            if (Funcs.TryGetValue(typeof(T), out var func))
                func.Clear();
        }
        public void ClearAll()
        {
            foreach (var value in Funcs.Values) { value.Clear(); }
        }
    }

    public class EasyFuncDic: IEasyFuncDic,IGetEasyFuncDic
    {
        public static EasyFuncDic Global => Instance.Value;
        private static readonly Lazy<EasyFuncDic> Instance = new Lazy<EasyFuncDic>(() => new EasyFuncDic());
        private readonly Dictionary<Type, IEasyFunc> _funcs = new();
        public EasyFunc<T,TReturn> GetOrAddEasyFunc<T,TReturn>() where T : struct
        {
            if (_funcs.TryGetValue(typeof(T), out var func))
                try
                {
                    return (EasyFunc<T,TReturn>) func;
                }
                catch (Exception)
                {
                    throw new Exception($"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                } 
            var easyFunc = new EasyFunc<T,TReturn>();
            _funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        public EasyFunc<T,IResult> GetOrAddEasyFunc<T>() where T : struct
        {
            if (_funcs.TryGetValue(typeof(T), out var func))
                return (EasyFunc<T,IResult>) func;
            var easyFunc = new EasyFunc<T,IResult>();
            _funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        
        public IUnRegisterHandle Register<T,TReturn>(Func<TReturn> func) where T : struct=>GetOrAddEasyFunc<T,TReturn>().Register(func);
        public IUnRegisterHandle Register<T,TReturn>(Func<T, TReturn> func) where T : struct=>GetOrAddEasyFunc<T,TReturn>().Register(func);
        public IUnRegisterHandle Register<T>(Func<IResult> func) where T : struct=>GetOrAddEasyFunc<T>().Register(func);
        public IUnRegisterHandle Register<T>(Func<T, IResult> func) where T : struct=>GetOrAddEasyFunc<T>().Register(func);
        
        public void UnRegister<T,TReturn>(Func<TReturn> func) where T : struct=>GetOrAddEasyFunc<T,TReturn>().UnRegister(func);
        public void UnRegister<T,TReturn>(Func<T, TReturn> func) where T : struct=>GetOrAddEasyFunc<T,TReturn>().UnRegister(func);
        public void UnRegister<T>(Func<IResult> func) where T : struct=>GetOrAddEasyFunc<T>().UnRegister(func);
        public void UnRegister<T>(Func<T, IResult> func) where T : struct=>GetOrAddEasyFunc<T>().UnRegister(func);
        
        public TReturn[] InvokeAndReturnAll<T,TReturn>(T t) where T : struct => GetOrAddEasyFunc<T,TReturn>().InvokeAndReturnAll(t);
        public IResult[] InvokeAndReturnAll<T>(T t) where T : struct => GetOrAddEasyFunc<T>().InvokeAndReturnAll(t);
        public TReturn Invoke<T,TReturn>(T t) where T : struct => GetOrAddEasyFunc<T,TReturn>().Invoke(t);
        public IResult Invoke<T>(T t) where T : struct => GetOrAddEasyFunc<T>().Invoke(t);

        public EasyFuncDic FuncDic => this;
        Dictionary<Type, IEasyFunc> IEasyFuncDic.Funcs => _funcs;
    }
}