using UnityEngine;

namespace EasyFramework
{
    public interface IEasyProgressAction: IEasyAction
    {
        float Progress { get;}
        float Duration { get;}
    }

    public interface IEasyLerpAction
    {
        IEase Ease { get;}
    }
    public interface IEasyLerpAction<T>:IEasyProgressAction, IEasyLerpAction
    {
        T StartValue { get;}
        T EndValue { get;}
        T GetValue();
    }
}