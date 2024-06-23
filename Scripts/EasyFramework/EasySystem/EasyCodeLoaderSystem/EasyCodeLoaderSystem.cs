using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFramework.EasySystem
{
    public class EasyCodeLoaderSystem: ASystem
    {
        public readonly Dictionary<string,Type> AllTypes = new ();
        public static event Action<Type> OnCodeLoaded;

        private const string Framework = "EasyFramework";
        
        protected override void OnInit()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == Framework ||
                    assembly.GetReferencedAssemblies().Any(targetAssembly=>targetAssembly.Name==Framework))
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