using UnityEditor;
using UnityEngine;

namespace EasyFramework
{
    public partial interface IBBValue
    {
        internal SerializedObject SerializedObject { get; }
    } 
}