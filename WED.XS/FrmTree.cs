using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using fuzhu;

namespace LKU8.shoukuan
{
    public partial class FrmTree : Form
    {
        private DataTable dtTree = null;

        private DataView dv = null;
        int iSosid;
        string mjcInvcode;
        public FrmTree()
        {
            InitializeComponent();
        }
        public FrmTree(int isosid,string cpcode)
        {
            InitializeComponent();
            iSosid = isosid;
            mjcInvcode = cpcode;
        }
        #region 创建树
        private void CreateTree()
        {

            dv = dtTree.DefaultView;

            dv.Sort = "parentcode ASC";

            DataRowView[] arrDRV = dv.FindRows(0);//Get root data info

            if (arrDRV.Length == 0) return;



            TreeNode tnNew = null;

            foreach (DataRowView drv in arrDRV)
            {

                tnNew = trvDBBinding.Nodes.Add(drv.Row["cinvcode"].ToString());

                tnNew.Tag = drv.Row["cinvcode"].ToString();//Save "TypeID" in node's tag

                CreateTreeNode(ref tnNew);

            }

        }


        #region 写序号
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgv.RowHeadersDefaultCellStyle.Font, rect, dgv.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            }
        }

        #endregion
        private void CreateTreeNode(ref TreeNode tnParent)
        {

            DataRowView[] arrDRV = dv.FindRows(tnParent.Tag);//Get children data info

            if (arrDRV.Length == 0) return;



            TreeNode tnNew = null;

            foreach (DataRowView drv in arrDRV)
            {

                tnNew = tnParent.Nodes.Add(drv.Row["cinvcode"].ToString());

                tnNew.Tag = drv.Row["cinvcode"].ToString();//Save "TypeID" in node's tag

                CreateTreeNode(ref tnNew);

            }

        }
        #endregion


        #region 加载
        private void FrmTree_Load(object sender, EventArgs e)
        {
           
            //zdy_zd_sp_xzqs](@dt1 datetime ,@dt2 datetime ,@lx varchar(20))

            string sql = String.Format(@"select cinvcode,cinvname,cinvstd   from inventory  where  
                                     cinvcode = '{0}'", mjcInvcode);
            using (SqlDataReader sdr = DbHelper.ExecuteReader(sql))
            {
                while (sdr.Read())
                {
                    label1.Text = DbHelper.GetDbString(sdr[0]);
                    label3.Text = DbHelper.GetDbString(sdr[1]);
                    label5.Text = DbHelper.GetDbString(sdr[2]);
                }
            }

            sql = String.Format(@"select isnull(mj.invcode,0) as parentcode,zj.invcode as cinvcode,a.parentid,ComponentId   from zdy_wed_bomcomp_qr a
                              left join bas_part  mj on a.parentid = mj.partid
                              left join bas_part zj on  a.componentid = zj.partid
                  where isosid = '{0}'", iSosid);

            dtTree = DbHelper.ExecuteTable(sql);
            CreateTree();

            dataGridView1.AutoGenerateColumns = false;

            ExtensionMethods.DoubleBuffered(dataGridView1, true);

            ExpandTree(mjcInvcode);
        }
        #endregion

        #region 双击树节点，打开bom单层明细
        private void trvDBBinding_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string cinvcode = trvDBBinding.SelectedNode.Text;
            ExpandTree(cinvcode);
        }

        private void ExpandTree(string cinvcode)
        {
            string sql = String.Format(@"select b.cinvcode,b.cinvname,b.cinvstd from inventory b where b.cinvcode = '{0}'", cinvcode);
            using (SqlDataReader sdr = DbHelper.ExecuteReader(sql))
            {
                while (sdr.Read())
                {
                    label11.Text = DbHelper.GetDbString(sdr[0]);
                    label13.Text = DbHelper.GetDbString(sdr[1]);
                    label15.Text = DbHelper.GetDbString(sdr[2]);
                }
            }

            sql = String.Format(@"
                  SELECT   a.dengji,c.invcode,c.InvName,c.InvStd,c.ComUnitName,a.BaseQtyN,a.BaseQtyD,a.CompScrap,a.bili ,a.yongliang FROM dbo.zdy_wed_bomcomp_qr a,bas_part b ,dbo.v_bas_inventory c,bas_part mj
                  WHERE a.ComponentId = b.PartId AND b.invcode = c.InvCode AND a.parentid = mj.PartId and mj.invcode = '{0}' and isosid = '{1}'", cinvcode, iSosid);
            dataGridView1.DataSource = DbHelper.ExecuteTable(sql);
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
