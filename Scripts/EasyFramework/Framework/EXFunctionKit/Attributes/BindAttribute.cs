using System;

namespace EasyFramework
{
    public class BaseBindAttribute : Attribute
    {
        
    }
    public class BindAttribute : BaseBindAttribute
    {  
        public Type Type { get; set; }
        public BindAttribute(Type type)  
        {  
            Type = type;  
        }
    }
}