using EasyFramework.EventKit;

namespace EasyFramework
{
    public abstract class AModel : IModel
    {
        private IStructure _structure;

        public bool IsInit { get; set; }
        public IEasyEvent InitEvent { get; }=new EasyEvent();
        public IEasyEvent StartEvent { get; }=new EasyEvent();
        public IEasyEvent DisposeEvent { get; }=new EasyEvent();

        protected virtual void OnInit() { }
        protected virtual void OnDispose(bool usePool) { }
        void IInitAble.OnInit()=>OnInit();
        void IDisposeAble.OnDispose(bool usePool)
        {
            OnDispose(usePool);
            if (_structure != null&& !_structure.IsDispose)
                _structure.Container.Remove(this.GetType());
            _structure = null;
        }


        void IInitAble.InitDo() { }
        void IDisposeAble.DisposeDo(bool usePool) { }

        public IStructure Structure => _structure;

        IStructure ISetStructureAbleAble.Structure
        {
            get => _structure;
            set => _structure = value;
        }
    }
}