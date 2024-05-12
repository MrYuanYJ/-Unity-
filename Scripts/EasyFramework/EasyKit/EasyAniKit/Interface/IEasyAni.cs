using System;
using System.Collections.Generic;

namespace EasyFramework
{
    public interface IEasyAni
    {
        float Duration { get; }
        bool IsUnscaledTime { get; }
        IEnumerable<ISingleAni> Anis{ get; }
        event Action<float> Playing;
        event Action Finished;
        
        CoroutineHandle Run(Action onFinished=null,params object[] args);
        CoroutineHandle Run(CoroutineHandle handle,Action onFinished=null,params object[] args);
        void Finish();
    }
}