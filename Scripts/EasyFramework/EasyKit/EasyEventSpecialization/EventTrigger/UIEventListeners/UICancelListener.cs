using UnityEngine.EventSystems;

namespace EasyFramework.EventKit
{
    public class UICancelListener: AUISelectEventListener,ICancelHandler
    {
        public void OnCancel(BaseEventData eventData) => Invoke(eventData);
    }
}