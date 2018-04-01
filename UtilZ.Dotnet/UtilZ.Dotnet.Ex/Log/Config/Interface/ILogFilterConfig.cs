﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilZ.Dotnet.Ex.Log.Model;

namespace UtilZ.Dotnet.Ex.Log.Config.Interface
{
    /// <summary>
    /// 日志过滤器配置接口
    /// </summary>
    public interface ILogFilterConfig
    {
        /// <summary>
        /// 过滤器[true:记录,false不记录,默认false]
        /// </summary>
        LogLevel Item { get; set; }
    }
}