using System;
using System.Threading;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public interface IEasyLife : IInitAble, IDisposeAble
    {
        bool IDisposeAble.IsDispose
        {
            get => !IsInit;
            set => IsInit = !value;
        }
    }

    public interface IEasyLife<T> : IInitAble<T>,IEasyLife
    {
    }

    public interface IEasyLife<T1, T2> : IInitAble<T1, T2>, IEasyLife
    {
    }

   


    public static class EasyLifeExtensions
    {
        public static void TryInit(this object self)
        {
            if(self is IInitAble easyLife)
                easyLife.Init();
        }
        public static void TryInit<T>(this object self, T t)
        {
            if (self is IInitAble<T> easyLife)
                easyLife.Init(t);
        }
        public static void TryInit<T1, T2>(this object self, T1 t1, T2 t2)
        {
            if (self is IInitAble<T1, T2> easyLife)
                easyLife.Init(t1, t2);
        }
        public static void TryDispose(this object self)
        {
            if (self is IDisposeAble easyLife)
                easyLife.Dispose();
        }

        public static void Init(this IEasyLife self)=> self.Init();
        public static void Dispose(this IEasyLife self)=> self.Dispose();
        public static void Dispose(this IEasyLife self,bool usePool)=> self.Dispose(usePool);
    }

    public static class EasyLifeCycleExtensions
    {
        public static T DisposeWith<T>(this T self, IDisposeAble disposable) where T : IDisposeAble
        {
            disposable.DisposeEvent.Register(self.Dispose).OnlyPlayOnce();
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnDispose(this IUnRegisterHandle self, IDisposeAble easyLife)
        {
            easyLife.DisposeEvent.Register(self.UnRegister).OnlyPlayOnce();
            return self;
        }
        public static IUnRegisterHandle RegisterOnDispose(this IDisposeAble self, Action action) => self.DisposeEvent.Register(action).OnlyPlayOnce();

        public static CancellationTokenSource CancelOnDispose(this IDisposeAble self)
        {
            CancellationTokenSource tokenSource = new();
            self.RegisterOnDispose(tokenSource.Cancel);
            return tokenSource;
        }
        public static CancellationTokenSource CancelOnDispose(this CancellationTokenSource self, IDisposeAble disposable)
        {
            disposable.RegisterOnDispose(self.Cancel);
            return self;
        }

    }
}