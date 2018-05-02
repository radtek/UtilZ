﻿using System;
using UtilZ.Dotnet.Ex.Base;

namespace UtilZ.Dotnet.DBIBase.DBModel.Common
{
    /// <summary>
    /// DB框架日志输出类
    /// </summary>
    public class DBLog
    {
        /// <summary>
        /// DB框架日志输输出事件
        /// </summary>
        public static Action<string, Exception> Log;

        /// <summary>
        /// 输出日志事件
        /// </summary>
        /// <param name="msg">消息</param>
        public static void OutLog(string msg)
        {
            OnRaiseLog(msg, null);
        }

        /// <summary>
        /// 输出日志事件
        /// </summary>
        /// <param name="ex">异常</param>
        public static void OutLog(Exception ex)
        {
            OnRaiseLog(null, ex);
        }

        /// <summary>
        /// 输出日志事件
        /// </summary>
        ///<param name="msg">消息</param>
        /// <param name="ex">异常</param>
        public static void OutLog(string msg, Exception ex)
        {
            OnRaiseLog(msg, ex);
        }

        /// <summary>
        /// 触发内部日志事件
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">异常</param>
        private static void OnRaiseLog(string msg, Exception ex)
        {
            try
            {
                Log.OnRaise(msg, ex);
            }
            catch (Exception exi)
            {
                Console.Write(exi.Message);
            }
        }
    }
}