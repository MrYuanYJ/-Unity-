using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class UIUpdateSelectedListener: AUISelectEventListener,IUpdateSelectedHandler
    {
        public void OnUpdateSelected(BaseEventData eventData) => Invoke(eventData);
    }
}