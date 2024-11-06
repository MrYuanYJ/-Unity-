using System;
using EasyFramework.EasySystem;
using UnityEngine;

namespace EasyFramework
{
    [Serializable]
    public struct TestArray
    {
        public float v;
    }
    [Menu("Unity Engine/TestArray")]
    public class BBArray: BBValue<TestArray>
    {
        
    }
}