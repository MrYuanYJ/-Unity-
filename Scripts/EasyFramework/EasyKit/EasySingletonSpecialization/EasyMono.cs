using System;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public class EasyMono: MonoSingleton<EasyMono>
    {
        readonly EasyEvent _updateEvent = new();
        readonly EasyEvent _fixedUpdateEvent= new();

        private void Update()
        {
            _updateEvent.Invoke();
        }

        private void FixedUpdate()=> _fixedUpdateEvent.Invoke();

        public void TryRegister(object obj)
        {
            if (obj is IActiveAble active)
            {
                active.IsActive.Register(active.ActiveChange);
                if (active.IsActive.Value)
                    active.ActiveInvoke();
            }
            else
            {
                _updateEvent.Register(()=>RegisterInActiveEvent(obj)).OnlyPlayOnce();
            }
        }

        public void RegisterInActiveEvent(object obj)
        {
            if (obj is IStartAble start && !start.IsStart)
                _updateEvent.Register(start.Start).OnlyPlayOnce();
            if (obj is IFixedUpdateAble fixedUpdate)
                _fixedUpdateEvent.Register(fixedUpdate.FixedUpdate);
            if (obj is IUpdateAble update)
                _updateEvent.Register(update.Update);
        }

        public void TryUnRegister(object obj)
        {
            if (obj is IActiveAble active)
                active.IsActive.UnRegister(active.ActiveChange);
            UnRegisterInActiveEvent(obj);
        }

        public void UnRegisterInActiveEvent(object obj)
        {
            if (obj is IStartAble start && !start.IsStart)
                _updateEvent.UnRegister(start.Start);
            if (obj is IFixedUpdateAble fixedUpdate)
                _fixedUpdateEvent.UnRegister(fixedUpdate.FixedUpdate);
            if (obj is IUpdateAble update)
                _updateEvent.UnRegister(update.Update);
        }


        public static IUnRegisterHandle Register<T>(Action action) where T: AMonoListener =>
            Instance.gameObject.Register<T>(action);

        public static void UnRegister<T>(Action action) where T: AMonoListener =>
            GetInstance()?.gameObject.UnRegister<T>(action);

        protected override void OnInit()
        {
            GlobalEvent.InitDo.RegisterEvent(obj => GetInstance()?.TryRegister(obj)).UnRegisterOnDispose(this);
            GlobalEvent.DisposeDo.RegisterEvent(obj => GetInstance()?.TryUnRegister(obj)).UnRegisterOnDispose(this);
            GlobalEvent.Enable.RegisterEvent(obj=>GetInstance()?.RegisterInActiveEvent(obj)).UnRegisterOnDispose(this);
            GlobalEvent.Disable.RegisterEvent(obj=>GetInstance()?.UnRegisterInActiveEvent(obj)).UnRegisterOnDispose(this);
        }

        protected override void OnDispose(bool usePool)
        {
            _updateEvent.Clear();
            _fixedUpdateEvent.Clear();
        }
    }
}