﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilZ.Lib.Base;
using UtilZ.Lib.Base.Foundation;
using UtilZ.Lib.DBModel.DBObject;

namespace UtilZ.Components.ConfigModel
{
    /// <summary>
    /// 配置参数作用域
    /// </summary>
    [Serializable]
    [DBTable("ConfigParaValidDomain")]
    public class ConfigParaValidDomain : BaseModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigParaValidDomain()
        {

        }

        private int _CID;

        /// <summary>
        /// 配置表ID
        /// </summary>
        [DBColumn("CID")]
        public int CID
        {
            get { return _CID; }
            set
            {
                _CID = value;
                this.OnRaisePropertyChanged("CID");
            }
        }

        private int _SID;

        /// <summary>
        /// 服务表ID
        /// </summary>
        [DBColumn("SID")]
        public int SID
        {
            get { return _SID; }
            set
            {
                _SID = value;
                this.OnRaisePropertyChanged("SID");
            }
        }
    }
}
