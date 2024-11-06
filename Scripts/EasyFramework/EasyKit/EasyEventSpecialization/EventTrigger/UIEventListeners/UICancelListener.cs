using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class UICancelListener: AUISelectEventListener,ICancelHandler
    {
        public void OnCancel(BaseEventData eventData) => Invoke(eventData);
    }
}