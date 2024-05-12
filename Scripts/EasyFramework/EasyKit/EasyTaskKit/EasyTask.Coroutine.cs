using System;
using System.Collections;

namespace EasyFramework.EasyTaskKit
{
    public static partial class EasyTask
    {
        public static CoroutineHandle RegisterEasyCoroutine(this IEnumerator self)
        {
            var handle = CoroutineHandle.Fetch();
            EasyCoroutine.RegisterCoroutine(handle, EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self));
            return handle;
        }
        public static CoroutineHandle RegisterCoroutine(this IEnumerator self,CoroutineHandle handle)
        {
            EasyCoroutine.RegisterCoroutine(handle, EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self));
            return handle;
        }
        
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterEasyCoroutine(this Func<IEnumerator> self)
        {
            var handle = CoroutineHandle.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self()));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterEasyCoroutine<T1>(this Func<T1, IEnumerator> self, T1 arg1)
        {
            var handle = CoroutineHandle.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterEasyCoroutine<T1, T2>(this Func<T1, T2, IEnumerator> self, T1 arg1, T2 arg2)
        {
            var handle = CoroutineHandle.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterEasyCoroutine<T1, T2, T3>(this Func<T1, T2, T3, IEnumerator> self, T1 arg1, T2 arg2, T3 arg3)
        {
            var handle = CoroutineHandle.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2, arg3)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterEasyCoroutine<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, IEnumerator> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var handle = CoroutineHandle.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2, arg3, arg4)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterEasyCoroutine<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, IEnumerator> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var handle = CoroutineHandle.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2, arg3, arg4, arg5)));
            return handle;
        }
        
        
        
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterCoroutine(this Func<IEnumerator> self, CoroutineHandle handle)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self()));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterCoroutine<T1>(this Func<T1, IEnumerator> self, CoroutineHandle handle, T1 arg1)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterCoroutine<T1, T2>(this Func<T1, T2, IEnumerator> self, CoroutineHandle handle, T1 arg1, T2 arg2)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterCoroutine<T1, T2, T3>(this Func<T1, T2, T3, IEnumerator> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2, arg3)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterCoroutine<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, IEnumerator> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2, arg3, arg4)));
            return handle;
        }
        /// <summary>
        /// 注册一个协程方法到协程系统中，并返回一个协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle RegisterCoroutine<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, IEnumerator> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(arg1, arg2, arg3, arg4, arg5)));
            return handle;
        }

        

        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterEasyResultCoroutine<TResult>(this Func<CoroutineHandle<TResult>, IEnumerator> self)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(handle)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterEasyResultCoroutine<TResult, T1>(this Func<CoroutineHandle<TResult>, T1, IEnumerator> self, T1 arg1)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(handle, arg1)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterEasyResultCoroutine<TResult, T1, T2>(this Func<CoroutineHandle<TResult>, T1, T2, IEnumerator> self, T1 arg1, T2 arg2)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(handle, arg1, arg2)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterEasyResultCoroutine<TResult, T1, T2, T3>(this Func<CoroutineHandle<TResult>, T1, T2, T3, IEnumerator> self, T1 arg1, T2 arg2, T3 arg3)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(handle, arg1, arg2, arg3)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterEasyResultCoroutine<TResult, T1, T2, T3, T4>(this Func<CoroutineHandle<TResult>, T1, T2, T3, T4, IEnumerator> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(handle, arg1, arg2, arg3, arg4)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterEasyResultCoroutine<TResult, T1, T2, T3, T4, T5>(this Func<CoroutineHandle<TResult>, T1, T2, T3, T4, T5, IEnumerator> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(handle, arg1, arg2, arg3, arg4, arg5)));
            return handle;
        }
        
        

        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterResultCoroutine<TResult>(this Func<CoroutineHandle<TResult>, IEnumerator> self, CoroutineHandle<TResult> handle)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle), self(handle)));
            return handle;
        }

        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterResultCoroutine<TResult, T1>(this Func<CoroutineHandle<TResult>, T1, IEnumerator> self, CoroutineHandle<TResult> handle, T1 arg1)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle),
                    self(handle, arg1)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterResultCoroutine<TResult, T1, T2>(this Func<CoroutineHandle<TResult>, T1, T2, IEnumerator> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle),
                    self(handle, arg1, arg2)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterResultCoroutine<TResult, T1, T2, T3>(this Func<CoroutineHandle<TResult>, T1, T2, T3, IEnumerator> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2, T3 arg3)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle),
                    self(handle, arg1, arg2, arg3)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterResultCoroutine<TResult, T1, T2, T3, T4>(this Func<CoroutineHandle<TResult>, T1, T2, T3, T4, IEnumerator> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle),
                    self(handle, arg1, arg2, arg3, arg4)));
            return handle;
        }
        /// <summary>
        /// 注册一个带返回值的协程方法到协程系统中，并返回一个带返回值的协程handle
        /// </summary>
        /// <param name="self">协程方法 </param>
        public static CoroutineHandle<TResult> RegisterResultCoroutine<TResult, T1, T2, T3, T4, T5>(this Func<CoroutineHandle<TResult>, T1, T2, T3, T4, T5, IEnumerator> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            EasyCoroutine.RegisterCoroutine(handle,
                EasyCoroutine.CustomCoroutine(() => EasyCoroutine.CoroutineCompleted(handle),
                    self(handle, arg1, arg2, arg3, arg4, arg5)));
            return handle;
        }
    }
}