using System;
using System.Collections.Generic;

namespace EasyFramework.EventKit
{
    public class EasyEventDic
    {
        public static EasyEventDic Global => instance.Value;
        private static readonly Lazy<EasyEventDic> instance=new Lazy<EasyEventDic>(()=>new EasyEventDic());
        private readonly Dictionary<Type, IEasyEvent> _events = new();

        public EasyEvent<T> GetEvent<T>() where T : struct
        {
            if (_events.TryGetValue(typeof(T), out var @event))
                return (EasyEvent<T>) @event;
            var easyEvent = new EasyEvent<T>();
            _events.Add(typeof(T), easyEvent);
            return easyEvent;
        }

        public IUnRegisterHandle Register<T>(Action action) where T : struct => GetEvent<T>().Register(action);
        public IUnRegisterHandle Register<T>(Action<T> action) where T : struct=> GetEvent<T>().Register(action);
        public void UnRegister<T>(Action action) where T : struct=> GetEvent<T>().UnRegister(action);
        public void UnRegister<T>(Action<T> action) where T : struct=>GetEvent<T>().UnRegister(action);

        public void Invoke<T>(T t) where T : struct=>  GetEvent<T>().Invoke(t);
        public void Clear<T>() where T : struct=> GetEvent<T>().Clear();
        public void ClearAll()
        {
            foreach (var value in _events.Values)
            {
                value.Clear();
            }
        }
    }
    public class EasyFuncDic
    {
        public static EasyFuncDic Global => instance.Value;
        private static readonly Lazy<EasyFuncDic> instance = new Lazy<EasyFuncDic>(() => new EasyFuncDic());
        private readonly Dictionary<Type, IEasyFunc> _funcs = new();

        public EasyFunc<TReturn,T> GetFunc<T,TReturn>() where T : struct
        {
            if (_funcs.TryGetValue(typeof(T), out var func))
                try
                {
                    return (EasyFunc<TReturn, T>) func;
                }
                catch (Exception)
                {
                    throw new Exception($"[{typeof(T).Name}]已经注册过返回值类型为[{func.ReturnType.Name}]的函数,无法注册或者获取返回值类型为[{typeof(TReturn).Name}]的函数");
                } 
            var easyFunc = new EasyFunc<TReturn,T>();
            _funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        public EasyFunc<IResult,T> GetFunc<T>() where T : struct
        {
            if (_funcs.TryGetValue(typeof(T), out var func))
                return (EasyFunc<IResult, T>) func;
            var easyFunc = new EasyFunc<IResult,T>();
            _funcs.Add(typeof(T), easyFunc);
            return easyFunc;
        }
        
        public IUnRegisterHandle Register<T,TReturn>(Func<TReturn> func) where T : struct=>GetFunc<T,TReturn>().Register(func);
        public IUnRegisterHandle Register<T,TReturn>(Func<T, TReturn> func) where T : struct=>GetFunc<T,TReturn>().Register(func);
        public IUnRegisterHandle Register<T>(Func<IResult> func) where T : struct=>GetFunc<T>().Register(func);
        public IUnRegisterHandle Register<T>(Func<T, IResult> func) where T : struct=>GetFunc<T>().Register(func);
        
        public void UnRegister<T,TReturn>(Func<TReturn> func) where T : struct=>GetFunc<T,TReturn>().UnRegister(func);
        public void UnRegister<T,TReturn>(Func<T, TReturn> func) where T : struct=>GetFunc<T,TReturn>().UnRegister(func);
        public void UnRegister<T>(Func<IResult> func) where T : struct=>GetFunc<T>().UnRegister(func);
        public void UnRegister<T>(Func<T, IResult> func) where T : struct=>GetFunc<T>().UnRegister(func);
        
        public Results<TReturn> InvokeAndReturnAll<T,TReturn>(T t) where T : struct => GetFunc<T,TReturn>().InvokeAndReturnAll(t);
        public Results<IResult> InvokeAndReturnAll<T>(T t) where T : struct => GetFunc<T>().InvokeAndReturnAll(t);
        public TReturn Invoke<T,TReturn>(T t) where T : struct => GetFunc<T,TReturn>().Invoke(t);
        public IResult Invoke<T>(T t) where T : struct => GetFunc<T>().Invoke(t);
        public void Clear<T,TReturn>() where T : struct =>GetFunc<T,TReturn>().Clear();
        public void Clear<T>() where T : struct => GetFunc<T>().Clear();
        public void ClearAll()
        {
            foreach (var value in _funcs.Values)
            {
                value.Clear();
            }
        }
    }
}