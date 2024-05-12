using System;

namespace EasyFramework
{

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]  
    public class EventScopeAttribute : Attribute  
    {  
        public EventScope Scope { get; set; }  
  
        public EventScopeAttribute(EventScope scope)  
        {  
            Scope = scope;  
        }  
        public EventScopeAttribute(EventScope scope,params EventScope[] scopes)  
        {  
            Scope = scope;  
            foreach(var s in scopes)  
            {  
                Scope |= s;  
            }  
        }  
    }
}