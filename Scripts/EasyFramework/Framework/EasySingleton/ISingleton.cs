using System;
using System.Reflection;
using UnityEngine;

namespace EasyFramework
{
    public interface ISingleton
    {
        public T Get<T>() where T : class;
    }

    public interface ISingleton<T> : ISingleton where T : class
    {
        public static T Instance;
    }
}