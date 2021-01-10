namespace LKU8.shoukuan
{
    partial class Frm_jihua
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cinvcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.invname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.invstd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.单位 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.单据类型 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.单据号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.日期 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.客户名称 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xcl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ztl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.wfhsl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.wlsl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xql = new DevExpress.XtraGrid.Columns.GridColumn();
            this.yjrkdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.输出ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 32);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1266, 583);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cinvcode,
            this.invname,
            this.invstd,
            this.单位,
            this.单据类型,
            this.单据号,
            this.日期,
            this.客户名称,
            this.xcl,
            this.ztl,
            this.wfhsl,
            this.wlsl,
            this.xql,
            this.yjrkdate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // cinvcode
            // 
            this.cinvcode.Caption = "存货编码";
            this.cinvcode.FieldName = "cinvcode";
            this.cinvcode.Name = "cinvcode";
            this.cinvcode.Visible = true;
            this.cinvcode.VisibleIndex = 0;
            this.cinvcode.Width = 91;
            // 
            // invname
            // 
            this.invname.Caption = "存货名称";
            this.invname.FieldName = "invname";
            this.invname.Name = "invname";
            this.invname.Visible = true;
            this.invname.VisibleIndex = 1;
            this.invname.Width = 86;
            // 
            // invstd
            // 
            this.invstd.Caption = "规格";
            this.invstd.FieldName = "invstd";
            this.invstd.Name = "invstd";
            this.invstd.Visible = true;
            this.invstd.VisibleIndex = 2;
            // 
            // 单位
            // 
            this.单位.Caption = "单位";
            this.单位.FieldName = "comunitname";
            this.单位.Name = "单位";
            this.单位.Visible = true;
            this.单位.VisibleIndex = 3;
            // 
            // 单据类型
            // 
            this.单据类型.Caption = "单据类型";
            this.单据类型.FieldName = "danjulx";
            this.单据类型.Name = "单据类型";
            this.单据类型.Visible = true;
            this.单据类型.VisibleIndex = 4;
            this.单据类型.Width = 99;
            // 
            // 单据号
            // 
            this.单据号.Caption = "单据号";
            this.单据号.FieldName = "ccode";
            this.单据号.Name = "单据号";
            this.单据号.Visible = true;
            this.单据号.VisibleIndex = 5;
            // 
            // 日期
            // 
            this.日期.Caption = "日期";
            this.日期.FieldName = "ddate";
            this.日期.Name = "日期";
            this.日期.Visible = true;
            this.日期.VisibleIndex = 6;
            // 
            // 客户名称
            // 
            this.客户名称.Caption = "客户名称";
            this.客户名称.FieldName = "ccusname";
            this.客户名称.Name = "客户名称";
            this.客户名称.Visible = true;
            this.客户名称.VisibleIndex = 7;
            this.客户名称.Width = 98;
            // 
            // xcl
            // 
            this.xcl.Caption = "现存量";
            this.xcl.DisplayFormat.FormatString = "0.00";
            this.xcl.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xcl.FieldName = "xcl";
            this.xcl.Name = "xcl";
            this.xcl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.xcl.Visible = true;
            this.xcl.VisibleIndex = 8;
            // 
            // ztl
            // 
            this.ztl.Caption = "在途数量";
            this.ztl.DisplayFormat.FormatString = "0.00";
            this.ztl.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ztl.FieldName = "ztl";
            this.ztl.Name = "ztl";
            this.ztl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.ztl.Visible = true;
            this.ztl.VisibleIndex = 12;
            this.ztl.Width = 106;
            // 
            // wfhsl
            // 
            this.wfhsl.Caption = "未发货数量";
            this.wfhsl.DisplayFormat.FormatString = "0.00";
            this.wfhsl.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.wfhsl.FieldName = "wfhsl";
            this.wfhsl.Name = "wfhsl";
            this.wfhsl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.wfhsl.Visible = true;
            this.wfhsl.VisibleIndex = 9;
            this.wfhsl.Width = 98;
            // 
            // wlsl
            // 
            this.wlsl.Caption = "未领数量";
            this.wlsl.DisplayFormat.FormatString = "0.00";
            this.wlsl.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.wlsl.FieldName = "wlsl";
            this.wlsl.Name = "wlsl";
            this.wlsl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.wlsl.Visible = true;
            this.wlsl.VisibleIndex = 10;
            this.wlsl.Width = 99;
            // 
            // xql
            // 
            this.xql.Caption = "占用数量";
            this.xql.DisplayFormat.FormatString = "0.00";
            this.xql.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xql.FieldName = "xql";
            this.xql.Name = "xql";
            this.xql.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.xql.Visible = true;
            this.xql.VisibleIndex = 11;
            this.xql.Width = 102;
            // 
            // yjrkdate
            // 
            this.yjrkdate.Caption = "预计入库日期";
            this.yjrkdate.FieldName = "yjrkdate";
            this.yjrkdate.Name = "yjrkdate";
            this.yjrkdate.Visible = true;
            this.yjrkdate.VisibleIndex = 13;
            this.yjrkdate.Width = 128;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.输出ExcelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1266, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 输出ExcelToolStripMenuItem
            // 
            this.输出ExcelToolStripMenuItem.Name = "输出ExcelToolStripMenuItem";
            this.输出ExcelToolStripMenuItem.Size = new System.Drawing.Size(101, 28);
            this.输出ExcelToolStripMenuItem.Text = "输出Excel";
            this.输出ExcelToolStripMenuItem.Click += new System.EventHandler(this.输出ExcelToolStripMenuItem_Click);
            // 
            // Frm_jihua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 615);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_jihua";
            this.Text = "计划计算数量查询";
            this.Load += new System.EventHandler(this.Frm_jihua_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn cinvcode;
        private DevExpress.XtraGrid.Columns.GridColumn invname;
        private DevExpress.XtraGrid.Columns.GridColumn invstd;
        private DevExpress.XtraGrid.Columns.GridColumn 单位;
        private DevExpress.XtraGrid.Columns.GridColumn 单据类型;
        private DevExpress.XtraGrid.Columns.GridColumn 单据号;
        private DevExpress.XtraGrid.Columns.GridColumn 日期;
        private DevExpress.XtraGrid.Columns.GridColumn 客户名称;
        private DevExpress.XtraGrid.Columns.GridColumn xcl;
        private DevExpress.XtraGrid.Columns.GridColumn wfhsl;
        private DevExpress.XtraGrid.Columns.GridColumn wlsl;
        private DevExpress.XtraGrid.Columns.GridColumn xql;
        private DevExpress.XtraGrid.Columns.GridColumn ztl;
        private DevExpress.XtraGrid.Columns.GridColumn yjrkdate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 输出ExcelToolStripMenuItem;
    }
}