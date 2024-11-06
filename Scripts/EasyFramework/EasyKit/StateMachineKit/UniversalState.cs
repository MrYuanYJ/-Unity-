namespace EasyFramework
{
    public interface IRecycleAbleState: IStateBase,IRecycleable
    {
        void IRecycleable.Recycle() => Reset();
    }
    public class UniversalState: AState,IRecycleAbleState
    {
        private UniversalState(){}
        public static EasyPool<UniversalState> Pool = new(()=>new UniversalState(),null,null);
        protected override void OnEnter() { }
        protected override void OnExit() { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
    public class UniversalState<TValue>: AState<TValue>,IRecycleAbleState
    {
        private UniversalState(){}
        public static EasyPool<UniversalState<TValue>> Pool = new (()=>new UniversalState<TValue>(),null,null);
        protected override void OnEnter(TValue param) { }
        protected override void OnExit(TValue param) { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
    public class UniversalEasyState<TMachine>:AEasyState<TMachine>,IRecycleAbleState where TMachine : IStateMachineBase
    {
        private UniversalEasyState(){}
        public static EasyPool<UniversalEasyState<TMachine>> Pool = new (()=>new UniversalEasyState<TMachine>(),null,null);
        protected override void OnEnter() { }
        protected override void OnExit() { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
    public class UniversalEasyState<TMachine, TValue>: AEasyState<TMachine, TValue>,IRecycleAbleState where TMachine : IStateMachineBase
    {
        private UniversalEasyState(){}
        public static EasyPool<UniversalEasyState<TMachine, TValue>> Pool = new (()=>new UniversalEasyState<TMachine, TValue>(),null,null);
        protected override void OnEnter(TValue param) { }
        protected override void OnExit(TValue param) { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
    public class UniversalProcedure: AProcedure,IRecycleAbleState
    {
        private UniversalProcedure(){}
        public static EasyPool<UniversalProcedure> Pool = new (()=>new UniversalProcedure(),null,null);
        protected override void OnEnter() { }
        protected override void OnExit() { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
    public class UniversalProcedure<TValue>: AProcedure<TValue>,IRecycleAbleState
    {
        private UniversalProcedure(){}
        public static EasyPool<UniversalProcedure<TValue>> Pool = new (()=>new UniversalProcedure<TValue>(),null,null);
        protected override void OnEnter(TValue param) { }
        protected override void OnExit(TValue param) { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
    
    public class UniversalEasyProcedure<TMachine>: AEasyProcedure<TMachine>,IRecycleAbleState where TMachine : IStateMachineBase
    {
        private UniversalEasyProcedure(){}
        public static EasyPool<UniversalEasyProcedure<TMachine>> Pool = new (()=>new UniversalEasyProcedure<TMachine>(),null,null);
        protected override void OnEnter() { }
        protected override void OnExit() { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
    public class UniversalEasyProcedure<TMachine, TValue>: AEasyProcedure<TMachine, TValue>,IRecycleAbleState where TMachine : IStateMachineBase
    {
        private UniversalEasyProcedure(){}
        public static EasyPool<UniversalEasyProcedure<TMachine, TValue>> Pool = new (()=>new UniversalEasyProcedure<TMachine, TValue>(),null,null);
        protected override void OnEnter(TValue param) { }
        protected override void OnExit(TValue param) { }
        public bool IsRecycled { get; set; }
        protected override bool OnAfterRemove()
        {
            Pool.Recycle(this);
            return true;
        }
    }
}