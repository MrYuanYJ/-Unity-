using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public class EasyMono: MonoSingleton<EasyMono>
    {
        private List<IUpdateAble> _updateAbles = new();
        private List<IFixedUpdateAble> _fixedUpdateAbles = new();
        
        private void Update()
        {
            EasyLifeCycle.Update.InvokeEvent();
        }

        private void FixedUpdate()
        {
            EasyLifeCycle.FixedUpdate.InvokeEvent();
        }

        public static IUnRegisterHandle Register<T>(Action action) where T: AMonoListener =>
            Instance.gameObject.Register<T>(action);

        public static void UnRegister<T>(Action action) where T: AMonoListener =>
            GetInstance()?.gameObject.UnRegister<T>(action);
        
        protected override void OnDispose(bool usePool)
        {
            EasyLifeCycle.Update.ClearEvent();
            EasyLifeCycle.FixedUpdate.ClearEvent();
        }
    }
}