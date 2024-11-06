using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public static class EasyEnum
    {
        private static readonly Dictionary<Type, Enum> SonToParentDic = new();
        public static T As<T>(this Enum value) where T : Enum
        {
            var sonType = value.GetType();
            if (sonType == typeof(T))
                return (T)value;
            if (!SonToParentDic.TryGetValue(sonType, out var parent))
            {
                var attributes = sonType.GetCustomAttributes(typeof(ParentEnumAttribute), false);
                if(attributes.Length==0)
                    throw new ArgumentException($"{sonType} is not a child of any enum.");
                parent=((ParentEnumAttribute)attributes[0]).ParentEnum;
                SonToParentDic[sonType] = parent;
            }

            if (parent is T tParent)
                return tParent;
            return As<T>(parent);
        }
    }
}