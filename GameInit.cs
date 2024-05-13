using System;
using UnityEngine;

namespace EasyFramework
{
    public class GameInit : AMonoEntity<GameInitComponent>
    {
        public override void OnStart()
        {
            Debug.Log("GameInit OnStart");
        }
    }
}
