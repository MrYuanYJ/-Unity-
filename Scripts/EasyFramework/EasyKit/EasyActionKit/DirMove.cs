using System;
using UnityEngine;

namespace EasyFramework
{
    public class DirMove: AEasyProgressAndLerpAndExLoopAction<Vector3>
    {
        private DirMove()
        {
            ActionID = EasyActionSingleton.GetActionId();
        }
        private static EasyPool<DirMove> _pool = new(() => new DirMove(), null, null);

        public static DirMove Fetch(Transform transform, Vector3 dir, float duration,int loopCount,LoopType loopType)
        {
            var dirMove = _pool.Fetch();
            dirMove._onUpdate = pos => dirMove._transform.localPosition = pos;
            dirMove._transform = transform;
            dirMove.targetDir = dir;
            dirMove.Reset(duration,loopCount,default,default,loopType);
            return dirMove;
        }
        
        protected Action<Vector3> _onUpdate;
        protected Transform _transform;
        protected Vector3 targetDir;
        
        
        protected override void OnActionEnd(){}

        protected override bool OnActionUpdate(float deltaTime)
        {
            Progress = Mathf.Clamp(RunTime / Duration, 0, MaxProgress);
            _onUpdate?.Invoke(GetValue());
            if (IsReverse)
            {
                return Progress <= 0;
            }
            else
            {
                if (Progress >= MaxProgress)
                {
                    if (LoopType == LoopType.YoYo)
                    {
                        Progress = MaxProgress;
                        RunTime = 0;
                        IsReverse = true;
                        StartValue = _transform.localPosition;
                        EndValue = StartValue - targetDir;
                        return false;
                    }
                    else return true;
                }

                return false;
            }
        }

        protected override void OnActionCompleted()
        {
            StartValue = _transform.localPosition;
            EndValue = StartValue + targetDir;
            if (LoopType == LoopType.ReStart)
                RunTime = 0;
            else if (LoopType == LoopType.YoYo)
            {
                RunTime = 0;
                IsReverse =false;
            }
            else if (LoopType == LoopType.Incremental)
            {
                MaxProgress++;
            }
        }

        protected override void OnActionCancel(){}

        public override Vector3 GetValue()
        {
            return Vector3.LerpUnclamped(StartValue, EndValue, Ease.Evaluate(Progress));
        }
    }

    public static class DirMoveExtension
    {
        public static DirMove MoveTo(this Transform transform, Vector3 dir, float duration, int loopCount = 1,
            LoopType loopType = LoopType.ReStart)
        {
            return EasyFramework.DirMove.Fetch(transform, dir, duration, loopCount, loopType);
        }

        public static Sequence DirMove(this Sequence self, Transform transform, Vector3 dir, float duration,Action<DirMove> setup = null)
        {
            var dirMove = EasyFramework.DirMove.Fetch(transform, dir, duration, 1, LoopType.ReStart);
            setup?.Invoke(dirMove);
            return self.Append(dirMove);
        }
    }
}