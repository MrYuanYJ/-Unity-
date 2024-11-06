using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework
{
    public interface IProperty
    {
        public bool BasedOnThis { get; }
        public int BaseValue { get; }
        public float CurrentPercent { get; }
    }
    public abstract class AProperty: AMonoEntityCarrier,IProperty
    {
        public bool basedOnThis=true;
        [ShowIf("@basedOnThis==true")]
        public int baseValue;
        [Range(0, 1f)]
        public float currentPercent = 1;

        public bool BasedOnThis => basedOnThis;
        public int BaseValue => baseValue;
        public float CurrentPercent => currentPercent;
    }
}