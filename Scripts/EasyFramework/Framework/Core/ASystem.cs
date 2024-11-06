namespace EasyFramework
{
    public abstract class ASystem : ISystem
    {
        private IStructure _structure;
        public bool IsInit { get; set; }
        public bool InitDone { get; set; }
        public bool Enable => IsActive;
        public bool IsActive { get; set; } = true;

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

        IStructure ISetStructureAbleAble.Structure
        {
            get => _structure;
            set => _structure = value;
        }
    }
}