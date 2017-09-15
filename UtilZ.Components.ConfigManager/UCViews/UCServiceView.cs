﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilZ.Components.ConfigModel;
using UtilZ.Lib.Base.Log;
using UtilZ.Lib.Base.DataStruct.UIBinding;

namespace UtilZ.Components.ConfigManager.UCViews
{
    public partial class UCServiceView : UCViewBase
    {
        private readonly UIBindingList<ConfigParaServiceMap> _serviceList = new UIBindingList<ConfigParaServiceMap>();
        private readonly UIBindingList<ConfigParaKeyValue> _serviceParaList = new UIBindingList<ConfigParaKeyValue>();
        public UCServiceView() : base()
        {
            InitializeComponent();
        }


        private void UCServiceView_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            this.pgServiceList.SelectionChanged += this.pgServiceList_SelectionChanged;
            this.pgServiceList.ShowData("UCServiceView.ConfigParaServiceMap", this._serviceList);
            this.pgServiceParaList.ShowData("UCServiceView.ConfigParaKeyValue", this._serviceParaList);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public override void RefreshData()
        {
            try
            {
                List<ConfigParaServiceMap> serviceList = this._configLogic.GetAllConfigParaServiceMap();
                ConfigParaServiceMap selectedItem = null;
                if (pgServiceList.SelectedRows.Length > 0)
                {
                    var oldSelectedItem = ((DataGridViewRow)pgServiceList.SelectedRows[0]).DataBoundItem as ConfigParaServiceMap;
                    if (oldSelectedItem != null)
                    {
                        var ret = (from tmpItem in serviceList where oldSelectedItem.ID == tmpItem.ID select tmpItem).ToList();
                        if (ret.Count > 0)
                        {
                            selectedItem = ret[0];
                        }
                    }
                }

                this.pgServiceList.SelectionChanged -= this.pgServiceList_SelectionChanged;
                try
                {
                    this._serviceList.Clear();
                    this._serviceList.AddRange(serviceList);
                }
                finally
                {
                    DataGridView dgv = (DataGridView)pgServiceList.GridControl;
                    foreach (DataGridViewRow selectedRow in dgv.SelectedRows)
                    {
                        selectedRow.Selected = false;
                    }

                    this.pgServiceList.SelectionChanged += this.pgServiceList_SelectionChanged;
                    if (selectedItem != null)
                    {
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (row.DataBoundItem == selectedItem)
                            {
                                row.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
            }
        }

        private void pgServiceList_SelectionChanged(object sender, Lib.WinformEx.PageGrid.SelectionChangedArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            var configParas = this._configLogic.GetConfigParaKeyValueByServiceId(((ConfigParaServiceMap)e.Row).ID);
            this._serviceParaList.Clear();
            this._serviceParaList.AddRange(configParas);
        }

        private void pgServiceList_DataRowDoubleClick(object sender, Lib.WinformEx.PageGrid.DataRowDoubleClickArgs e)
        {
            if (e.Row == null)
            {
                return;
            }

            ConfigParaServiceMap serviceMap = (ConfigParaServiceMap)e.Row;
            var frm = new FServiceConfigParaKeyValueEdit(this._configLogic, serviceMap);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.pgServiceList_SelectionChanged(sender, new Lib.WinformEx.PageGrid.SelectionChangedArgs(-1, -1, e.Row, null, null));
            }
        }

        private void pgServiceParaList_DataRowDoubleClick(object sender, Lib.WinformEx.PageGrid.DataRowDoubleClickArgs e)
        {
            try
            {
                var configParaKeyValue = (ConfigParaKeyValue)e.Row;
                var frm = new FConfigParaKeyValueEdit(this._configLogic, configParaKeyValue);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.pgServiceList_SelectionChanged(sender, new Lib.WinformEx.PageGrid.SelectionChangedArgs(-1, -1, ((DataGridViewRow)pgServiceList.SelectedRows[0]).DataBoundItem, null, null));
                }
            }
            catch (Exception ex)
            {
                Loger.Error(ex);
            }
        }
    }
}