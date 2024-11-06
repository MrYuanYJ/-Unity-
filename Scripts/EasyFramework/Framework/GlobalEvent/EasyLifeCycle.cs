

using System;

namespace EasyFramework
{
    public struct EasyLifeCycle
    {
        public sealed class InitDo : AEventIndex<InitDo, IInitAble> { }
        public sealed class DisposeDo : AEventIndex<DisposeDo, IDisposeAble> { }
        public sealed class Update: AEventIndex<Update>{}
        public sealed class FixedUpdate: AEventIndex<FixedUpdate>{}
        public sealed class WaitEndOfFrame: AEventIndex<WaitEndOfFrame,Action>{}
    }
}