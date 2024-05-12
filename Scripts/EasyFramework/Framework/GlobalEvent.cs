using System;
using EasyFramework.EventKit;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    public struct GlobalEvent
    {
        public abstract class ApplicationInit : AEventIndex<ApplicationInit, IStructure> { }
        public abstract class ApplicationQuit : AEventIndex<ApplicationQuit, IStructure> { }
        public abstract class InitDo : AEventIndex<InitDo, IEasyLife> { }
        public abstract class DisposeDo : AEventIndex<DisposeDo, IEasyLife> { }
        public abstract class LifeCycleRegister : AEventIndex<LifeCycleRegister, IEasyLife> { }
        public abstract class RegisterAutoEvent : AEventIndex<RegisterAutoEvent, Type> { }
        
        public abstract class RecycleAsset: AEventIndex<RecycleAsset,Object> { }
        public abstract class RecycleGObject: AEventIndex<RecycleGObject,GameObject> { }
        
        
        public abstract class GetScopeClassEventDic<TScope>: AFuncIndex<GetScopeClassEventDic<TScope>,ClassEvent,TScope> {}
        public abstract class GetScopeEasyEventDic<TScope>: AFuncIndex<GetScopeEasyEventDic<TScope>,EasyEventDic,TScope> {}
        public abstract class GetScopeClassFuncDic<TScope>: AFuncIndex<GetScopeClassFuncDic<TScope>,ClassFunc,TScope> {}
        public abstract class GetScopeEasyFuncDic<TScope>: AFuncIndex<GetScopeEasyFuncDic<TScope>,EasyFuncDic,TScope> {}

        public static ClassEvent GetClassEventDic<TScope>(TScope scope) => GetScopeClassEventDic<TScope>.InvokeFunc(scope);
        public static EasyEventDic GetEasyEventDic<TScope>(TScope scope)=>GetScopeEasyEventDic<TScope>.InvokeFunc(scope);
        public static ClassFunc GetClassFuncDic<TScope>(TScope scope)=>GetScopeClassFuncDic<TScope>.InvokeFunc(scope);
        public static EasyFuncDic GetEasyFuncDic<TScope>(TScope scope)=>GetScopeEasyFuncDic<TScope>.InvokeFunc(scope);

    }
}