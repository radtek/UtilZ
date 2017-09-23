﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestUtilZDB.Model;
using UtilZ.Lib.DBBase.Core;
using UtilZ.Lib.DBBase.Interface;
using UtilZ.Lib.DBModel.Config;
using UtilZ.Lib.Winform.DropdownBox;
using UtilZ.Lib.Winform.PageGrid.Interface;

namespace TestUtilZDB
{
    public partial class FTestPageGrid : Form
    {
        public FTestPageGrid()
        {
            InitializeComponent();
        }

        private void FTestPageGrid_Load(object sender, EventArgs e)
        {
            try
            {
                List<DBConfigElement> configItems = ConfigManager.GetAllConfigItems();
                DropdownBoxHelper.BindingIEnumerableGenericToComboBox<DBConfigElement>(comDB, configItems, "ConName");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DBConfigElement GetConfig()
        {
            return DropdownBoxHelper.GetGenericFromComboBox<DBConfigElement>(comDB);
        }

        private IDBAccess GetDBAccess()
        {
            return this.GetDBAccess(this.GetConfig().DBID);
        }

        private IDBAccess GetDBAccess(int dbid)
        {
            return DBAccessManager.GetDBAccessInstance(dbid);
        }
        private void QueryPageInfo(int pageSize)
        {
            var dal = GetDBAccess();
            //var dbPageInfo = dal.QueryPageInfo(pageSize, "select count(0) from Stu");
            var dbPageInfo = dal.QueryPageInfoT<Stu>(pageSize);
            ucPageGridControl1.SetPageInfo(new PageInfo(dbPageInfo.PageCount, dbPageInfo.PageSize, 1, dbPageInfo.TotalCount));
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            this.QueryPageInfo(ucPageGridControl1.PageSizeMaximum);
        }

        private void ucPageGridControl1_PageSizeChanged(object sender, UtilZ.Lib.Winform.PageGrid.Interface.PageSizeChangedArgs e)
        {
            this.QueryPageInfo(e.PageSize);
        }


        private void ucPageGridControl1_QueryData(object sender, UtilZ.Lib.Winform.PageGrid.Interface.QueryDataArgs e)
        {
            var dal = GetDBAccess();
            var stus = dal.QueryTPaging<Stu>(e.PageSize, e.PageIndex, "ID", true);
            ucPageGridControl1.ShowData(stus, "TestUtilZDB.FTestPageGrid.ucPageGridControl1_QueryData");
        }

        private void btnSwitchPageSize_Click(object sender, EventArgs e)
        {
            ucPageGridControl1.PageSizeVisible = !ucPageGridControl1.PageSizeVisible;
        }
    }
}
