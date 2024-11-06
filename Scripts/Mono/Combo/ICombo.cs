
using System;

namespace EasyFramework
{
    public interface ICombo
    {
        Enum ComboName { get; }
        int Priority { get; }
        bool UsableOnStart { get; }
        InputCondition[] InputConditions { get; }
        Enum[] UsableCombo { get; }
    }
}