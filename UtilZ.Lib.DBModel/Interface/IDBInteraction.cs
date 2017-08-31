﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UtilZ.Lib.DBModel.Config;
using UtilZ.Lib.DBModel.Model;

namespace UtilZ.Lib.DBModel.Interface
{
    /// <summary>
    /// 数据库交互接口
    /// </summary>
    public interface IDBInteraction
    {
        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <param name="config">数据库配置</param>
        /// <returns>数据库连接对象</returns>
        IDbConnection CreateConnection(DBConfigElement config);

        /// <summary>
        /// 创建DbDataAdapter
        /// </summary>
        /// <returns>创建好的DbDataAdapter</returns>
        IDbDataAdapter CreateDbDataAdapter();

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="config">数据库配置</param>
        /// <param name="visitType">访问类型</param>
        /// <returns>数据库连接字符串</returns>
        string GetDBConStr(DBConfigElement config, DBVisitType visitType);

        /// <summary>
        /// 创建命令参数
        /// </summary>
        /// <param name="parameter">命令参数</param>
        /// <returns>创建好的命令参数</returns>
        IDbDataParameter CreateDbParameter(NDbParameter parameter);

        /// <summary>
        /// 设置命令参数
        /// </summary>
        /// <param name="cmdParameter">命令参数</param>
        /// <param name="parameter">参数</param>
        void SetParameter(IDbDataParameter cmdParameter, NDbParameter parameter);

        /// <summary>
        /// 设置命令参数
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="collection">参数集合</param>
        /// <returns>创建好的命令参数</returns>
        void SetParameter(IDbCommand cmd, NDbParameterCollection collection);

        /// <summary>
        /// 设置命令参数
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>创建好的命令参数</returns>
        void SetParameter(IDbCommand cmd, IEnumerable<IDbDataParameter> parameters);

        /// <summary>
        /// 设置命令参数
        /// </summary>
        /// <param name="cmd">命令参数</param>
        /// <param name="paraNames">参数名集合</param>
        /// <param name="values">参数值集合</param>
        void SetParameter(IDbCommand cmd, IEnumerable<string> paraNames, IEnumerable<object> values);

        /// <summary>
        /// 设置命令参数
        /// </summary>
        /// <param name="cmd">命令参数</param>
        /// <param name="paraValues">参数名及值字典集合</param>
        void SetParameter(IDbCommand cmd, Dictionary<string, object> paraValues);

        /// <summary>
        /// 生成插入SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="paraSign">数据库参数字符</param>
        /// <param name="cols">列名集合</param>
        /// <returns>插入SQL语句</returns>
        string GenerateSqlInsert(string tableName, string paraSign, IEnumerable<string> cols);

        /// <summary>
        /// 生成SQL删除语句
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="tableName">表名</param>
        /// <param name="paraSign">数据库参数字符</param>
        /// <param name="priKeyColValues">主键列名值映射字典</param>
        void GenerateSqlDelete(IDbCommand cmd, string tableName, string paraSign, Dictionary<string, object> priKeyColValues);

        /// <summary>
        /// 生成SQL删除语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="paraSign">数据库参数字符</param>
        /// <param name="priKeyCols">主键列名集合</param>
        string GenerateSqlDelete(string tableName, string paraSign, IEnumerable<string> priKeyCols);

        /// <summary>
        /// 生成SQL更新语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="paraSign">数据库参数字符</param>
        /// <param name="priKeyColValues">主键列名值映射字典</param>
        /// <param name="colValues">列名值字典</param>
        /// <param name="paraValues">参数值字典集合</param>
        /// <returns>SQL更新语句</returns>
        string GenerateSqlUpdate(string tableName, string paraSign, Dictionary<string, object> priKeyColValues, Dictionary<string, object> colValues, out Dictionary<string, object> paraValues);
    }
}
