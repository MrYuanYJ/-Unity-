using System;
using System.Collections.Generic;
using EasyFramework.EventKit;

namespace EasyFramework
{
    public class EasyLifeCycleEventSystem: ASystem
    {
        private readonly Dictionary<Type,HashSet<IAutoRegisterLifeCycleEvent>> _allAutoEvents= new ();
        public override void OnInit()
        {
            GlobalEvent.RegisterAutoEvent.RegisterEvent(RegisterAutoEvent).UnRegisterOnDispose(this);
            GlobalEvent.LifeCycleRegister<IEntity>.RegisterEvent(OnEntityInit).UnRegisterOnDispose(this);
        }

        public override void OnDispose()
        {
            base.OnDispose();
            _allAutoEvents.Clear();
        }

        private void OnEntityInit<T>(T easyLife) where T: IEasyLife
        {
            if (_allAutoEvents.TryGetValue(easyLife.GetType(), out var set))
            {
                foreach (var autoEvent in set)
                {
                    autoEvent.Register(easyLife).UnRegisterOnDispose(this);
                }
            }
        }

        public void RegisterAutoEvent(Type type)
        {
            if (typeof(IAutoRegisterLifeCycleEvent).IsAssignableFrom(type))
            {
                var obj = (IAutoRegisterLifeCycleEvent)Activator.CreateInstance(type);
                if (!_allAutoEvents.TryGetValue(obj.RegisterType, out var set))
                {
                    set = new HashSet<IAutoRegisterLifeCycleEvent>();
                    _allAutoEvents[obj.RegisterType]= set;
                }
                set.Add(obj);
            }
        }
    }
}