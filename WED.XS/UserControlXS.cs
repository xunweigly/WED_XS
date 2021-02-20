using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using fuzhu;
using System.Data.SqlClient;

namespace LKU8.shoukuan
{
    public partial class UserControlXS : UserControl
    {


        //string cSocode;
        Int32 iSosid;
        string cpCode;
        int iXgbs=0;
        decimal dKscsl;
        int iLastrow=-1;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dtBanChengPin = new DataTable();


        public UserControlXS()
        {
            InitializeComponent();

            ExtensionMethods.DoubleBuffered(dataGridView1, true);
            ExtensionMethods.DoubleBuffered(dataGridView2, true);
            ExtensionMethods.DoubleBuffered(dataGridView3, true);
            ExtensionMethods.DoubleBuffered(dataGridView4, true);
        }
   
        public UserControlXS(DataTable dt)
        {
            InitializeComponent();

            ExtensionMethods.DoubleBuffered(dataGridView1, true);
            ExtensionMethods.DoubleBuffered(dataGridView2, true);
            ExtensionMethods.DoubleBuffered(dataGridView3, true);
            ExtensionMethods.DoubleBuffered(dataGridView4, true);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;
            dataGridView4.AutoGenerateColumns = false;
            dt1 = dt;
            dataGridView1.DataSource = dt1;
            
        }

    
        #region 加载
      private void UserControl1_Load(object sender, EventArgs e)
        {
            Dgvfuzhu.BindReadDataGridViewStyle("销售订单处理1", dataGridView1); // 初始化布局
            Dgvfuzhu.BindReadDataGridViewStyle("销售订单处理2", dataGridView2); // 初始化布局
            Dgvfuzhu.BindReadDataGridViewStyle("销售订单处理3", dataGridView3); // 初始化布局
            Dgvfuzhu.BindReadDataGridViewStyle("销售订单处理4", dataGridView4); // 初始化布局

          
           

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
            Dgvfuzhu.SaveDataGridViewStyle("销售订单处理1", dataGridView1);
            Dgvfuzhu.SaveDataGridViewStyle("销售订单处理2", dataGridView2);
            Dgvfuzhu.SaveDataGridViewStyle("销售订单处理3", dataGridView3);
            Dgvfuzhu.SaveDataGridViewStyle("销售订单处理4", dataGridView4);
            MessageBox.Show("布局保存成功！");
        }

       public void DelBuju()
        {
            Dgvfuzhu.deleteDataGridViewStyle("销售订单处理1", dataGridView1);
            Dgvfuzhu.deleteDataGridViewStyle("销售订单处理2", dataGridView2);
            Dgvfuzhu.deleteDataGridViewStyle("销售订单处理3", dataGridView3);
            Dgvfuzhu.deleteDataGridViewStyle("销售订单处理4", dataGridView4);
            //Dgvfuzhu.BindReadDataGridViewStyle(this.Name, dataGridView1);
            MessageBox.Show("请关掉界面重新打开，恢复初始布局！");
        }
        #endregion
  

        #region BOM展开
       public void expand()
        {
            try
            {

                string cZt = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["czt"].Value);
                if (cZt!= "录入")
                {


                    CommonHelper.MsgError("当前行已处理，无法再次进行展开");
                    return;
                }

                dataGridView1.EndEdit();
               //按行展开
                iSosid = DbHelper.GetDbInt(dataGridView1.CurrentRow.Cells["isosid2"].Value);
               cpCode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvcode"].Value);
                        //检查是否有BOM
                        string sql = String.Format("select count(1) from bom_parent a,bas_part b,bom_bom c  where a.bomid= c.bomid and c.status  = 3 and  a.parentid = b.partid and b.invcode ='{0}'", cpCode);

                        int iCntb = DbHelper.GetDbInt(DbHelper.ExecuteScalar(sql));
                        if (iCntb == 0)
                        {
                            CommonHelper.MsgInformation("当前行没有BOM，无法展开，请先添加BOM");

                              return;
                        }


                        //判断是否展开过,如果是，需要把之前的先删除



                        sql = String.Format("select count(1) from zdy_wed_sobom_qr where isosid ='{0}'", iSosid);
                        int iCnt = DbHelper.GetDbInt(DbHelper.ExecuteScalar(sql));

                        if (iCnt > 0)
                        {
                            DialogResult dr = CommonHelper.MsgQuestion("当前行已展开过BOM，是否重新展开?");

                            if (dr == DialogResult.No)
                               return;
                            else
                            {
                                sql = String.Format("delete  from zdy_wed_sobom_qr where isosid ='{0}'", iSosid);
                                DbHelper.ExecuteNonQuery(sql);
                               
                            }
                        }


                        SqlParameter[] param = new SqlParameter[]{ 
                                         new SqlParameter("@isosid",iSosid),
                                         new SqlParameter("@error",SqlDbType.NVarChar,1000)};
                        param[1].Direction = ParameterDirection.Output;
                        DbHelper.ExecuteNonQuery("zdy_wed_sp_bomexpandso_qr", param, CommandType.StoredProcedure);
                        

                        string cError = param[1].Value.ToString();
                        if (cError != "ok")
                        {
                            MessageBox.Show("展开错误：" + cError);
                            return;
                        
                        
                        }

                    
                
                MessageBox.Show("展开完成！");
                CxMx(iSosid.ToString());
            
                
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

       #region 联查BOM
       public void lianchaBOM()
       {
           
           FrmTree frm = new FrmTree(iSosid,cpCode);
           frm.Show();
          
       }
       #endregion

       #region 刷新
       public  void TuiSuanYL()
       {

           CxMx(iSosid.ToString());
       }
       #endregion


       #region 联查现存量
       public void xcl()
       {
           try{
               Frm_jihua frm = new Frm_jihua(iSosid);
               frm.Show();
           }

           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               return;
           }

       }
       #endregion


       #region 处理完成
       public void chuli()
       {
           string cZt = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["czt"].Value);
           if (cZt != "录入")
           {
               CommonHelper.MsgError("当前行已处理，无法再次进行处理");
               return;
           }
           
           if (dt3.Rows.Count == 0)
           {
               CommonHelper.MsgError("没有计算生产数量，无法进行处理");
               return;
           
           }

           dataGridView1.EndEdit();
           dataGridView3.EndEdit();


           //判断展开日期间隔，做成参数
           string sqljg = string.Format("select DATEDIFF(MINUTE,max(zkdate),GETDATE())  from zdy_wed_sobom_qr where isosid = '{0}'", iSosid);
           object oJiange = DbHelper.ExecuteScalar(sqljg);
           if (oJiange != null)
           {

               sqljg = "SELECT cvalue FROM zdy_wed_para WHERE cno='99'";
               object ojg2  =  DbHelper.ExecuteScalar(sqljg);
               if (ojg2 == null)
               {
                 CommonHelper.MsgError("请到计划参数中配置，id=99的间隔时间");
               return;
               }
               else
               {
                  int dJg = DbHelper.GetDbInt(ojg2);
                  if (DbHelper.GetDbInt(oJiange) > dJg)
                  {
                      CommonHelper.MsgError(string.Format("当前处理间隔{0}分钟，超过处理时间间隔{1}，请重新展开BOM",DbHelper.GetDbInt(oJiange), dJg.ToString("")));
                      return;
                  
                  }
               
               }
                

           }



           //写销售订单可生产数量，写zdy_wed_sobom，母件用量和需求量
           try
           {
             
                       string cBz = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cmemo1"].Value);
                       string cSocode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["订单号"].Value);
                        string cInvcode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvcode"].Value);
                       if (iXgbs == 1)
                       {
                           decimal dXql =DbHelper.GetDbdecimal(dt3.Rows[0]["ixql"]);
                           decimal dKscsl2 = DbHelper.GetDbdecimal(dt3.Rows[0]["kscsl"]);
                           //dataGridView1.Rows[iLastrow].Cells["可生产数量"].Value = dXql;
                           dataGridView1.CurrentRow.Cells["czt"].Value = "已处理";
                           string sql = String.Format("update SO_SODetails set cdefine26= '{0}',cdefine33 = '已处理',cMemo = '{1}' where isosid = '{2}'", dXql, cBz, iSosid);

                           DbHelper.ExecuteNonQuery(sql);



                           sql = string.Format("insert into zdy_wed_zixun(id,czr,czdate,comment,action,lx) values ('{0}','{1}','{2}','{3}', '处理',1) ", iSosid, canshu.userName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), cBz);
                           DbHelper.ExecuteNonQuery(sql);

                           //更新母件，更新需求量
                           sql = String.Format("update zdy_wed_sobom_qr set ixql= '{0}',kscsl = '{2}' where isosid = '{1}'and lx = 2 ", dXql, iSosid, dKscsl2);
                           DbHelper.ExecuteNonQuery(sql);

                           //更新其他半成品和材料
                           for (int i2 = 0; i2 < dataGridView2.Rows.Count; i2++)
                           {
                               sql = String.Format("update zdy_wed_sobom_qr set shangjiyl='{3}', iqtybom2= '{0}',ixql = '{1}' where  id = '{2}'",
                                   dataGridView2.Rows[i2].Cells["iqtybom2"].Value,
                                         dataGridView2.Rows[i2].Cells["ixql"].Value, dataGridView2.Rows[i2].Cells["id"].Value,
                                         dataGridView2.Rows[i2].Cells["上级用量"].Value);
                               DbHelper.ExecuteNonQuery(sql);
                           }

                           for (int i2 = 0; i2 < dataGridView4.Rows.Count; i2++)
                           {
                               sql = String.Format("update zdy_wed_sobom_qr set shangjiyl='{3}', iqtybom2= '{0}',ixql = '{1}' where  id = '{2}'",
                                   dataGridView4.Rows[i2].Cells["iqtybom24"].Value,
                                         dataGridView4.Rows[i2].Cells["ixql"].Value, dataGridView4.Rows[i2].Cells["id"].Value,
                                          dataGridView4.Rows[i2].Cells["上级用量2"].Value);
                               DbHelper.ExecuteNonQuery(sql);
                           }
                           //重新更新咨询状态
                           sql = string.Format(@"  UPDATE  zdy_wed_sobom_qr
        SET     caozuo = CASE WHEN ixql = 0 THEN '4'
                              ELSE '0'
                         END
        WHERE   lx = 1  and isosid = '{0}'; 
       
        UPDATE  zdy_wed_sobom_qr
        SET     caozuo = CASE WHEN ixql = 0 THEN '4'
                              ELSE '5'
                         END
        WHERE   lx <> 1 and isosid = '{0}' ", iSosid);
                           DbHelper.ExecuteNonQuery(sql);
                           sql = string.Format("exec zdy_wed_sp_zixun {0}", iSosid);
                           DbHelper.ExecuteNonQuery(sql);
                          

                       }
                       else
                       {
                           dataGridView1.CurrentRow.Cells["czt"].Value = "已处理";
                           string sql = String.Format("update SO_SODetails set cdefine33 = '已处理',cMemo = '{0}' where isosid = '{1}'", cBz, iSosid);


                           DbHelper.ExecuteNonQuery(sql);
                           sql = string.Format("insert into zdy_wed_zixun(id,czr,czdate,comment,action,lx) values ('{0}','{1}','{2}','{3}', '处理',1) ", iSosid, canshu.userName,DateTime.Now.ToString("yyyy-MM-dd HH:mm"), cBz);
                           DbHelper.ExecuteNonQuery(sql);
                       }
                       iXgbs = 0;
                 
               CommonHelper.MsgInformation("处理完成");
               dataGridView3.ReadOnly = true;
                //发送通知,通知审核
               string re = FaSongXiaoxi.FaSong("313561", String.Format("有新的订单需要审核，订单号：{0}，产品{1}已处理完成", cSocode, cInvcode));
               if (re != "ok")
               {
                   CommonHelper.MsgError(re);

               }

               
           }
           catch (Exception ex)
           {
               CommonHelper.MsgInformation(ex.Message);
               return;
           }

       }
       #endregion

       #region 处理退回
       public void tuihui()
       {

           string cZt = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["czt"].Value);
           if (cZt == "录入")
           {


               CommonHelper.MsgError("当前行是录入状态，无需退回！");
               return;
           }
           else if (cZt != "已处理")
           
           {


               CommonHelper.MsgError(string.Format("当前行是状态{0}，不能退回，请把后面操作先退回！", cZt));
               return;
           }
           
                      
           dataGridView1.Update();
           dataGridView1.EndEdit();


          



           try
           {

               DialogResult dr = CommonHelper.MsgQuestion("确定要退回当前行吗?");

               if (dr == DialogResult.No)
                   return;
               else
               {
                   //退回，删除展开信息。bom等在重建时会删除
                   string cBz = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cmemo1"].Value);
                   string cSocode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["订单号"].Value);
                   string cInvcode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvcode"].Value);
                   int id = DbHelper.GetDbInt(dataGridView1.CurrentRow.Cells["isosid2"].Value);
                   dataGridView1.CurrentRow.Cells["czt"].Value = "录入";
                   string sql = String.Format("update SO_SODetails set cdefine33 = '录入' where isosid = '{0}'", id);
                   DbHelper.ExecuteNonQuery(sql);
                   sql = String.Format("delete  from zdy_wed_sobom_qr where isosid ='{0}'", iSosid);
                   DbHelper.ExecuteNonQuery(sql);
                   sql = string.Format("insert into zdy_wed_zixun(id,czr,czdate,comment,action,lx) values ('{0}','{1}','{2}','{3}', '退回',1) ", iSosid, canshu.userName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), cBz);
                   DbHelper.ExecuteNonQuery(sql);

                   dataGridView3.ReadOnly = false;
                   CxMx(id.ToString());
                   //    }
                   //DataView dv = dt1.DefaultView;
                   //dv.RowFilter = "czt = '录入' or czt ='审核'";
                   //dt1 = dv.ToTable();
                   //dataGridView1.DataSource = dt1;

                   //}
                   MessageBox.Show("退回完成");

                   //发送通知,通知审核
                   string re = FaSongXiaoxi.FaSong("313560", String.Format("订单号：{0}，产品：{1}已退回", cSocode, cInvcode));
                   if (re != "ok")
                   {
                       CommonHelper.MsgError(re);

                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               return;
           }

       }
       #endregion

       #region 订单关闭
       public void GuanBi()
       {

           DialogResult dr = CommonHelper.MsgQuestion("确定要关闭当前行吗?");

           if (dr == DialogResult.No)
               return;
           else
           {
               int iId = DbHelper.GetDbInt(dataGridView1.CurrentRow.Cells["isosid2"].Value);
               string cBz = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cmemo1"].Value);
               string sql = String.Format("update SO_SODetails set cdefine33 = '关闭',cSCloser ='{1}',dbclosedate=getdate(), dbclosesystime = getdate() where isosid = '{0}'", iId, canshu.userName);
               dataGridView1.CurrentRow.Cells["czt"].Value = "关闭";
               DbHelper.ExecuteNonQuery(sql);
               sql = String.Format("delete  from zdy_wed_sobom_qr where isosid ='{0}'", iSosid);
               DbHelper.ExecuteNonQuery(sql);
               sql = string.Format("insert into zdy_wed_zixun(id,czr,czdate,comment,action,lx) values ('{0}','{1}','{2}','{3}', '关闭',1) ", iId, canshu.userName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), cBz);
               DbHelper.ExecuteNonQuery(sql);

               CxMx(iId.ToString());

               MessageBox.Show("关闭完成");
           }

         
            
           
            

          

       }
       #endregion


       #region 订单打开
       public void DaKai()
       {
           string cZt = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["czt"].Value);
           if (cZt != "关闭")
           {


               CommonHelper.MsgError("当前行不是关闭状态，无需打开！");
               return;
           }

           DialogResult dr = CommonHelper.MsgQuestion("确定要打开当前行吗?");

           if (dr == DialogResult.No)
               return;
           else
           {
               int iId = DbHelper.GetDbInt(dataGridView1.CurrentRow.Cells["isosid2"].Value);
               string cBz = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cmemo1"].Value);
               string sql = String.Format("update SO_SODetails set cdefine33 = '录入',cSCloser =NULL,dbclosedate=NULL, dbclosesystime =NULL where isosid = '{0}'", iId);
               dataGridView1.CurrentRow.Cells["czt"].Value = "录入";
               DbHelper.ExecuteNonQuery(sql);
               sql = string.Format("insert into zdy_wed_zixun(id,czr,czdate,comment,action,lx) values ('{0}','{1}','{2}','{3}', '打开',1) ", iId, canshu.userName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), cBz);
               DbHelper.ExecuteNonQuery(sql);

               CxMx(iId.ToString());

               MessageBox.Show("打开完成");
           }








       }
       #endregion
       #region 点击显示明细
      
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
       {
           if (e.RowIndex != -1 && iLastrow != e.RowIndex)
           {
               if (iXgbs == 1)
               {
                   DialogResult dr = CommonHelper.MsgQuestion("已更改可生产数量，是否进行处理？");

                   if (dr == DialogResult.Yes)
                   {
                       chuli();

                   }
                   else
                       iXgbs = 0;

               }
               
               iSosid = DbHelper.GetDbInt(dataGridView1.Rows[e.RowIndex].Cells["isosid2"].Value);
           
               cpCode = DbHelper.GetDbString(dataGridView1.Rows[e.RowIndex].Cells["cinvcode"].Value);
               iLastrow = e.RowIndex;
               ZtShezhi(e.RowIndex);
               CxMx(iSosid.ToString());

               
           }
       }

        /// <summary>
        /// 设置datagridview3的只读状态
        /// </summary>
        /// <param name="e"></param>
       private void ZtShezhi(int iRow)
       {
           string cZt = DbHelper.GetDbString(dataGridView1.Rows[iRow].Cells["czt"].Value);
           if (cZt != "录入")
           {

               dataGridView3.ReadOnly = true;

           }
           else
           {
               dataGridView3.ReadOnly = false;

           }
       }


       private void CxMx(string id)
       {
           //更改类型=1
           dt2 = DbHelper.ExecuteTable(String.Format(@"select a.cinvcode,b.InvName,b.InvStd,b.ComUnitName,dengji,bili2,djyl,iqtybom,djyl2,iqtybom2,ixql,xcl,ztl,kyl,buse,id,lx,sjcinvcode,
        i.cinvname as sjname,i.cinvstd as sjgg,i.cinvdefine7 as sjgy,mjyl,dancengyl,shangjiyl,dancengyl2,csortseq
           from zdy_wed_sobom_qr a  
            inner join v_bas_inventory b on a.cinvcode = b.InvCode 
            left join inventory i on a.sjcinvcode = i.cinvcode
           where lx =1  and isosid ='{0}' order by id", id));
           dataGridView2.DataSource = dt2;
           //for (int i = 0; i < dt2.Rows.Count; i++)
           //{
           // string lx = dt2.Rows[i]["lx"].ToString();
           //    if (lx!="1")
           //    {
           //        dataGridView2.Rows[i].Visible = false;
               
           //    }

           //} 
           
           //类型= 2
               dt3 = DbHelper.ExecuteTable(String.Format(@"
select a.cinvcode,b.InvName,b.InvStd,b.ComUnitName,djyl,iqtybom,ixql,xcl,ztl,kyl,kscsl,mjyl,a.id,c.iQuantity,
CASE when ISNULL(b.InvDefine6,'')='1' THEN '否' ELSE '是' end  bZhengbao FROM zdy_wed_sobom_qr a,v_bas_inventory b,dbo.SO_SODetails c where a.cinvcode = b.InvCode 
AND a.cinvcode = c.cinvcode AND a.isosid = c.iSOsID and a.isosid ='{0}'", id));
               dataGridView3.DataSource = dt3;

               //类型= 3
               dtBanChengPin = DbHelper.ExecuteTable(String.Format(@"select a.cinvcode,b.InvName,b.InvStd,b.ComUnitName,dengji,bili2,djyl,iqtybom,djyl2,iqtybom2,ixql,xcl,ztl,kyl,buse,id,lx,sjcinvcode,
        i.cinvname as sjname,i.cinvstd as sjgg,i.cinvdefine7 as sjgy,mjyl,dancengyl,shangjiyl,dancengyl2,csortseq,dGgsl
           from zdy_wed_sobom_qr a  
            inner join v_bas_inventory b on a.cinvcode = b.InvCode 
            left join inventory i on a.sjcinvcode = i.cinvcode
           where lx =3  and isosid ='{0}' order by id", id));
               dataGridView4.DataSource = dtBanChengPin;
       }
       #endregion


                private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
                {
  
                        dataGridView1.CommitEdit((DataGridViewDataErrorContexts)123);
                        dataGridView1.BindingContext[dataGridView1.DataSource].EndCurrentEdit();

                }


                private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
                {
        try
                    {
                        dataGridView3.EndEdit();

                        if (e.RowIndex != -1 && dataGridView3.Columns[e.ColumnIndex].Name == "kscsl")
                        {

                            //不反算成品用量
                            //获得当前行的  cSort
                            iXgbs = 1;
                            string bZhengbao = DbHelper.GetDbString(dataGridView3.Rows[e.RowIndex].Cells["bZhengbao"].Value);

                            dKscsl = DbHelper.GetDbdecimal(dataGridView3.Rows[e.RowIndex].Cells["kscsl"].Value);
                            decimal dXql = DbHelper.GetDbdecimal(dataGridView3.Rows[e.RowIndex].Cells["ixql3"].Value);//之前的数量
                            string cSort = "1";

                            if (dKscsl != dXql)
                            {
                               for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                {
                                    //如果是第一层，取dKscsl


                                    string cSortCur = DbHelper.GetDbString(dtBanChengPin.Rows[i]["csortseq"]);
                                    string cSortUp = cSortCur.Substring(0, cSortCur.LastIndexOf('-'));


                                    //根据序号进行判断，是本层下级的
                                    if (cSortUp.Contains(cSort))
                                    {
                                        DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                        if (dr.Length > 0)
                                        {
                                            dtBanChengPin.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                        }
                                        else
                                        {
                                            dtBanChengPin.Rows[i]["shangjiyl"] = dKscsl;
                                        }
                                        //计算需求量，这个就不考虑相同物料重复的问题了
                                        dtBanChengPin.Rows[i]["iqtybom2"] = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["shangjiyl"]) * DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["dancengyl2"]);

                                        if (DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) > DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]))
                                        {
                                            dtBanChengPin.Rows[i]["ixql"] = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) - DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]);
                                        }
                                        else
                                        {
                                            dtBanChengPin.Rows[i]["ixql"] = 0;
                                        }
                                    }


                                }
                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {

                                    string cSortCur = DbHelper.GetDbString(dt2.Rows[i]["csortseq"]);
                                    string cSortUp = cSortCur.Substring(0, cSortCur.LastIndexOf('-'));
                                    if (cSortUp.Contains(cSort))
                                    {
                                        DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                        if (dr != null)
                                        {
                                            dt2.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                        }
                                        dt2.Rows[i]["iqtybom2"] = DbHelper.GetDbdecimal(dt2.Rows[i]["shangjiyl"]) * DbHelper.GetDbdecimal(dt2.Rows[i]["dancengyl2"]);

                                    }
                                }
                                //重写可用量
                                string sql = string.Format(@"select cinvcode,sum(kyl) kyl from zdy_wed_sobom_qr where isosid = '{0}' and lx =1 and buse= 1
group by cinvcode having count(1)>1", iSosid);
                                DataTable dtcf = DbHelper.ExecuteTable(sql);
                                if (dtcf.Rows.Count > 0)
                                {
                                    //查询重复datatable
                                    for (int n = 0; n < dtcf.Rows.Count; n++)
                                    {
                                        string cInvcodecf = DbHelper.GetDbString(dtcf.Rows[n][0]);
                                        decimal dKyl = DbHelper.GetDbdecimal(dtcf.Rows[n][1]);
                                        decimal dljkyl = 0.00m;
                                        int iM = 0;
                                        decimal iQtybom = 0;
                                        decimal dKyl2 = 0;
                                        for (int i = 0; i < dt2.Rows.Count; i++)
                                        {
                                            string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                            if (cInvcode == cInvcodecf)
                                            {
                                                iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);
                                                dKyl2 = dKyl - dljkyl;
                                                if (dKyl2 > iQtybom)
                                                {
                                                    dt2.Rows[i]["kyl"] = iQtybom;
                                                    dt2.Rows[i]["ixql"] = 0;
                                                    dljkyl = dljkyl + iQtybom;

                                                }
                                                else
                                                {
                                                    dt2.Rows[i]["kyl"] = dKyl2;
                                                    dt2.Rows[i]["ixql"] = iQtybom - dKyl2;
                                                    dljkyl = dljkyl + dKyl2;
                                                }
                                                iM = i;
                                            }

                                        }
                                        if (dKyl2 > 0)
                                        {
                                            dt2.Rows[iM]["kyl"] = dKyl2;

                                            if (iQtybom > dKyl2)
                                            {
                                                dt2.Rows[iM]["ixql"] = iQtybom - dKyl2;

                                            }
                                            else
                                            {
                                                dt2.Rows[iM]["ixql"] = 0;
                                            }
                                        }

                                    }
                                }
                                //材料可用量
                                //string bZhengbao = DbHelper.GetDbString(dataGridView3.CurrentRow.Cells["bZhengbao"].Value);
                                //最大值的id号和用量
                                int iMax = 0;
                                decimal dMaxqty = 0, dMaxyl = 1, dMaxqtyZheng = 0;  //最大用量，取整的最大
                                decimal dMaxKscsl;
                                if (bZhengbao == "是")
                                {
                                    //反算可生产数量,凑整包料，取dt2 最大值
                                   
                                    for (int i = 0; i < dt2.Rows.Count; i++)
                                    {
                                        //只计算使用的
                                        if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True")
                                        {
                                            string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                            decimal iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);
                                            decimal dDjyl = DbHelper.GetDbdecimal(dt2.Rows[i]["djyl2"]);
                                            //如果是原料的
                                            if (cInvcode.StartsWith("Y"))
                                            {
                                                if (iQtybom > dMaxqty)
                                                {
                                                    iMax = i;
                                                    dMaxqty = iQtybom;
                                                    dMaxyl = dDjyl;
                                                }
                                            }
                                        }
                                    }
                                    dMaxqtyZheng = Math.Ceiling(dMaxqty / 25) * 25;
                                    //凑整包料用量
                                    dt2.Rows[iMax]["iqtybom2"] = dMaxqtyZheng;

                                    //判断最大值是不是刚才更改的子件
                                    //DialogResult dgr = CommonHelper.MsgQuestion("是否反算成品生产量？");
                                    //if (dgr == DialogResult.Yes)
                                    //{
                                        //string cSortMax = DbHelper.GetDbString(dt2.Rows[iMax]["csortseq"]);
                                        //if (cSortMax.Contains(cSort))
                                        //{
                                            dKscsl = DbHelper.GetDbdecimal(dataGridView3.CurrentRow.Cells["kscsl"].Value);
                                            dMaxKscsl = Math.Floor((dMaxqtyZheng - dMaxqty) / dMaxyl) + dKscsl;

                                            dataGridView3.CurrentRow.Cells["ixql3"].Value = dMaxKscsl;
                                            dataGridView3.CurrentRow.Cells["kscsl"].Value = dMaxKscsl;
                                            //凑整包料 重算半成品
                                            for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                            {
                                                //如果是第一层，取dKscsl


                                                string cSortCur = DbHelper.GetDbString(dtBanChengPin.Rows[i]["csortseq"]);
                                                string cSortUp = cSortCur.Substring(0, cSortCur.LastIndexOf('-'));


                                                //根据序号进行判断，是本层下级的
                                                if (cSortUp.Contains(cSort))
                                                {
                                                    DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                                    if (dr.Length > 0)
                                                    {
                                                        dtBanChengPin.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                                    }
                                                    else
                                                    {
                                                        dtBanChengPin.Rows[i]["shangjiyl"] = dKscsl;
                                                    }
                                                    //计算需求量，这个就不考虑相同物料重复的问题了
                                                    dtBanChengPin.Rows[i]["iqtybom2"] = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["shangjiyl"]) * DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["dancengyl2"]);

                                                    if (DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) > DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]))
                                                    {
                                                        dtBanChengPin.Rows[i]["ixql"] = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) - DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]);
                                                    }
                                                    else
                                                    {
                                                        dtBanChengPin.Rows[i]["ixql"] = 0;
                                                    }
                                                }


                                            }
                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {

                                  
                                }
                                         

                                            for (int i = 0; i < dt2.Rows.Count; i++)
                                            {
                                                

                                                    string cSortCur = DbHelper.GetDbString(dt2.Rows[i]["csortseq"]);
                                                    string cSortUp = cSortCur.Substring(0, cSortCur.LastIndexOf('-'));
                                                    if (cSortUp.Contains(cSort))
                                                    {
                                                        DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                                        if (dr != null)
                                                        {
                                                            dt2.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                                        }
                                                  if (i != iMax)
                                                    {
                                                        dt2.Rows[i]["iqtybom2"] = DbHelper.GetDbdecimal(dt2.Rows[i]["shangjiyl"]) * DbHelper.GetDbdecimal(dt2.Rows[i]["dancengyl2"]);

                                                    }

                                                    ////只计算使用的
                                                    //if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True" || DbHelper.GetDbString(dt2.Rows[i]["lx"]) != "1")
                                                    //{



                                                    //}

                                                }
                                                
                                            }

                                            //重写 可用量
                                            sql = string.Format(@"select cinvcode,sum(kyl) kyl from zdy_wed_sobom_qr where isosid = '{0}' and lx =1 and buse= 1
group by cinvcode having count(1)>1", iSosid);
                                            dtcf = DbHelper.ExecuteTable(sql);
                                            if (dtcf.Rows.Count > 0)
                                            {
                                                //查询重复datatable
                                                for (int n = 0; n < dtcf.Rows.Count; n++)
                                                {
                                                    string cInvcodecf = DbHelper.GetDbString(dtcf.Rows[n][0]);
                                                    decimal dKyl = DbHelper.GetDbdecimal(dtcf.Rows[n][1]);
                                                    decimal dljkyl = 0.00m;
                                                    int iM = 0;
                                                    decimal iQtybom = 0;
                                                    decimal dKyl2 = 0;
                                                    for (int i = 0; i < dt2.Rows.Count; i++)
                                                    {
                                                        string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                                        if (cInvcode == cInvcodecf)
                                                        {
                                                            iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);
                                                            dKyl2 = dKyl - dljkyl;
                                                            //MessageBox.Show(iQtybom.ToString());
                                                            //MessageBox.Show(dKyl.ToString() + "/" + dljkyl.ToString());
                                                            if (dKyl2 > iQtybom)
                                                            {
                                                                dt2.Rows[i]["kyl"] = iQtybom;
                                                                dt2.Rows[i]["ixql"] = 0;
                                                                dljkyl = dljkyl + iQtybom;
                                                            }
                                                            else
                                                            {
                                                                dt2.Rows[i]["kyl"] = dKyl2;
                                                                dt2.Rows[i]["ixql"] = iQtybom - dKyl2;
                                                                dljkyl = dljkyl + dKyl2;
                                                            }
                                                            iM = i;
                                                        }





                                                    }
                                                    if (dKyl2 > 0)
                                                    {
                                                        dt2.Rows[iM]["kyl"] = dKyl2;

                                                        if (iQtybom > dKyl2)
                                                        {
                                                            dt2.Rows[iM]["ixql"] = iQtybom - dKyl2;

                                                        }
                                                        else
                                                        {
                                                            dt2.Rows[iM]["ixql"] = 0;
                                                        }
                                                    }

                                                }


                                            //}


                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("最大用量不是此半成品原料，不更改成品生产量");
                                        //}
                                    }

                                }

                                //不凑整包料或者其他料，取整
                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    decimal iQtybom;
                                    string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                    decimal dKyl = DbHelper.GetDbdecimal(dt2.Rows[i]["kyl"]);
                                    iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);

                                    if (cInvcode.StartsWith("S") || cInvcode.StartsWith("T"))
                                    {
                                        iQtybom = Math.Ceiling(iQtybom * 100) / 100;
                                    }
                                    else
                                    {
                                        iQtybom = Math.Ceiling(iQtybom);
                                    }

                                    //只计算使用的
                                    if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True")
                                    {
                                        dt2.Rows[i]["iqtybom2"] = iQtybom;
                                        if (iQtybom > dKyl)
                                        {

                                            dt2.Rows[i]["ixql"] = iQtybom - dKyl;
                                        }
                                        else
                                        {
                                            dt2.Rows[i]["ixql"] = 0;
                                        }
                                    }
                                }
                               
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        CommonHelper.MsgAsterisk(ex.Message);
                        return;

                    }

}
                #region 更改可生产数量
                private void dataGridView3_CellValueChanged1(object sender, DataGridViewCellEventArgs e)
                {
                    try
                    {
                        decimal dSl= 0;
                        dataGridView3.EndEdit();
                        if (e.RowIndex != -1 && dataGridView3.Columns[e.ColumnIndex].Name == "kscsl")
                        {
                            iXgbs = 1;
                            string bZhengbao =DbHelper.GetDbString(dataGridView3.Rows[e.RowIndex].Cells["bZhengbao"].Value);
                           
                                dKscsl = DbHelper.GetDbdecimal(dataGridView3.Rows[e.RowIndex].Cells["kscsl"].Value);
                                dSl = DbHelper.GetDbdecimal(dataGridView3.Rows[e.RowIndex].Cells["ixql3"].Value);//之前的数量
                                //dKscslbf = DbHelper.GetDbdecimal(dataGridView3.Rows[e.RowIndex].Cells["kscsl"].);

                                for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                {
                                    string cInvcode = DbHelper.GetDbString(dtBanChengPin.Rows[i]["cinvcode"]);
                                    decimal dDjyl = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["dancengyl2"]);
                                    decimal dKyl = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]);
                                        decimal iQtyChayi = dDjyl * (dKscsl - dSl);   //获得差异值
                                        decimal iQtybom = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) + iQtyChayi;

                                        dtBanChengPin.Rows[i]["iqtybom2"] = iQtybom;
                                        if (iQtybom > dKyl)
                                        {
                                            dtBanChengPin.Rows[i]["ixql"] = iQtybom - dKyl;
                                        }
                                        else
                                            dtBanChengPin.Rows[i]["ixql"] = 0;
                                }

                                //最大值的id号和用量
                                int iMax = 0;
                                decimal dMaxqty = 0, dMaxyl = 1, dMaxqtyZheng = 0;  //最大用量，取整的最大
                                decimal dMaxKscsl;
                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    //只计算使用的
                                    if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True" || DbHelper.GetDbString(dt2.Rows[i]["lx"]) != "1")
                                    {
                                        string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                        decimal dDjyl = DbHelper.GetDbdecimal(dt2.Rows[i]["dancengyl2"]);
                                        decimal dKyl = DbHelper.GetDbdecimal(dt2.Rows[i]["kyl"]);

                                        decimal iQtyChayi = dDjyl * (dKscsl - dSl);   //获得差异值
                                        decimal iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]) + iQtyChayi;

                                       dt2.Rows[i]["iqtybom2"] = iQtybom;
                                        if (iQtybom > dKyl)
                                        {
                                            dt2.Rows[i]["ixql"] = iQtybom - dKyl;

                                        }
                                        else
                                            dt2.Rows[i]["ixql"] = 0;

                                        //如果是原料的
                                        if (cInvcode.StartsWith("Y"))
                                        {
                                            if (iQtybom > dMaxqty)
                                            {
                                                iMax = i;
                                                dMaxqty = iQtybom;
                                                dMaxyl = dDjyl;

                                            }

                                        }
                                    }


                                }


                                if (bZhengbao == "是")
                                {
                                    //反算可生产数量,凑整包料，取dt2 最大值
                                    dMaxqtyZheng = Math.Ceiling(dMaxqty / 25) * 25;
                                    dMaxKscsl = Math.Floor((dMaxqtyZheng - dMaxqty) / dMaxyl) + dKscsl;
                                    dataGridView3.Rows[e.RowIndex].Cells["ixql3"].Value = dMaxKscsl;
                                    //凑整包料 重算半成品
                                    for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                    {
                                        decimal dDjyl = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["dancengyl2"]);
                                        decimal dKyl = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]);
                                        decimal iQtyChayi = dDjyl * (dMaxKscsl - dKscsl);   //获得差异值
                                        decimal iQtybom = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) + iQtyChayi;

                                        dtBanChengPin.Rows[i]["iqtybom2"] = iQtybom;
                                        if (iQtybom > dKyl)
                                        {
                                            dtBanChengPin.Rows[i]["ixql"] = iQtybom - dKyl;

                                        }
                                        else
                                            dtBanChengPin.Rows[i]["ixql"] = 0;
                                    }

                                    for (int i = 0; i < dt2.Rows.Count; i++)
                                    {
                                        decimal iQtybom;
                                        string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                        decimal dDjyl = DbHelper.GetDbdecimal(dt2.Rows[i]["dancengyl2"]);
                                        decimal dKyl = DbHelper.GetDbdecimal(dt2.Rows[i]["kyl"]);
                                        decimal iQtyChayi = dDjyl * (dMaxKscsl - dKscsl);   //获得差异值，和录入的值对比
                                        iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]) + iQtyChayi;
                                        
                                            if (cInvcode.StartsWith("S") || cInvcode.StartsWith("T"))
                                            {
                                                iQtybom = Math.Ceiling(iQtybom * 100) / 100;
                                            }
                                            else
                                            {
                                                iQtybom = Math.Ceiling(iQtybom);
                                            }
                                       


                                        //只计算使用的
                                        if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True" || DbHelper.GetDbString(dt2.Rows[i]["lx"]) != "1")
                                        {
                                            dt2.Rows[i]["iqtybom2"] = iQtybom;
                                            if (iQtybom > dKyl)
                                            {

                                                dt2.Rows[i]["ixql"] = iQtybom - dKyl;
                                            }
                                            else
                                            {
                                                dt2.Rows[i]["ixql"] = 0;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    //不凑整包料，没有imax
                                    iMax = -1;
                                    dMaxKscsl = dKscsl;
                                    dataGridView3.Rows[e.RowIndex].Cells["ixql3"].Value = dMaxKscsl;
                                }


                            //重写上级用量
                               
                                for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                {
                                    string cSort =DbHelper.GetDbString(dtBanChengPin.Rows[i]["csortseq"]);
                                    if (cSort.Split('-').Length == 2)
                                    {
                                        dtBanChengPin.Rows[i]["shangjiyl"] = dMaxKscsl;
                                    }
                                    else
                                    { 
                                        string cSortUp= cSort.Substring(0, cSort.LastIndexOf('-'));
                                        DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                        if( dr!=null)
                                        {
                                            dtBanChengPin.Rows[i]["shangjiyl"] =DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                        }

                                    }
                                  
                                }

                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    string cSort = DbHelper.GetDbString(dt2.Rows[i]["csortseq"]);
                                    if (cSort.Split('-').Length == 2)
                                    {
                                        dt2.Rows[i]["shangjiyl"] = dMaxKscsl;
                                    }
                                    else
                                    {
                                        string cSortUp = cSort.Substring(0, cSort.LastIndexOf('-'));
                                        DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                        if (dr != null)
                                        {
                                            dt2.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                        }
                                        else
                                        {
                                            dr = dt2.Select(string.Format("csortseq='{0}'", cSortUp));
                                            if (dr != null)
                                            {
                                                dt2.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                            }
                                        }
                                    }


                                }

                               

                           //重写 可用量
                                string sql = string.Format(@"select cinvcode,sum(kyl) kyl from zdy_wed_sobom_qr where isosid = '{0}' and lx =1 and buse= 1
group by cinvcode having count(1)>1", iSosid);
                                DataTable dtcf = DbHelper.ExecuteTable(sql);
                                if (dtcf.Rows.Count > 0)
                                {
                                    //查询重复datatable
                                    for (int n = 0; n < dtcf.Rows.Count; n++)
                                    {
                                        string cInvcodecf = DbHelper.GetDbString(dtcf.Rows[n][0]);
                                        decimal dKyl = DbHelper.GetDbdecimal(dtcf.Rows[n][1]);
                                        decimal dljkyl = 0.00m;
                                        int iM = 0;
                                        decimal  iQtybom=0;
                                        decimal dKyl2 = 0;
                                        for (int i = 0; i < dt2.Rows.Count; i++)
                                        {
                                            
                                            string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                            if (cInvcode == cInvcodecf)
                                            {

                                                iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);
                                                dKyl2 = dKyl - dljkyl;
                                                //MessageBox.Show(iQtybom.ToString());
                                                //MessageBox.Show(dKyl.ToString() + "/" + dljkyl.ToString());

                                                if (dKyl2 > iQtybom)
                                                {
                                                    dt2.Rows[i]["kyl"] = iQtybom;
                                                    dt2.Rows[i]["ixql"] = 0;
                                                    dljkyl = dljkyl + iQtybom;

                                                }
                                                else
                                                {
                                                    dt2.Rows[i]["kyl"] = dKyl2;
                                                    dt2.Rows[i]["ixql"] = iQtybom-dKyl2;
                                                    dljkyl = dljkyl + dKyl2;
                                                }
                                                iM = i;
                                            }

                                            



                                        }
                                        if (dKyl2 > 0)
                                        {
                                            dt2.Rows[iM]["kyl"] = dKyl2;

                                            if (iQtybom > dKyl2)
                                            {
                                                dt2.Rows[iM]["ixql"] = iQtybom - dKyl2;

                                            }
                                            else
                                            {
                                                dt2.Rows[iM]["ixql"] =0 ;
                                            }
                                        }
                                    
                                    }
                                
                                
                                }


                        }
                    }
                    catch (Exception ex)
                    {
                        CommonHelper.MsgAsterisk(ex.Message);
                        return;
                    
                    }
                }
                #endregion
           
                private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
                {
                    dataGridView3.CommitEdit((DataGridViewDataErrorContexts)123);
                    dataGridView3.BindingContext[dataGridView1.DataSource].EndCurrentEdit();
                }

                private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
                {
                    //TuiSuanYL();
                }

                private void dataGridView1_SelectionChanged(object sender, EventArgs e)
                {
                   
                        iSosid = DbHelper.GetDbInt(dataGridView1.CurrentRow.Cells["isosid2"].Value);

                        cpCode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvcode"].Value);
                        //iLastrow = e.RowIndex;
                        int iRow = dataGridView1.CurrentRow.Index;
                        ZtShezhi(iRow);
                        CxMx(iSosid.ToString());


                }

#region 半成品回写可生产数量

                private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
                {
                    try
                    {
                        dataGridView4.EndEdit();

                        //取消启用，dGgsl  更改为 dGgsl22
                        if (e.RowIndex != -1 && dataGridView4.Columns[e.ColumnIndex].Name == "dGgsl22")
                        {

                            //不反算成品用量
                            //获得当前行的  cSort
                            string cSort = DbHelper.GetDbString(dataGridView4.Rows[e.RowIndex].Cells["csortseq"].Value);
                            decimal dGgsl = DbHelper.GetDbdecimal(dataGridView4.Rows[e.RowIndex].Cells["dGgsl"].Value);
                            decimal dXql = DbHelper.GetDbdecimal(dataGridView4.Rows[e.RowIndex].Cells["ixql4"].Value);
                            if (dGgsl != dXql)
                            {
                                dataGridView4.Rows[e.RowIndex].Cells["ixql4"].Value = dGgsl;
                                for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                {
                                    string cSortCur = DbHelper.GetDbString(dtBanChengPin.Rows[i]["csortseq"]);
                                    string cSortUp = cSortCur.Substring(0, cSortCur.LastIndexOf('-'));
                                    //根据序号进行判断，是本层下级的
                                    if (cSortUp.Contains(cSort))
                                    {
                                        DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                        if (dr != null)
                                        {
                                            dtBanChengPin.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                        }
                                        //计算需求量，这个就不考虑相同物料重复的问题了
                                        dtBanChengPin.Rows[i]["iqtybom2"] = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["shangjiyl"]) * DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["dancengyl2"]);

                                        if (DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) > DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]))
                                        {
                                            dtBanChengPin.Rows[i]["ixql"] = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["iqtybom2"]) - DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]);
                                        }
                                        else
                                        {
                                            dtBanChengPin.Rows[i]["ixql"] = 0;
                                        }
                                    }


                                }
                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {

                                    string cSortCur = DbHelper.GetDbString(dt2.Rows[i]["csortseq"]);
                                    string cSortUp = cSortCur.Substring(0, cSortCur.LastIndexOf('-'));
                                    if (cSortUp.Contains(cSort))
                                    {
                                        DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                        if (dr != null)
                                        {
                                            dt2.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                        }
                                        dt2.Rows[i]["iqtybom2"] = DbHelper.GetDbdecimal(dt2.Rows[i]["shangjiyl"]) * DbHelper.GetDbdecimal(dt2.Rows[i]["dancengyl2"]);

                                    }
                                }
                                //重写可用量
                                string sql = string.Format(@"select cinvcode,sum(kyl) kyl from zdy_wed_sobom_qr where isosid = '{0}' and lx =1 and buse= 1
group by cinvcode having count(1)>1", iSosid);
                                DataTable dtcf = DbHelper.ExecuteTable(sql);
                                if (dtcf.Rows.Count > 0)
                                {
                                    //查询重复datatable
                                    for (int n = 0; n < dtcf.Rows.Count; n++)
                                    {
                                        string cInvcodecf = DbHelper.GetDbString(dtcf.Rows[n][0]);
                                        decimal dKyl = DbHelper.GetDbdecimal(dtcf.Rows[n][1]);
                                        decimal dljkyl = 0.00m;
                                        int iM = 0;
                                        decimal iQtybom = 0;
                                        decimal dKyl2 = 0;
                                        for (int i = 0; i < dt2.Rows.Count; i++)
                                        {
                                            string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                            if (cInvcode == cInvcodecf)
                                            {
                                                iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);
                                                dKyl2 = dKyl - dljkyl;
                                                if (dKyl2 > iQtybom)
                                                {
                                                    dt2.Rows[i]["kyl"] = iQtybom;
                                                    dt2.Rows[i]["ixql"] = 0;
                                                    dljkyl = dljkyl + iQtybom;

                                                }
                                                else
                                                {
                                                    dt2.Rows[i]["kyl"] = dKyl2;
                                                    dt2.Rows[i]["ixql"] = iQtybom - dKyl2;
                                                    dljkyl = dljkyl + dKyl2;
                                                }
                                                iM = i;
                                            }

                                        }
                                        if (dKyl2 > 0)
                                        {
                                            dt2.Rows[iM]["kyl"] = dKyl2;

                                            if (iQtybom > dKyl2)
                                            {
                                                dt2.Rows[iM]["ixql"] = iQtybom - dKyl2;

                                            }
                                            else
                                            {
                                                dt2.Rows[iM]["ixql"] = 0;
                                            }
                                        }

                                    }
                                }
                                //材料可用量
                                string bZhengbao = DbHelper.GetDbString(dataGridView3.CurrentRow.Cells["bZhengbao"].Value);
                                //最大值的id号和用量
                                int iMax = 0;
                                decimal dMaxqty = 0, dMaxyl = 1, dMaxqtyZheng = 0;  //最大用量，取整的最大
                                decimal dMaxKscsl;
                                if (bZhengbao == "是")
                                {
                                    //反算可生产数量,凑整包料，取dt2 最大值
                                   
                                    for (int i = 0; i < dt2.Rows.Count; i++)
                                    {
                                        //只计算使用的
                                        if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True")
                                        {
                                            string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                            decimal iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);
                                            decimal dDjyl = DbHelper.GetDbdecimal(dt2.Rows[i]["dancengyl2"]);
                                            //如果是原料的
                                            if (cInvcode.StartsWith("Y"))
                                            {
                                                if (iQtybom > dMaxqty)
                                                {
                                                    iMax = i;
                                                    dMaxqty = iQtybom;
                                                    dMaxyl = dDjyl;
                                                }
                                            }
                                        }
                                    }
                                    dMaxqtyZheng = Math.Ceiling(dMaxqty / 25) * 25;
                                    //凑整包料用量
                                    dt2.Rows[iMax]["iqtybom2"] = dMaxqtyZheng;

                                    //判断最大值是不是刚才更改的子件



                                    DialogResult dgr = CommonHelper.MsgQuestion("是否反算成品生产量？");
                                    if (dgr == DialogResult.Yes)
                                    {
                                        string cSortMax = DbHelper.GetDbString(dt2.Rows[iMax]["csortseq"]);
                                        if (cSortMax.Contains(cSort))
                                        {
                                            dKscsl = DbHelper.GetDbdecimal(dataGridView3.CurrentRow.Cells["kscsl"].Value);
                                            dMaxKscsl = Math.Floor((dMaxqtyZheng - dMaxqty) / dMaxyl) + dKscsl;

                                            dataGridView3.CurrentRow.Cells["ixql3"].Value = dMaxKscsl;
                                            dataGridView3.CurrentRow.Cells["kscsl"].Value = dMaxKscsl;
                                            //凑整包料 重算半成品
                                            for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                            {
                                                decimal dDjyl = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["dancengyl2"]);
                                                decimal dKyl = DbHelper.GetDbdecimal(dtBanChengPin.Rows[i]["kyl"]);
                                                decimal iQtyChayi = dDjyl * (dMaxKscsl - dKscsl);   //获得差异值
                                                decimal iQtybom = DbHelper.GetDbInt(dtBanChengPin.Rows[i]["iqtybom2"]) + iQtyChayi;  //取整

                                                dtBanChengPin.Rows[i]["iqtybom2"] = iQtybom;
                                                if (iQtybom > dKyl)
                                                {
                                                    dtBanChengPin.Rows[i]["ixql"] = iQtybom - dKyl;

                                                }
                                                else
                                                    dtBanChengPin.Rows[i]["ixql"] = 0;
                                            }

                                            for (int i = 0; i < dt2.Rows.Count; i++)
                                            {
                                                if (i != iMax)
                                                {
                                                    decimal iQtybom;
                                                    string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                                    decimal dDjyl = DbHelper.GetDbdecimal(dt2.Rows[i]["dancengyl2"]);
                                                    decimal dKyl = DbHelper.GetDbdecimal(dt2.Rows[i]["kyl"]);
                                                    decimal iQtyChayi = dDjyl * (dMaxKscsl - dKscsl);   //获得差异值，和录入的值对比
                                                    iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]) + iQtyChayi;

                                                    if (cInvcode.StartsWith("S") || cInvcode.StartsWith("T"))
                                                    {
                                                        iQtybom = Math.Ceiling(iQtybom * 100) / 100;
                                                    }
                                                    else
                                                    {
                                                        iQtybom = Math.Ceiling(iQtybom);
                                                    }



                                                    //只计算使用的
                                                    if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True" || DbHelper.GetDbString(dt2.Rows[i]["lx"]) != "1")
                                                    {
                                                        dt2.Rows[i]["iqtybom2"] = iQtybom;
                                                        if (iQtybom > dKyl)
                                                        {

                                                            dt2.Rows[i]["ixql"] = iQtybom - dKyl;
                                                        }
                                                        else
                                                        {
                                                            dt2.Rows[i]["ixql"] = 0;
                                                        }
                                                    }

                                                }
                                            }

                                            for (int i = 0; i < dtBanChengPin.Rows.Count; i++)
                                            {
                                                cSort = DbHelper.GetDbString(dtBanChengPin.Rows[i]["csortseq"]);
                                                if (cSort.Split('-').Length == 2)
                                                {
                                                    dtBanChengPin.Rows[i]["shangjiyl"] = dMaxKscsl;
                                                }
                                                else
                                                {
                                                    string cSortUp = cSort.Substring(0, cSort.LastIndexOf('-'));
                                                    DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                                    if (dr != null)
                                                    {
                                                        dtBanChengPin.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                                    }

                                                }

                                            }

                                            for (int i = 0; i < dt2.Rows.Count; i++)
                                            {
                                                cSort = DbHelper.GetDbString(dt2.Rows[i]["csortseq"]);
                                                if (cSort.Split('-').Length == 2)
                                                {
                                                    dt2.Rows[i]["shangjiyl"] = dMaxKscsl;
                                                }
                                                else
                                                {
                                                    string cSortUp = cSort.Substring(0, cSort.LastIndexOf('-'));
                                                    DataRow[] dr = dtBanChengPin.Select(string.Format("csortseq='{0}'", cSortUp));
                                                    if (dr != null)
                                                    {
                                                        dt2.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                                    }
                                                    else
                                                    {
                                                        dr = dt2.Select(string.Format("csortseq='{0}'", cSortUp));
                                                        if (dr != null)
                                                        {
                                                            dt2.Rows[i]["shangjiyl"] = DbHelper.GetDbdecimal(dr[0]["ixql"]);
                                                        }
                                                    }
                                                }


                                            }



                                            //重写 可用量
                                            sql = string.Format(@"select cinvcode,sum(kyl) kyl from zdy_wed_sobom_qr where isosid = '{0}' and lx =1 and buse= 1
group by cinvcode having count(1)>1", iSosid);
                                            dtcf = DbHelper.ExecuteTable(sql);
                                            if (dtcf.Rows.Count > 0)
                                            {
                                                //查询重复datatable
                                                for (int n = 0; n < dtcf.Rows.Count; n++)
                                                {
                                                    string cInvcodecf = DbHelper.GetDbString(dtcf.Rows[n][0]);
                                                    decimal dKyl = DbHelper.GetDbdecimal(dtcf.Rows[n][1]);
                                                    decimal dljkyl = 0.00m;
                                                    int iM = 0;
                                                    decimal iQtybom = 0;
                                                    decimal dKyl2 = 0;
                                                    for (int i = 0; i < dt2.Rows.Count; i++)
                                                    {

                                                        string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                                        if (cInvcode == cInvcodecf)
                                                        {

                                                            iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);
                                                            dKyl2 = dKyl - dljkyl;
                                                            //MessageBox.Show(iQtybom.ToString());
                                                            //MessageBox.Show(dKyl.ToString() + "/" + dljkyl.ToString());

                                                            if (dKyl2 > iQtybom)
                                                            {
                                                                dt2.Rows[i]["kyl"] = iQtybom;
                                                                dt2.Rows[i]["ixql"] = 0;
                                                                dljkyl = dljkyl + iQtybom;

                                                            }
                                                            else
                                                            {
                                                                dt2.Rows[i]["kyl"] = dKyl2;
                                                                dt2.Rows[i]["ixql"] = iQtybom - dKyl2;
                                                                dljkyl = dljkyl + dKyl2;
                                                            }
                                                            iM = i;
                                                        }





                                                    }
                                                    if (dKyl2 > 0)
                                                    {
                                                        dt2.Rows[iM]["kyl"] = dKyl2;

                                                        if (iQtybom > dKyl2)
                                                        {
                                                            dt2.Rows[iM]["ixql"] = iQtybom - dKyl2;

                                                        }
                                                        else
                                                        {
                                                            dt2.Rows[iM]["ixql"] = 0;
                                                        }
                                                    }

                                                }


                                            }


                                        }
                                        else
                                        {
                                            MessageBox.Show("最大用量不是此半成品原料，不更改成品生产量");
                                        }
                                    }

                                }

                                //不凑整包料或者其他料，取整
                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    decimal iQtybom;
                                    string cInvcode = DbHelper.GetDbString(dt2.Rows[i]["cinvcode"]);
                                    decimal dKyl = DbHelper.GetDbdecimal(dt2.Rows[i]["kyl"]);
                                    iQtybom = DbHelper.GetDbdecimal(dt2.Rows[i]["iqtybom2"]);

                                    if (cInvcode.StartsWith("S") || cInvcode.StartsWith("T"))
                                    {
                                        iQtybom = Math.Ceiling(iQtybom * 100) / 100;
                                    }
                                    else
                                    {
                                        iQtybom = Math.Ceiling(iQtybom);
                                    }

                                    //只计算使用的
                                    if (DbHelper.GetDbString(dt2.Rows[i]["buse"]) == "True")
                                    {
                                        dt2.Rows[i]["iqtybom2"] = iQtybom;
                                        if (iQtybom > dKyl)
                                        {

                                            dt2.Rows[i]["ixql"] = iQtybom - dKyl;
                                        }
                                        else
                                        {
                                            dt2.Rows[i]["ixql"] = 0;
                                        }
                                    }
                                }
                               
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        CommonHelper.MsgAsterisk(ex.Message);
                        return;

                    }
                }

    }
#endregion

}
