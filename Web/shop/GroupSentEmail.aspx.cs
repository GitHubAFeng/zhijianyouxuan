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
using System.Threading;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class shop_GroupSentEmail : System.Web.UI.Page
{
    ECustomer CBLL = new ECustomer();
    EmailGsmRecord bll = new EmailGsmRecord();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!Page.IsPostBack)
        {
            AlertScript.RegisterAdminPageClientScriptBlock(this.Page);
        }

        string UserEmailList = "";

        EmailGsmRecordInfo info = new EmailGsmRecordInfo();
        //info = bll.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
        string strID = HjNetHelper.GetQueryString("id");
        if (strID != "")
        {
            info = bll.GetModel(Convert.ToInt32(strID));
        }

        if (info == null)
        {
            AlertScript.RegisterStartupScript(this.Page, "", "<script>alert('您输入的接收邮件用户名未能查找到相关用户,因此邮件无法发送');</script>");
        }
        else
        {
            hidUserIdList.Value = info.UserIdList;

            DataTable dt = CBLL.GetEmailAddressList(info.UserIdList);

            foreach (DataRow dr in dt.Rows)
            {
                UserEmailList += dr["Email"].ToString();
                UserEmailList += ",";
            }

            if (UserEmailList.Length > 0)
            {
                UserEmailList = UserEmailList.Substring(0, UserEmailList.Length - 1);
            }

            tbEmailList.Text = UserEmailList;

            hidEmailList.Value = UserEmailList;
            //AlertScript.LoadRegisterStartupScript(this.Page,"PAGE", "window.location.href='GroupSentEmail.aspx';");
            //AlertScript.RegisterStartupScript(this.Page,"PAGE", "window.location.href='GroupSentEmail.aspx';");
        }
    }

    protected void btSent_Click(object sender, EventArgs e)
    {
        EmailGsmRecordInfo info = new EmailGsmRecordInfo();
        info = bll.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
        if (info.Status == 2)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "jtip('该此批量发送邮件已经进行，禁止重新发送，如需重新发送需要重新进行筛选用户并发送');");
            return;
        }

        if (FCKContent.Value.Length < 10)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "jtip('邮件内容太短');");
            return;
        }

        string[] list = tbEmailList.Text.Split(',');

        DataTable dt = CBLL.GetEmailAddressList(hidUserIdList.Value);

        if (dt.Rows.Count <= 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "jtip('您输入的接收邮件用户名未能查找到相关用户,因此邮件无法发送');");
            return;
        }

        AlertScript.LoadRegisterStartupScript(this.Page, "PAGE", "window.location.href='GroupSentEmail.aspx?id=" + HjNetHelper.GetQueryString("id") + "';");

        Thread[] lThreads = new Thread[dt.Rows.Count];
        int count = 0;
        int sum = 0;

        foreach (DataRow dr in dt.Rows)
        {
            EmailMultiThread emt = new EmailMultiThread(dr["Name"].ToString(), dr["EMail"].ToString(), tbTitle.Text.Trim(), FCKContent.Value);
            lThreads[count] = new Thread(new ThreadStart(emt.Send));
            lThreads[count].Start();

            if (count >= 500)
            {
                Thread.Sleep(5000);
                count = 0;
            }
            count++;
            sum++;
        }

        info.Status = 2;
        info.Sum = sum;
        info.Content = FCKContent.Value;
        info.DelMoney = (decimal)(0.05 * sum);

        bll.Update(info);

        AlertScript.RegScript(this.Page, UpdatePanel1, "jtip('邮件已经发送成功.');");
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("SelectUserEmail.aspx?id=" + HjNetHelper.GetQueryString("id") + "");
    }
}
