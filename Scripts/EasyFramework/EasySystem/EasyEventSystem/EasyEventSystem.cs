using System;
using System.Collections.Generic;
using EasyFramework.EventKit;
using EXFunctionKit;

namespace EasyFramework
{
    public class EasyEventSystem: ASystem
    {
        private readonly Dictionary<EventScope, EasyEventDic> _easyEventScope = new();
        private readonly Dictionary<EventScope, EasyFuncDic> _easyFuncScope = new();
        private readonly Dictionary<EventScope, ClassEvent> _classEventScope = new();
        private readonly Dictionary<EventScope, ClassFunc> _classFuncScope = new();
        
        private readonly Dictionary<Type,IAutoRegisterEvent> _allAutoEvents= new ();

        public override void OnInit()
        {
            _easyEventScope.TryAdd(EventScope.Global, EasyEventDic.Global);
            _easyFuncScope.TryAdd(EventScope.Global, EasyFuncDic.Global);
            _classEventScope.TryAdd(EventScope.Global, ClassEvent.Global);
            _classFuncScope.TryAdd(EventScope.Global, ClassFunc.Global);
            foreach (EventScope scope in Enum.GetValues(typeof(EventScope)))
            {
                if (scope != EventScope.Global && scope != EventScope.All)
                {
                    _easyEventScope.TryAdd(scope, new EasyEventDic());
                    _easyFuncScope.TryAdd(scope, new EasyFuncDic());
                    _classEventScope.TryAdd(scope, new ClassEvent());
                    _classFuncScope.TryAdd(scope, new ClassFunc());
                }
            }
            
            GlobalEvent.GetScopeEasyEventDic<EventScope>.RegisterFunc(scope => _easyEventScope[scope]).UnRegisterOnDispose(this);
            GlobalEvent.GetScopeEasyFuncDic<EventScope>.RegisterFunc(scope => _easyFuncScope[scope]).UnRegisterOnDispose(this);
            GlobalEvent.GetScopeClassEventDic<EventScope>.RegisterFunc(scope => _classEventScope[scope]).UnRegisterOnDispose(this);
            GlobalEvent.GetScopeClassFuncDic<EventScope>.RegisterFunc(scope => _classFuncScope[scope]).UnRegisterOnDispose(this);

            GlobalEvent.RegisterAutoEvent.RegisterEvent(RegisterAutoEvent).UnRegisterOnDispose(this);
        }

        public override void OnDispose()
        {
            _allAutoEvents.Clear();
            _easyEventScope.ForEachValue(dic => dic.ClearAll());
            _easyFuncScope.ForEachValue(dic => dic.ClearAll());
            _classEventScope.ForEachValue(dic => dic.ClearAll());
            _classFuncScope.ForEachValue(dic => dic.ClearAll());
            _easyEventScope.Clear();
            _easyFuncScope.Clear();
            _classEventScope.Clear();
            _classFuncScope.Clear();
        }

        public void RegisterAutoEvent(Type type)
        {
            if (typeof(IAutoRegisterEvent).IsAssignableFrom(type))
            {
                var obj= (IAutoRegisterEvent)Activator.CreateInstance(type);
                _allAutoEvents.Add(type, obj);
                object[] attrs = type.GetCustomAttributes(typeof(EventScopeAttribute), false);
                if (attrs.Length == 0)
                {
                    obj.Register();
                    return;
                }
                
                var attr = (EventScopeAttribute)attrs[0];
                var scopeValue = (long)attr.Scope;
                foreach (EventScope scope in Enum.GetValues(typeof(EventScope)))
                {
                    if (attr.Scope.HasFlag(scope))
                    {
                        obj.Register(scope);
                        scopeValue -= (long)scope;
                        if (scopeValue <= 0)
                            break;
                    }
                }
            }
        }
        
        public void UnregisterAutoEvent(Type type)
        {
            if (_allAutoEvents.ContainsKey(type))
            {
                object[] attrs = type.GetCustomAttributes(typeof(EventScopeAttribute), false);
                if (attrs.Length == 0)
                {
                    _allAutoEvents[type].UnRegister();
                    _allAutoEvents.Remove(type);
                    return;
                }
                var attr = (EventScopeAttribute)attrs[0];
                var scopeValue = (long)attr.Scope;
                foreach (EventScope scope in Enum.GetValues(typeof(EventScope)))
                {
                    if (attr.Scope.HasFlag(scope))
                    {
                        _allAutoEvents[type].UnRegister(scope);
                        scopeValue -= (long)scope;
                        if (scopeValue <= 0)
                            break;
                    }
                }
                _allAutoEvents.Remove(type);
            }
        }

        public void UnregisterAutoEvent(Type type, EventScope scope,params EventScope[] scopes)
        {
            if (_allAutoEvents.TryGetValue(type, out var @event))
            {
                foreach (var value in scopes)
                    scope |= value;
                var scopeValue = (long)scope;
                foreach (EventScope s in Enum.GetValues(typeof(EventScope)))
                {
                    if (scope.HasFlag(s))
                    {
                        @event.UnRegister(s);
                        scopeValue -= (long)s;
                        if (scopeValue <= 0)
                            break;
                    }
                }
            }
        }
    }

    public static class EventSystem
    {
        public static void InvokeEvent<T>() where T : struct =>EasyEventDic.Global.Invoke<T>(default);
        public static void InvokeEvent<T>(T arg) where T : struct => EasyEventDic.Global.Invoke(arg);
        public static IUnRegisterHandle RegisterEvent<T>(Action<T> action) where T : struct => EasyEventDic.Global.Register(action);
        public static void UnregisterEvent<T>(Action<T> action) where T : struct => EasyEventDic.Global.UnRegister(action);
        public static void ClearEvent<T>() where T : struct => EasyEventDic.Global.Clear<T>();


        private static void InvokeEventByScope<T>(T arg,EventScope scope) where T : struct
        {
            if (scope == EventScope.All)
                foreach (var s in Enum.GetValues(typeof(EventScope)))
                    GlobalEvent.GetEasyEventDic(s).Invoke(arg);
            else
                GlobalEvent.GetEasyEventDic(scope).Invoke(arg);
        }
        public static void InvokeEvent<T>(EventScope scope) where T : struct=> InvokeEventByScope<T>(default, scope);
        public static void InvokeEvent<T>(T arg, EventScope scope) where T : struct => InvokeEventByScope(arg, scope);
        public static IUnRegisterHandle RegisterEvent<T>(Action<T> action, EventScope scope) where T : struct => GlobalEvent.GetEasyEventDic(scope).Register(action);
        public static void UnregisterEvent<T>(Action<T> action, EventScope scope) where T : struct => GlobalEvent.GetEasyEventDic(scope).UnRegister(action);
        public static void ClearEvent<T>(EventScope scope) where T : struct => GlobalEvent.GetEasyEventDic(scope).Clear<T>();
        

        public static Results<R> InvokeAll<T, R>() where T : struct => EasyFuncDic.Global.InvokeAndReturnAll<T, R>(default);
        public static Results<R> InvokeAll<T, R>(T arg) where T : struct => EasyFuncDic.Global.InvokeAndReturnAll<T, R>(arg);
        public static R InvokeFunc<T, R>() where T : struct => EasyFuncDic.Global.Invoke<T, R>(default);
        public static R InvokeFunc<T, R>(T arg) where T : struct => EasyFuncDic.Global.Invoke<T, R>(arg);
        public static IUnRegisterHandle RegisterFunc<T, R>(Func<T, R> func) where T : struct => EasyFuncDic.Global.Register(func);
        public static void UnregisterFunc<T, R>(Func<T, R> func) where T : struct => EasyFuncDic.Global.UnRegister(func);
        public static void ClearFunc<T, R>() where T : struct => EasyFuncDic.Global.Clear<T, R>();
        
        
        private static Results<R> InvokeAllByScope<T,R>(T arg,EventScope scope) where T : struct
        {
            if (scope == EventScope.All)
            {
                var resultLst = new List<Results<R>>();
                foreach (var s in Enum.GetValues(typeof(EventScope)))
                {
                    var results = GlobalEvent.GetEasyFuncDic(s).InvokeAndReturnAll<T, R>(arg);
                    if (results.Count > 0)
                        resultLst.Add(results);
                }
                return new Results<R>(resultLst.ToArray());
            }

            return GlobalEvent.GetEasyFuncDic(scope).InvokeAndReturnAll<T, R>(arg);
        }

        private static R InvokeFuncByScope<T, R>(T arg, EventScope scope) where T : struct
        {
            if (scope == EventScope.All)
            {
                Func<T, R> func = null;
                foreach (var s in Enum.GetValues(typeof(EventScope)))
                    func += GlobalEvent.GetEasyFuncDic(s).Invoke<T,R>;
                if(func==null)
                    return default;
                return func(arg);
            }

            return GlobalEvent.GetEasyFuncDic(scope).Invoke<T,R>(arg);
        }
        public static Results<R> InvokeAll<T, R>(EventScope scope) where T : struct => InvokeAllByScope<T,R>(default, scope);
        public static Results<R> InvokeAll<T, R>(T arg, EventScope scope) where T : struct => InvokeAllByScope<T, R>(arg,scope);
        public static R InvokeFunc<T, R>(EventScope scope) where T : struct => InvokeFuncByScope<T, R>(default, scope);
        public static R InvokeFunc<T, R>(T arg, EventScope scope) where T : struct => InvokeFuncByScope<T, R>(arg, scope);
        public static IUnRegisterHandle RegisterFunc<T, R>(Func<T, R> func, EventScope scope) where T : struct => GlobalEvent.GetEasyFuncDic(scope).Register(func);
        public static void UnregisterFunc<T, R>(Func<T, R> func, EventScope scope) where T : struct => GlobalEvent.GetEasyFuncDic(scope).UnRegister(func);
        public static void ClearFunc<T, R>(EventScope scope) where T : struct => GlobalEvent.GetEasyFuncDic(scope).Clear<T, R>();
        
        
        public static Results<IResult> InvokeAll<T>() where T : struct => EasyFuncDic.Global.InvokeAndReturnAll<T>(default);
        public static Results<IResult> InvokeAll<T>(T arg) where T : struct => EasyFuncDic.Global.InvokeAndReturnAll(arg);
        public static IResult InvokeFunc<T>() where T : struct => EasyFuncDic.Global.Invoke<T>(default);
        public static IResult InvokeFunc<T>(T arg) where T : struct => EasyFuncDic.Global.Invoke(arg);
        public static IUnRegisterHandle RegisterFunc<T>(Func<T, IResult> func) where T : struct => EasyFuncDic.Global.Register(func);
        public static void UnregisterFunc<T>(Func<T, IResult> func) where T : struct => EasyFuncDic.Global.UnRegister(func);
        public static void ClearFunc<T>() where T : struct => EasyFuncDic.Global.Clear<T>();
        
        
        public static Results<IResult> InvokeAll<T>(EventScope scope) where T : struct => InvokeAllByScope<T,IResult>(default,scope);
        public static Results<IResult> InvokeAll<T>(T arg, EventScope scope) where T : struct => InvokeAllByScope<T,IResult>(arg,scope);
        public static IResult InvokeFunc<T>(EventScope scope) where T : struct => InvokeFuncByScope<T, IResult>(default, scope);
        public static IResult InvokeFunc<T>(T arg, EventScope scope) where T : struct => InvokeFuncByScope<T, IResult>(arg, scope);
        public static IUnRegisterHandle RegisterFunc<T>(Func<T, IResult> func, EventScope scope) where T : struct => GlobalEvent.GetEasyFuncDic(scope).Register(func);
        public static void UnregisterFunc<T>(Func<T, IResult> func, EventScope scope) where T : struct => GlobalEvent.GetEasyFuncDic(scope).UnRegister(func);
        public static void ClearFunc<T>(EventScope scope) where T : struct => GlobalEvent.GetEasyFuncDic(scope).Clear<T>();
    }
}