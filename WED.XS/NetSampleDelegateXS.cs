using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFIDA.U8.Portal.Framework.Actions;
using System.Windows.Forms;

namespace LKU8.shoukuan
{
    class NetSampleDelegateXS : IActionDelegate
    {
        #region IActionDelegate 成员

        //代理，和菜单项关联。
        void IActionDelegate.Run(IAction action)
        {
            //action.tag 把实例化的 user control 传过来了。
            //使用时，直接使用 action.tag就可以了。方法要使用public，才可以用。

            
            UserControlXS us = (UserControlXS)action.Tag;
           
                switch (action.Id)
                {
                  
                    case "expand":
                        {
                            us.expand();


                            return;
                        }
                    case "xcl":
                        {
                            us.xcl();


                            return;
                        }
                    case "lianchaBOM":
                        {
                            us.lianchaBOM();


                            return;
                        }
                    case "chuli":
                        {
                            us.chuli();


                            return;
                        }
                    case "tuihui":
                        {
                            us.tuihui();


                            return;
                        }
                    case "TuiSuanYL":
                        {
                            us.TuiSuanYL();


                            return;
                        }
                    case "savebuju":
                        {
                            us.SaveBuju();


                            return;
                        }
                    case "delbuju":
                        {
                            us.DelBuju();


                            return;
                        }
                    case "GuanBi":
                        {
                            us.GuanBi();


                            return;
                        }
                    case "DaKai":
                        {
                            us.DaKai();


                            return;
                        }
                        
                   
                }
                MessageBox.Show("press a Toolbar ID is '"+ action.Id+"'");
            
        }


    
        void IActionDelegate.SelectionChanged(IAction action, UFIDA.U8.Portal.Common.Core.ISelection selection)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
