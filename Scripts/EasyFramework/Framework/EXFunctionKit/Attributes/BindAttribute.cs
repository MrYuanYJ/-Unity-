using System;

namespace EasyFramework
{
    public class BindAttribute : Attribute
    {  
        public Type Type { get; set; }
        public BindAttribute(Type type)  
        {  
            Type = type;  
        }
    }
}