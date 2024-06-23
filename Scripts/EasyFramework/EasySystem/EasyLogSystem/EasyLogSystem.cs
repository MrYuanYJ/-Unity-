using UnityEngine;

namespace EasyFramework.EasySystem
{
    public class EasyLogSystem: ASystem
    {
        protected override void OnInit()
        {
            EasyLog.LogEvent.RegisterEvent(Debug.unityLogger.Log);
        }

        protected override void OnDispose(bool usePool)
        {
            EasyLog.LogEvent.UnRegisterEvent(Debug.unityLogger.Log);
        }
    }
}