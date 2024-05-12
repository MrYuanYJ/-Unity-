using System.Threading;

namespace EasyFramework.EventKit
{
    public interface IAutoRegisterEvent
    {
        void Register();
        void Register<T>(T arg);
        void UnRegister();
        void UnRegister<T>(T arg);
    }
    public abstract class AutoClassEvent<T> :IAutoRegisterEvent where T : AEventIndex<T>
    {
        protected abstract void Run();
        public void Register()=>AEventIndex<T>.RegisterEvent(Run);
        public void Register<TScope>(TScope scope) => AEventIndex<T>.RegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
        public void UnRegister()=>AEventIndex<T>.UnRegisterEvent(Run);
        public void UnRegister<TScope>(TScope scope) => AEventIndex<T>.UnRegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
    }
    public abstract class AutoClassEvent<T,A> :IAutoRegisterEvent where T : AEventIndex<T,A>
    {
        protected abstract void Run(A a);
        public void Register()=>AEventIndex<T,A>.RegisterEvent(Run);
        public void Register<TScope>(TScope scope)=> AEventIndex<T,A>.RegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
        public void UnRegister()=>AEventIndex<T,A>.UnRegisterEvent(Run);
        public void UnRegister<TScope>(TScope scope)=> AEventIndex<T,A>.UnRegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
    }
    public abstract class AutoClassEvent<T,A,B> :IAutoRegisterEvent where T : AEventIndex<T,A,B>
    {
        protected abstract void Run(A a, B b);
        public void Register()=>AEventIndex<T,A,B>.RegisterEvent(Run);
        public void Register<TScope>(TScope scope)=> AEventIndex<T,A,B>.RegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
        public void UnRegister()=>AEventIndex<T,A,B>.UnRegisterEvent(Run);
        public void UnRegister<TScope>(TScope scope)=> AEventIndex<T,A,B>.UnRegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
    }
    public abstract class AutoClassEvent<T,A,B,C> :IAutoRegisterEvent where T : AEventIndex<T,A,B,C>
    {
        protected abstract void Run(A a, B b, C c);
        public void Register()=>AEventIndex<T,A,B,C>.RegisterEvent(Run);
        public void Register<TScope>(TScope scope)=> AEventIndex<T,A,B,C>.RegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
        public void UnRegister()=>AEventIndex<T,A,B,C>.UnRegisterEvent(Run);
        public void UnRegister<TScope>(TScope scope)=> AEventIndex<T,A,B,C>.UnRegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
    }
    
    public abstract class AutoClassEvent<T,A,B,C,D> :IAutoRegisterEvent where T : AEventIndex<T,A,B,C,D>
    {
        protected abstract void Run(A a, B b, C c, D d);
        public void Register()=>AEventIndex<T,A,B,C,D>.RegisterEvent(Run);
        public void Register<TScope>(TScope scope)=> AEventIndex<T,A,B,C,D>.RegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
        public void UnRegister()=>AEventIndex<T,A,B,C,D>.UnRegisterEvent(Run);
        public void UnRegister<TScope>(TScope scope)=> AEventIndex<T,A,B,C,D>.UnRegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
    }

    public abstract class AutoClassEvent<T, A, B, C, D, E> : IAutoRegisterEvent where T : AEventIndex<T, A, B, C, D, E>
    {
        protected abstract void Run(A a, B b, C c, D d, E e);

        public void Register()=>AEventIndex<T, A, B, C, D, E>.RegisterEvent(Run);
        public void Register<TScope>(TScope scope)=> AEventIndex<T, A, B, C, D, E>.RegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
        public void UnRegister()=>AEventIndex<T, A, B, C, D, E>.UnRegisterEvent(Run);
        public void UnRegister<TScope>(TScope scope)=> AEventIndex<T, A, B, C, D, E>.UnRegisterEvent(GlobalEvent.GetClassEventDic(scope), Run);
    }
}