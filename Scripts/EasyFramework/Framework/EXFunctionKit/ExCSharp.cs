using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Sirenix.Utilities;
using UnityEngine;

namespace EXFunctionKit
{
    public delegate void RefAction<T>(ref T refValue);
    public delegate void RefAction<T1,T2>(T1 value,ref T2 refValue);
    public delegate ref T RefFunc<T>();
    public delegate ref T2 RefFunc<T1, T2>(ref T1 value);

    public struct And<T>: ICollection<T>
    {
        public List<T> Values;
        public IEnumerator<T> GetEnumerator()=> Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()=>GetEnumerator();
        public void Add(T item)=> Values.Add(item);
        public void Clear()=> Values.Clear();
        public bool Contains(T item)=> Values.Contains(item);
        public void CopyTo(T[] array, int arrayIndex)=> Values.CopyTo(array, arrayIndex);
        public bool Remove(T item)=> Values.Remove(item);
        public int Count => Values.Count;
        bool ICollection<T>.IsReadOnly => false;
    }

    public static class ExCSharp
    {
        private static Lazy<Dictionary<string, Delegate>> _expressionCache =
            new Lazy<Dictionary<string, Delegate>>(() => new Dictionary<string, Delegate>());
        public static Type Type<T>(this T self)=> typeof(T);

        public static And<T> And<T>(this T self, params T[] values)
        {
            var and = new And<T>(){Values = new List<T>()};
            and.Values.Add(self);
            and.Values.AddRange(values);
            return and;
        }
        public static And<T> And<T>(this And<T> self, params T[] values)
        {
            self.Values.AddRange(values);
            return self;
        }
        public static T Modify<TClass, T>(TClass self, Expression<Func<TClass, T>> expression, T newValue,Action<TClass> onChange=null,Func<T,T,bool> compare=null)
        {
            if (!_expressionCache.Value.TryGetValue($"[{typeof(TClass).Name}]{expression}", out var Action))
            {
                var getOldValue = expression.Compile();
                var memberExpression = expression.Body;
                var newValueExpression = Expression.Constant(newValue);
                var assignExpression = Expression.Assign(memberExpression, newValueExpression);
                var lambdaExpression = Expression.Lambda<Action<TClass>>(assignExpression, expression.Parameters);
                var action = lambdaExpression.Compile();
                compare ??= EqualityComparer<T>.Default.Equals;
                Action = new Action<TClass, T>((TClass tClass, T value) =>
                {
                    if (!compare(getOldValue(tClass), value))
                    {
                        action(tClass);
                        onChange?.Invoke(tClass);
                    }
                });
                _expressionCache.Value.Add($"[{typeof(TClass).Name}]{expression}", Action);
            }

            ((Action<TClass, T>)Action)(self, newValue);
            return newValue;
        }
        
        /// <summary>
        /// 将对象转换为指定类型。
        /// </summary>
        /// <typeparam name="T">要转换的类型。</typeparam>
        /// <param name="self">要转换的对象。</param>
        /// <returns>转换后的对象。</returns>
        public static T As<T>(this object self) where T : class => (T)self;

        /// <summary>
        /// 将对象转换为指定类型，并执行指定操作。
        /// </summary>
        /// <typeparam name="T">要转换的类型。</typeparam>
        /// <param name="self">要转换的对象。</param>
        /// <param name="set">要执行的操作。</param>
        /// <returns>转换后的对象。</returns>
        public static T Is<T>(this object self, Action<T> set) where T : class
        {
            if (self is T t)
            {
                set?.Invoke(t);
                return t;
            }

            return default;
        }

        /// <summary>
        /// 将对象赋值给 out 参数，并返回该对象。
        /// </summary>
        /// <typeparam name="T">要赋值的对象类型。</typeparam>
        /// <param name="self">要赋值的对象。</param>
        /// <param name="outValue">接收赋值的 out 参数。</param>
        /// <returns>赋值后的对象。</returns>
        public static T Out<T>(this T self, out T outValue) => outValue = self;

        /// <summary>
        /// 将对象作为引用参数，执行指定操作，并返回该对象。
        /// </summary>
        /// <typeparam name="T1">要操作的对象类型。</typeparam>
        /// <typeparam name="T2">引用参数类型。</typeparam>
        /// <param name="self">要操作的对象。</param>
        /// <param name="refValue">引用参数。</param>
        /// <param name="set">要执行的操作。</param>
        /// <returns>操作后的对象。</returns>
        public static T1 Ref<T1, T2>(this T1 self, ref T2 refValue, RefAction<T2> set)
        {
            set?.Invoke(ref refValue);
            return self;
        }

        /// <summary>
        /// 执行指定操作，并返回该对象。
        /// </summary>
        /// <typeparam name="T">要执行操作的对象类型。</typeparam>
        /// <param name="self">要执行操作的对象。</param>
        /// <param name="set">要执行的操作。</param>
        /// <returns>执行操作后的对象。</returns>
        public static T Set<T>(this T self, Action<T> set) where T : class
        {
            set?.Invoke(self);
            return self;
        }

        /// <summary>
        /// 将字符串转换为整数。
        /// </summary>
        /// <param name="self">要转换的字符串。</param>
        /// <returns>转换后的整数。</returns>
        public static int AsInt(this string self) => int.Parse(self);

        /// <summary>
        /// 将浮点数转换为整数。
        /// </summary>
        /// <param name="self">要转换的浮点数。</param>
        /// <returns>转换后的整数。</returns>
        public static int AsInt(this float self) => (int)self;

        /// <summary>
        /// 将浮点数四舍五入为最接近的整数
        /// </summary>
        /// <param name="self">要四舍五入的浮点数</param>
        /// <returns>最接近的整数值</returns>
        public static int AsRoundInt(this float self) => Mathf.RoundToInt(self);

        /// <summary>
        /// 将浮点数向上取整为最接近的整数
        /// </summary>
        /// <param name="self">要向上取整的浮点数</param>
        /// <returns>最接近的整数值</returns>
        public static int Ceil(this float self) => Mathf.CeilToInt(self);

        /// <summary>
        /// 将浮点数向下取整为最接近的整数
        /// </summary>
        /// <param name="self">要向下取整的浮点数</param>
        /// <returns>最接近的整数值</returns>
        public static int Floor(this float self) => Mathf.FloorToInt(self);

        /// <summary>
        /// 将长整型转换为整型
        /// </summary>
        /// <param name="self">要转换的长整型值</param>
        /// <returns>转换后的整型值</returns>
        public static int AsInt(this long self) => (int)self;

        /// <summary>
        /// 将双精度浮点数转换为整型
        /// </summary>
        /// <param name="self">要转换的双精度浮点数</param>
        /// <returns>转换后的整型值</returns>
        public static int AsInt(this double self) => (int)self;

        /// <summary>
        /// 返回指定整数的绝对值
        /// </summary>
        /// <param name="self">要获取绝对值的整数</param>
        /// <returns>整数的绝对值</returns>
        public static int Abs(this int self) => Mathf.Abs(self);

        public static int Max(this (int, int) self) => Mathf.Max(self.Item1, self.Item2);
        public static int Min(this (int, int) self) => Mathf.Min(self.Item1, self.Item2);

        public static float AsFloat(this string self) => float.Parse(self);
        public static float AsFloat(this int self) => self;
        public static float AsFloat(this long self) => self;
        public static float AsFloat(this double self) => (float)self;

        /// <summary>
        /// 返回指定数字的绝对值。
        /// </summary>
        /// <param name="self">要取绝对值的数字</param>
        /// <returns>输入数字的绝对值</returns>
        public static float Abs(this float self) => Mathf.Abs(self);

        /// <summary>
        /// 返回指定数字的正负号。
        /// </summary>
        /// <param name="self">要获取正负号的数字</param>
        /// <returns>如果输入为正数，则返回1；如果输入为负数，则返回-1；如果输入为零，则返回0</returns>
        public static float Sign(this float self) => Mathf.Sign(self);

        /// <summary>
        /// 返回指定数字的平方根。
        /// </summary>
        /// <param name="self">要计算平方根的数字</param>
        /// <returns>输入数字的平方根</returns>
        public static float Sqrt(this float self) => Mathf.Sqrt(self);

        /// <summary>
        /// 返回指定数字的指定次幂。
        /// </summary>
        /// <param name="self">底数</param>
        /// <param name="pow">指数</param>
        /// <returns>底数的指定次幂</returns>
        public static float Pow(this float self, float pow) => Mathf.Pow(self, pow);

        /// <summary>
        /// 返回指定整数的指定次幂。
        /// </summary>
        /// <param name="self">底数</param>
        /// <param name="pow">指数</param>
        /// <returns>底数的指定次幂</returns>
        public static float Pow(this int self, int pow) => Mathf.Pow(self, pow);

        /// <summary>
        /// 返回元组中两个数中的最大值。
        /// </summary>
        /// <param name="self">包含两个数的元组</param>
        /// <returns>两个数中的最大值</returns>
        public static float Max(this (float, float) self) => Mathf.Max(self.Item1, self.Item2);

        /// <summary>
        /// 返回元组中两个数中的最小值。
        /// </summary>
        /// <param name="self">包含两个数的元组</param>
        /// <returns>两个数中的最小值</returns>
        public static float Min(this (float, float) self) => Mathf.Min(self.Item1, self.Item2);


        public static float Bezier(float t, params float[] c)
        {
            int n = c.Length - 1; // 控制点数量
            float result = 0;

            float oneMinusT = 1f - t; // 1 - t 预先计算，减少重复计算
            float bernstein = 1; // Bernstein基函数的初始值

            for (int i = 0; i <= n; i++)
            {
                result += c[i] * bernstein;
                bernstein *= oneMinusT / (i + 1) * (n - i) / t; // 更新Bernstein基函数的值
            }

            return result;
        }

        public static float LerpAngle(this float self, float a, float b) => Mathf.LerpAngle(a, b, self);
        public static float MoveTowards(this float self, float a, float b) => Mathf.MoveTowards(a, b, self);
        public static float LerpFloat(this float self, float a, float b) => Mathf.Lerp(a, b, self);

        public static float LerpFloat(this float self, float a, float b, params float[] c)
        {
            List<float> temp = new List<float>() { a, b };
            temp.AddRange(c);
            return Bezier(self, temp.ToArray());
        }

        public static float LerpFloatUnclamped(this float self, float a, float b) => Mathf.LerpUnclamped(a, b, self);

        public static float LerpFloatUnclamped(this float self, float a, float b, params float[] c)
        {
            List<float> temp = new List<float>() { a, b };
            temp.AddRange(c);
            return Bezier(Mathf.Clamp01(self), temp.ToArray());
        }

        public static float Clamp(this float self, float a, float b) => Mathf.Clamp(self, a, b);
        public static float Clamp01(this float self) => Mathf.Clamp01(self);
        public static float ReMap(this float self, float oldMin, float oldMax, float newMin, float newMax) => (self - oldMin) / (oldMax - oldMin) * (newMax - newMin) + newMin;
        public static float ReMapTo01(this float self, float oldMin, float oldMax) => (self - oldMin) / (oldMax - oldMin);
        public static float ReMapFrom01(this float self, float newMin, float newMax) => self * (newMax - newMin) + newMin;

        public static long AsLong(this string self) => long.Parse(self);
        public static long AsLong(this int self) => self;
        public static long AsLong(this float self) => (long)self;
        public static long AsLong(this double self) => (long)self;

        public static double AsDouble(this string self) => double.Parse(self);
        public static double AsDouble(this int self) => self;
        public static double AsDouble(this long self) => self;
        public static double AsDouble(this float self) => self;

        public static bool AsBool<T>(this T self) where T : class => null != self;
        public static bool AsBool(this string self) => !self.IsNullOrWhitespace();
        public static bool AsBool(this int self) => self != 0;
        public static bool AsBool(this float self) => self != 0;
        public static bool AsBool(this long self) => self != 0;
        public static bool AsBool(this double self) => self != 0;
        public static bool AsBool(this bool self) => self;
        public static bool AsBool(this IEnumerable self) => self != null && self.Cast<object>().Any();
        public static bool AsBool<T>(this IEnumerable<T> self) => self != null && self.Any();
        public static bool InRange(this int self, int min, int max) => self >= min && self < max;
        public static bool InRange(this float self, float min, float max) => self >= min && self < max;
        public static bool InRange(this long self, long min, long max) => self >= min && self < max;
        public static bool InRange(this double self, double min, double max) => self >= min && self < max;


        /// <summary>
        /// 交换两个引用类型对象的值。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="self">第一个对象</param>
        /// <param name="other">第二个对象</param>
        public static void ExChange<T>(this T self, T other) where T : class => (self, other) = (other, self);

        /// <summary>
        /// 交换元组中两个引用类型对象的值。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="self">包含需要交换值的元组</param>
        public static void ExChange<T>(this (T, T) self) where T : class => self = (self.Item2, self.Item1);

        /// <summary>
        /// 判断两个整数是否有交集。
        /// </summary>
        /// <param name="self">第一个整数</param>
        /// <param name="value">第二个整数</param>
        /// <returns>如果有交集返回true，否则返回false</returns>
        public static bool Intersect(this int self, int value) => (self & value) != 0;

        /// <summary>
        /// 判断目标整数是否包含指定的整数。
        /// </summary>
        /// <param name="self">目标整数</param>
        /// <param name="value">指定的整数</param>
        /// <returns>如果包含返回true，否则返回false</returns>
        public static bool Contain(this int self, int value) => (self & value) == value;

        /// <summary>
        /// 将两个字符串组合成一个路径。
        /// </summary>
        /// <param name="self">第一个字符串作为路径</param>
        /// <param name="path">第二个字符串作为路径的一部分</param>
        /// <returns>组合后的路径</returns>
        public static string Combine(this string self, string path) => Path.Combine(self, path);

        /// <summary>
        /// 移除字符串中的所有空格。
        /// </summary>
        /// <param name="self">要处理的字符串</param>
        /// <returns>移除空格后的字符串</returns>
        public static string RemoveSpaces(this string self) => Regex.Replace(self, @"\s", "");

        /// <summary>
        /// 保留字符串中的字母字符，移除其他字符。
        /// </summary>
        /// <param name="self">要处理的字符串</param>
        /// <returns>只包含字母字符的字符串</returns>
        public static string OnlyLetters(this string self) => Regex.Replace(self, @"[^a-zA-Z]+", "");

        /// <summary>
        /// 保留字符串中的数字字符，移除其他字符。
        /// </summary>
        /// <param name="self">要处理的字符串</param>
        /// <returns>只包含数字字符的字符串</returns>
        public static string OnlyNumbers(this string self) => Regex.Replace(self, @"[^0-9]+", "");

        /// <summary>
        /// 保留字符串中的字母和数字字符，移除其他字符。
        /// </summary>
        /// <param name="self">要处理的字符串</param>
        /// <returns>只包含字母和数字字符的字符串</returns>
        public static string OnlyLettersAndNumbers(this string self) => Regex.Replace(self, @"[^a-zA-Z0-9]+", "");

        public static string[] ToStrings<T>(this IEnumerable<T> self)
        {
            var enumerable = self as T[] ?? self.ToArray();
            var str = new string[enumerable.Length];
            int i = 0;
            foreach (var item in enumerable)
            {
                str[i] = item.ToString();
                i++;
            }

            return str;
        }

        /// <summary>
        /// 在集合中查找第一个符合条件的元素。
        /// </summary>
        /// <typeparam name="T">要查找的元素类型。</typeparam>
        /// <param name="self">要搜索的集合。</param>
        /// <returns>如果找到符合条件的元素，则返回该元素；否则返回类型 T 的默认值。</returns>
        public static T Find<T>(this IEnumerable self)
        {
            foreach (var item in self)
            {
                if (item is T t)
                    return t;
            }

            return default;
        }

        /// <summary>
        /// 在集合中查找第一个符合条件的元素。
        /// </summary>
        /// <typeparam name="T">要查找的元素类型。</typeparam>
        /// <param name="self">要搜索的集合。</param>
        /// <param name="set">用于指定查找条件的函数。</param>
        /// <returns>如果找到符合条件的元素，则返回该元素；否则返回类型 T 的默认值。</returns>
        public static T Find<T>(this IEnumerable self, Func<T, bool> set)
        {
            foreach (var item in self)
            {
                if (item is T t && set(t))
                    return t;
            }

            return default;
        }

        /// <summary>
        /// 在集合中查找所有符合条件的元素。
        /// </summary>
        /// <typeparam name="T">要查找的元素类型。</typeparam>
        /// <param name="self">要搜索的集合。</param>
        /// <returns>返回所有符合条件的元素组成的序列。</returns>
        public static IEnumerable<T> Finds<T>(this IEnumerable self)
        {
            foreach (var item in self)
            {
                if (item is T t)
                    yield return t;
            }
        }

        /// <summary>
        /// 在集合中查找所有符合条件的元素。
        /// </summary>
        /// <typeparam name="T">要查找的元素类型。</typeparam>
        /// <param name="self">要搜索的集合。</param>
        /// <param name="set">用于指定查找条件的函数。</param>
        /// <returns>返回所有符合条件的元素组成的序列。</returns>
        public static IEnumerable<T> Finds<T>(this IEnumerable self, Func<T, bool> set)
        {
            foreach (var item in self)
            {
                if (item is T t && set(t))
                    yield return t;
            }
        }

        /// <summary>
        /// 执行指定操作指定次数。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">要执行的操作。</param>
        public static void Repeat(this int self, Action set)
        {
            for (int i = 0; i < self; i++)
                set.Invoke();
        }

        /// <summary>
        /// 执行指定操作指定次数，同时传入循环计数器的值。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">带有循环计数器的操作。</param>
        public static void Repeat(this int self, Action<int> set)
        {
            for (int i = 0; i < self; i++)
                set.Invoke(i);
        }

        /// <summary>
        /// 根据指定条件，执行指定操作指定次数，直到条件不满足为止。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">带有循环计数器的条件函数。</param>
        public static void Repeat(this int self, Func<bool> set)
        {
            for (int i = 0; i < self; i++)
                if (!set.Invoke())
                    break;
        }

        /// <summary>
        /// 根据指定条件，执行指定操作指定次数，直到条件不满足为止。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">带有循环计数器的条件函数。</param>
        public static void Repeat(this int self, Func<int, bool> set)
        {
            for (int i = 0; i < self; i++)
                if (!set.Invoke(i))
                    break;
        }

        /// <summary>
        /// 根据指定条件，反向执行指定操作指定次数，直到条件不满足为止。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">带有循环计数器的操作。</param>
        public static void RepeatReverse(this int self, Action set)
        {
            for (int i = self - 1; i > -1; i--)
                set.Invoke();
        }

        /// <summary>
        /// 根据指定条件，反向执行指定操作指定次数，直到条件不满足为止。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">带有循环计数器的操作。</param>
        public static void RepeatReverse(this int self, Action<int> set)
        {
            for (int i = self - 1; i > -1; i--)
                set.Invoke(i);
        }

        /// <summary>
        /// 根据指定条件，反向执行指定操作指定次数，直到条件不满足为止。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">带有循环计数器的条件函数。</param>
        public static void RepeatReverse(this int self, Func<bool> set)
        {
            for (int i = self - 1; i > -1; i--)
                if (!set.Invoke())
                    break;
        }

        /// <summary>
        /// 根据指定条件，反向执行指定操作指定次数，直到条件不满足为止。
        /// </summary>
        /// <param name="self">要执行的次数。</param>
        /// <param name="set">带有循环计数器的条件函数。</param>
        public static void RepeatReverse(this int self, Func<int, bool> set)
        {
            for (int i = self - 1; i > -1; i--)
                if (!set.Invoke(i))
                    break;
        }

        /// <summary>
        /// 对集合进行索引循环操作，并执行指定的操作。
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="self">当前集合</param>
        /// <param name="set">要执行的操作</param>
        /// <returns>当前集合</returns>
        public static T ForIndex<T>(this T self, Action<int> set) where T : ICollection
        {
            for (int i = 0; i < self.Count; i++)
                set(i);
            return self;
        }

        /// <summary>
        /// 对集合进行索引循环操作，并执行指定的操作。
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="self">当前集合</param>
        /// <param name="set">要执行的条件判断操作</param>
        /// <returns>当前集合</returns>
        public static T ForIndex<T>(this T self, Func<int, bool> set) where T : ICollection
        {
            for (int i = 0; i < self.Count; i++)
                if (set(i))
                    break;
            return self;
        }

        /// <summary>
        /// 对集合进行倒序索引循环操作，并执行指定的操作。
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="self">当前集合</param>
        /// <param name="set">要执行的操作</param>
        /// <returns>当前集合</returns>
        public static T ForIndexReverse<T>(this T self, Action<int> set) where T : ICollection
        {
            for (int i = self.Count - 1; i > -1; i--)
                set(i);
            return self;
        }

        /// <summary>
        /// 对集合进行倒序索引循环操作，并执行指定的条件判断操作。
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="self">当前集合</param>
        /// <param name="set">要执行的条件判断操作</param>
        /// <returns>当前集合</returns>
        public static T ForIndexReverse<T>(this T self, Func<int, bool> set) where T : ICollection
        {
            for (int i = self.Count - 1; i > -1; i--)
                if (set(i))
                    break;
            return self;
        }


        /// <summary>
        /// 遍历 List 并应用操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">列表中的元素类型。</typeparam>
        /// <param name="self">要遍历的 List 实例。</param>
        /// <param name="set">应用于元素的操作。</param>
        /// <returns>已经遍历的 List 实例。</returns>
        public static List<T> For<T>(this List<T> self, Action<int, T> set)
        {
            for (int i = 0; i < self.Count; i++)
                set.Invoke(i, self[i]);
            return self;
        }

        /// <summary>
        /// 遍历 List 并应用操作，直到操作返回 false。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">列表中的元素类型。</typeparam>
        /// <param name="self">要遍历的 List 实例。</param>
        /// <param name="set">应用于元素的操作。</param>
        /// <returns>已经遍历的 List 实例。</returns>
        public static List<T> For<T>(this List<T> self, Func<int, T, bool> set)
        {
            for (int i = 0; i < self.Count; i++)
                if (!set.Invoke(i, self[i]))
                    break;
            return self;
        }

        /// <summary>
        /// 遍历 T[] 并应用操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">数组中的元素类型。</typeparam>
        /// <param name="self">要遍历的 T[] 实例。</param>
        /// <param name="set">应用于元素的操作。</param>
        /// <returns>已经遪历的 T[] 实例。</returns>
        public static T[] For<T>(this T[] self, Action<int, T> set)
        {
            for (int i = 0; i < self.Length; i++)
                set.Invoke(i, self[i]);
            return self;
        }

        /// <summary>
        /// 遍历 T[] 并应用操作，直到操作返回 false。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">数组中的元素类型。</typeparam>
        /// <param name="self">要遍历的 T[] 实例。</param>
        /// <param name="set">应用于元素的操4。</param>
        /// <returns>已经遍历的 T[] 实例。</returns>
        public static T[] For<T>(this T[] self, Func<int, T, bool> set)
        {
            for (int i = 0; i < self.Length; i++)
                if (!set.Invoke(i, self[i]))
                    break;
            return self;
        }

        /// <summary>
        /// 逆向遍历 List 并应用操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">列表中的元素类型。</typeparam>
        /// <param name="self">要遍历的 List 实例。</param>
        /// <param name="set">应用于元素的操作。</param>
        /// <returns>已经遍历的 List 实例。</returns>
        public static List<T> ForReverse<T>(this List<T> self, Action<int, T> set)
        {
            for (int i = self.Count - 1; i > -1; i--)
                set.Invoke(i, self[i]);
            return self;
        }

        /// <summary>
        /// 逆向遍历 List 并应用操作，直到操作返回 false。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">列表中的元素类型。</typeparam>
        /// <param name="self">要遍历的 List 实例。</param>
        /// <param name="set">应用于元素的操作。</param>
        /// <returns>已经遍历的 List 实例。</returns>
        public static List<T> ForReverse<T>(this List<T> self, Func<int, T, bool> set)
        {
            for (int i = self.Count - 1; i > -1; i--)
                if (!set.Invoke(i, self[i]))
                    break;
            return self;
        }

        /// <summary>
        /// 逆向遍历 T[] 并应用操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">数组中的元素类型。</typeparam>
        /// <param name="self">要遍历的 T[] 实例。</param>
        /// <param name="set">应用于元素的操作。</param>
        /// <returns>已经遍历的 T[] 实例。</returns>
        public static T[] ForReverse<T>(this T[] self, Action<int, T> set)
        {
            for (int i = self.Length - 1; i > -1; i--)
                set.Invoke(i, self[i]);
            return self;
        }

        /// <summary>
        /// 逆向遍历 T[] 并应用操作，直到操作返回 false。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        /// <typeparam name="T">数组中的元素类型。</typeparam>
        /// <param name="self">要遍历的 T[] 实例。</param>
        /// <param name="set">应用于元素的操作。</param>
        /// <returns>已经遍历的 T[] 实例。</returns>
        public static T[] ForReverse<T>(this T[] self, Func<int, T, bool> set)
        {
            for (int i = self.Length - 1; i > -1; i--)
                if (!set.Invoke(i, self[i]))
                    break;
            return self;
        }


        /// <summary>
        /// 对集合中的每个项执行操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Action<T> set)
        {
            var forEach = self as T[] ?? self.ToArray();
            foreach (var item in forEach)
                set.Invoke(item);

            return forEach;
        }

        /// <summary>
        /// 对集合中的每个项执行操作，如果操作返回 false，则停止遍历。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Func<T, bool> set)
        {
            var forEach = self as T[] ?? self.ToArray();
            foreach (var item in forEach)
                if (!set.Invoke(item))
                    break;

            return forEach;
        }

        /// <summary>
        /// 对字典中的每个键值对执行操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static Dictionary<K, V> ForEachKey<K, V>(this Dictionary<K, V> self, Action<K, V> set)
        {
            foreach (var item in self)
                set.Invoke(item.Key, item.Value);
            return self;
        }

        /// <summary>
        /// 对字典中的每个键值对执行操作，如果操作返回 true，则停止遍历。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static Dictionary<K, V> ForEachKey<K, V>(this Dictionary<K, V> self, Func<K, V, bool> set)
        {
            foreach (var item in self)
                if (set.Invoke(item.Key, item.Value))
                    break;
            return self;
        }

        /// <summary>
        /// 对字典中的每个键执行操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static Dictionary<K, V> ForEachKey<K, V>(this Dictionary<K, V> self, Action<K> set)
        {
            foreach (var key in self.Keys)
                set.Invoke(key);
            return self;
        }

        /// <summary>
        /// 对字典中的每个键执行操作，如果操作返回 true，则停止遍历。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static Dictionary<K, V> ForEachKey<K, V>(this Dictionary<K, V> self, Func<K, bool> set)
        {
            foreach (var key in self.Keys)
                if (set.Invoke(key))
                    break;
            return self;
        }

        /// <summary>
        /// 对字典中的每个值执行操作。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static Dictionary<K, V> ForEachValue<K, V>(this Dictionary<K, V> self, Action<V> set)
        {
            foreach (var value in self.Values)
                set.Invoke(value);
            return self;
        }

        /// <summary>
        /// 对字典中的每个值执行操作，如果操作返回 true，则停止遍历。注意！！遍历值类型时，事件传出的值是副本，而不是原值。
        /// </summary>
        public static Dictionary<K, V> ForEachValue<K, V>(this Dictionary<K, V> self, Func<V, bool> set)
        {
            foreach (var value in self.Values)
                if (set.Invoke(value))
                    break;
            return self;
        }


        /// <summary>
        /// 将 IEnumerable 转换为带有键类型 K 和值类型 List 的 Dictionary。
        /// </summary>
        /// <typeparam name="K">键的类型</typeparam>
        /// <typeparam name="V">值的类型</typeparam>
        /// <param name="self">要转换的 IEnumerable</param>
        /// <param name="keySelector">用于从元素中提取键的函数</param>
        /// <returns>转换后的 Dictionary 对象</returns>
        public static Dictionary<K, List<V>> AsDic<K, V>(this IEnumerable<V> self, Func<V, K> keySelector)
        {
            Dictionary<K, List<V>> dic = new Dictionary<K, List<V>>();
            foreach (var item in self)
            {
                if (item == null) continue;

                K key = keySelector(item);
                if (!dic.ContainsKey(key))
                    dic.Add(key, new List<V>());
                dic[key].Add(item);
            }

            return dic;
        }

        /// <summary>
        /// 将 IEnumerable 转换为带有键类型 K 和值类型 List 的 Dictionary，同时支持值筛选功能。
        /// </summary>
        /// <typeparam name="K">键的类型</typeparam>
        /// <typeparam name="V">值的类型</typeparam>
        /// <param name="self">要转换的 IEnumerable</param>
        /// <param name="valueFilter">值筛选的条件函数</param>
        /// <param name="keySelector">用于从元素中提取键的函数</param>
        /// <returns>转换后的 Dictionary 对象</returns>
        public static Dictionary<K, List<V>> AsDic<K, V>(this IEnumerable<V> self, Func<V, bool> valueFilter,
            Func<V, K> keySelector)
        {
            Dictionary<K, List<V>> dic = new Dictionary<K, List<V>>();
            foreach (var item in self)
            {
                if (item != null && valueFilter(item))
                {
                    K key = keySelector(item);
                    if (!dic.ContainsKey(key))
                        dic.Add(key, new List<V>());
                    dic[key].Add(item);
                }
            }

            return dic;
        }

        /// <summary>
        /// 将字典用默认值填充，值类型为 V，键类型为 K（通过枚举得到所有键）
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="self">要填充的字典</param>
        public static Dictionary<K, V> Fill<K, V>(this Dictionary<K, V> self) where K : Enum where V : new()
        {
            self ??= new();
            foreach (K key in Enum.GetValues(typeof(K)))
                self[key] = new V();
            return self;
        }

        /// <summary>
        /// 将字典用给定的默认值填充，值类型为 V，键类型为 K（通过枚举得到所有键）
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="self">要填充的字典</param>
        /// <param name="defaultValue">用于提供默认值的方法</param>
        public static Dictionary<K, V> Fill<K, V>(this Dictionary<K, V> self, Func<K, V> defaultValue) where K : Enum
        {
            self ??= new();
            foreach (K key in Enum.GetValues(typeof(K)))
                self[key] = defaultValue(key);
            return self;
        }

        /// <summary>
        /// 将字典用默认值填充，值类型为 V，键类型为 K（使用给定的键集合）
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="self">要填充的字典</param>
        /// <param name="keys">键的集合</param>
        public static Dictionary<K, V> Fill<K, V>(this Dictionary<K, V> self, IEnumerable<K> keys) where V : new()
        {
            self ??= new();
            foreach (var key in keys)
                self[key] = new V();
            return self;
        }

        /// <summary>
        /// 将字典用给定的默认值填充，值类型为 V，键类型为 K（使用给定的键集合）
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="self">要填充的字典</param>
        /// <param name="keys">键的集合</param>
        /// <param name="defaultValue">用于提供默认值的方法</param>
        public static Dictionary<K, V> Fill<K, V>(this Dictionary<K, V> self, IEnumerable<K> keys, Func<K, V> defaultValue)
        {
            self ??= new();
            foreach (var key in keys)
                self[key] = defaultValue(key);
            return self;
        }


        /// <summary>
        /// 在链表中查找满足指定条件的下一个节点。
        /// </summary>
        /// <typeparam name="T">链表节点的类型。</typeparam>
        /// <param name="self">当前节点。</param>
        /// <param name="judge">判断条件的委托。</param>
        /// <returns>满足条件的下一个节点，如果未找到则返回null。</returns>
        public static LinkedListNode<T> FindNext<T>(this LinkedListNode<T> self, Func<LinkedListNode<T>, bool> judge)
        {
            if (self != null && self.Next != null)
                if (judge(self.Next))
                    return self.Next;
                else
                    return FindNext(self.Next, judge);
            return null;
        }

        /// <summary>
        /// 在链表中查找满足指定条件的上一个节点。
        /// </summary>
        /// <typeparam name="T">链表节点的类型。</typeparam>
        /// <param name="self">当前节点。</param>
        /// <param name="judge">判断条件的委托。</param>
        /// <returns>满足条件的上一个节点，如果未找到则返回null。</returns>
        public static LinkedListNode<T> FindPrevious<T>(this LinkedListNode<T> self,
            Func<LinkedListNode<T>, bool> judge)
        {
            if (self != null && self.Previous != null)
                if (judge(self.Previous))
                    return self.Previous;
                else
                    return FindPrevious(self.Previous, judge);
            return null;
        }


        public static V GetOrAdd<K, V>(this Dictionary<K, V> self, K key, Func<V> getDefaultValue)
        {
            if (!self.TryGetValue(key, out V value))
            {
                value = getDefaultValue();
                self[key] = value;
            }

            return value;
        }

        /// <summary>
        /// 将元组转换为可枚举的 IEnumerable.注意!!若元组元素为值类型,建议使用ExCSharp.AsIEnumerable()代替,否则会有装箱的开销.
        /// </summary>
        /// <param name="self">元组</param>
        /// <returns> 可枚举的 IEnumerable </returns>
        public static IEnumerable AsEnumerable(this ITuple self)
        {
            for (int i = 0; i < self.Length; i++)
            {
                yield return self[i];
            }
        }
        /// <summary>
        /// 将元组转换为可枚举的 IEnumerable.注意!!必须保证元组的元素类型一致;若元组元素为值类型,建议使用ExCSharp.AsIEnumerable()代替,否则会有装箱拆箱的开销.
        /// </summary>
        /// <param name="self">元组</param>
        /// <typeparam name="T">元组中的元素类型</typeparam>
        /// <returns> 可枚举的 IEnumerable </returns>
        public static IEnumerable<T> AsEnumerable<T>(this ITuple self)
        {
            for (int i = 0; i < self.Length; i++)
            {
                yield return (T) self[i];
            }
        }
        /// <summary>
        /// 遍历元组，并对每个元素执行操作.注意!!若元组元素为值类型,建议使用ExCSharp.ForEach()代替,否则会有装箱的开销.
        /// </summary>
        /// <param name="self"> 元组 </param>
        /// <param name="set"> 操作 </param>
        /// <returns> 可枚举的 IEnumerable </returns>
        public static void ForEach(this ITuple self, Action<object> set)
        {
            for (int i = 0; i < self.Length; i++)
            {
                set.Invoke(self[i]);
            }
        }
        /// <summary>
        /// 遍历元组，并对每个元素执行操作.注意!!必须保证元组的元素类型一致;若元组元素为值类型,建议使用ExCSharp.ForEach()代替,否则会有装箱拆箱的开销.
        /// </summary>
        /// <param name="self"> 元组 </param>
        /// <param name="set"> 操作 </param>
        /// <typeparam name="T"> 元组中的元素类型 </typeparam>
        /// <returns> 可枚举的 IEnumerable </returns>
        public static void ForEach<T>(this ITuple self, Action<T> set)
        {
            for (int i = 0; i < self.Length; i++)
            {
                set.Invoke((T)self[i]);
            }
        }
        /// <summary>
        ///  遍历参数，并对每个元素执行操作.注意!!若参数元素为值类型,则事件传出的值是副本,而不是原值.
        /// </summary>
        /// <param name="action"> 操作 </param>
        /// <param name="self"> 参数 </param>
        /// <typeparam name="T"> 参数类型 </typeparam>
        /// <returns> 可枚举的 IEnumerable </returns>
        public static void ForEach<T>(Action<T> action,params T[] self)
        {
            foreach (var t in self)
            {
                action(t);
            }
        }
        public static HashSet<object> AsHashSet(this ITuple self)
        {
            return self.AsEnumerable<object>().ToHashSet();
        }
        public static HashSet<T> AsHashSet<T>(this ITuple self)
        {
            return self.AsEnumerable<T>().ToHashSet();
        }
        public static IEnumerable<T> AppendTo<T>(this T self, IEnumerable<T> targetEnumerable) =>
            targetEnumerable.Append(self);

        public static T AppendToLst<T>(this T self, List<T> lst)
        {
            lst.Add(self);
            return self;
        }

        public static T AppendToLst<T>(this T self, List<T> lst, int index)
        {
            lst.Insert(index, self);
            return self;
        }

        public static T AppendToDic<T, K>(this T self, K key, Dictionary<K, T> dic)
        {
            dic.TryAdd(key, self);
            return self;
        }

        public static T AppendToStack<T>(this T self, Stack<T> stack)
        {
            stack.Push(self);
            return self;
        }

        public static T AppendToQueue<T>(this T self, Queue<T> queue)
        {
            queue.Enqueue(self);
            return self;
        }

        public static T AppendToLinkedList<T>(this T self, LinkedList<T> linkedList)
        {
            linkedList.AddLast(self);
            return self;
        }

        public static T AppendToLinkedListAfter<T>(this T self, LinkedListNode<T> node)
        {
            node.List.AddAfter(node, self);
            return self;
        }

        public static T AppendToLinkedListBefore<T>(this T self, LinkedListNode<T> node)
        {
            node.List.AddBefore(node, self);
            return self;
        }

        public static T AppendToHashSet<T>(this T self, HashSet<T> hashSet)
        {
            hashSet.Add(self);
            return self;
        }

        public static T[] AppendToArr<T>(this T self, T[] arr)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = self;
            return arr;
        }

        public static T[] AppendToArr<T>(this T self, T[] arr, int index)
        {
            Array.Resize(ref arr, arr.Length + 1);
            for (int i = arr.Length - 1; i > index; i--)
                arr[i] = arr[i - 1];
            arr[index] = self;
            return arr;
        }

        public static bool IsEqualsAny<T>(this T self, T item, params T[] items)
        {
            bool result = EqualityComparer<T>.Default.Equals(self, item);
            if (result)
                return true;
            foreach (var e in items)
            {
                result = result || EqualityComparer<T>.Default.Equals(self, e);
                if (result)
                    break;
            }

            return result;
        }

        public static T Get<T>(this IEnumerable<T> self, Predicate<T> predicate, T defaultValue = default)
        {
            if(self == null)
                return defaultValue;
            foreach (var item in self)
            {
                if(predicate(item))
                    return item;
            }
            return defaultValue;
        }
        public static float MaxFloat<T>(this IEnumerable<T> self, Func<T, float> selector)
        {
            var max=float.MinValue;
            foreach (var t in self)
            {
                max = Math.Max(max, selector(t));
            }
            return max;
        }
        public static float MinFloat<T>(this IEnumerable<T> self, Func<T, float> selector)
        {
            var min = float.MaxValue;
            foreach (var t in self)
            {
                min = Math.Min(min, selector(t));
            }

            return min;
        }
        public static int MaxInt<T>(this IEnumerable<T> self, Func<T, int> selector)
        {
            var max = int.MinValue;
            foreach (var t in self)
            {
                max = Math.Max(max, selector(t));
            }

            return max;
        }
        public static int MinInt<T>(this IEnumerable<T> self, Func<T, int> selector)
        {
            var min = int.MaxValue;
            foreach (var t in self)
            {
                min = Math.Min(min, selector(t));
            }

            return min;
        }
        public static IEnumerable<TResult> AsEnumerable<TResult>(this IEnumerable self, Func<object, TResult> selector)
        {
            foreach (var t in self)
                yield return selector(t);
        }
        public static IEnumerable<TResult> AsEnumerable<T, TResult>(this IEnumerable<T> self, Func<object, TResult> selector, Func<T, bool> filter)
        {
            foreach (var t in self)
            {
                if (filter(t))
                    yield return selector(t);
            }
        }
        public static IEnumerable<TResult> AsEnumerable<T, TResult>(this IEnumerable<T> self, Func<T, TResult> selector)
        {
            foreach (var t in self)
                yield return selector(t);
        }
        public static IEnumerable<TResult> AsEnumerable<T, TResult>(this IEnumerable<T> self, Func<T, TResult> selector, Func<T, bool> filter)
        {
            foreach (var t in self)
            {
                if (filter(t))
                    yield return selector(t);
            }
        }
        public static object OneLast(this IEnumerable self)
        {
            var last = default(object);
            foreach (var t in self)
                last = t;
            return last;
        }
        public static T OneLast<T>(this IEnumerable<T> self)=> self.LastOrDefault();
        
        
        public static string TryCreateDirectory(this string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
        
    }
}