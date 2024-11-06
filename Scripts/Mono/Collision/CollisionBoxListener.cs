using System;
using UnityEngine;

namespace EasyFramework
{
    public class CollisionBoxListener: MonoBehaviour
    {
        public ColliderBaseData baseData;
        public ColliderData data;
        public IUnitEntity belongEntity;
        public readonly EasyEvent<CollisionBoxListener, CollisionBoxListener> OnEnter = new();
        public readonly EasyEvent<CollisionBoxListener, CollisionBoxListener> OnExit = new();

        public void Init(ColliderBaseData baseData, ColliderData data, IUnitEntity belongEntity)
        {
            this.baseData = baseData;
            this.data = data;
            this.belongEntity = belongEntity;
        }

        private void OnEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CollisionBoxListener otherListener))
                OnEnter.Invoke(this, otherListener);
        }
        private void OnExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out CollisionBoxListener otherListener))
                OnExit.Invoke(this, otherListener);
        }
        private void OnEnter3D(Collider other)
        {
            if (other.TryGetComponent(out CollisionBoxListener otherListener))
                OnEnter.Invoke(this, otherListener);
        }
        private void OnExit3D(Collider other)
        {
            if (other.TryGetComponent(out CollisionBoxListener otherListener))
                OnExit.Invoke(this, otherListener);
        }

        private void OnTriggerEnter2D(Collider2D other)=>OnEnter2D(other);
        private void OnTriggerExit2D(Collider2D other)=>OnExit2D(other);
        private void OnCollisionEnter2D(Collision2D other)=>OnEnter2D(other.collider);
        private void OnCollisionExit2D(Collision2D other)=>OnExit2D(other.collider);
        
        
        private void OnTriggerEnter(Collider other)=>OnEnter3D(other);
        private void OnTriggerExit(Collider other)=>OnExit3D(other);
        private void OnCollisionEnter(Collision collision)=>OnEnter3D(collision.collider);
        private void OnCollisionExit(Collision collision)=>OnExit3D(collision.collider);
    }
}