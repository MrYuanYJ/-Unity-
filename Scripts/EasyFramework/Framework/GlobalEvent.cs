using System;
using EasyFramework.EventKit;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    public struct GlobalEvent
    {
        public sealed class ApplicationInit : AEventIndex<ApplicationInit, IStructure> { }
        public sealed class ApplicationQuit : AEventIndex<ApplicationQuit, IStructure> { }
        public sealed class InitDo : AEventIndex<InitDo, IInitAble> { }
        public sealed class DisposeDo : AEventIndex<DisposeDo, IDisposeAble> { }
        public sealed class LifeCycleRegister : AEventIndex<LifeCycleRegister, IEasyLife> { }
        public sealed class RegisterAutoEvent : AEventIndex<RegisterAutoEvent, Type> { }
        
        public sealed class RecycleClass: AEventIndex<RecycleClass,object> { }
        public sealed class RecycleAsset: AEventIndex<RecycleAsset,Object> { }
        public sealed class RecycleGObject: AEventIndex<RecycleGObject,GameObject> { }
        
        
        public sealed class GetScopeClassEventDic<TScope>: AFuncIndex<GetScopeClassEventDic<TScope>,ClassEvent,TScope> {}
        public sealed class GetScopeEasyEventDic<TScope>: AFuncIndex<GetScopeEasyEventDic<TScope>,EasyEventDic,TScope> {}
        public sealed class GetScopeClassFuncDic<TScope>: AFuncIndex<GetScopeClassFuncDic<TScope>,ClassFunc,TScope> {}
        public sealed class GetScopeEasyFuncDic<TScope>: AFuncIndex<GetScopeEasyFuncDic<TScope>,EasyFuncDic,TScope> {}

        public static ClassEvent GetClassEventDic<TScope>(TScope scope) => GetScopeClassEventDic<TScope>.InvokeFunc(scope);
        public static EasyEventDic GetEasyEventDic<TScope>(TScope scope)=>GetScopeEasyEventDic<TScope>.InvokeFunc(scope);
        public static ClassFunc GetClassFuncDic<TScope>(TScope scope)=>GetScopeClassFuncDic<TScope>.InvokeFunc(scope);
        public static EasyFuncDic GetEasyFuncDic<TScope>(TScope scope)=>GetScopeEasyFuncDic<TScope>.InvokeFunc(scope);

    }
}