using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFramework
{
   /* public static partial class EasyTask
    {
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterEasyTask(this Func<Task> self)
        {
            var handle = CoroutineHandle.Fetch();
            self().RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterEasyTask<T1>(this Func<T1, Task> self, T1 arg1)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterEasyTask<T1, T2>(this Func<T1, T2, Task> self, T1 arg1, T2 arg2)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterEasyTask<T1, T2, T3>(this Func<T1, T2, T3, Task> self, T1 arg1, T2 arg2, T3 arg3)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2, arg3).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterEasyTask<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, Task> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2, arg3, arg4).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterEasyTask<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, Task> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2, arg3, arg4, arg5).RunTask(handle).ViewError();
            return handle;
        }

        
        
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <param name="handle">协程handle</param>
        /// <returns></returns>
        public static CoroutineHandle RegisterTask(this Func<Task> self, CoroutineHandle handle)
        {
            self().RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <param name="handle">协程handle</param>
        /// <returns></returns>
        public static CoroutineHandle RegisterTask<T1>(this Func<T1, Task> self, CoroutineHandle handle, T1 arg1)
        {
            self(arg1).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <param name="handle">协程handle</param>
        /// <returns></returns>
        public static CoroutineHandle RegisterTask<T1, T2>(this Func<T1, T2, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2)
        {
            self(arg1, arg2).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <param name="handle">协程handle</param>
        /// <returns></returns>
        public static CoroutineHandle RegisterTask<T1, T2, T3>(this Func<T1, T2, T3, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3)
        {
            self(arg1, arg2, arg3).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法到，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <param name="handle">协程handle</param>
        /// <returns></returns>
        public static CoroutineHandle RegisterTask<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            self(arg1, arg2, arg3, arg4).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步方法，并返回一个协程handle,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <param name="handle">协程handle</param>
        /// <returns></returns>
        public static CoroutineHandle RegisterTask<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            self(arg1, arg2, arg3, arg4, arg5).RunTask(handle).ViewError();
            return handle;
        }
        
        

        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask(this Func<CancellationToken,Task> self)
        {
            var handle = CoroutineHandle.Fetch();
            self(handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1>(this Func<T1, CancellationToken, Task> self, T1 arg1)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2>(this Func<T1, T2, CancellationToken, Task> self, T1 arg1, T2 arg2)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2, T3>(this Func<T1, T2, T3, CancellationToken, Task> self, T1 arg1, T2 arg2, T3 arg3)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2, arg3, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, CancellationToken, Task> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2, arg3, arg4, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, CancellationToken, Task> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var handle = CoroutineHandle.Fetch();
            self(arg1, arg2, arg3, arg4, arg5, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
       
        
        
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask(this Func<CancellationToken,Task> self,CoroutineHandle handle)
        {
            self(handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1>(this Func<T1, CancellationToken, Task> self, CoroutineHandle handle, T1 arg1)
        {
            self(arg1, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2>(this Func<T1, T2, CancellationToken, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2)
        {
            self(arg1, arg2, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2, T3>(this Func<T1, T2, T3, CancellationToken, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3)
        {
            self(arg1, arg2, arg3, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, CancellationToken, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            self(arg1, arg2, arg3, arg4, handle.Token).RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>        
        /// 注册一个异步带有CancellationToken参数的方法，并返回一个协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle RegisterCancellableTask<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, CancellationToken, Task> self, CoroutineHandle handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            self(arg1, arg2, arg3, arg4, arg5, handle.Token).RunTask(handle).ViewError();
            return handle;
        }

        
       
        /// <summary>
        /// 注册一个带有返回值的异步方法，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyResultTask<TResult>(this Func<Task<TResult>> self)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self().RunTask(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyResultTask<T1, TResult>(this Func<T1, Task<TResult>> self, T1 arg1)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyResultTask<T1, T2, TResult>(this Func<T1, T2, Task<TResult>> self, T1 arg1, T2 arg2)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyResultTask<T1, T2, T3, TResult>(this Func<T1, T2, T3, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2, arg3).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>        
        /// 注册一个带有返回值的异步方法，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyResultTask<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2, arg3, arg4).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyResultTask<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2, arg3, arg4, arg5).RunTaskResult(handle).ViewError();
            return handle;
        }
        
        
        
        /// <summary>
        /// 注册一个带有返回值的异步方法到协程系统中，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterResultTask<TResult>(this Func<Task<TResult>> self, CoroutineHandle<TResult> handle)
        {
            self().RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法到协程系统中，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterResultTask<T1, TResult>(this Func<T1, Task<TResult>> self, T1 arg1, CoroutineHandle<TResult> handle)
        {
            self(arg1).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法到协程系统中，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterResultTask<T1, T2, TResult>(this Func<T1, T2, Task<TResult>> self, T1 arg1, T2 arg2, CoroutineHandle<TResult> handle)
        {
            self(arg1, arg2).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法到协程系统中，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterResultTask<T1, T2, T3, TResult>(this Func<T1, T2, T3, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3, CoroutineHandle<TResult> handle)
        {
            self(arg1, arg2, arg3).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法到协程系统中，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterResultTask<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, CoroutineHandle<TResult> handle)
        {
            self(arg1, arg2, arg3, arg4).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步方法到协程系统中，并返回一个带有返回值的协程handle,注册的异步方法第一个参数必须是带有返回值的协程handle类型,注意！！这样注册的异步方法无法使用handle.Cancel()方法取消
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterResultTask<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, CoroutineHandle<TResult> handle)
        {
            self(arg1, arg2, arg3, arg4, arg5).RunTaskResult(handle).ViewError();
            return handle;
        }
        
        

        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterCancellableResultTask<TResult>(this Func<CancellationToken, Task<TResult>> self)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterCancellableResultTask<T1, TResult>(this Func<T1, CancellationToken, Task<TResult>> self, T1 arg1)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterCancellableResultTask<T1, T2, TResult>(this Func<T1, T2, CancellationToken, Task<TResult>> self, T1 arg1, T2 arg2)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterCancellableResultTask<T1, T2, T3, TResult>(this Func<T1, T2, T3, CancellationToken, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2, arg3, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>        
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterCancellableResultTask<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, CancellationToken, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2, arg3, arg4, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterCancellableResultTask<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, CancellationToken, Task<TResult>> self, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var handle = CoroutineHandle<TResult>.Fetch();
            self(arg1, arg2, arg3, arg4, arg5, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        
        
        
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyCancellableResultTask<TResult>(this Func<CancellationToken, Task<TResult>> self, CoroutineHandle<TResult> handle)
        {
            self(handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyCancellableResultTask<T1, TResult>(this Func<T1, CancellationToken, Task<TResult>> self, CoroutineHandle<TResult> handle, T1 arg1)
        {
            self(arg1, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyCancellableResultTask<T1, T2, TResult>(this Func<T1, T2, CancellationToken, Task<TResult>> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2)
        {
            self(arg1, arg2, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyCancellableResultTask<T1, T2, T3, TResult>(this Func<T1, T2, T3, CancellationToken, Task<TResult>> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2, T3 arg3)
        {
            self(arg1, arg2, arg3, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyCancellableResultTask<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, CancellationToken, Task<TResult>> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            self(arg1, arg2, arg3, arg4, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
        /// <summary>
        /// 注册一个带有返回值的异步带有CancellationToken参数的方法到协程系统中，并返回一个带有返回值的协程handle
        /// </summary>
        /// <param name="self">异步方法</param>
        /// <returns>协程handle</returns>
        public static CoroutineHandle<TResult> RegisterEasyCancellableResultTask<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, CancellationToken, Task<TResult>> self, CoroutineHandle<TResult> handle, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            self(arg1, arg2, arg3, arg4, arg5, handle.Token).RunTaskResult(handle).ViewError();
            return handle;
        }
    }*/
}