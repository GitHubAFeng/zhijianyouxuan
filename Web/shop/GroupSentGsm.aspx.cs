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

public partial class shop_GroupSentGsm : System.Web.UI.Page
{
    ECustomer CBLL = new ECustomer();
    EmailGsmRecord bll = new EmailGsmRecord();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        //UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

        if (!Page.IsPostBack)
        {
            AlertScript.RegisterAdminPageClientScriptBlock(this.Page);
        }

        string UserPhonelList = "";

        EmailGsmRecordInfo info = new EmailGsmRecordInfo();

        info = bll.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));

        if (info == null)
        {
            AlertScript.RegisterStartupScript(this.Page, "", "<script>alert('您输入的接收用户名未能查找到相关用户,因此邮件无法发送');</script>");
        }
        else
        {
            hidUserIdList.Value = info.UserIdList;

            DataTable dt = CBLL.GetPhoneList(info.UserIdList);

            foreach (DataRow dr in dt.Rows)
            {
                UserPhonelList += dr["Tell"].ToString();
                UserPhonelList += ",";
            }

            if (UserPhonelList.Length > 0)
            {
                UserPhonelList = UserPhonelList.Substring(0, UserPhonelList.Length - 1);
            }

            tbPhoneList.Text = UserPhonelList;

            //AlertScript.LoadRegisterStartupScript(this.Page,"PAGE", "window.location.href='GroupSentGsm.aspx';");

            //AlertScript.RegisterStartupScript(this.Page,"PAGE", "window.location.href='GroupSentGsm.aspx';");
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        EmailGsmRecordInfo info = new EmailGsmRecordInfo();

        info = bll.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));

        if (info.Status == 2)
        {
            AlertScript.RegisterStartupScript(this.Page, "", "<script>alert('该此批量发送短信已经进行，禁止重新发送，如需重新发送需要重新进行筛选用户并发送');</script>");
            return;
        }

        if (tbContent.Text.Length < 2)
        {
            AlertScript.RegisterStartupScript(this.Page, "", "<script>alert('短信内容太短');</script>");
            return;
        }

        string UserPhonelList = "";
        DataTable dt = CBLL.GetPhoneList(info.UserIdList);

        foreach (DataRow dr in dt.Rows)
        {
            UserPhonelList += dr["tell"].ToString();
            UserPhonelList += ",";
        }

        if (UserPhonelList.Length > 0)
        {
            UserPhonelList = UserPhonelList.Substring(0, UserPhonelList.Length - 1);
        }

        tbPhoneList.Text = UserPhonelList;

        if (dt.Rows.Count <= 0)
        {
            AlertScript.RegisterStartupScript(this.Page, "", "<script>alert('您输入的接收用户名未能查找到相关用户,因此邮件无法发送');</script>");
            return;
        }

        AlertScript.LoadRegisterStartupScript(this.Page, "PAGE", "window.location.href='SelectGsmUser.aspx';");

        /*
        * 发送即时短信 SendSMS(这里是软件序列号, 手机号码,短信内容, 优先级)
        * 
        * 参数说明：
        * 软件序列号即注册序列号
        * 手机号码(最多一次发送200个手机号码,号码间用逗号分隔，逗号必须是半角状态的)
        * 短信内容(最多500个汉字或1000个纯英文，emay服务器程序能够自动分割；
        *  亿美有多个通道为客户提供服务，所以分割原则采用最短字数的通道为分割短信长度的规则，
        *  请客户应用程序不要自己分割短信以免造成混乱).亿美推荐短信长度70字以内 [扩展号]默认必须为空
        * 优先级代表优先级，范围1~5，数值越高优先级越高，当亿美通道的短信量特别大的时候，
        * 短信会在通道队列上排队，如果优先级越高，提交短信的速度会越快。
        */

        //即时发送                这里是软件序列号    手机号         短信内容         优先级
        int result = SentGsm.sendMsg(tbPhoneList.Text, tbContent.Text.Trim());
        string _Response = "";

        if (result == 1)
            _Response = "发送成功";
        else if (result == 101)
            _Response = "网络故障";
        else if (result == 102)
            _Response = "其它故障";
        else if (result == 0)
            _Response = "失败";
        else if (result == 100)
            _Response = "序列号码为空或无效";
        else if (result == 107)
            _Response = "手机号码为空或者超过1000个";
        else if (result == 108)
            _Response = "手机号码分割符号不正确";
        else if (result == 109)
            _Response = "部分手机号码不正确，已删除，其余手机号码被发送";
        else if (result == 110)
            _Response = "短信内容为空或超长（70个汉字）";
        else if (result == 201)
            _Response = "计费失败，请充值";
        else
            _Response = "其他故障值：" + result.ToString();

        if (_Response == "发送成功")
        {
            info.Status = 2;
            info.Sum = dt.Rows.Count;
            info.Content = tbContent.Text.Trim();
            info.DelMoney = (decimal)(0.05 * info.Sum);

            bll.Update(info);

            //AlertScript.RegScript(this.Page,"tipsWindown('提示信息','text:发送成功!','250','150','true','1000','true','text');");

            AlertScript.RegisterStartupScript(this.Page, "PAGE", "window.location.href='SelectGsmUser.aspx';");
        }
        else
        {
            AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:发送失败，请联系技术支持!','250','150','true','1000','true','text');");
        }
    }

    protected void btSent_Click(object sender, EventArgs e)
    {
        Response.Redirect("SelectUserGsm.aspx?id=" + HjNetHelper.GetQueryString("id") + "");
    }
}
