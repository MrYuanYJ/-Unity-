namespace EasyFramework
{
    public static class EasyTime
    {
        public static float GameSpeed = 1;
        
        public static float DeltaTime=> GetDeltaTime.InvokeFunc() * GameSpeed;
        public static float UnscaledDeltaTime => GetUnscaledDeltaTime.InvokeFunc();

        public static float FixedDeltaTime => GetFixedDeltaTime.InvokeFunc() * GameSpeed;
        public static float UnscaledFixedDeltaTime => GetUnscaledFixedDeltaTime.InvokeFunc();

        
        public sealed class GetDeltaTime : AFuncIndex<GetDeltaTime, float>{}
        public sealed class GetUnscaledDeltaTime : AFuncIndex<GetUnscaledDeltaTime, float>{}
        public sealed class GetFixedDeltaTime : AFuncIndex<GetFixedDeltaTime, float>{}
        public sealed class GetUnscaledFixedDeltaTime : AFuncIndex<GetUnscaledFixedDeltaTime, float>{}
        
    }
}