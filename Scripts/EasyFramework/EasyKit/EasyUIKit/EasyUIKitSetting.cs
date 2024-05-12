using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace EasyFramework.EasyUIKit
{
    public class EasyUIKitSetting : AScriptableObjectSingleton<EasyUIKitSetting>
    {
        public bool hideLastPanelWhenOpenNewPanel = false;
#if UNITY_EDITOR
        
        [ShowInInspector] public static List<SingleUIAni> DefaultUIShowAni = new();
        [ShowInInspector] public static List<SingleUIAni> DefaultUIHideAni = new();
        [ShowInInspector] public static float DefaultDuration = 0.15f;
        [ShowInInspector] public static bool DefaultIsIgnoreTimeScale = true;
        
#endif
    }
}