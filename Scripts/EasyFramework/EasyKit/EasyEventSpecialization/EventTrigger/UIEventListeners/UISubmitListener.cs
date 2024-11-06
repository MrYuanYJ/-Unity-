using UnityEngine.EventSystems;

namespace EasyFramework
{
    public class UISubmitListener: AUISelectEventListener,ISubmitHandler
    {
        public void OnSubmit(BaseEventData eventData) => Invoke(eventData);
    }
}