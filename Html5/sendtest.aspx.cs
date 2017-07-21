using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    /// <summary>
    /// 发送消息测试
    /// </summary>
    public partial class sendtest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["id"];

            new Hangjing.Weixin.SendMsg(Context).sendTemplateMsg(id,"测试");


        }
    }
}
