using UnityEngine;

namespace EasyFramework
{
    public class TestCombo2Entity: AComboEntity<TestCombo2,EKatanaCombo>
    {
        protected override void OnEnterCombo()
        {
            Debug.Log($"Enter Combo {Mono.ComboName}");
        }
    }
}