using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace EasyFramework.Editor
{
    public interface IDataView
    {
        string Key { get; }
        object Value { get; }
    }

    public interface IDataView<T> : IDataView
    {
        object IDataView.Value => Value;
        new T Value { get; }
    }

    public interface IDataViewList: IList
    {
        void Add(IDataView dataView) => ((IList)this).Add(dataView);

        void AddDefault(string key);
        //void ShowBySearchPopup(Rect activatorRect,T selectedData,DataViewList<T> dataList,Action<T> onSelected)
    }
    public interface IDataViewList<T> : IDataViewList
    {
        void IDataViewList.Add(IDataView dataView) => Add((IDataView<T>) dataView);
        void Add(IDataView<T> dataView);
        void Add(T value);
    }
    public struct DataView<T>: IDataView<T>
    {
        private string _key;
        private T _value;
        public string Key => _key;
        public T Value => _value;
        public DataView(string key, T value)
        {
            _key = key;
            _value = value;
        }
        public override string ToString() => Key;
        public override bool Equals(object obj) => Equals((DataView<T>) obj);
        public bool Equals(DataView<T> other)
        {
            return Key == other.Key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Value);
        }


    }
    public class DataViewList<T>: List<DataView<T>>,IDataViewList<T>
    {
        public void Add(string text, T value) => ((List<DataView<T>>)this).Add(new DataView<T>(text, value));
        public void Add(IDataView<T> dataView)=>((List<DataView<T>>)this).Add(new DataView<T>(dataView.Key, dataView.Value));
        public void Add(T value)
        {
            string GetKey(T value)
            {
                if(value is Object obj)
                    return obj.name;
                else
                    return value.ToString();
            }
            ((List<DataView<T>>) this).Add(new DataView<T>(GetKey(value), value));
        }

        public void AddDefault(string key)=> ((List<DataView<T>>)this).Add(new DataView<T>(key, default(T)));
    }

    public static class DataViewListExtensions
    {
        internal static DataViewList<T> ToDataViewList<T>(this IEnumerable<T> enumerable)
        {
            var list =new DataViewList<T>();
            foreach (var item in enumerable)
            {
                list.Add(item);
            }
            return list;
        }
        internal static DataViewList<object> ToDataViewList(this IEnumerable enumerable)
        {
            var list = new DataViewList<object>();
            foreach (var item in enumerable)
            {
                list.Add(item);
            }
            return list;
        }
        internal static DataViewList<Enum> ToDataViewList(this Enum enumValue)
        {
            var list = new DataViewList<Enum>();
            foreach (Enum item in Enum.GetValues(enumValue.GetType()))
            {
                list.Add(item);
            }
            return list;
        }
    }
}