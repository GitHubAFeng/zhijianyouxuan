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

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class shop_SystemgsmlistDeTail : System.Web.UI.Page
{
    Hangjing.SQLServerDAL.SystemGsmList EmailbLL = new SystemGsmList();

    SystemGsmListInfo info = new SystemGsmListInfo();
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
          UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (HjNetHelper.GetQueryString("id") != "")
        {
            int id = Convert.ToInt32(HjNetHelper.GetQueryInt("id", 0));
            info = new SystemGsmListInfo();
            info = EmailbLL.GetModel(id);
            this.lbDataId.Text = Convert.ToString(info.DataId);
            this.lbTogoName.Text = Convert.ToString(info.TogoName);
            this.lbSentTime.Text = info.SentTime.ToShortDateString();
            if (info.SentType == 0)
            {
                this.lbSentType.Text = "邮件";
            }
            else
            {
                this.lbSentType.Text = "短信";
            }
            this.lbContent.Text = Convert.ToString(info.Content);
            this.lbAddDate.Text = Convert.ToString(info.AddDate);
            this.lbUserIdList.Text = Convert.ToString(info.UserIdList);
            if (info.Status == 0)
            {
                this.lbStatus.Text = "新增";
            }
            if (info.Status == 1)
            {
                this.lbStatus.Text = "进行中";
            }
            if (info.Status == 2)
            {
                this.lbStatus.Text = "完成";
            }
            if (info.Status == 3)
            {
                this.lbStatus.Text = "失败";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../shop/SetEmailGsmRecordList.aspx");
    }
}
