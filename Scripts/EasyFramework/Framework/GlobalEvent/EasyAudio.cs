using System.Collections;
using UnityEngine;

namespace EasyFramework
{
    public struct EasyAudio
    {
        public sealed class PlayAudio<T> : AFuncIndex<PlayAudio<T>,T, string, bool,bool, IEnumerator> { }
        
    }
}