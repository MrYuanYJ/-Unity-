#if UNITY_EDITOR

using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


namespace EasyFramework
{
    public abstract partial class AMonoEntityCarrier
    {
        [SerializeField][ShowIf("@childrenTrans != null")] private Transform childrenTrans;

        protected virtual void OnValidate()
        {
            ReFreshChildren();
            if (transform.parent != null)
                foreach (var aMonoEntity in transform.parent.GetComponents<AMonoEntityCarrier>())
                    aMonoEntity.ReFreshChildren();
            foreach (var aMonoEntity in transform.GetComponents<AMonoEntityCarrier>())
                aMonoEntity.ReFreshChildren();
        }

        private void ReFreshChildren()
        {
            foreach (var child in children)
            {
                if (child == null)
                {
                    children.Remove(child);
                    ReFreshChildren();
                    break;
                }
                child.parent = null;
            }
            children.Clear();
            if(childrenTrans == null)
                return;
            if (childrenTrans.parent != transform)
            {
                childrenTrans = null;
                throw new System.Exception($"[{GetType().Name}] childrenTrans must be a child of [{transform.name}]\n[{GetType().Name}]的子级必须是其Transform的子级");
            }
            foreach (var aMonoEntity in transform.GetComponents<AMonoEntityCarrier>())
                if (aMonoEntity!= this &&aMonoEntity.childrenTrans == childrenTrans)
                {
                    childrenTrans = null;
                    throw new System.Exception($"[{GetType().Name}] can not have same children with [{aMonoEntity.GetType().Name}]\n[{GetType().Name}]不能和[{aMonoEntity.GetType().Name}]有相同的子级");
                }

            var childs = childrenTrans.GetComponents<AMonoEntityCarrier>();
            foreach (var child in childs)
            {
                child.parent = this;
                if (child.enabled)
                    children.Add(child);
            }
        }

        [ButtonGroup("Buttons")]
        [ShowIf("@childrenTrans == null")]
        private void CreateChildrenTrans()
        {
            var go = new GameObject( $"{GetType().Name}->");
            if (parent == null)
                go.SetActive(false);
            go.transform.SetParent(transform, false);
            childrenTrans = go.transform;
            Selection.activeObject= childrenTrans.gameObject;
        }
        
        [ButtonGroup("Buttons")]
        [ShowIf("@childrenTrans == null")]
        private void FindChildrenTrans()
        {
            var childs = transform.Find($"{GetType().Name}->");
            if (childs != null)
            {
                childrenTrans = childs;
                childrenTrans.gameObject.SetActive(false);
                ReFreshChildren();
            }
        }
        [MenuItem("CONTEXT/AMonoEntityCarrier/Remove Component")]
        public static void RemoveMono(MenuCommand menuCommand)
        {
            var mono = menuCommand.context as AMonoEntityCarrier;
            if (mono != null)
            {
                var transform = mono.transform;
                DestroyImmediate(mono);
                if (transform.parent != null)
                    foreach (var aMonoEntity in transform.parent.GetComponents<AMonoEntityCarrier>())
                        aMonoEntity.ReFreshChildren();
            }
        }
    }
}
#endif