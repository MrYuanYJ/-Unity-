using System;
using EXFunctionKit;
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyFramework.EasyUIKit
{
    public abstract class AEasyFollowUI: AEasyUI
    {
        private Vector3 _followPosition;
        [SerializeField]private Vector3 offset;

        public bool follow = true;

        public Vector3 FollowPosition
        {
            get => _followPosition;
            set
            {
                if (follow&&_followPosition != value)
                {
                    _followPosition = value;
                    transform.ModifyPosition(_followPosition.x + offset.x, _followPosition.y + offset.y);
                }
            }
        }
        public void SetOffset(Vector3 offset)
        {
            this.offset = offset;
        }
        protected abstract Vector3 GetFollowPosition();

        protected virtual void Update()
        {
            if (follow)
                FollowPosition = GetFollowPosition();
        }
    }
}