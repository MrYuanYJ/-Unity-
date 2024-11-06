using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EasyFramework
{
    public class BBProperty: AMonoEntityCarrier
    {
        public BlackBoard blackBoard = new();
        /*[ShowIf("@IsInit==false")]
        public List<BBPropertyInitData> InitData= new();

#if UNITY_EDITOR
        [ShowIf("@IsInit==true")] [ListDrawerSettings(ListElementLabelName = "Key")]
        public List<BBPropertyData> DataView = new();
#endif*/
    }

    [System.Serializable]
    public struct BBPropertyInitData
    {
        public string Key;
        public int BaseValue;
        [Range(0,1)]
        public float InitPercent;
    }
    [System.Serializable]
    public struct BBPropertyData
    {
        [HideInInspector]
        public string Key;
        public RoleProperties Value;
    }
}