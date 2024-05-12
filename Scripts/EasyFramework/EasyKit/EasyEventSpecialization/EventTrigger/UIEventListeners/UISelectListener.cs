using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class UISelectListener: AUISelectEventListener,ISelectHandler
    {
        public void OnSelect(BaseEventData eventData) => Invoke(eventData);
    }
}