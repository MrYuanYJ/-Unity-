using System;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class AStructure<TStructure> : Singleton<TStructure>, IStructure where TStructure : AStructure<TStructure>
    {
        public ContainerDic<object> Container { get; } = new();
        public EasyEventDic Event { get; } = new();
        public EasyFuncDic Func { get; } = new();

        void IInitAble.InitDo()
        {
            Container.TryInitAll();
        }
        
        void IDisposeAble.DisposeDo(bool usePool)
        {
            Container.TryDisposeAll();
            Container.Clear();
            Event.ClearAll();
            Func.ClearAll();
            ISingleton<TStructure>.Instance = null;
        }
    }
}