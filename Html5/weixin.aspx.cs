using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

using Hangjing.Common;
using Hangjing.Weixin;

public partial class weixin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        weixinHelper wx = new weixinHelper(Context);

        if (wx.isJoin())//如果是网站接入
        {
            Response.Write(wx.isValidRequest());
            Response.End();
            //HJlog.toLog("如果是网站接入");
            return;
        }
        else//接收消息
        {
            Response.Write(wx.HandleData());
            HJlog.toLog("接收消息");
            Response.End();
            return;
        }

    }
}
