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
using Hangjing.Common;
using Hangjing.Model;

public partial class Admin_WebMessageLogDetail :AdminPageBase
{
    WebMessageLog bll = new WebMessageLog();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            GetwebmessageLog();
        }

    }

    protected void GetwebmessageLog()
    {
        if (HjNetHelper.GetQueryString("id") != "")
        {
            this.pageType.Text = "查看站内信发送记录";
            int id = HjNetHelper.GetQueryInt("id",0);
             WebMessageLogInfo info= bll.GetModel(id);
             this.tbname.Text = info.UserName;
             this.tbTitle.Text = info.Title;
             this.tbAddDate.Text = info.AddDate.ToShortDateString();
             this.hidDataId.Value =HjNetHelper.GetQueryInt("id",0).ToString();
        }       
    }
}
