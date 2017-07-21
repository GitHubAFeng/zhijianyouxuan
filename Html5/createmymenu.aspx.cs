using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    /// <summary>
    /// 创建自定义菜单
    /// </summary>
    public partial class createmymenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["c"] == "1")
            {
                new Hangjing.Weixin.UserDefinedMenu(Context).create();
            }
            if (Request["c"] == "2")
            {
                new Hangjing.Weixin.UserDefinedMenu(Context).deletemenu();
            }
            
        }
    }
}
