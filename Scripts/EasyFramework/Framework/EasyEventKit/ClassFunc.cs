using System;
using System.Collections.Generic;

namespace EasyFramework.EventKit
{
    #region AFuncIndex
    
    public interface IFuncIndex{}
    public abstract class IFuncIndex<T>:IFuncIndex where T : IFuncIndex<T>
    {
        public static void ClearEvent() => ClassFunc.Global.Clear<T>();
        public static void ClearEvent(ClassFunc E) => E.Clear<T>();
    }
    public abstract class IFuncIndex<T,R>:IFuncIndex<T> where T : IFuncIndex<T,R>{}
    public abstract class AFuncIndex<T, R> : IFuncIndex<T,R> where T : AFuncIndex<T, R>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => ClassFunc.Global.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<R> a) => f.Register<R, T>(a);
        public static void UnRegisterFunc(Func<R> a) => ClassFunc.Global.UnRegister<R, T>(a);
        public static void UnRegisterFunc(ClassFunc f, Func<R> a) => f.UnRegister<R, T>(a);
        public static Results<R> InvokeAndReturnAll() => ClassFunc.Global.InvokeAndReturnAll<R, T>();
        public static Results<R> InvokeAndReturnAll(ClassFunc f) => f.InvokeAndReturnAll<R, T>();
        public static R InvokeFunc() => ClassFunc.Global.Invoke<R, T>();
        public static R InvokeFunc(ClassFunc f) => f.Invoke<R, T>();

    }

    public abstract class AFuncIndex<T, R, T1> : IFuncIndex<T,R> where T : AFuncIndex<T, R, T1>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => ClassFunc.Global.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<R> a) => f.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, R> a) => ClassFunc.Global.Register<R, T, T1>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<T1, R> a) => f.Register<R, T, T1>(a);
        public static void UnRegisterFunc(Func<T1, R> a) => ClassFunc.Global.UnRegister<R, T, T1>(a);
        public static void UnRegisterFunc(ClassFunc f, Func<T1, R> a) => f.UnRegister<R, T, T1>(a);
        public static Results<R> InvokeAndReturnAll(T1 t1) => ClassFunc.Global.InvokeAndReturnAll<R, T, T1>(t1);
        public static Results<R> InvokeAndReturnAll(ClassFunc F, T1 t1) => F.InvokeAndReturnAll<R, T, T1>(t1);
        public static R InvokeFunc(T1 t1) => ClassFunc.Global.Invoke<R, T, T1>(t1);
        public static R InvokeFunc(ClassFunc f, T1 t1) => f.Invoke<R, T, T1>(t1);
    }

    public abstract class AFuncIndex<T, R, T1, T2>:IFuncIndex<T,R> where T : AFuncIndex<T, R, T1, T2>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => ClassFunc.Global.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<R> a) => f.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, R> a) => ClassFunc.Global.Register<R, T, T1, T2>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<T1, T2, R> a) => f.Register<R, T, T1, T2>(a);
        public static void UnRegisterFunc(Func<T1, T2, R> a) => ClassFunc.Global.UnRegister<R, T, T1, T2>(a);
        public static void UnRegisterFunc(ClassFunc f, Func<T1, T2, R> a) => f.UnRegister<R, T, T1, T2>(a);
        public static Results<R> InvokeAndReturnAll(T1 t1, T2 t2) => ClassFunc.Global.InvokeAndReturnAll<R, T, T1, T2>(t1, t2);
        public static Results<R> InvokeAndReturnAll(ClassFunc f, T1 t1, T2 t2) => f.InvokeAndReturnAll<R, T, T1, T2>(t1, t2);
        public static R InvokeFunc(T1 t1, T2 t2) => ClassFunc.Global.Invoke<R, T, T1, T2>(t1, t2);
        public static R InvokeFunc(ClassFunc f, T1 t1, T2 t2) => f.Invoke<R, T, T1, T2>(t1, t2);
    }
    public abstract class AFuncIndex<T, R, T1, T2, T3>:IFuncIndex<T,R> where T : AFuncIndex<T, R, T1, T2, T3>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => ClassFunc.Global.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<R> a) => f.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, T3, R> a) => ClassFunc.Global.Register<R, T, T1, T2, T3>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<T1, T2, T3, R> a) => f.Register<R, T, T1, T2, T3>(a);
        public static void UnRegisterFunc(Func<T1, T2, T3, R> a) => ClassFunc.Global.UnRegister<R, T, T1, T2, T3>(a);
        public static void UnRegisterFunc(ClassFunc f, Func<T1, T2, T3, R> a) => f.UnRegister<R, T, T1, T2, T3>(a);  
        public static Results<R> InvokeAndReturnAll(T1 t1, T2 t2, T3 t3) => ClassFunc.Global.InvokeAndReturnAll<R, T, T1, T2, T3>(t1, t2, t3);
        public static Results<R> InvokeAndReturnAll(ClassFunc f, T1 t1, T2 t2, T3 t3) => f.InvokeAndReturnAll<R, T, T1, T2, T3>(t1, t2, t3);
        public static R InvokeFunc(T1 t1, T2 t2, T3 t3) => ClassFunc.Global.Invoke<R, T, T1, T2, T3>(t1, t2, t3);
        public static R InvokeFunc(ClassFunc f, T1 t1, T2 t2, T3 t3) => f.Invoke<R, T, T1, T2, T3>(t1, t2, t3);
    }
    public abstract class AFuncIndex<T, R, T1, T2, T3, T4>:IFuncIndex<T,R> where T : AFuncIndex<T, R, T1, T2, T3, T4>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => ClassFunc.Global.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<R> a) => f.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, T3, T4, R> a) => ClassFunc.Global.Register<R, T, T1, T2, T3, T4>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<T1, T2, T3, T4, R> a) => f.Register<R, T, T1, T2, T3, T4>(a);
        public static void UnRegisterFunc(Func<T1, T2, T3, T4, R> a) => ClassFunc.Global.UnRegister<R, T, T1, T2, T3, T4>(a);
        public static void UnRegisterFunc(ClassFunc f, Func<T1, T2, T3, T4, R> a) => f.UnRegister<R, T, T1, T2, T3, T4>(a);
        public static Results<R> InvokeAndReturnAll(T1 t1, T2 t2, T3 t3, T4 t4) => ClassFunc.Global.InvokeAndReturnAll<R, T, T1, T2, T3, T4>(t1, t2, t3, t4);
        public static Results<R> InvokeAndReturnAll(ClassFunc f, T1 t1, T2 t2, T3 t3, T4 t4) => f.InvokeAndReturnAll<R, T, T1, T2, T3, T4>(t1, t2, t3, t4);
        public static R InvokeFunc(T1 t1, T2 t2, T3 t3, T4 t4) => ClassFunc.Global.Invoke<R, T, T1, T2, T3, T4>(t1, t2, t3, t4);
        public static R InvokeFunc(ClassFunc f, T1 t1, T2 t2, T3 t3, T4 t4) => f.Invoke<R, T, T1, T2, T3, T4>(t1, t2, t3, t4);
    }
    public abstract class AFuncIndex<T, R, T1, T2, T3, T4, T5>:IFuncIndex<T,R> where T : AFuncIndex<T, R, T1, T2, T3, T4, T5>
    {
        public static IUnRegisterHandle RegisterFunc(Func<R> a) => ClassFunc.Global.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<R> a) => f.Register<R, T>(a);
        public static IUnRegisterHandle RegisterFunc(Func<T1, T2, T3, T4, T5, R> a) => ClassFunc.Global.Register<R, T, T1, T2, T3, T4, T5>(a);
        public static IUnRegisterHandle RegisterFunc(ClassFunc f, Func<T1, T2, T3, T4, T5, R> a) => f.Register<R, T, T1, T2, T3, T4, T5>(a);
        public static void UnRegisterFunc(Func<T1, T2, T3, T4, T5, R> a) => ClassFunc.Global.UnRegister<R, T, T1, T2, T3, T4, T5>(a);
        public static void UnRegisterFunc(ClassFunc f, Func<T1, T2, T3, T4, T5, R> a) => f.UnRegister<R, T, T1, T2, T3, T4, T5>(a);
        public static Results<R> InvokeAndReturnAll(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => ClassFunc.Global.InvokeAndReturnAll<R, T, T1, T2, T3, T4, T5>(t1, t2, t3, t4, t5);
        public static Results<R> InvokeAndReturnAll(ClassFunc f, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => f.InvokeAndReturnAll<R, T, T1, T2, T3, T4, T5>(t1, t2, t3, t4, t5);
        public static R InvokeFunc(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => ClassFunc.Global.Invoke<R, T, T1, T2, T3, T4, T5>(t1, t2, t3, t4, t5);
        public static R InvokeFunc(ClassFunc f, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => f.Invoke<R, T, T1, T2, T3, T4, T5>(t1, t2, t3, t4, t5);
    }

    #endregion

    public class ClassFunc
    {
        public static ClassFunc Global = new();
        protected Dictionary<Type, IEasyFunc> FuncDic = new();

        public IUnRegisterHandle Register<R, T>(Func<R> a) where T : IFuncIndex<T, R>
        {
            if (!FuncDic.ContainsKey(typeof(T))) { FuncDic.Add(typeof(T), new EasyFunc<R>()); }

            return ((IEasyFunc<R>)FuncDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<R, T, T1>(Func<T1, R> a) where T : AFuncIndex<T, R, T1>
        {
            if (!FuncDic.ContainsKey(typeof(T))) { FuncDic.Add(typeof(T), new EasyFunc<R, T1>()); }

            return ((EasyFunc<R, T1>)FuncDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<R, T, T1, T2>(Func<T1, T2, R> a) where T : AFuncIndex<T, R, T1, T2>
        {
            if (!FuncDic.ContainsKey(typeof(T))) { FuncDic.Add(typeof(T), new EasyFunc<R, T1, T2>()); }

            return ((EasyFunc<R, T1, T2>)FuncDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<R, T, T1, T2, T3>(Func<T1, T2, T3, R> a) where T : AFuncIndex<T, R, T1, T2, T3>
        {
            if (!FuncDic.ContainsKey(typeof(T))) { FuncDic.Add(typeof(T), new EasyFunc<R, T1, T2, T3>()); }

            return ((EasyFunc<R, T1, T2, T3>)FuncDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<R, T, T1, T2, T3, T4>(Func<T1, T2, T3, T4, R> a) where T : AFuncIndex<T, R, T1, T2, T3, T4>
        {
            if (!FuncDic.ContainsKey(typeof(T))) { FuncDic.Add(typeof(T), new EasyFunc<R, T1, T2, T3, T4>()); }

            return ((EasyFunc<R, T1, T2, T3, T4>)FuncDic[typeof(T)]).Register(a);
        }

        public IUnRegisterHandle Register<R, T, T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, R> a) where T : AFuncIndex<T, R, T1, T2, T3, T4, T5>
        {
            if (!FuncDic.ContainsKey(typeof(T))) { FuncDic.Add(typeof(T), new EasyFunc<R, T1, T2, T3, T4, T5>()); }

            return ((EasyFunc<R, T1, T2, T3, T4, T5>)FuncDic[typeof(T)]).Register(a);
        }

        public void UnRegister<R, T>(Func<R> a) where T : IFuncIndex<T, R>
        {
            if (FuncDic.ContainsKey(typeof(T))) { ((IEasyFunc<R>)FuncDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<R, T, T1>(Func<T1, R> a) where T : AFuncIndex<T, R, T1>
        {
            if (FuncDic.ContainsKey(typeof(T))) { ((EasyFunc<R, T1>)FuncDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<R, T, T1, T2>(Func<T1, T2, R> a) where T : AFuncIndex<T, R, T1, T2>
        {
            if (FuncDic.ContainsKey(typeof(T))) { ((EasyFunc<R, T1, T2>)FuncDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<R, T, T1, T2, T3>(Func<T1, T2, T3, R> a) where T : AFuncIndex<T, R, T1, T2, T3>
        {
            if (FuncDic.ContainsKey(typeof(T))) { ((EasyFunc<R, T1, T2, T3>)FuncDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<R, T, T1, T2, T3, T4>(Func<T1, T2, T3, T4, R> a) where T : AFuncIndex<T, R, T1, T2, T3, T4>
        {
            if (FuncDic.ContainsKey(typeof(T))) { ((EasyFunc<R, T1, T2, T3, T4>)FuncDic[typeof(T)]).UnRegister(a); }
        }

        public void UnRegister<R, T, T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, R> a) where T : AFuncIndex<T, R, T1, T2, T3, T4, T5>
        {
            if (FuncDic.ContainsKey(typeof(T)))
            {
                ((EasyFunc<R, T1, T2, T3, T4, T5>)FuncDic[typeof(T)]).UnRegister(a);
            }
        }

        public Results<R> InvokeAndReturnAll<R, T>() where T : AFuncIndex<T, R>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R>)FuncDic[typeof(T)]).InvokeAndReturnAll();
        }

        public Results<R> InvokeAndReturnAll<R, T, T1>(T1 t1) where T : AFuncIndex<T, R, T1>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1>)FuncDic[typeof(T)]).InvokeAndReturnAll(t1);
        }

        public Results<R> InvokeAndReturnAll<R, T, T1, T2>(T1 t1, T2 t2) where T : AFuncIndex<T, R, T1, T2>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2>)FuncDic[typeof(T)]).InvokeAndReturnAll(t1, t2);
        }

        public Results<R> InvokeAndReturnAll<R, T, T1, T2, T3>(T1 t1, T2 t2, T3 t3) where T : AFuncIndex<T, R, T1, T2, T3>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2, T3>)FuncDic[typeof(T)]).InvokeAndReturnAll(t1, t2, t3);
        }

        public Results<R> InvokeAndReturnAll<R, T, T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4) where T : AFuncIndex<T, R, T1, T2, T3, T4>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2, T3, T4>)FuncDic[typeof(T)]).InvokeAndReturnAll(t1, t2, t3, t4);
        }

        public Results<R> InvokeAndReturnAll<R, T, T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) where T : AFuncIndex<T, R, T1, T2, T3, T4, T5>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2, T3, T4, T5>)FuncDic[typeof(T)]).InvokeAndReturnAll(t1, t2, t3, t4, t5);
        }

        public R Invoke<R, T>() where T : AFuncIndex<T, R>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R>)FuncDic[typeof(T)]).Invoke();
        }

        public R Invoke<R, T, T1>(T1 t1) where T : AFuncIndex<T, R, T1>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1>)FuncDic[typeof(T)]).Invoke(t1);
        }

        public R Invoke<R, T, T1, T2>(T1 t1, T2 t2) where T : AFuncIndex<T, R, T1, T2>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2>)FuncDic[typeof(T)]).Invoke(t1, t2);
        }

        public R Invoke<R, T, T1, T2, T3>(T1 t1, T2 t2, T3 t3) where T : AFuncIndex<T, R, T1, T2, T3>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2, T3>)FuncDic[typeof(T)]).Invoke(t1, t2, t3);
        }

        public R Invoke<R, T, T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4) where T : AFuncIndex<T, R, T1, T2, T3, T4>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2, T3, T4>)FuncDic[typeof(T)]).Invoke(t1, t2, t3, t4);
        }

        public R Invoke<R, T, T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) where T : AFuncIndex<T, R, T1, T2, T3, T4, T5>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return default;
            return ((EasyFunc<R, T1, T2, T3, T4, T5>)FuncDic[typeof(T)]).Invoke(t1, t2, t3, t4, t5);
        }

        public void Clear<T>() where T : IFuncIndex<T>
        {
            if (!FuncDic.ContainsKey(typeof(T)))
                return;
            FuncDic[typeof(T)].Clear();
        }
        public void ClearAll()
        {
            foreach (var item in FuncDic)
            {
                item.Value.Clear();
            }
            FuncDic.Clear();
        }
    }
}
