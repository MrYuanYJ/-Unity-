using System;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class AModel : IModel
    {
        private IStructure _structure;

        public bool IsInit { get; set; }
        public EasyEvent<IInitAble> InitEvent { get; set; } = new();
        public EasyEvent<IStartAble> StartEvent { get; set; } = new();
        public EasyEvent<IDisposeAble> DisposeEvent { get; set; } = new();

        public virtual void OnInit() { }
        public virtual void OnStart() { }
        public virtual void OnDispose()
        {
            if (_structure != null&& !_structure.IsDispose)
                _structure.Container.Remove(this.GetType());
            _structure = null;
        }


        void IInitAble.InitDo() { }
        void IDisposeAble.DisposeDo() { }

        public IStructure GetStructure() => _structure;

        IStructure ISetStructureAbleAble.Structure
        {
            get => _structure;
            set => _structure = value;
        }
    }
}