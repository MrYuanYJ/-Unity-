#if UNITY_EDITOR

using EasyFramework.EasyResKit;
using EasyFramework.EasySystem;
using EasyFramework.EasyUIKit;
using UnityEditor;

namespace EasyFramework
{
    public class SettingCreator
    {
        [MenuItem(itemName: "EasyFramework/Settings/Create All Setting")]
        public static void CreateAllSetting()
        {
            var uiKitSetting = EasyUIKitSetting.Instance;
            var resKitSetting = EasyResKitSetting.Instance;
            var buffSetting = EasyBuffSetting.Instance;
            var inputSetting = EasyInputSetting.Instance;
        }
        [MenuItem(itemName: "EasyFramework/Settings/Create EasyResKit Setting")]
        public static void CreateEasyUIKitSetting()
        {
            var uiKitSetting = EasyUIKitSetting.Instance;
        }

        [MenuItem(itemName: "EasyFramework/Settings/Create EasyUIKit Setting")]
        public static void CreateEasyResKitSetting()
        {
            var resKitSetting = EasyResKitSetting.Instance;
        }
        [MenuItem(itemName: "EasyFramework/Settings/Create EasyBuff Setting")]
        public static void CreateEasyBuffSetting()
        {
            var buffSetting = EasyBuffSetting.Instance;
        }
        [MenuItem(itemName: "EasyFramework/Settings/Create EasyInput Setting")]
        public static void CreateEasyInputSetting()
        {
            var inputSetting = EasyInputSetting.Instance;
        }
    }
}

#endif