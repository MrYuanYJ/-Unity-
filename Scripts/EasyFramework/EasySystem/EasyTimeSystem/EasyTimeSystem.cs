using UnityEngine;

namespace EasyFramework
{
    public class EasyTimeSystem: ASystem
    {
        protected override void OnActive()
        {
            base.OnActive();
            EasyTime.GetDeltaTime.RegisterFunc(GetDeltaTime);
            EasyTime.GetUnscaledDeltaTime.RegisterFunc(GetUnscaledDeltaTime);
            EasyTime.GetFixedDeltaTime.RegisterFunc(GetFixedDeltaTime);
            EasyTime.GetUnscaledFixedDeltaTime.RegisterFunc(GetUnscaledFixedDeltaTime);
        }

        protected override void OnUnActive()
        {
            base.OnUnActive();
            EasyTime.GetDeltaTime.UnRegisterFunc(GetDeltaTime);
            EasyTime.GetUnscaledDeltaTime.UnRegisterFunc(GetUnscaledDeltaTime);
            EasyTime.GetFixedDeltaTime.UnRegisterFunc(GetFixedDeltaTime);
            EasyTime.GetUnscaledFixedDeltaTime.UnRegisterFunc(GetUnscaledFixedDeltaTime);
        }


        private float GetTime()
        {
            return Time.time;
        }
        private float GetUnscaledTime()
        {
            return Time.unscaledTime;
        }
        private float GetDeltaTime()
        {
            return Time.deltaTime;
        }
        private float GetUnscaledDeltaTime()
        {
            return Time.unscaledDeltaTime;
        }
        private float GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime;
        }
        private float GetUnscaledFixedDeltaTime()
        {
            return Time.fixedUnscaledDeltaTime;
        }
    }
}