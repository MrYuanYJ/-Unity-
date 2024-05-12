using EXFunctionKit;
using UnityEngine;

namespace EasyFramework
{
    public class Fire: AElement
    {
        public override void OnInit(IBuffAddData data)
        {
            Debug.Log($"I am {data.As<NormalBuffAddData>().Damage} Fire!");
        }

        public override void OnExecute()
        {
            Debug.Log("Fire is executing!");
        }
    }
}