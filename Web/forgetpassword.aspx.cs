using Hangjing.Model;
using Hangjing.SQLServerDAL;
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

public partial class forgetpassword : System.Web.UI.Page
{
    ECustomer userBLL = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BYEmailLogin_Click(object sender, EventArgs e)
    {

        string cookie_gsmcode = WebUtility.FixgetCookie("gsmcode");
        if (cookie_gsmcode == null || cookie_gsmcode != tbphonevalid.Value.Trim())
        {
            AlertScript.RegScript(Page, "alert('手机验证码错误');");//bug:密码输入框内容会消失掉
            return;
        }

        string Tell = WebUtility.InputText(tbphone.Value);
        string sql = "Tell = '" + Tell + "'";
        int count = userBLL.GetCount(sql);
        if (count == 0)
        {
            AlertScript.RegScript(Page, "alert('手机号码不存在，请重新输入。');");
            return;
        }
        string url = "updatepwd.aspx?tell=" + Tell;
        WebUtility.FixsetCookie("phonevalid", "1", 1);

        Response.Redirect(url);



    }
}