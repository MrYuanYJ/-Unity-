using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EasyFramework
{
    [Serializable]
    public partial class BlackBoard : ISerializationCallbackReceiver
    {
        [SerializeField] public List<BBValueView> view = new();
        private Dictionary<string, IBBValue> _dic = new();
        internal static Dictionary<Type, Type> ValueTypeMap = new();
        static BlackBoard()
        {
            var bbValueTypes = GetAllBBValueType();
            foreach (var t in bbValueTypes)
                ValueTypeMap[t.BaseType.GetGenericArguments()[0]] = t;
        }
        public static List<Type> GetAllBBValueType()
        {
            List<Type> bbValueTypes = new();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == "EasyFramework" ||
                    assembly.GetReferencedAssemblies()
                        .Any(targetAssembly => targetAssembly.Name == "EasyFramework"))
                {
                    var q = assembly.GetTypes()
                        .Where(x =>
                            !x.IsAbstract
                            && !x.IsGenericTypeDefinition
                            && x.BaseType != null
                            && x.BaseType.IsGenericType
                            && typeof(BBValue<>).MakeGenericType(x.BaseType.GetGenericArguments()[0])
                                .IsAssignableFrom(x));
                    bbValueTypes.AddRange(q);
                }
            }
            return bbValueTypes;
        }
        public static Type GetBBValueType(Type type)
        {
            ValueTypeMap.TryGetValue(type, out Type bbValueType);

            if (bbValueType == null)
                bbValueType = ValueTypeMap[typeof(object)];
             
            return bbValueType;
        }
        
        public void OnBeforeSerialize()
        {
            view.Clear();
            foreach (var key in _dic.Keys)
            {
                var bbValue = _dic[key];
                view.Add(new BBValueView()
                {
                    key = key,
                    serializedValue = (ScriptableObject)bbValue
                });
            }
        }
        public void OnAfterDeserialize()
        {
            _dic.Clear();
            for (int i = 0; i < view.Count; i++)
            {
                var value = view[i].serializedValue;
                _dic[view[i].key] = (IBBValue)value;
            }
        }
        public void Add<T>(string key, T value)
        {
            var bbValue = (IBBValue)ScriptableObject.CreateInstance(GetBBValueType(typeof(T)));
            if(bbValue is BBValue<T> bbValueT)
                bbValueT.value = value;
            else
                bbValue.Value = value;
            _dic[key] = bbValue;
        }

        public void Add(string key, object value)
        {
            var bbValue = (IBBValue)ScriptableObject.CreateInstance(GetBBValueType(value.GetType()));
            bbValue.Value = value;
            _dic[key] = bbValue;
        }
        public void Add(string key, Type valueType)
        {
            var bbValue = (IBBValue)ScriptableObject.CreateInstance(GetBBValueType(valueType));
            _dic[key] = bbValue;
        }

        public void AddByBBValueType(string key, Type bbValueType)
        {
            var bbValue = (IBBValue)ScriptableObject.CreateInstance(bbValueType);
            _dic[key] = bbValue;
        }
        public Object GetBBValue(string key)=>(Object)_dic[key];
        public T Get<T>(string key)
        {
            if (_dic[key] is BBValue<T> bbValueT)
                return bbValueT.value;
            return (T)_dic[key].Value;
        }
        public object Get(string key)
        {
            return _dic[key].Value;
        }
        public bool TryGetValue<T>(string key, out T value)
        {
            if (_dic.TryGetValue(key, out var bbValue))
            {
                value = ((BBValue<T>)bbValue).value;
                return true;
            }
            value = default;
            return false;
        }
        public bool TryGetValue(string key, out object value)
        {
            if (_dic.TryGetValue(key, out var bbValue))
            {
                value = bbValue.Value;
                return true;
            }
            value = default;
            return false;
        }
        public void Remove(string key)
        {
            _dic.Remove(key);
        }

        public bool HasKey(string key)
        {
            return _dic.ContainsKey(key);
        }
    }
}