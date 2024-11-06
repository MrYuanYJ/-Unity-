using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework
{
    public class GameInit : MonoBehaviour
    {
        private void Start()
        {
            Game.TryRegister();
        }
            
        private void OnDestroy()
        {
            Game.Instance?.Dispose();
        }
    }
}
