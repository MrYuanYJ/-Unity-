using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class PointerClickListener: APointerEventListener,IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData) =>Invoke(eventData);
    }
}