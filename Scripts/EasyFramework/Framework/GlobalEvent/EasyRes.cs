using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    public struct EasyRes
    {
        public sealed class GetAssetPath: AFuncIndex<GetAssetPath,Object,string>{}
        public sealed class LoadAssetAsync : AFuncIndex<LoadAssetAsync,Type, string, bool, IEnumerator> { }
        public sealed class LoadPrefabByTypeAsync: AFuncIndex<LoadPrefabByTypeAsync,Type,bool,IEnumerator> { }
        public sealed class LoadPrefabByPathAsync: AFuncIndex<LoadPrefabByPathAsync,string,bool,IEnumerator> { }
        
        
        public sealed class FetchAsset: AFuncIndex<FetchAsset,string, Object> { }
        public sealed class FetchGObject: AFuncIndex<FetchGObject,string, GameObject> { }
        public sealed class FetchComponent: AFuncIndex<FetchComponent,Type, Component> { }
        public sealed class PoolGObject<T>: AFuncIndex<PoolGObject<T>,string,GameObject> { }
        
        
        public sealed class RecycleAsset: AEventIndex<RecycleAsset,Object> { }
        public sealed class RecycleGo: AEventIndex<RecycleGo,GameObject> { }
        public sealed class RecycleComponent: AEventIndex<RecycleComponent,Component> { }
        
        
        public sealed class ReleaseAsset : AEventIndex<ReleaseAsset,string> { }
        public sealed class ReleaseGObject : AEventIndex<ReleaseGObject,string> { }
        public sealed class ReleaseAll : AEventIndex<ReleaseAll> { }
    }
}