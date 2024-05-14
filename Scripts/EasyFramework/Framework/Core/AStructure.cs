using System;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class AStructure<TStructure> : AutoSingleton<TStructure>, IStructure where TStructure : AStructure<TStructure>
    {
        public ContainerDic<object> Container { get; } = new();
        public EasyEventDic Event { get; } = new();
        public EasyFuncDic Func { get; } = new();
        
        public bool IsInit { get; set; }
        public IEasyEvent InitEvent { get; }=new EasyEvent();
        public IEasyEvent StartEvent { get; }=new EasyEvent();
        public IEasyEvent DisposeEvent { get; }=new EasyEvent();


        public abstract void OnInit();
        public virtual void OnStart(){}
        public virtual void OnDispose(){}

        void IInitAble.InitDo()
        {
            Container.TryInitAll();
        }
        
        void IDisposeAble.DisposeDo()
        {
            Container.TryDisposeAll();
            Container.Clear();
            Event.ClearAll();
            Func.ClearAll();
            ISingleton<TStructure>.Instance = null;
        }
    }
}