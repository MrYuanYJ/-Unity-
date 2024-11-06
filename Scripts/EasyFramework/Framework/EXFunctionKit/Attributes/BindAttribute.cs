using System;

namespace EasyFramework
{
    public class BaseBindAttribute : Attribute
    {
        
    }
    public class BindAttribute : BaseBindAttribute
    {  
        public Type BindType { get; set; }
        public object BindObject { get; set; }
        public BindAttribute(Type bindType)  
        {  
            BindType = bindType;
            BindObject = BindType;
        }
        public BindAttribute(object bindObject)
        {
            BindObject = bindObject;
        }
    }
}