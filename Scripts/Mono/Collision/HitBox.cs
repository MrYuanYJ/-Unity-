using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyFramework
{
    [System.Serializable]
    public struct ColliderBaseData
    {
        public string groupName;
        public ERelationship checkRelationship;
    }
    [System.Serializable]
    public struct ColliderData
    {
        public Collider collider;
        public Collider2D collider2D;
        [Range(-1, 1)]
        public float weight;
    }
    [System.Serializable]
    public struct ColliderGroupData
    {
        public ColliderBaseData colliderBaseData;
        public List<ColliderData> colliderDataLst;
    }

    public class HitBox: AMonoEntityCarrier
    {
        public List<ColliderGroupData> colliderData;
    }
            
}