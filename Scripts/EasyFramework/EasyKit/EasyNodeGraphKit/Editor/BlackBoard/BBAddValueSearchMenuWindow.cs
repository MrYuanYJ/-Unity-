using System;
using System.Collections;
using System.Reflection;
using EasyFramework.Editor;

namespace EasyFramework
{
    public class BBAddValueSearchMenuWindow: SearchMenuWindowProvider
    {
        public override IEnumerable AllEntry => BlackBoard.GetAllBBValueType();
        public override Func<object, string> GetPath => entry => ((Type) entry).GetCustomAttribute<MenuAttribute>()?.Path;
    }
}