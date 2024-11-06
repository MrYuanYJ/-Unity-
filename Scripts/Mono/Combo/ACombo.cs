using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyFramework
{
    public abstract class ACombo<TKey>: AMonoEntityCarrier,ICombo where TKey : Enum
    {
        public abstract Enum ComboName { get; }
        public int Priority => priority;
        public bool UsableOnStart => usableOnStart;
        public InputCondition[] InputConditions => inputConditions;
        public Enum[] UsableCombo
        {
            get
            {
                if (_usableCombo == null)
                {
                    _usableCombo=new Enum[usableCombo.Length];
                    for (int i = 0; i < usableCombo.Length; i++)
                    {
                        _usableCombo[i] = usableCombo[i];
                    }
                }
                return _usableCombo;
            }

        }

        [SerializeField]private int priority;
        [SerializeField]private bool usableOnStart;
        [SerializeField]private InputCondition[] inputConditions;
        [SerializeField]private TKey[] usableCombo;

        private Enum[] _usableCombo;
    }
}