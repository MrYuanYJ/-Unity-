using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class UIUpdateSelectedListener: AUISelectEventListener,IUpdateSelectedHandler
    {
        public void OnUpdateSelected(BaseEventData eventData) => Invoke(eventData);
    }
}