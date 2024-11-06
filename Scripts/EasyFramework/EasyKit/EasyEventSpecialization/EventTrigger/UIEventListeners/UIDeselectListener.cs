using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class UIDeselectListener: AUISelectEventListener,IDeselectHandler
    {
        public void OnDeselect(BaseEventData eventData) => Invoke(eventData);
    }
}