using System;

namespace EasyFramework
{
    public interface IMeanCd
    {
        float Cd { get; }
    }

    public interface IMeanCd<T> : IMeanCd where T : Enum
    {
        T Mean { get; }
    }
}