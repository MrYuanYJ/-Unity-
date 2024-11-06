using UnityEngine;

namespace EasyFramework
{
    public class TestCombo1Entity: AComboEntity<TestCombo1,EKatanaCombo>
    {
        protected override void OnEnterCombo()
        {
            Debug.Log($"Enter Combo {Mono.ComboName}");
        }
    }
}