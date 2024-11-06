using System;
using System.Threading;

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
    
    public static class EasyLifeExtensions
    {
        public static void TryInit(this object self)
        {
            if(self is IInitAble easyLife)
                easyLife.Init();
        }
        public static void TryDispose(this object self)
        {
            if (self is IDisposeAble easyLife)
                easyLife.Dispose();
        }
        public static void Init(this IInitAble self)=> self.Init();
        public static void Dispose(this IDisposeAble self)=> self.Dispose();
        public static void Dispose(this IEasyLife self,bool usePool)=> self.Dispose(usePool);
        public static void Enable(this IActiveAble self) => self._isActive = true;
        public static void Disable(this IActiveAble self) => self._isActive = false;
        public static void SetActive(this IActiveAble self, bool isActive) => self._isActive = isActive;
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
        public static IUnRegisterHandle UnRegisterOnActive(this IUnRegisterHandle self, IActiveAble activeAble, bool onlyUnRegisterOnce = false)
        {
            if (onlyUnRegisterOnce)
                activeAble.ActiveEvent.Register(self.UnRegister).OnlyPlayOnce();
            else
                activeAble.ActiveEvent.Register(self.UnRegister);
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnUnActive(this IUnRegisterHandle self, IActiveAble activeAble, bool onlyUnRegisterOnce = false)
        {
            if (onlyUnRegisterOnce)
                activeAble.UnActiveEvent.Register(self.UnRegister).OnlyPlayOnce();
            else
                activeAble.UnActiveEvent.Register(self.UnRegister);
            return self;
        }
        public static IUnRegisterHandle UnRegisterOnStart(this IUnRegisterHandle self, IStartAble startAble, bool onlyUnRegisterOnce = false)
        {
            if (onlyUnRegisterOnce)
                startAble.StartEvent.Register(self.UnRegister).OnlyPlayOnce();
            else
                startAble.StartEvent.Register(self.UnRegister);
            return self;
        }
        public static IUnRegisterHandle RegisterOnDispose(this IDisposeAble self, Action action) => self.DisposeEvent.Register(action);
        public static IUnRegisterHandle RegisterOnActive(this IActiveAble self, Action action) => self.ActiveEvent.Register(action);
        public static IUnRegisterHandle RegisterOnUnActive(this IActiveAble self, Action action) => self.UnActiveEvent.Register(action);
        public static IUnRegisterHandle RegisterOnStart(this IStartAble self, Action action) => self.StartEvent.Register(action);
        public static void UnRegisterDisposeEvent(this IDisposeAble self, Action action) => self.DisposeEvent.UnRegister(action);
        public static void UnRegisterActiveEvent(this IActiveAble self, Action action) => self.ActiveEvent.UnRegister(action);
        public static void UnRegisterUnActiveEvent(this IActiveAble self, Action action) => self.UnActiveEvent.UnRegister(action);
        public static void UnRegisterStartEvent(this IStartAble self, Action action) => self.StartEvent.UnRegister(action);

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