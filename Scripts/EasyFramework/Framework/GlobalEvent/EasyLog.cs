
using UnityEngine;

namespace EasyFramework
{
    public struct EasyLog
    {
        public sealed class LogEvent : AEventIndex<LogEvent, LogType, object>{}

        public static void Log(object message, LogType logType = LogType.Log)
        {
            LogEvent.InvokeEvent(logType, message);
        }
    }
}