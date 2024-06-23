using System;
using System.Collections.Generic;

namespace EasyFramework.EasySystem.EasyAttributeSystem
{
    public class EasyMono2EntityRelationalMappingSystem: ASystem
    {
        public Dictionary<Type,Type> Mono2EntityMapping = new();
        protected override void OnInit()
        {
            EasyCodeLoaderSystem.OnCodeLoaded += RegisterAutoEvent;
        }

        protected override void OnDispose(bool usePool)
        {
            Mono2EntityMapping.Clear();
        }

        private void RegisterAutoEvent(Type type)
        {
            if (typeof(IMonoEntity).IsAssignableFrom(type))
            {
                if (type.BaseType != null && type.BaseType.IsGenericType)
                {
                    var monoType = type.BaseType.GetGenericArguments()[0];
                    Mono2EntityMapping[monoType] = type;
                }
            }
        }
    }
}