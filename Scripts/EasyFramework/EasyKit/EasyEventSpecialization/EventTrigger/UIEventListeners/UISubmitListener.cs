using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class UISubmitListener: AUISelectEventListener,ISubmitHandler
    {
        public void OnSubmit(BaseEventData eventData) => Invoke(eventData);
    }
}