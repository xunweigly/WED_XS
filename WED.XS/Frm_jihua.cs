using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fuzhu ;

namespace LKU8.shoukuan
{
    public partial class Frm_jihua : Form
    {
        int iSosid;
        public Frm_jihua()
        {
            InitializeComponent();
        }

        public Frm_jihua(int isosid)
        {
            InitializeComponent();
            iSosid = isosid;
        }

        private void Frm_jihua_Load(object sender, EventArgs e)
        {
            string sql = String.Format(@"select a.cinvcode,b.invname,b.invstd,b.comunitname, a.danjulx,a.ccode,a.ddate,c.ccusname,a.xcl,a.ztl,a.wfhsl,a.wlsl,a.xql,a.yjrkdate
from zdy_wed_jihua_qr  a inner join v_bas_inventory b
    on a.cinvcode = b.invcode 
left join customer c on a.ccuscode  = c.ccuscode
            where isosid = '{0}'", iSosid);
            gridControl1.DataSource = DbHelper.ExecuteTable(sql);
        }
               #region 导出excel
        private void 输出ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.pdf)|*.pdf";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls"; DialogResult dialogResult = saveFileDialog.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                //DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();

                gridView1.OptionsPrint.AutoWidth = false;
                gridView1.ExportToXls(saveFileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

      
        #endregion
        }
    }
}
