using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class ASystem : ISystem
    {
        private IStructure _structure;
        public bool IsInit { get; set; }
        public ESProperty<bool> IsActive { get; set; } = new(true);
        public IEasyEvent InitEvent { get; } = new EasyEvent();
        public IEasyEvent DisposeEvent { get; } = new EasyEvent();
        public IEasyEvent ActiveEvent { get; } = new EasyEvent();
        public IEasyEvent UnActiveEvent { get; } = new EasyEvent();
        protected virtual void OnInit() { }
        protected virtual void OnActive() { }
        protected virtual void OnUnActive() { }
        protected virtual void OnDispose(bool usePool) { }

        void IInitAble.OnInit() => OnInit();
        void IActiveAble.OnActive() => OnActive();
        void IActiveAble.OnUnActive() => OnUnActive();

        void IDisposeAble.OnDispose(bool usePool)
        {
            if (_structure != null && !_structure.IsDispose)
                _structure.Container.Remove(this.GetType());
            _structure = null;
            OnDispose(usePool);
        }

        public IStructure Structure => _structure;

        IStructure ISetStructureAbleAble.Structure { get => _structure; set => _structure = value; }
    }
}