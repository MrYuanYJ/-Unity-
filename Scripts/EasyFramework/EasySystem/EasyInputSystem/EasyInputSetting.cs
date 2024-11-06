using System;
using System.Collections.Generic;
using EXFunctionKit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyFramework
{
    public class EasyInputSetting: AScriptableObjectSingleton<EasyInputSetting>
    {
        public void Init()
        {
            foreach (var data in KeyCodeListenable)
            {
                if (!KeyCodeListenerDict.TryGetValue(data.eInput, out var set))
                {
                    set = new HashSet<KeyCodeListenerData>();
                    KeyCodeListenerDict[data.eInput] = set;
                }
                set.Add(data);
            }
            InputStateDict.Fill();
        }
        
        
        [SerializeField,TableList]
        private List<KeyCodeListenerData> KeyCodeListenable;
        public float InputSmooth = 999f;
        public float InputSmoothMultiplier = 999f;
        
        public Dictionary<EInput,HashSet<KeyCodeListenerData>> KeyCodeListenerDict = new();
        public Dictionary<EInput, InputState> InputStateDict = new();
        public SESProperty<Vector2> MoveInput = new(Vector2.zero, null, v2 => v2.magnitude < 1 ? v2 : v2.normalized);
    }
    
    
    public enum EInput
    {
        Up,
        Down,
        Left,
        Right,
        Jump,
        Attack,
        Use,
        Interact,
        Inventory,
        Map,
        Menu,
    }
    [Serializable]
    public class KeyCodeListenerData
    {
        [FormerlySerializedAs("InputType")] public EInput eInput;
        public KeyCode KeyCode;
        public bool IsPressed;
        public float TimePressed;
    }

    public class InputState
    {
        public SESProperty<bool> IsPressed=new(false);
        public SESProperty<float> TimePressed=new();
    }
}