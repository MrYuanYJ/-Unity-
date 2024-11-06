using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class UISelectListener: AUISelectEventListener,ISelectHandler
    {
        public void OnSelect(BaseEventData eventData) => Invoke(eventData);
    }
}