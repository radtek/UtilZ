﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilZ.Dotnet.DBIBase.Connection;
using UtilZ.Dotnet.DBIBase.Model;
using UtilZ.Dotnet.Ex.Model;

namespace UtilZ.Dotnet.DBIBase.Core
{
    public abstract partial class DBAccessAbs
    {
        #region 判断表或字段是否存在
        /// <summary>
        /// 判断表是否存在[存在返回true,不存在返回false]
        /// </summary>
        /// <param name="tableName">表名[表名区分大小写的数据库:Oracle,SQLite]</param>
        /// <returns>存在返回true,不存在返回false</returns>
        public bool ExistTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveExistTable(connectionInfo.DbConnection, tableName);
            }
        }

        /// <summary>
        /// 判断表是否存在[存在返回true,不存在返回false]
        /// </summary>
        /// <param name="con">IDbConnection</param>
        /// <param name="tableName">表名[表名区分大小写的数据库:Oracle,SQLite]</param>
        /// <returns>存在返回true,不存在返回false</returns>
        protected abstract bool PrimitiveExistTable(IDbConnection con, string tableName);

        /// <summary>
        /// 判断表中是否存在字段[存在返回true,不存在返回false]
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>存在返回true,不存在返回false</returns>
        public bool ExistField(string tableName, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveExistField(connectionInfo.DbConnection, tableName, fieldName);
            }
        }

        /// <summary>
        /// 判断表中是否存在字段[存在返回true,不存在返回false]
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>存在返回true,不存在返回false</returns>
        protected abstract bool PrimitiveExistField(IDbConnection con, string tableName, string fieldName);
        #endregion

        /// <summary>
        /// 获取表二进制字段名称集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>字段信息集合</returns>
        public List<string> GetTableBinaryFieldInfo(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            //string sqlStr = $"select * from {tableName} where 0=1";
            string sqlStr = $"select * from {tableName}";
            string pagingAssistFieldName;
            sqlStr = this.PrimitiveConvertSqlToPagingQuerySql(sqlStr, null, 1, 1, null, out pagingAssistFieldName);
            DataTable dt = this.PrimitiveQueryDataToDataTable(sqlStr);
            if (!string.IsNullOrWhiteSpace(pagingAssistFieldName))
            {
                dt.Columns.Remove(pagingAssistFieldName);
            }

            var binaryCols = new List<string>();

            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == ClrSystemType.BytesType)
                {
                    binaryCols.Add(col.ColumnName);
                }
            }

            return binaryCols;
        }

        #region 获取表的字段信息
        /// <summary>
        /// 获取表的字段信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>字段信息集合</returns>
        public List<DBFieldInfo> GetTableFieldInfoList(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveGetTableFieldInfo(connectionInfo.DbConnection, tableName);
            }
        }

        /// <summary>
        /// 获取表的字段信息
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <param name="tableName">表名</param>
        /// <returns>字段信息集合</returns>
        protected abstract List<DBFieldInfo> PrimitiveGetTableFieldInfo(IDbConnection con, string tableName);

        /// <summary>
        /// 查询主键字段集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>主键列名集合</returns>
        public List<string> QueryPriKeyField(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveQueryPriKeyField(connectionInfo.DbConnection, tableName);
            }
        }

        /// <summary>
        /// 查询主键列名集合
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <param name="tableName">表名</param>
        /// <returns>主键列名集合</returns>
        protected abstract List<string> PrimitiveQueryPriKeyField(IDbConnection con, string tableName);

        /// <summary>
        /// 获取字段的公共语言运行时类型字典集合
        /// </summary>        
        /// <param name="cols">列集合</param>
        /// <returns>字段的公共语言运行时类型字典集合</returns>
        public Dictionary<string, DBFieldType> GetFieldDbClrFieldType(DataColumnCollection cols)
        {
            var dicFieldDbClrFieldType = new Dictionary<string, DBFieldType>();
            foreach (DataColumn col in cols)
            {
                dicFieldDbClrFieldType.Add(col.ColumnName, DBHelper.GetDbClrFieldType(col.DataType));
            }

            return dicFieldDbClrFieldType;
        }
        #endregion

        #region 获取表信息
        /// <summary>
        /// 获取当前用户有权限的所有表集合
        /// </summary>
        /// <param name="getFieldInfo">是否获取字段信息[true:获取字段信息;false:不获取;默认不获取]</param>
        /// <returns>当前用户有权限的所有表集合</returns>
        public List<DBTableInfo> GetTableInfoList(bool getFieldInfo = false)
        {
            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveGetTableInfoList(connectionInfo.DbConnection, getFieldInfo);
            }
        }

        /// <summary>
        /// 获取当前用户有权限的所有表集合
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <param name="getFieldInfo">是否获取字段信息[true:获取字段信息;false:不获取;默认不获取]</param>
        /// <returns>当前用户有权限的所有表集合</returns>
        protected abstract List<DBTableInfo> PrimitiveGetTableInfoList(IDbConnection con, bool getFieldInfo);

        /// <summary>
        /// 获取表信息[表不存在返回null]
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="getFieldInfo">是否获取字段信息[true:获取字段信息;false:不获取;默认不获取]</param>
        /// <returns>表信息</returns>
        public DBTableInfo GetTableInfoByName(string tableName, bool getFieldInfo = false)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                DBIndexInfoCollection indexInfoCollection;
                if (getFieldInfo)
                {
                    indexInfoCollection = this.PrimitiveGetTableIndexs(connectionInfo.DbConnection, tableName);
                }
                else
                {
                    indexInfoCollection = null;
                }

                return this.PrimitiveGetTableInfoByName(connectionInfo.DbConnection, tableName, getFieldInfo, indexInfoCollection);
            }
        }

        /// <summary>
        /// 获取表信息[表不存在返回null]
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <param name="tableName">表名</param>
        /// <param name="getFieldInfo">是否获取字段信息[true:获取字段信息;false:不获取;默认不获取]</param>
        /// <param name="indexInfoCollection">索引集合</param>
        /// <returns>表信息</returns>
        protected abstract DBTableInfo PrimitiveGetTableInfoByName(IDbConnection con, string tableName, bool getFieldInfo, DBIndexInfoCollection indexInfoCollection);
        #endregion

        /// <summary>
        /// 获取表索引信息集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>表索引信息集合</returns>
        public DBIndexInfoCollection GetTableIndexs(string tableName)
        {
            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveGetTableIndexs(connectionInfo.DbConnection, tableName);
            }
        }

        /// <summary>
        /// 获取表索引信息集合
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <param name="tableName">表名</param>
        /// <returns>表索引信息集合</returns>
        protected abstract DBIndexInfoCollection PrimitiveGetTableIndexs(IDbConnection con, string tableName);

        /// <summary>
        /// 获取数据库版本信息
        /// </summary>
        /// <returns>数据库版本信息</returns>
        public DataBaseVersionInfo GetDataBaseVersion()
        {
            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveGetDataBaseVersion(connectionInfo.DbConnection);
            }
        }

        /// <summary>
        /// 获取数据库版本信息
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <returns>数据库版本信息</returns>
        protected abstract DataBaseVersionInfo PrimitiveGetDataBaseVersion(IDbConnection con);

        /// <summary>
        /// 获取数据库系统时间
        /// </summary>
        /// <returns>数据库系统时间</returns>
        public DateTime GetDataBaseSysTime()
        {
            using (var connectionInfo = new DbConnectionInfo(this._dbid, Model.DBVisitType.R))
            {
                return this.PrimitiveGetDataBaseSysTime(connectionInfo.DbConnection);
            }
        }

        /// <summary>
        /// 获取数据库系统时间
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <returns>数据库系统时间</returns>
        protected abstract DateTime PrimitiveGetDataBaseSysTime(IDbConnection con);
    }
}
