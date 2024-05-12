 using System;
 using UnityEngine;
 using UnityEngine.PlayerLoop;

namespace EasyECS
{
    public abstract class AEntity : AbstractContainer, IEntity
    {
        public long EntityID { get; set; }
        public Action OnDisposed { get; set; }

        public AEntity()
        {
            EntityMgr.AddEntityToDic( this );
        }

        public abstract void Init();
        public abstract void OnDispose();
    }
    public abstract class AMonoEntity : AMonobstractContainer, IEntity
    {
        public long EntityID { get; set; }
        public Action OnDisposed { get; set; }
        public bool isDispose { get; set; } = true;

        protected void OnEnable()
        {
            if ( isDispose )
            {
                isDispose = false;
                EntityMgr.AddEntityToDic( this );
            }

            this.OnMonoEnable();
        }

        protected void OnDestroy()
        {
            if ( !isDispose )
            {
                ( (IEntity) this ).Dispose();
            }
            this.OnMonoDestroy();
        }

        protected virtual void OnMonoEnable() { }
        protected virtual void OnMonoDestroy() { }
        public abstract void Init();

        public abstract void OnDispose();
    }
}