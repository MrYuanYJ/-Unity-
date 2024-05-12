using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFramework.EasySystem
{
    public class EasyCodeLoaderSystem: ASystem
    {
        private readonly Dictionary<string,Type> _allTypes = new ();

        private const string Framework = "EasyFramework";
        
        public override void OnInit()
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
                        _allTypes.Add(type.FullName, type);
                        GlobalEvent.RegisterAutoEvent.InvokeEvent(type);
                    }
                }
            }
        }

        public override void OnDispose()
        {
            _allTypes.Clear();
        }
    }
}