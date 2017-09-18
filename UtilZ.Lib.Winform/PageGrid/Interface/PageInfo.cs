﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilZ.Lib.Winform.PageGrid.Interface
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前查询的数据页</param>
        /// <param name="pageCount">页数</param>
        /// <param name="count">数据条数</param>
        /// <param name="totalCount">总数据记录数</param>
        /// <param name="pageSize">页大小</param>
        public PageInfo(int pageIndex, int pageCount, int count, long totalCount, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageCount = pageCount;
            this.Count = count;
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 分总页数
        /// </summary>
        public int PageCount { get; private set; }

        /// <summary>
        /// 当前页数据记录数
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 数据总记录数
        /// </summary>
        public long TotalCount { get; private set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 记录索引是否显示为当前页索引
        /// </summary>
        private bool _isCurrentPage = false;

        /// <summary>
        /// 记录索引是否显示为当前页索引[true:显示当前页索引;false:显示总记录索引;默认为false]
        /// </summary>
        public bool IsCurrentPage
        {
            get { return _isCurrentPage; }
            set { _isCurrentPage = value; }
        }
    }
}
