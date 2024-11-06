using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace EasyFramework.EasySystem
{
    [System.Serializable]
    public struct RandomPathDataView
    {
        public string mainPath;
        public string[] additionalInformation;
    }
    
    public class RandomPathData
    {
        public string mainPath;
        public List<string> additionalInformation;
        private string previousPath;

        public string GetRandomPath()
        {
            if (additionalInformation == null || additionalInformation.Count == 0)
                return mainPath;

            var random = additionalInformation[EasyRandom.RandomInt(0, additionalInformation.Count)];
            if (!previousPath.IsNullOrWhitespace())
                additionalInformation.Add(previousPath);
            additionalInformation.Remove(random);
            previousPath = random;
            return $"{mainPath}{random}";
        }
    }
}