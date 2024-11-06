

namespace EasyFramework
{
    public static class EasyLifeEx
    {
        internal static void EnableLifeCycle(object obj)
        {
            if (obj is IStartAble start && !start.IsStart)
                IStartAble.EnableStartAble(start);
            if (obj is IFixedUpdateAble fixedUpdate)
                IFixedUpdateAble.EnableFixedUpdateAble(fixedUpdate);
            if (obj is IUpdateAble update)
                IUpdateAble.EnableUpdateAble(update);
        }

        internal static void DisableLifeCycle(object obj)
        {
            if (obj is IStartAble start && !start.IsStart)
                IStartAble.DisableStartAble(start);
            if (obj is IFixedUpdateAble fixedUpdate)
                IFixedUpdateAble.DisableFixedUpdateAble(fixedUpdate);
            if (obj is IUpdateAble update)
                IUpdateAble.DisableUpdateAble(update);
        }
    }
}