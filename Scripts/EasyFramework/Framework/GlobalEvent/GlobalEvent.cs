using System;

namespace EasyFramework
{
    public partial struct GlobalEvent
    {
        public sealed class ApplicationInit : AEventIndex<ApplicationInit, IStructure> { }
        public sealed class ApplicationQuit : AEventIndex<ApplicationQuit, IStructure> { }
        public sealed class MainStructure: AFuncIndex<MainStructure, IStructure> { }
        
        public sealed class LifeCycleRegister<T> : AEventIndex<LifeCycleRegister<T>, T> { }
        
        
        public sealed class GetScopeEasyEventDic<TScope>: AFuncIndex<GetScopeEasyEventDic<TScope>,TScope,EasyEventDic> {}
        public sealed class GetScopeEasyFuncDic<TScope>: AFuncIndex<GetScopeEasyFuncDic<TScope>,TScope,EasyFuncDic> {}
    }
    public partial struct GlobalEvent
    {
        public static EasyEventDic GetEasyEventDic<TScope>(TScope scope)=> GetScopeEasyEventDic<TScope>.InvokeFunc(scope);
        public static EasyFuncDic GetEasyFuncDic<TScope>(TScope scope)=> GetScopeEasyFuncDic<TScope>.InvokeFunc(scope);
    }
}