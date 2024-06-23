using System;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public partial struct GlobalEvent
    {
        public sealed class ApplicationInit : AEventIndex<ApplicationInit, IStructure> { }
        public sealed class ApplicationQuit : AEventIndex<ApplicationQuit, IStructure> { }
        
        public sealed class MainStructure: AFuncIndex<MainStructure, IStructure> { }
        
        
        public sealed class InitDo : AEventIndex<InitDo, object> { }
        public sealed class DisposeDo : AEventIndex<DisposeDo, object> { }
        public sealed class Enable: AEventIndex<Enable, object> { }
        public sealed class Disable: AEventIndex<Disable, object> { }
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