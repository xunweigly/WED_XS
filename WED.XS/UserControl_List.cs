using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fuzhu;
using ADODB;
using MSXML2;
using UFIDA.U8.U8APIFramework;
using UFIDA.U8.U8MOMAPIFramework;
using UFIDA.U8.U8APIFramework.Parameter;
using System.Threading;
using System.Data.SqlClient;
using Process;
using UFIDA.U8.Portal.Proxy.editors;

namespace LKU8.shoukuan
{
    public partial class UserControl_List : UserControl
    {


        DataTable dtXunjia;
        //string sColname;
        

        public UserControl_List()
        {
            InitializeComponent();

            ExtensionMethods.DoubleBuffered(dataGridView1, true);
            ExtensionMethods.DoubleBuffered(dataGridView2, true);
        }

    

        #region 单元格显示按钮，参照档案
        //private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    this.dataGridView1.Controls.Clear();//移除所有控件
        //    sColname = this.dataGridView1.Columns[e.ColumnIndex].Name.ToString();
        //    if (sColname == "cinvcode" || sColname == "xmbm" || sColname == "ccusname" || sColname == "zdxjr" || sColname == "cdefine1")
        //    //if (e.ColumnIndex.Equals(this.dataGridView1.Columns["dinvcode"].Index) || e.ColumnIndex.Equals(this.dataGridView1.Columns["Dopseq"].Index) || e.ColumnIndex.Equals(this.dataGridView1.Columns["Ddefine_23"].Index))
        //    {
        //        System.Windows.Forms.Button btn = new System.Windows.Forms.Button();//创建Buttonbtn   
        //        btn.Text = "...";//设置button文字   
        //        btn.Font = new System.Drawing.Font("Arial", 7);//设置文字格式   
        //        btn.Visible = true;//设置控件允许显示  
        //        btn.BackColor = dataGridView1.ColumnHeadersDefaultCellStyle.BackColor;


        //        btn.Width = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex,
        //                        e.RowIndex, true).Height;//获取单元格高并设置为btn的宽   
        //        btn.Height = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex,
        //                        e.RowIndex, true).Height;//获取单元格高并设置为btn的高   

        //        btn.Click += new EventHandler(btn_Click);//为btn添加单击事件   

        //        this.dataGridView1.Controls.Add(btn);//dataGridView1中添加控件btn   

        //        btn.Location = new System.Drawing.Point(((this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Right) -
        //                (btn.Width)), this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Y);//设置btn显示位置   
        //    }
        //}



        //void btn_Click(object sender, EventArgs e)
        //{
        //    if (sColname == "cinvcode")
        //    {
        //        try
        //        {

        //            U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
        //            obj.RefID = "Inventory_AA";
        //            obj.Mode = U8RefService.RefModes.modeRefing;
        //            //obj.FilterSQL = " bbomsub =1";
        //            obj.FillText = dataGridView1.CurrentCell.Value.ToString();
        //            obj.Web = false;
        //            obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
        //            obj.RememberLastRst = false;
        //            ADODB.Recordset retRstGrid = null, retRstClass = null;
        //            string sErrMsg = "";
        //            obj.GetPortalHwnd((int)this.Handle);

        //            Object objLogin = canshu.u8Login;
        //            if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
        //            {
        //                MessageBox.Show(sErrMsg);
        //            }
        //            else
        //            {
        //                if (retRstGrid != null)
        //                {
        //                    //dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);
        //                    //dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells["cinvname"].Value = DbHelper.GetDbString(retRstGrid.Fields["cinvname"].Value);
        //                    //dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells["cinvstd"].Value = DbHelper.GetDbString(retRstGrid.Fields["cinvstd"].Value);
        //                    //this.textBox3.Text = DbHelper.GetDbString(retRstGrid.Fields["cdepcode"].Value);
        //                    dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["cinvcode"] = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);
        //                    dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["cinvname"] = DbHelper.GetDbString(retRstGrid.Fields["cinvname"].Value);
        //                    dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["cinvstd"] = DbHelper.GetDbString(retRstGrid.Fields["cinvstd"].Value);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("参照失败，原因：" + ex.Message);
        //        }


             
        //    }
        //    else if (sColname == "ccusname")
        //    {
        //        try
        //        {

        //            U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
        //            obj.RefID = "customer_AA";
        //            obj.Mode = U8RefService.RefModes.modeRefing;
        //            //obj.FilterSQL = " bbomsub =1";
        //            obj.FillText = dataGridView1.CurrentCell.Value.ToString();
        //            obj.Web = false;
        //            obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
        //            obj.RememberLastRst = false;
        //            ADODB.Recordset retRstGrid = null, retRstClass = null;
        //            string sErrMsg = "";
        //            obj.GetPortalHwnd((int)this.Handle);

        //            Object objLogin = canshu.u8Login;
        //            if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
        //            {
        //                MessageBox.Show(sErrMsg);
        //            }
        //            else
        //            {
        //                if (retRstGrid != null)
        //                {
        //                    //dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["ccusname"].Value);
        //                    dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["ccusname"] = DbHelper.GetDbString(retRstGrid.Fields["ccusname"].Value);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("参照失败，原因：" + ex.Message);
        //        }
        //    }
        //    else if (sColname == "xmbm")
        //    {
        //        try
        //        {

        //            U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
        //            obj.RefID = "LK_XMLXS_AA";
        //            obj.Mode = U8RefService.RefModes.modeRefing;
        //            //obj.FilterSQL = " bbomsub =1";
        //            obj.FillText = dataGridView1.CurrentCell.Value.ToString();
        //            obj.Web = false;
        //            obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
        //            obj.RememberLastRst = false;
        //            ADODB.Recordset retRstGrid = null, retRstClass = null;
        //            string sErrMsg = "";
        //            obj.GetPortalHwnd((int)this.Handle);

        //            Object objLogin = canshu.u8Login;
        //            if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
        //            {
        //                MessageBox.Show(sErrMsg);
        //            }
        //            else
        //            {
        //                if (retRstGrid != null)
        //                {
        //                    //dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cNo"].Value);
        //                    dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["xmbm"] = DbHelper.GetDbString(retRstGrid.Fields["cNo"].Value);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("参照失败，原因：" + ex.Message);
        //        }
        //    }
        //    else if (sColname == "zdxjr")
        //    {
        //        try
        //        {

        //            U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
        //            obj.RefID = "U8CUSTDEF_0016_AA";
        //            obj.Mode = U8RefService.RefModes.modeRefing;
        //            //obj.FilterSQL = " bbomsub =1";
        //            obj.FillText = dataGridView1.CurrentCell.Value.ToString();
        //            obj.Web = false;
        //            obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
        //            obj.RememberLastRst = false;
        //            ADODB.Recordset retRstGrid = null, retRstClass = null;
        //            string sErrMsg = "";
        //            obj.GetPortalHwnd((int)this.Handle);

        //            Object objLogin = canshu.u8Login;
        //            if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
        //            {
        //                MessageBox.Show(sErrMsg);
        //            }
        //            else
        //            {
        //                if (retRstGrid != null)
        //                {
        //                    dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cNo"].Value);

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("参照失败，原因：" + ex.Message);
        //        }
        //    }
        //    else if (sColname == "cdefine1")
        //    {
        //        try
        //        {

        //            U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
        //            obj.RefID = "userdefine_aa";
        //            obj.Mode = U8RefService.RefModes.modeRefing;
        //            obj.FilterSQL = " cid =01";
        //            obj.FillText = dataGridView1.CurrentCell.Value.ToString();
        //            obj.Web = false;
        //            obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
        //            obj.RememberLastRst = false;
        //            ADODB.Recordset retRstGrid = null, retRstClass = null;
        //            string sErrMsg = "";
        //            obj.GetPortalHwnd((int)this.Handle);

        //            Object objLogin = canshu.u8Login;
        //            if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
        //            {
        //                MessageBox.Show(sErrMsg);
        //            }
        //            else
        //            {
        //                if (retRstGrid != null)
        //                {
        //                    dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cvalue"].Value);

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("参照失败，原因：" + ex.Message);
        //        }
        //    }
        //}

        //private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        //{
        //    this.dataGridView1.Controls.Clear();//宽度调整时移除所有控件   
        //}

        //private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        //{
        //    this.dataGridView1.Controls.Clear();//滚动条移动时移除所有控件   
        //}
        #endregion


      
        #region 加载
      private void UserControl1_Load(object sender, EventArgs e)
        {
            Dgvfuzhu.BindReadDataGridViewStyle(this.Name, dataGridView1); // 初始化布局
            //Dgvfuzhu.BindReadDataGridViewStyle(this.Name, dataGridView2); // 初始化布局

            dataGridView1.AutoGenerateColumns = false;

            dataGridView2.AutoGenerateColumns = false;
             Cx();
        }
        #endregion


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


        #region 布局设置
       public void SaveBuju()
        {
            Dgvfuzhu.SaveDataGridViewStyle(this.Name, dataGridView1);
            MessageBox.Show("布局保存成功！");
        }

       public void DelBuju()
        {
            Dgvfuzhu.deleteDataGridViewStyle(this.Name, dataGridView1);
            //Dgvfuzhu.BindReadDataGridViewStyle(this.Name, dataGridView1);
            MessageBox.Show("请关掉界面重新打开，恢复初始布局！");
        }
        #endregion


       #region 查询
       public void Cx()
       {
           SearchCondition searchObj = new SearchCondition();
           searchObj.AddCondition("b.cinvcode", txtcinvcode.Text, SqlOperator.Like);
           searchObj.AddCondition("a.csocode", txtywy.Text, SqlOperator.Like);
           //searchObj.AddCondition("a.cmaker", txtywy.Text, SqlOperator.Equal);
           //searchObj.AddCondition("cpersoncode", txtywy2.Text, SqlOperator.Equal);
           searchObj.AddCondition("ccusname", txtcCusname.Text, SqlOperator.Like);
           searchObj.AddCondition("ddate", dateTimePicker1.Value.ToString("yyyy-MM-dd"), SqlOperator.MoreThanOrEqual, dateTimePicker1.Checked == false);
           searchObj.AddCondition("ddate", dateTimePicker2.Value.ToString("yyyy-MM-dd"), SqlOperator.LessThanOrEqual, dateTimePicker2.Checked == false);
           //searchObj.AddCondition("isnull(b.cdefine33,'录入')", comboBox1.Text, SqlOperator.Equal);


           string conditionSql = searchObj.BuildConditionSql(2);

           //StringBuilder strb = new StringBuilder(@"SELECT 0 as xz,* from zdy_lk_xunjia where cmaker = '"+canshu.userName+"'");
           StringBuilder strb = new StringBuilder(@"select 0 as xz, lastaction.action,  a.cSOCode ,a.cCusCode ,a.cCusName,p.cPsn_Name,a.dDate,b.cInvCode,i.InvName,i.InvStd,i.ComUnitName,b.iQuantity,dpredate,isnull(cdefine33,'录入') czt,
(case when ISNULL(cdefine9,'')<>'' then ISNULL(cdefine12,'')+'/'+ISNULL(cdefine9,'') else cdefine12 end) AS cdefine12,cdefine26 ,isosid,'' as cmemo1 from SO_SOMain a
inner join SO_SODetails b on a.ID = b.id
inner join Customer c on a.cCusCode = c.cCusCode
inner join v_bas_inventory i on b.cInvCode = i.InvCode
left join hr_hi_person p on a.cPersonCode = p.cPsn_Num
LEFT JOIN (SELECT  a.id, action FROM dbo.zdy_wed_zixun a
INNER JOIN (SELECT id,MAX(autoid) autoid FROM dbo.zdy_wed_zixun GROUP BY id) b ON a.autoid = b.autoid
) lastaction ON lastaction.id =b.isosid
where 1=1 and  isnull(i.invdefine3,'')<>'是' ");
           strb.Append(conditionSql);


           if (comboBox1.Text == "录入")
           {

               strb.Append(" and isnull(b.cdefine33,'录入') = '录入' ");
           }
           else if (comboBox1.Text == "已处理未审核")
           {
              
               strb.Append(" and isnull(b.cdefine33,'录入') ='已处理' ");
           }
           else if (comboBox1.Text == "关闭")
           {

               strb.Append(" and isnull(b.cdefine33,'录入') ='关闭' ");
           }
           else
           {

               strb.Append(" and isnull(b.cdefine33,'录入') in ('已处理','已审核','咨询完成','已确认') ");
           }
       


           dtXunjia = DbHelper.ExecuteTable(strb.ToString());



           dataGridView1.DataSource = dtXunjia;

           //grid1.c


       }
       #endregion


        #region 订单处理
       public void Add()
        {
            if (dtXunjia != null)
            {
                dataGridView1.EndEdit();

                txtcinvcode.Focus();

                try
                {
                    //如果状态是已审核或者已确认，无法进行处理

                    //DataRow[] drs3 = dtXunjia.Select("xz =  1 and czt <> '未审核' ");
                    //if (drs3.Length > 0)
                    //{
                    //    MessageBox.Show("本次选择集中包含了已审核行，不可再次处理，请取消");
                    //    return;


                    //}
                    DataRow[] drs = dtXunjia.Select("xz =  1  ");

                    if (drs.Length == 0)
                    {
                        MessageBox.Show("没有选择数据，请选择！");
                        return;


                    }

                    DataTable dt = dtXunjia.Clone();
                    foreach (DataRow dr in drs)
                    {
                        dt.ImportRow(dr);//复制行数据到新表                
                    }


                    //销售处理
                    INetUserControl mycontrol = new MyNetUserControlXS();
                    mycontrol.Title = "项目订单处理";
                    canshu.dDt = dt;
                    GlobalParameters.gLoginable.ShowEmbedControl(mycontrol, "010103", true);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
        #endregion

   


       #region 联查
       public void Liancha()
       {
          

           string cInvcode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvcode"].Value);
           string cInvname = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvname"].Value);
           string cInvstd = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvstd"].Value);


           INetUserControl mycontrol = new MyNetUserControlXS();
           mycontrol.Title = "项目订单确认";
           canshu.cSocode = "123";
           GlobalParameters.gLoginable.ShowEmbedControl(mycontrol, "010103", true);
       }
       #endregion



       #region 输入条件 参照
       private void button1_Click(object sender, EventArgs e)
       {
           try
           {

               U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
               obj.RefID = "Inventory_AA";
               obj.Mode = U8RefService.RefModes.modeRefing;
               //obj.FilterSQL = " bbomsub =1";
               obj.FillText =txtcinvcode.Text;
               obj.Web = false;
               obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
               obj.RememberLastRst = false;
               ADODB.Recordset retRstGrid = null, retRstClass = null;
               string sErrMsg = "";
               obj.GetPortalHwnd((int)this.Handle);

               Object objLogin = canshu.u8Login;
               if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
               {
                   MessageBox.Show(sErrMsg);
               }
               else
               {
                   if (retRstGrid != null)
                   {

                       this.txtcinvcode.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("参照失败，原因：" + ex.Message);
           }
       }

       private void button2_Click(object sender, EventArgs e)
       {
           try
           {

               U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
               obj.RefID = "hr_hi_person_AA";
               obj.Mode = U8RefService.RefModes.modeRefing;
               //obj.FilterSQL = " cdepcode in ('01','04','07')  and rpersontype =10";
               obj.FillText = txtywy.Text;
               obj.Web = false;
               obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
               obj.RememberLastRst = false;
               ADODB.Recordset retRstGrid = null, retRstClass = null;
               string sErrMsg = "";
               obj.GetPortalHwnd((int)this.Handle);

               Object objLogin = canshu.u8Login;
               if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
               {
                   MessageBox.Show(sErrMsg);
               }
               else
               {
                   if (retRstGrid != null)
                   {

                       this.txtywy.Text = DbHelper.GetDbString(retRstGrid.Fields["cpsn_name"].Value);
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("参照失败，原因：" + ex.Message);
           }
       }


       private void button3_Click(object sender, EventArgs e)
       {
           try
           {

               U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
               obj.RefID = "hr_hi_person_AA";
               obj.Mode = U8RefService.RefModes.modeRefing;
               //obj.FilterSQL = " cdepcode in ('01','04','07')  and rpersontype =10";
               obj.FillText = txtywy2.Text;
               obj.Web = false;
               obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
               obj.RememberLastRst = false;
               ADODB.Recordset retRstGrid = null, retRstClass = null;
               string sErrMsg = "";
               obj.GetPortalHwnd((int)this.Handle);

               Object objLogin = canshu.u8Login;
               if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
               {
                   MessageBox.Show(sErrMsg);
               }
               else
               {
                   if (retRstGrid != null)
                   {

                       this.txtywy2.Text = DbHelper.GetDbString(retRstGrid.Fields["cpsn_name"].Value);
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("参照失败，原因：" + ex.Message);
           }
       }
       #endregion


                private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
                {


                        dataGridView1.CommitEdit((DataGridViewDataErrorContexts)123);
                        dataGridView1.BindingContext[dataGridView1.DataSource].EndCurrentEdit();

                }

                private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
                {
                    if (dataGridView1 != null)
                    {
                        string sv = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        Clipboard.SetData(DataFormats.Text, sv);
                    }
                }

              

                #region
                private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
                {
                    dataGridView1.CommitEdit((DataGridViewDataErrorContexts)123);
                    dataGridView1.BindingContext[dataGridView1.DataSource].EndCurrentEdit();
          

                }
                #endregion
 #region 进入当期行，显示咨询历史记录
                private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
                {

                    int iId = DbHelper.GetDbInt(dataGridView1.Rows[e.RowIndex].Cells["isosid"].Value);
                    string sql = string.Format(@"SELECT  id,czr,czdate,comment,action    FROM dbo.zdy_wed_zixun where id = '{0}' and lx = 1 order by autoid desc", iId);
                   dataGridView2.DataSource =   DbHelper.ExecuteTable(sql);

                }
 #endregion











    }
}
