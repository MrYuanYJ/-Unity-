
namespace EasyFramework.EventKit
{
    public abstract class AutoClassFunc<T,R> :IAutoRegisterEvent where T : AFuncIndex<T,R>
    {
        protected abstract R Run();
        public IUnRegisterHandle Register()=>AFuncIndex<T,R>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,R>.RegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,R>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,R>.UnRegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
    }
    public abstract class AutoClassFunc<T,A,R> :IAutoRegisterEvent where T : AFuncIndex<T,A,R>
    {
        protected abstract R Run(A a);
        public IUnRegisterHandle Register()=>AFuncIndex<T,A,R>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,A,R>.RegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,A,R>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,A,R>.UnRegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
    }
    public abstract class AutoClassFunc<T,A,B,R> :IAutoRegisterEvent where T : AFuncIndex<T,A,B,R>
    {
        protected abstract R Run(A a, B b);
        public IUnRegisterHandle Register()=>AFuncIndex<T,A,B,R>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T,A,B,R>.RegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
        public void UnRegister()=>AFuncIndex<T,A,B,R>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T,A,B,R>.UnRegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
    }

    public abstract class AutoClassFunc<T, A, B, C, R> : IAutoRegisterEvent where T : AFuncIndex<T, A, B, C, R>
    {
        protected abstract R Run(A a, B b, C c);
        public IUnRegisterHandle Register() => AFuncIndex<T, A, B, C, R>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T, A, B, C, R>.RegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
        public void UnRegister() => AFuncIndex<T, A, B, C, R>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T, A, B, C, R>.UnRegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
    }

    public abstract class AutoClassFunc<T, A, B, C, D, R> : IAutoRegisterEvent where T : AFuncIndex<T, A, B, C, D, R>
    {
        protected abstract R Run(A a, B b, C c, D d);
        public IUnRegisterHandle Register() => AFuncIndex<T, A, B, C, D, R>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T, A, B, C, D, R>.RegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
        public void UnRegister() => AFuncIndex<T, A, B, C, D, R>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T, A, B, C, D, R>.UnRegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
    }
    public abstract class AutoClassFunc<T, A, B, C, D, E, R> : IAutoRegisterEvent where T : AFuncIndex<T, A, B, C, D, E, R>
    {
        protected abstract R Run(A a, B b, C c, D d, E e);
        public IUnRegisterHandle Register() => AFuncIndex<T, A, B, C, D, E, R>.RegisterFunc(Run);
        public IUnRegisterHandle Register<TScope>(TScope scope) => AFuncIndex<T, A, B, C, D, E, R>.RegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
        public void UnRegister() => AFuncIndex<T, A, B, C, D, E, R>.UnRegisterFunc(Run);
        public void UnRegister<TScope>(TScope scope) => AFuncIndex<T, A, B, C, D, E, R>.UnRegisterFunc(GlobalEvent.GetEasyFuncDic(scope), Run);
    }
}