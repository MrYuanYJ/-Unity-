using System.Threading.Tasks;
using UnityEngine;

namespace EasyFramework
{
    [System.Serializable]
    public class SequenceNode: BaseNode
    {
        protected override Task OnEnter()
        {
            Debug.Log("SequenceNode OnEnter");
            return base.OnEnter();
        }
    }
}