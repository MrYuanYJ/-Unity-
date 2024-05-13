using System.Threading;

namespace EasyFramework.EventKit
{
    public abstract class AutoClassFunc<T,R> :IAutoRegisterEvent where T : AFuncIndex<T,R>
    {
        protected abstract R Run();
        public IUnRegisterHandle Register()=>AFuncIndex<T,R>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,R>.RegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,R>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,R>.UnRegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
    }
    public abstract class AutoClassFunc<T,R,A> :IAutoRegisterEvent where T : AFuncIndex<T,R,A>
    {
        protected abstract R Run(A a);
        public IUnRegisterHandle Register()=>AFuncIndex<T,R,A>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,R,A>.RegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,R,A>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,R,A>.UnRegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
    }
    public abstract class AutoClassFunc<T,R,A,B> :IAutoRegisterEvent where T : AFuncIndex<T,R,A,B>
    {
        protected abstract R Run(A a, B b);
        public IUnRegisterHandle Register()=>AFuncIndex<T,R,A,B>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,R,A,B>.RegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,R,A,B>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,R,A,B>.UnRegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
    }
    public abstract class AutoClassFunc<T,R,A,B,C> :IAutoRegisterEvent where T : AFuncIndex<T,R,A,B,C>
    {
        protected abstract R Run(A a, B b, C c);
        public IUnRegisterHandle Register()=>AFuncIndex<T,R,A,B,C>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,R,A,B,C>.RegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,R,A,B,C>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,R,A,B,C>.UnRegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
    }
    
    public abstract class AutoClassFunc<T,R,A,B,C,D> :IAutoRegisterEvent where T : AFuncIndex<T,R,A,B,C,D>
    {
        protected abstract R Run(A a, B b, C c, D d);
        public IUnRegisterHandle Register()=>AFuncIndex<T,R,A,B,C,D>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,R,A,B,C,D>.RegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,R,A,B,C,D>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,R,A,B,C,D>.UnRegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
    }

    public abstract class AutoClassFunc<T,R, A, B, C, D, E> : IAutoRegisterEvent where T : AFuncIndex<T,R, A, B, C, D, E>
    {
        protected abstract R Run(A a, B b, C c, D d, E e);

        public IUnRegisterHandle Register()=>AFuncIndex<T,R, A, B, C, D, E>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,R, A, B, C, D, E>.RegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,R, A, B, C, D, E>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,R, A, B, C, D, E>.UnRegisterFunc(GlobalEvent.GetClassFuncDic(scope), Run);
    }
}