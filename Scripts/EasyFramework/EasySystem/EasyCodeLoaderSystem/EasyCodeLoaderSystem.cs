using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFramework.EasySystem
{
    public class EasyCodeLoaderSystem: ASystem
    {
        public readonly Dictionary<string,Type> AllTypes = new ();
        public static event Action<Type> OnCodeLoaded;
        
        protected override void OnInit()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == FrameworkSettings.Framework ||
                    assembly.GetReferencedAssemblies().Any(targetAssembly=>targetAssembly.Name==FrameworkSettings.Framework))
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if(type.IsAbstract||
                           type.IsInterface)
                            continue;
                        AllTypes.Add(type.FullName, type);
                        OnCodeLoaded?.Invoke(type);
                    }
                }
            }
        }

        protected override void OnDispose(bool usePool)
        {
            AllTypes.Clear();
            OnCodeLoaded = null;
        }
    }
}