using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EXFunctionKit
{
    public static partial class ExUnity
    {
        public static bool AsBool(this Vector2 self) => self!= Vector2.zero;
        public static bool AsBool(this Vector3 self) => self!= Vector3.zero;
        public static bool AsBool(this Vector4 self) => self!= Vector4.zero;
        public static bool AsBool(this Vector2Int self) => self!= Vector2Int.zero;
        public static bool AsBool(this Vector3Int self) => self!= Vector3Int.zero;
        public static bool AsBool(this Quaternion self) => self != Quaternion.identity;
        public static bool AsBool(this Color self) => self != Color.clear;
        public static bool InRange(this Vector2 self, Vector2 min, Vector2 max) => self.x >= min.x && self.x < max.x && self.y >= min.y && self.y < max.y;
        public static bool InRange(this Vector3 self, Vector3 min, Vector3 max) => self.x >= min.x && self.x < max.x && self.y >= min.y && self.y < max.y && self.z >= min.z && self.z < max.z;
        public static bool InRange(this Vector2Int self, Vector2Int min, Vector2Int max) => self.x >= min.x && self.x < max.x && self.y >= min.y && self.y < max.y;
        public static bool InRange(this Vector3Int self, Vector3Int min, Vector3Int max) => self.x >= min.x && self.x < max.x && self.y >= min.y && self.y < max.y && self.z >= min.z && self.z < max.z;
    
        
        public static Vector2 NewV2(float x = 0, float y = 0) => new Vector2(x, y);
        public static Vector2 AsVector2(this (float x, float y) self) => new Vector2(self.x, self.y);
        public static Vector2 AsVector2(this Vector3 self) => self;
        public static Vector2 AsVector2(this Vector4 self) => self;
        public static Vector2 AsVector2(this Vector2Int self) => self;
        public static Vector2 AsVector2(this Vector3Int self) => new Vector2(self.x, self.y);

        public static Vector2 Modify(this Vector2 self, float? x=null, float? y=null) => new Vector2(x ?? self.x, y ?? self.y);
        public static Vector2 Add(this Vector2 self, float x = 0, float y = 0) => new Vector2(self.x + x, self.y + y);
        public static Vector2 Multiply(this Vector2 self, float x = 1, float y = 1) => new Vector2(self.x * x, self.y * y);
        public static Vector2 Reflect(this Vector2 self, Vector2 normal) => Vector2.Reflect(self, normal);
        public static Vector2 DirectionTo(this Vector2 self, Vector2 target) => target - self;
        public static Vector2 DirectionFrom(this Vector2 self, Vector2 target) => self - target;

        public static Vector2 Bezier(float t, params Vector2[] controlPoints)
        {
            int n = controlPoints.Length - 1;
            Vector2 result = Vector2.zero;

            for (int i = 0; i <= n; i++)
            {
                result += BinomialCoefficient(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * controlPoints[i];
            }

            return result;
        }
        
        public static Vector3 Bezier(float t, params Vector3[] controlPoints)
        {
            int n = controlPoints.Length - 1;
            Vector3 result = Vector3.zero;

            for (int i = 0; i <= n; i++)
            {
                result += BinomialCoefficient(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * controlPoints[i];
            }

            return result;
        }

        private static int BinomialCoefficient(int n, int k)
        {
            if (k < 0 || k > n) return 0;
            int coeff = 1;
            for (int i = 1; i <= k; i++)
            {
                coeff *= (n - (k - i));
                coeff /= i;
            }
            return coeff;
        }

        public static Vector2 MoveTowards(this float self, Vector2 a, Vector2 b) => Vector2.MoveTowards(a, b, self);
        public static Vector2 LerpVector2(this float self, Vector2 a, Vector2 b) => Vector2.Lerp(a, b, self);

        public static Vector2 LerpVector2(this float self, Vector2 a, Vector2 b, params Vector2[] c)
        {
            var list=c.ToList();
            list.Add(a);
            list.Add(b);
            return Bezier(Mathf.Clamp01(self), list.ToArray());
        }
        
        public static Vector2 LerpVector2Unclamped(this float self, Vector2 a, Vector2 b) => Vector2.LerpUnclamped(a, b, self);
        public static Vector2 LerpVector2Unclamped(this float self, Vector2 a, Vector2 b, params Vector2[] c)
        {
            var list=c.ToList();
            list.Add(a);
            list.Add(b);
            return Bezier(self, list.ToArray());
        }
        public static Vector3 MoveTowards(this float self, Vector3 a, Vector3 b) => Vector3.MoveTowards(a, b, self);
        public static Vector3 LerpVector3(this float self, Vector3 a, Vector3 b) => Vector3.Lerp(a, b, self);
        public static Vector3 LerpVector3(this float self, Vector3 a, Vector3 b, params Vector3[] c)
        {
            var list=c.ToList();
            list.Add(a);
            list.Add(b);
            return Bezier(Mathf.Clamp01(self), list.ToArray());
        }
        public static Vector3 LerpVector3Unclamped(this float self, Vector3 a, Vector3 b) => Vector3.LerpUnclamped(a, b, self);
        public static Vector3 LerpVector3Unclamped(this float self, Vector3 a, Vector3 b, params Vector3[] c)
        {
            var list=c.ToList();
            list.Add(a);
            list.Add(b);
            return Bezier(self, list.ToArray());
        }
        public static Color LerpColor(this float self, Color a, Color b) => Color.Lerp(a, b, self);
        public static Color LerpColorUnclamped(this float self, Color a, Color b) => Color.LerpUnclamped(a, b, self);

        public static Vector3 NewV3(float x = 0, float y = 0, float z = 0) => new Vector3(x, y, z);
        public static Vector3 AsVector3(this (float x, float y) self) => new Vector3(self.x, self.y);
        public static Vector3 AsVector3(this (float x, float y, float z) self) => new Vector3(self.x, self.y, self.z);
        public static Vector3 AsVector3(this Vector2 self) => self;
        public static Vector3 AsVector3(this Vector2 self, float z) => new Vector3(self.x, self.y, z);
        public static Vector3 AsVector3(this Vector4 self) => self;
        public static Vector3 AsVector3(this Vector2Int self) => new Vector3(self.x, self.y);
        public static Vector3 AsVector3(this Vector2Int self, float z) => new Vector3(self.x, self.y, z);
        public static Vector3 AsVector3(this Vector3Int self) => self;

        public static Vector3 Modify(this Vector3 self, float? x=null, float? y=null,float? z=null) => new Vector3(x ?? self.x, y ?? self.y,z?? self.z);
        public static Vector3 Add(this Vector3 self, float x=0, float y=0,float z=0) => new Vector3(self.x+x, self.y+y, self.z+z);
        public static Vector3 Multiply(this Vector3 self, float x=1, float y=1,float z=1) => new Vector3(self.x*x, self.y*y, self.z*z);
        public static Vector3 Reflect(this Vector3 self, Vector3 normal) => Vector3.Reflect(self, normal);
        public static Vector3 DirectionTo(this Vector3 self, Vector3 target) => target - self;
        public static Vector3 DirectionFrom(this Vector3 self, Vector3 target) => self - target;

        public static Vector4 NewV4(float x = 0, float y = 0, float z = 0, float w = 0) => new Vector4(x, y, z);
        public static Vector4 AsVector4(this (float x, float y) self) => new Vector4(self.x, self.y);
        public static Vector4 AsVector4(this (float x, float y, float z) self) => new Vector4(self.x, self.y, self.z);
        public static Vector4 AsVector4(this (float x, float y, float z, float w) self) => new Vector4(self.x, self.y, self.z, self.w);
        public static Vector4 AsVector4(this Vector2 self) => self;
        public static Vector4 AsVector4(this Vector2 self, float z, float w) => new Vector4(self.x, self.y, z, w);
        public static Vector4 AsVector4(this Vector3 self) => self;
        public static Vector4 AsVector4(this Vector3 self, float w) => new Vector4(self.x, self.y, self.z, w);
        public static Vector4 Modify(this Vector4 self, float? x=null, float? y=null,float? z=null,float? w=null) => new Vector4(x ?? self.x, y ?? self.y,z?? self.z,w??self.w);
        public static Vector4 Add(this Vector4 self, float x=0, float y=0,float z=0,float w=0) => new Vector4(self.x+x, self.y+y,self.z+z,self.w+w);
        public static Vector4 Multiply(this Vector4 self, float x=1, float y=1,float z=1,float w=1) => new Vector4(self.x*x, self.y*y, self.z*z, self.w*w);
        
        public static Vector2Int NewV2Int(int x = 0, int y = 0) => new Vector2Int(x, y);
        public static Vector2Int AsVector2Int(this (int x, int y) self) => new Vector2Int(self.x, self.y);
        public static Vector2Int AsVector2Int(this Vector2 self) => new Vector2Int(self.x.AsInt(), self.y.AsInt());
        public static Vector2Int AsVector2Int(this Vector3 self) => new Vector2Int(self.x.AsInt(), self.y.AsInt());
        public static Vector2Int AsVector2Int(this Vector4 self) => new Vector2Int(self.x.AsInt(), self.y.AsInt());
        public static Vector2Int AsVector2Int(this Vector3Int self) => (Vector2Int)self;
        public static Vector2Int Modify(this Vector2Int self, int? x=null, int? y=null) => new Vector2Int(x ?? self.x, y ?? self.y);
        public static Vector2Int Add(this Vector2Int self, int x=0, int y=0) => new Vector2Int(self.x+x, self.y+y);
        public static Vector2Int Multiply(this Vector2Int self, int x=1, int y=1) => new Vector2Int(self.x*x, self.y*y);
        
        public static Vector3Int NewV3Int(int x = 0, int y = 0, int z = 0) => new Vector3Int(x, y, z);
        public static Vector3Int AsVector3Int(this (int x, int y) self) => new Vector3Int(self.x, self.y);
        public static Vector3Int AsVector3Int(this Vector2 self) => new Vector3Int(self.x.AsInt(), self.y.AsInt());
        public static Vector3Int AsVector3Int(this Vector3 self) => new Vector3Int(self.x.AsInt(), self.y.AsInt(), self.z.AsInt());
        public static Vector3Int AsVector3Int(this Vector4 self) => new Vector3Int(self.x.AsInt(), self.y.AsInt(), self.z.AsInt());
        public static Vector3Int AsVector3Int(this Vector2Int self) => (Vector3Int)self;
        public static Vector3Int AsVector3Int(this Vector2Int self, int z) => new Vector3Int(self.x, self.y, z);
        public static Vector3Int Modify(this Vector3Int self, int? x=null, int? y=null, int? z=null) => new Vector3Int(x ?? self.x, y ?? self.y,z??self.z);
        public static Vector3Int Add(this Vector3Int self, int x=0, int y=0, int z=0) => new Vector3Int(self.x+x, self.y+y,self.z+z);
        public static Vector3Int Multiply(this Vector3Int self, int x=1, int y=1, int z=1) => new Vector3Int(self.x*x, self.y*y, self.z*z);

        
        public static T Component<T>(this GameObject self) where T : Component
        {
            if (!self.TryGetComponent<T>(out var component)) { return self.AddComponent<T>(); }

            return component;
        }
        public static T Component<T>(this Component self) where T : Component
        {
            if (!self.gameObject.TryGetComponent<T>(out var component)) { return self.gameObject.AddComponent<T>(); }

            return component;
        }

        public static Component Component(this GameObject self, Type type)
        {
            if (!self.TryGetComponent(type, out var component)) { return self.AddComponent(type); }

            return component;
        }
        public static Component Component(this Component self, Type type)
        {
            if (!self.gameObject.TryGetComponent(type, out var component)) { return self.gameObject.AddComponent(type); }

            return component;
        }



        public static Component[] GetAllComponents(this GameObject self, bool findChildren = false)
        {
            if (!findChildren)
                return self.GetComponents<Component>();
            return self.GetComponentsInChildren<Component>();
        }
        public static Component[] GetAllComponents(this Component self, bool findChildren = false)
        {
            if (!findChildren)  
                return self.gameObject.GetComponents<Component>();
            return self.gameObject.GetComponentsInChildren<Component>();
        }
        public static void RemoveComponent<T>(this GameObject self) where T : Component => Object.Destroy(self.GetComponent<T>());
        public static void RemoveComponent<T>(this Component self) where T : Component => Object.Destroy(self.GetComponent<T>());
        public static void RemoveComponents<T>(this GameObject self) where T : Component
        {
            T[] components = self.GetComponents<T>();
            foreach (T component in components)
                Object.Destroy(component);
        }
        public static void RemoveComponents<T>(this Component self) where T : Component
        {
            T[] components = self.GetComponents<T>();
            foreach (T component in components)
                Object.Destroy(component);
        }

        public static GameObject Show(this GameObject self)
        {
            self.SetActive(true);
            return self;
        }
        public static GameObject Hide(this GameObject self)
        {
            self.SetActive(false);
            return self;
        }

        public static bool IsInLayerMask(this GameObject self, LayerMask layerMask)
        {
            int layer = 1 << self.layer;
            if ((layer & layerMask.value) == self.layer)
                return true;
            return false;
        }

        public static bool IsInLayerMask(this Component self, LayerMask layerMask)
        {
            int layer = 1 << self.gameObject.layer;
            if ((layer & layerMask.value) == self.gameObject.layer)
                return true;
            return false;
        }
        
        public static GameObject SetParent(this GameObject self, GameObject parent)
        {
            self.transform.SetParent(parent == null ? null : parent.transform);
            return self;
        }
        public static GameObject SetParent(this GameObject self, Component parent)
        {
            self.transform.SetParent(parent == null ? null : parent.transform);
            return self;
        }
        
        public static Transform SetParentRetainLocal(this Transform self, Component parent)
        {
            var local = (self.localPosition,self.localRotation,self.localScale);
            self.SetParent(parent.transform);
            (self.localPosition, self.localRotation, self.localScale) = local;
            return self;
        }
        public static Transform SetParentRetainLocal(this Transform self, GameObject parent)
        {
            var local = (self.localPosition,self.localRotation,self.localScale);
            self.SetParent(parent.transform);
            (self.localPosition, self.localRotation, self.localScale) = local;
            return self;
        }
        public static GameObject SetParentRetainLocal(this GameObject self, Component parent)
        {
            self.transform.SetParentRetainLocal(parent);
            return self;
        }
        public static GameObject SetParentRetainLocal(this GameObject self, GameObject parent)
        {
            self.transform.SetParentRetainLocal(parent);
            return self;
        }

        public static Transform ResetLocal(this Transform self)
        {
            self.localPosition = Vector3.zero;
            self.localRotation = Quaternion.identity;
            self.localScale = Vector3.one;
            return self;
        }
        
        public static Transform ModifyLocalPosition(this Transform self, float? x = null, float? y = null, float? z = null)
        {
            self.localPosition = self.localPosition.Modify(x, y, z);
            return self;
        }
        public static Transform ModifyLocalEulerAngles(this Transform self, float? x = null, float? y = null, float? z = null)
        {
            self.localEulerAngles = self.localEulerAngles.Modify(x, y, z);
            return self;
        }
        public static Transform ModifyLocalScale(this Transform self, float? x = null, float? y = null, float? z = null)
        {
            self.localScale = self.localScale.Modify(x, y, z);
            return self;
        }
        public static Transform ModifyPosition(this Transform self, float? x = null, float? y = null, float? z = null)
        {
            self.position = self.position.Modify(x, y, z);
            return self;
        }
        public static Transform ModifyEulerAngles(this Transform self, float? x = null, float? y = null, float? z = null)
        {
            self.eulerAngles = self.eulerAngles.Modify(x, y, z);
            return self;
        }
        public static Transform ModifyScale(this Transform self, float? x = null, float? y = null, float? z = null)
        {
            self.localScale = self.localScale.Modify(x, y, z);
            return self;
        }

        public static Transform AddLocalPosition(this Transform self, Vector3 position)
        {
            self.localPosition += position;
            return self;
        }
        public static Transform AddLocalEulerAngles(this Transform self, Vector3 eulerAngles)
        {
            self.localEulerAngles += eulerAngles;
            return self;
        }
        public static Transform AddLocalScale(this Transform self, Vector3 scale)
        {
            self.localScale += scale;
            return self;
        }
        public static Transform AddPosition(this Transform self, Vector3 position)
        {
            self.position += position;
            return self;
        }
        public static Transform AddEulerAngles(this Transform self, Vector3 eulerAngles)
        {
            self.eulerAngles += eulerAngles;
            return self;
        }
        public static Transform AddScale(this Transform self, Vector3 scale)
        {
            self.localScale += scale;
            return self;
        }
        public static Transform AddLocalPosition(this Transform self, float x = 0, float y = 0, float z = 0)
        {
            self.localPosition += new Vector3(x, y, z);
            return self;
        }
        public static Transform AddLocalEulerAngles(this Transform self, float x = 0, float y = 0, float z = 0)
        {
            self.localEulerAngles += new Vector3(x, y, z);
            return self;
        }
        public static Transform AddLocalScale(this Transform self, float x = 0, float y = 0, float z = 0)
        {
            self.localScale += new Vector3(x, y, z);
            return self;
        }
        public static Transform AddPosition(this Transform self, float x = 0, float y = 0, float z = 0)
        {
            self.position += new Vector3(x, y, z);
            return self;
        }
        public static Transform AddEulerAngles(this Transform self, float x = 0, float y = 0, float z = 0)
        {
            self.eulerAngles += new Vector3(x, y, z);
            return self;
        }
        public static Transform AddScale(this Transform self, float x = 0, float y = 0, float z = 0)
        {
            self.localScale += new Vector3(x, y, z);
            return self;
        }
        public static GameObject ResetLocal(this GameObject self)
        {
            self.transform.localPosition = Vector3.zero;
            self.transform.localRotation = Quaternion.identity;
            self.transform.localScale = Vector3.one;
            return self;
        }

        public static float DistanceTo(this Vector2 self, Vector2 target) => Vector2.Distance(self, target);
        public static float DistanceTo(this Vector3 self, Vector3 target) => Vector3.Distance(self, target);
        
        public static float Distance_V2(this Transform self, Component target) => Vector2.Distance(self.position, target.Position());
        public static float Distance_V3(this Transform self, Component target) => Vector3.Distance(self.position, target.Position());
        public static Vector2 DirectionTo_V2(this Transform self, Component target) => target.Position() - self.position;
        public static Vector3 DirectionTo_V3(this Transform self, Component target) => target.Position() - self.position;
        public static Vector2 DirectionFrom_V2(this Transform self, Component target) => self.position - target.Position();
        public static Vector3 DirectionFrom_V3(this Transform self, Component target) => self.position - target.Position();
        

        public static IEnumerable<Transform> Children(this Transform self)
        {
            foreach (Transform child in self)
                yield return child;
        }
        public static void ChildrenDo(this Transform self, Action<Transform> action)
        {
            for (int i = self.childCount-1; i >-1; i--)
                action?.Invoke(self.GetChild(i));
        }
        public static void DestroyChildren(this Transform self)
        {
            for (int i = self.childCount-1; i >-1; i--)
                Object.Destroy(self.GetChild(i));
        }

        public static Vector3 Position(this GameObject self) => self.transform.position;
        public static Vector3 Position(this Component self) => self.transform.position;
        public static Vector3 LocalPosition(this GameObject self) => self.transform.localPosition;
        public static Vector3 LocalPosition(this Component self) => self.transform.localPosition;
        public static Vector3 LocalEulerAngles(this GameObject self) => self.transform.localEulerAngles;
        public static Vector3 LocalEulerAngles(this Component self) => self.transform.localEulerAngles;
        public static Quaternion Rotation(this GameObject self) => self.transform.rotation;
        public static Quaternion Rotation(this Component self) => self.transform.rotation;
        public static Vector3 EulerAngles(this GameObject self) => self.transform.eulerAngles;
        public static Vector3 EulerAngles(this Component self) => self.transform.eulerAngles;
        public static Vector3 LocalScale(this GameObject self) => self.transform.localScale;
        public static Vector3 LocalScale(this Component self) => self.transform.localScale;

        public static GameObject TryGetGameObject(this Object self)
        {
            if (self is GameObject go)
                return go;
            if (self is Component c)
                return c.gameObject;
            throw new ArgumentException($"Object [{self}] is not a GameObject or Component");
        }
        public static GameObject TryGetGameObject(this object self)
        {
            if (self is GameObject go)
                return go;
            if (self is Component c)
                return c.gameObject;
            throw new ArgumentException($"object [{self}] is not a GameObject or Component");
        }

        public static T TryGetComponent<T>(this Object self)
        {
            if (self is GameObject go)
                return go.GetComponent<T>();
            if (self is Component c)
                return c.gameObject.GetComponent<T>();
            throw new ArgumentException($"Object [{self}] is not a GameObject or Component");
        }
        public static Component TryGetComponent(this Object self,Type type)
        {
            if (self is GameObject go)
                return go.GetComponent(type);
            if (self is Component c)
                return c.gameObject.GetComponent(type);
            throw new ArgumentException($"Object [{self}] is not a GameObject or Component");
        }
        public static T TryGetComponent<T>(this object self)
        {
            if (self is GameObject go)
                return go.GetComponent<T>();
            if (self is Component c)
                return c.gameObject.GetComponent<T>();
            throw new ArgumentException($"Object [{self}] is not a GameObject or Component");
        }
        public static Component TryGetComponent(this object self,Type type)
        {
            if (self is GameObject go)
                return go.GetComponent(type);
            if (self is Component c)
                return c.gameObject.GetComponent(type);
            throw new ArgumentException($"Object [{self}] is not a GameObject or Component");
        }
    }
}