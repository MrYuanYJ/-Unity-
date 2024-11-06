using System;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyFramework
{
    [System.Serializable]
    public class ActionNode: BaseNode
    {
        public string test1;
        [SerializeField]
        private string test2;
        [SerializeField]
        protected string test3;
        [NonSerialized]
        public string test4;

        protected override Task OnEnter()
        {
            Debug.Log("ActionNode OnEnter");
            return base.OnEnter();
        }
    }
}