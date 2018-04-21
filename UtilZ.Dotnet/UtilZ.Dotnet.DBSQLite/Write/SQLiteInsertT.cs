﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UtilZ.Dotnet.DBIBase.DBBase.Core;
using UtilZ.Dotnet.DBIBase.DBBase.Interface;
using UtilZ.Dotnet.DBIBase.DBModel.DBObject;
using UtilZ.Dotnet.DBSQLite.Interface;

namespace UtilZ.Dotnet.DBSQLite.Write
{
    /// <summary>
    /// SQLite泛型插入操作对象
    /// </summary>
    [Serializable]
    public class SQLiteInsert<T> : SQLiteWriteBase where T : class
    {
        /// <summary>
        /// 插入项
        /// </summary>
        private T _item;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="waitTimeout">等待超时时间(-1表示无限等待)</param>
        /// <param name="item">插入项</param>
        public SQLiteInsert(int waitTimeout, T item) : base(waitTimeout, 1)
        {
            this._item = item;
        }

        /// <summary>
        /// 执行写入操作
        /// </summary>
        /// <param name="sqliteDBAccess">SQLite数据库访问对象</param>
        public override object Excute(ISQLiteDBAccessBase sqliteDBAccess)
        {
            switch (this._type)
            {
                case 1:
                    this.Result = sqliteDBAccess.BaseInsertT<T>(this._item);
                    break;
                default:
                    throw new NotImplementedException(string.Format("未实现的泛型插入类型{0}", this._type));
            }

            return this.Result;
        }
    }
}
