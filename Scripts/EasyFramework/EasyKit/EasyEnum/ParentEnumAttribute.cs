using System;

namespace EasyFramework
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class ParentEnumAttribute: Attribute
    {
        public Enum ParentEnum;
        
        public ParentEnumAttribute(object parentEnum)
        {
            ParentEnum = (Enum)parentEnum;
        }
    }
}