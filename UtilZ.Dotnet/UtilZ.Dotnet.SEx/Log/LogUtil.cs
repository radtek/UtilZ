﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using UtilZ.Dotnet.SEx.Base;

namespace UtilZ.Dotnet.SEx.Log
{
    /// <summary>
    /// 日志操作公共类
    /// </summary>
    internal static class LogUtil
    {
        /// <summary>
        /// 获取节点指定特性值
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="attriName"></param>
        /// <returns></returns>
        public static string GetAttributeValue(this XElement ele, string attriName)
        {
            string value;
            var attri = ele.Attribute(attriName);
            if (attri != null)
            {
                value = attri.Value;
            }
            else
            {
                value = string.Empty;
            }

            return value;

        }

        /// <summary>
        /// 获取节点下指定名称子节点特性值
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="childName"></param>
        /// <param name="attriName"></param>
        /// <returns></returns>
        public static string GetChildXElementValue(this XElement ele, string childName, string attriName = null)
        {
            string value;
            var childEle = ele.XPathSelectElement(string.Format(@"param[@name='{0}']", childName));
            if (childEle == null)
            {
                value = string.Empty;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(attriName))
                {
                    attriName = "value";
                }

                value = GetAttributeValue(childEle, attriName);
            }

            return value;
        }

        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <param name="typeFullName">类型名称[格式:类型名,程序集命名.例如:Oracle.ManagedDataAccess.Client.OracleConnection,Oracle.ManagedDataAccess, Version=4.121.1.0, Culture=neutral, PublicKeyToken=89b483f429c47342]</param>
        /// <returns>实例</returns>
        public static object CreateInstance(string typeFullName)
        {
            if (string.IsNullOrWhiteSpace(typeFullName))
            {
                return null;
            }

            string[] segs = typeFullName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (segs.Length < 2)
            {
                throw new NotSupportedException(string.Format("不支持的格式{0}", typeFullName));
            }

            string assemblyFileName = segs[1].Trim();//程序集文件名称
            string assemblyPath;
            if (string.IsNullOrEmpty(Path.GetPathRoot(assemblyFileName)))
            {
                //相对工作目录的路径
                assemblyPath = Path.Combine(DirectoryInfoEx.CurrentAssemblyDirectory, assemblyFileName);
            }
            else
            {
                //全路径
                assemblyPath = assemblyFileName;
            }

            if (!File.Exists(assemblyPath))
            {
                string srcExtension = Path.GetExtension(assemblyPath).ToLower();
                List<string> extensions = new List<string> { ".dll", ".exe" };
                if (extensions.Contains(srcExtension))
                {
                    return null;
                }

                bool isFind = false;
                string tmpAssemblyPath;
                foreach (var extension in extensions)
                {
                    tmpAssemblyPath = assemblyPath + extension;
                    if (File.Exists(tmpAssemblyPath))
                    {
                        assemblyPath = tmpAssemblyPath;
                        isFind = true;
                        break;
                    }
                }

                if (!isFind)
                {
                    return null;
                }
            }

            string assemblyName = AssemblyName.GetAssemblyName(assemblyPath).FullName;
            Assembly[] assemblys = AppDomain.CurrentDomain.GetAssemblies();
            Assembly assembly = (from item in assemblys where assemblyName.Equals(item.FullName) select item).FirstOrDefault();
            if (assembly == null)
            {
                assembly = Assembly.LoadFile(assemblyPath);
            }

            Type type = assembly.GetType(segs[0].Trim(), false, true);
            if (type == null)
            {
                return null;
            }

            return Activator.CreateInstance(type);
        }
    }
}