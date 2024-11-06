using UnityEditor;

namespace EasyFramework
{
    public abstract partial class BBValue<T>
    {
        internal SerializedObject _serializedObject;

        SerializedObject IBBValue.SerializedObject
        {
            get
            {
                if (_serializedObject == null)
                {
                    _serializedObject = new SerializedObject(this);
                }
                return _serializedObject;
            }
        }
    }
}