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

public partial class shop_EmailGsmRecDeTail : System.Web.UI.Page
{
    EmailGsmRecord EmailbLL = new EmailGsmRecord();
    EmailGsmRecordInfo info = new EmailGsmRecordInfo();
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
        if (HjNetHelper.GetQueryString("id") != "")
        {
            int id = Convert.ToInt32(HjNetHelper.GetQueryInt("id", 0));
            info = new EmailGsmRecordInfo();
            info = EmailbLL.GetModel(id);
            this.lbDataId.Text = Convert.ToString(info.DataId);
            //this.lbTogoName.Text = Convert.ToString(info.TogoName);
            this.lbDelMoney.Text = Convert.ToString(info.DelMoney);
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
            this.lbStatus.Text = Convert.ToString(info.Status);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../shop/EmailGsmRecordList.aspx");
    }
}
