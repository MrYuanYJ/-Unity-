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
        public EasyEvent<IEasyLife> InitEvent { get; set; } = new();
        public EasyEvent<IEasyLife> StartEvent { get; set; } = new();
        public EasyEvent<IEasyLife> DisposeEvent { get; set; } = new();


        public abstract void OnInit();
        public virtual void OnStart(){}
        public virtual void OnDispose(){}

        void IEasyLife.InitDo()
        {
            Container.TryInitAll();
        }
        
        void IEasyLife.DisposeDo()
        {
            Container.TryDisposeAll();
            Container.Clear();
            Event.ClearAll();
            Func.ClearAll();
            ISingleton<TStructure>.Instance = null;
        }
    }
}