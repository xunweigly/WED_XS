using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using LKU8.shoukuan;
using System.Text.RegularExpressions;

namespace fuzhu
{
    static class FaSongXiaoxi
    {
        public static string FaSong(string xsid,string cmemo)
        {
            try
            {
                //获得消息通知参数
                string sql = String.Format("select ccardname,lx,cmemnu  from zdy_wed_xiaoxi where cno = '{0}'", xsid);
                DataTable dt = DbHelper.ExecuteTable(sql);
                if (dt.Rows.Count == 0)
                {
                    return xsid + "没有配置通知参数，无法进行通知";


                }
                else
                {
                    string cMc = DbHelper.GetDbString(dt.Rows[0]["ccardname"]);
                    string clx = DbHelper.GetDbString(dt.Rows[0]["lx"]);
                    string cmemnu = DbHelper.GetDbString(dt.Rows[0]["cmemnu"]);

                    //判断通知人员
                    sql =string.Format( "SELECT cpersoncode FROM zdy_wed_tongzhi where lx = '{0}'",clx);
                    DataTable dtry = DbHelper.ExecuteTable(sql);
                    if (dtry.Rows.Count == 0)
                    {
                        return clx + "没有设置通知人员，无法进行通知";


                    }
                    else
                    {
                    //设置通知
                        sql =string.Format(@"If Not Exists (Select 1 From UFSystem..UA_MessageType where MsgTypeId = {0})
    Insert Into UFSystem..UA_MessageType(MsgTypeId,MsgTypeName,HandlerName,SourceId,NeedProcess,OnlySupportBS)
    values( {0},'{1}','Notice','04',1,0)
if not exists(select 1 from UFSystem..UA_NavigationNode where Name=N'{0}')
   
    insert into Ufsystem..UA_NavigationNode(Name,Text,Visible,MsgTypeID,ParentNodeName)
    values(N'{0}',N'{1}',0,N'{0}',N'Alarmed')", xsid,cMc);
                        DbHelper.ExecuteNonQuery(sql);

                        //通知人员

                        string cMsg = string.Format("		{0}		a:{1}	y:{2}", cmemnu, canshu.acc, canshu.ztYear);
                        for (int i = 0; i < dtry.Rows.Count ; i++)
                        {

                        	sql=string.Format(@"INSERT INTO UFSystem..ua_message (cmsgid,nmsgtype,cmsgtitle,cmsgcontent,csender,creceiver,dsend,nvalidday,bhasread,nurgent, account,[year],cmsgpara)
                        VALUES(newid(),{0},'{1}','{2}','{3}','{4}',getdate(),4,0,0,'{5}','{6}','{7}')",
                        xsid, cmemo, cmemo, canshu.u8Login.cUserId, DbHelper.GetDbString(dtry.Rows[i]["cpersoncode"]), canshu.acc, canshu.ztYear, cMsg);
                            DbHelper.ExecuteNonQuery(sql);
                        }
                    
                    
                    }
                
                }




            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "ok";
        }

    }
}
