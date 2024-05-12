using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class UIDeselectListener: AUISelectEventListener,IDeselectHandler
    {
        public void OnDeselect(BaseEventData eventData) => Invoke(eventData);
    }
}