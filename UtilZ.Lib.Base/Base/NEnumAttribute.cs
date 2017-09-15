﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breeze.Lib.Base.NBase
{
    /// <summary>
    /// 枚举特性
    /// </summary>
    public class NEnumAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NEnumAttribute()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">显示文本</param>
        public NEnumAttribute(string text)
            : this(text, string.Empty)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">显示的文本</param>
        /// <param name="description">描述</param>
        public NEnumAttribute(string text, string description)
            : this(text, description, 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">显示的文本</param>
        /// <param name="description">描述</param>
        /// <param name="index">显示顺序</param>
        public NEnumAttribute(string text, string description, int index)
        {
            this.Text = text;
            this.Description = description;
            this.Index = index;
        }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>返回特性文本</returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}