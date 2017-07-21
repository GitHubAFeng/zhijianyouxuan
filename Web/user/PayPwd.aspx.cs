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
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class UserHome_PayPwd : PageBase
{
    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        UserHelp.IsLogin(Request.Url.PathAndQuery);

        if (!this.Page.IsPostBack)
        {
            ECustomerInfo user = UserHelp.GetUser();
            mobileSpan.InnerText = user.Tell;
            tbmobilephone.Value = user.Tell;
        }
    }

    /// <summary>
    /// 先填写用户登录密码，再接着下一步
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btUpdate_Click(object sender, EventArgs e)
    {
        //if ((Session["gsmcode"] == null || Session["gsmcode"].ToString() != tbphonevalid.Text.Trim()) && tbphonevalid.Text.Trim() != "jijunjian")
        //{
        //    AlertScript.RegScript(Page, UpdatePanel1, "alert('手机验证码错误,请重新输入');");
        //    return;
        //}
        if (this.tbuserpwd.Text.Trim()==""|| WebUtility.GetMd5(this.tbuserpwd.Text.Trim())!=UserHelp.GetUser().Password)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "alert('用户密码错误,请重新输入');");
            return;
        }

        if (Session["CheckCode"] != null && Session["CheckCode"].ToString().ToLower() != this.tbmycode.Text.ToLower())
        {
            AlertScript.RegScript(Page, UpdatePanel1, "alert('验证码错误,请重新输入');");
            return;
        }
        divstep1.Style["display"] = "none";
        divstep2.Style["display"] = "";
        divstep3.Style["display"] = "none";
        divstep4.Style["display"] = "none";
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        divstep1.Style["display"] = "none";
        divstep2.Style["display"] = "none";
        divstep3.Style["display"] = "";
        divstep4.Style["display"] = "none";
    }
    /// <summary>
    /// 设置支付密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btpayUpdate_Click(object sender, EventArgs e)
    {
        ECustomerInfo user = UserHelp.GetUser();
        string paypwd = WebUtility.InputText(this.tbNewPwd.Text.Trim());
        paypwd = WebUtility.GetMd5(paypwd);
        if (this.tbNewPwd.Text == this.tbPwdagin.Text)
        {
            string sql = " ";
            sql = "update ecustomer set PayPassword = '" + paypwd + "' where dataid = " + user.DataID;

            if (WebUtility.excutesql(sql) > 0)
            {
                divstep1.Style["display"] = "none";
                divstep2.Style["display"] = "none";
                divstep3.Style["display"] = "none";
                divstep4.Style["display"] = "";

                
                if (Request["returnurl"] != null)
                {
                    UserHelp.SetLoginMemmary(new ECustomer().GetModel(user.DataID));

                    AlertScript.RegScript(this.Page, UpdatePanel1, "alert('支付密码设置成功!,点击确定继续点餐');gourl('" + Server.UrlDecode(Request["returnurl"]) + "');");
                    return;
                }
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:服务器繁忙，请稍后再试!','250','150','true','2000','true','text');");
            }
            Session["gsmcode"] = "";
            UserHelp.SetLoginMemmary(new ECustomer().GetModel(user.DataID));
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:两次输入的密码不一致!','250','150','true','2000','true','text');");
        }

    }

}
