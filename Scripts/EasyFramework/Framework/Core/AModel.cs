using System;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class AModel : IModel
    {
        private IStructure _structure;

        public bool IsInit { get; set; }
        public EasyEvent<IEasyLife> InitEvent { get; set; } = new();
        public EasyEvent<IEasyLife> StartEvent { get; set; } = new();
        public EasyEvent<IEasyLife> DisposeEvent { get; set; } = new();

        public virtual void OnInit() { }
        public virtual void OnStart() { }
        public virtual void OnDispose()
        {
            if (_structure != null&& !_structure.IsDispose)
                _structure.Container.Remove(this.GetType());
            _structure = null;
        }


        void IEasyLife.InitDo() { }
        void IEasyLife.DisposeDo() { }

        public IStructure GetStructure() => _structure;

        IStructure ISetStructureAbleAble.Structure
        {
            get => _structure;
            set => _structure = value;
        }
    }
}