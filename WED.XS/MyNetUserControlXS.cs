using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFIDA.U8.Portal.Proxy.editors;
using UFIDA.U8.Portal.Framework.MainFrames;
using UFIDA.U8.Portal.Framework.Actions;
using UFIDA.U8.Portal.Proxy.Actions;

namespace LKU8.shoukuan
{
    class MyNetUserControlXS : INetUserControl
    {
        #region INetUserControl 成员

        UserControlXS usercontrol = null;
        //private IEditorInput _editInput = null;
        //private IEditorPart _editPart = null;
        private string _title;
        //private string _csocode;
        public System.Windows.Forms.Control CreateControl(UFSoft.U8.Framework.Login.UI.clsLogin login, string MenuID, string Paramters)
        {
            usercontrol = new UserControlXS(canshu.dDt);
            usercontrol.Name = "LKxsxunjia";
            return usercontrol;
            //throw new NotImplementedException();
        }

        public UFIDA.U8.Portal.Proxy.Actions.NetAction[] CreateToolbar(UFSoft.U8.Framework.Login.UI.clsLogin login)
        {
            IActionDelegate nsd = new NetSampleDelegateXS();
            ////string skey = "mynewcontrol";
            
            NetAction[] aclist;
            aclist = new NetAction[10];
            NetAction ac = new NetAction("expand", nsd);
            ac.Text = "BOM展开";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.add;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "增行";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[0] = ac;

            ac = new NetAction("lianchaBOM", nsd);
            //aclist = new NetAction[1];
            ac.Text = "联查BOM";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.bom;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "删行";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[1] = ac;

            ac = new NetAction("xcl", nsd);
            //aclist = new NetAction[1];
            ac.Text = "联查可用量";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.approval_query;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "询价";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[2] = ac;


            ac = new NetAction("TuiSuanYL", nsd);
            //aclist = new NetAction[1];
            ac.Text = "刷新";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.calculate_banlance;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "保存";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[3] = ac;



            ac = new NetAction("chuli", nsd);
            //aclist = new NetAction[1];
            ac.Text = "单据处理";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Approve_all;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "打开";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[4] = ac;


            
            



           

            ac = new NetAction("tuihui", nsd);
            //aclist = new NetAction[1];
            ac.Text = "处理退回";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Cancel;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "打开";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[5] = ac;

            ac = new NetAction("GuanBi", nsd);
            //aclist = new NetAction[1];
            ac.Text = "关闭订单";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.Cancel_discount ;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "打开";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[6] = ac;

            ac = new NetAction("DaKai", nsd);
            //aclist = new NetAction[1];
            ac.Text = "打开订单";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.open;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "打开";
            ac.SetGroupRow = 1;
            ac.RowSpan = 3;
            aclist[7] = ac;


            ac = new NetAction("savebuju", nsd);
            //aclist = new NetAction[1];
            ac.Text = "保存布局";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.import;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "保存布局";
            ac.SetGroupRow = 1;
            ac.RowSpan = 1;
            aclist[8] = ac;

            ac = new NetAction("delbuju", nsd);
            //aclist = new NetAction[1];
            ac.Text = "删除布局";
            ac.Tag = usercontrol;
            ac.Image = Properties.Resources.import;
            ac.ActionType = NetAction.NetActionType.Edit;
            ac.DisplayStyle = 1;
            ac.Style = 1;
            ac.SetGroup = "保存布局";
            ac.SetGroupRow = 1;
            ac.RowSpan = 1;
            aclist[9] = ac;
            return aclist;
            ////return null;
        }
        public bool CloseEvent()
        {
            //throw new Exception("The method or operation is not implemented.");
            return true;
        }
        #endregion



        IEditorInput INetUserControl.EditorInput
        {
            get;
            set;
        }

        IEditorPart INetUserControl.EditorPart
        {
            get;set;

        }

        string INetUserControl.Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

     


    }


}
