using System.Collections.Generic;
using EasyFramework.EasyTagKit;
using EasyFramework.EasyTaskKit;
using UnityEngine;

namespace EasyFramework
{
    public interface IBuff: ITagAble<BuffTag>
    {
        public static readonly Dictionary<EBuff, EasyPool<IBuff>> BuffPool = new(); 
        IBuffableUnit Unit { get; protected set; }
        float Duration { get;}
        float ExistingTime { get; protected set; }
        float Magnification { get; protected set; }
        float MaxMagnification { get;}
        EBuff EBuff{ get; protected set; }
        protected bool IsDeInit { get; set; }

        void Init(IBuffAddData data)
        {
            Unit = data.Unit;
            EBuff = data.EBuff;
            Magnification = Mathf.Min(data.Magnification, MaxMagnification);
            IsDeInit = false;
            OnInit(data);
        }
        void DeInit()
        {
            if(IsDeInit)
                return;
            
            OnDeInit();
            Unit = null;
            EBuff = EBuff.None;
            Magnification = 0;
            ExistingTime = 0;
            IsDeInit = true;
        }
        void Running(float dateTime)
        {
            OnRunning();
            Execute();
            ExistingTime += dateTime;
            if (Duration==0||Duration > 0 && ExistingTime >= Duration)
                BuffEvent.RemoveBuff.InvokeEvent(Unit,EBuff,float.MaxValue);
        }
        void Execute()
        {
            OnExecute();
        }
        void Stack(float count)
        {
            Magnification = Mathf.Min(Magnification + count, MaxMagnification);
            OnStack();
        }
        void Unstack(float count)
        {
            if (Magnification > count)
            {
                Magnification += count;
                OnUnstack();
            }
            else
                BuffEvent.RemoveBuff.InvokeEvent(Unit,EBuff,float.MaxValue);
        }

        void OnInit(IBuffAddData data);
        void OnDeInit();
        void OnRunning();
        void OnExecute();
        void OnStack();
        void OnUnstack();
    }

    public interface IIntervalTriggerBuff: IBuff
    {
        float Interval { get;}
        protected CoroutineHandle ExecuteHandle { get; set; }

        void IBuff.Init(IBuffAddData data)
        {
            Unit = data.Unit;
            EBuff = data.EBuff;
            Magnification = Mathf.Min(data.Magnification, MaxMagnification);
            IsDeInit = false;
            ExecuteHandle =CoroutineHandle.Fetch(); 
            OnInit(data);
            EasyTask.Seconds(ExecuteHandle,Interval,Duration, _=>Execute());
        }

        void IBuff.DeInit()
        {
            if(IsDeInit)
                return;
            
            OnDeInit();
            ExecuteHandle.Cancel();
            ExecuteHandle = null;
            Unit = null;
            EBuff = EBuff.None;
            Magnification = 0;
            ExistingTime = 0;
            IsDeInit = true;
        }

        void IBuff.Running(float dateTime)
        {
            ExistingTime += dateTime;
            if (Duration==0||Duration > 0 && ExistingTime >= Duration)
                BuffEvent.RemoveBuff.InvokeEvent(Unit,EBuff,float.MaxValue);
        }
    }


    public interface IConditionalTriggerBuff : IBuff
    {
        int TriggerCount { get; protected set; }
        int MaxTriggerCount { get; }

        void IBuff.DeInit()
        {
            if(IsDeInit)
                return;
            
            OnDeInit();
            Unit = null;
            EBuff = EBuff.None;
            Magnification = 0;
            ExistingTime = 0;
            TriggerCount = 0;
            IsDeInit = true;
        }
        void IBuff.Running(float dateTime)
        {
            ExistingTime += dateTime;
            if (Duration==0||Duration > 0 && ExistingTime >= Duration)
                BuffEvent.RemoveBuff.InvokeEvent(Unit,EBuff,float.MaxValue);
        }

        void IBuff.Execute()
        {
            if (TriggerCount < MaxTriggerCount)
            {
                OnExecute();
                TriggerCount++;
            }
            else
                BuffEvent.RemoveBuff.InvokeEvent(Unit,EBuff,float.MaxValue);
        }
    }
}