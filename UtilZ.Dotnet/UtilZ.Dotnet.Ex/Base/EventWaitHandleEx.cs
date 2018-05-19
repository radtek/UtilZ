﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

namespace UtilZ.Dotnet.Ex.Base
{
    /// <summary>
    /// EventWaitHandle扩展类,通知正在等待的线程已发生事件
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
    public class EventWaitHandleEx : IDisposable
    {
        /// <summary>
        /// 通知正在等待的线程已发生事件对象
        /// </summary>
        private readonly EventWaitHandle _eventWaitHandle;

        /// <summary>
        /// 对象是否已释放[true:已释放;false:未释放]
        /// </summary>
        private bool _isDisposed = false;

        /// <summary>
        /// 对象是否已释放线程锁
        /// </summary>
        private readonly object _isDisposedLock = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="mode"></param>
        public EventWaitHandleEx(bool initialState, EventResetMode mode)
        {
            if (mode == EventResetMode.AutoReset)
            {
                this._eventWaitHandle = new AutoResetEvent(initialState);
            }
            else if (mode == EventResetMode.ManualReset)
            {
                this._eventWaitHandle = new ManualResetEvent(initialState);
            }
            else
            {
                throw new NotImplementedException(mode.ToString());
            }
        }

        /// <summary>
        /// 将事件状态设置为非终止，从而导致线程受阻[如果该操作成功，则为 true；否则为 false]
        /// </summary>
        /// <returns>如果该操作成功，则为 true；否则为 false</returns>
        public bool Reset()
        {
            lock (this._isDisposedLock)
            {
                if (this._isDisposed)
                {
                    return false;
                    //throw new ObjectDisposedException("_eventWaitHandle");
                }

                return this._eventWaitHandle.Reset();
            }
        }

        /// <summary>
        /// 将事件状态设置为有信号，从而允许一个或多个等待线程继续执行[如果该操作成功，则为 true；否则为 false]
        /// </summary>
        /// <returns>如果该操作成功，则为 true；否则为 false</returns>
        public bool Set()
        {
            lock (this._isDisposedLock)
            {
                if (this._isDisposed)
                {
                    return false;
                    //throw new ObjectDisposedException("_eventWaitHandle");
                }

                return this._eventWaitHandle.Set();
            }
        }

        /// <summary>
        /// 设置已命名的系统事件的访问控制安全性
        /// 异常:
        ///   T:System.UnauthorizedAccessException:用户不具备 System.Security.AccessControl.EventWaitHandleRights.ChangePermissions。- 或 - 事件不是以 System.Security.AccessControl.EventWaitHandleRights.ChangePermissions打开的。
        ///   T:System.SystemException:当前 System.Threading.EventWaitHandle 对象不表示已命名的系统事件。
        ///   T:System.ObjectDisposedException:之前已在此 System.Threading.EventWaitHandle 上调用 System.Threading.WaitHandle.Close方法。
        /// </summary>
        /// <param name="eventSecurity">一个 System.Security.AccessControl.EventWaitHandleSecurity 对象，表示应用于已命名的系统事件的访问控制安全性</param>
        public void SetAccessControl(EventWaitHandleSecurity eventSecurity)
        {
            if (eventSecurity == null)
            {
                throw new ArgumentNullException("eventSecurity");
            }

            lock (this._isDisposedLock)
            {
                if (this._isDisposed)
                {
                    return;
                    //throw new ObjectDisposedException("_eventWaitHandle");
                }

                this._eventWaitHandle.SetAccessControl(eventSecurity);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// 释放资源方法
        /// </summary>
        /// <param name="isDispose">是否释放标识</param>
        protected virtual void Dispose(bool isDispose)
        {
            lock (this._isDisposedLock)
            {
                if (this._isDisposed)
                {
                    return;
                }

                this._eventWaitHandle.Dispose();
                this._isDisposed = true;
            }
        }
    }
}
