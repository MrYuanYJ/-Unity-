using System.Collections.Generic;
using System.IO;

namespace EasyFramework
{
    public class FrameworkSettings: AScriptableObjectSingleton<FrameworkSettings>
    {
        public static string Framework => Instance.framework;

        public static string FrameworkSOFolderPath => Path.Combine("Assets/EasyFramework/Resources", FrameworkSOFolder);
        public static string FrameworkSOFolder=> _instance? Instance.frameworkSOFolder : "Settings";
        public string framework = "EasyFramework";
        public string frameworkSOFolder = "Settings";
        public HashSet<object> AllModule = new();
    }
}