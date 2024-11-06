using System;
using System.Collections;
using System.Reflection;

namespace EasyFramework.Editor
{
    public class NodeSearchMenuWindow: SearchMenuWindowProvider
    {
        public override IEnumerable AllEntry=> IEditorWindow.GetClassList(typeof(BaseNodeView));
        public override Func<object, string> GetPath => node => ((Type) node).GetCustomAttribute<MenuAttribute>()?.Path;
    }
}