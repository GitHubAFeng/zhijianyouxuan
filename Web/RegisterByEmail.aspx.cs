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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

using DS.Web.UCenter.Client;
using DS.Web.UCenter;

/// <summary>
/// 根据手机号 和 验证码注册 
/// </summary>
public partial class RegisterByEmail : System.Web.UI.Page
{
    ECustomer userBLL = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 好友推荐
            if (Request["username"] != null)
            {
                lbusername.InnerText = Server.UrlDecode(Request["username"]);
            }
            else
            {
                div_appfriend.Visible = false;
            }
            #endregion

            this.tbphone.Value = Request["phone"];

            string state = SectionProxyData.GetSetValue(39);
            if (state == "0")
            {
                p_gsmvalied.Style["display"] = "none";
            }

        }
    }

    protected void BYEmailLogin_Click(object sender, EventArgs e)
    {
        //验证相关
        string state = SectionProxyData.GetSetValue(39);
        if (state == "1")//表示要验证    
        {

            string cookie_gsmcode = WebUtility.FixgetCookie("gsmcode");
            if (cookie_gsmcode == null || cookie_gsmcode != tbphonevalid.Value.Trim())
            {
                AlertScript.RegScript(Page, "alert('短信验证码错误');");//bug:密码输入框内容会消失掉
                return;
            }
        }

        string _rid = WebUtility.InputText(Request["userid"]);
        string tell = WebUtility.InputText(tbphone.Value);
        string password = WebUtility.GetMd5(TBpassword.Value);
        int point = Convert.ToInt32(SectionProxyData.GetSetValue(19));


        string sql = " [Name] = '" + tell + "' ";
        int count = userBLL.GetCount(sql);
        if (count > 0)
        {
            string url = "RegisterByEmail.aspx?phone=" + tell;
            AlertScript.RegScript(Page, "alert('该手机号已注册，请重新输入！');gourl('" + url + "');");
            return;
        }

        sql = " Tell = '" + tell + "' ";
        count = userBLL.GetCount(sql);
        if (count > 0)
        {
            string url = "RegisterByEmail.aspx?phone=" + tell;
            AlertScript.RegScript(Page, "alert('手机号码重复了，请重新输入！');gourl('" + url + "');");
            return;
        }

        ECustomerInfo model = new ECustomerInfo();
        model.EMAIL = "";
        model.Name = tell;
        model.TrueName = "";
        model.Tell = tell;
        model.Password = password;
        model.RegTime = DateTime.Now;
        model.Point = point;
        model.IsActivate = -1;
        model.ActivateCode = WebUtility.RandStr(200);
        model.GroupID = 0;
        model.WebSite = ((int)OrderSource.web).ToString();
        model.RID = _rid;
        model.Usermoney = 0;
        model.PhoneActivate = 0;
        model.Sex = "";
        model.MSN = "";

        if (userBLL.Add(model) > 0)
        {
            ECustomerInfo customer = userBLL.GetModelByNameAPassword(tell, model.Password);
            UserHelp.SetLogin(customer);
            EPointRecordInfo info = new EPointRecordInfo();
            info.UserID = customer.DataID;
            info.Point = point;
            info.Event = "新注册用户,获得积分" + point + "个";
            info.Time = DateTime.Now;
            EPointRecord bll = new EPointRecord();
            bll.Add(info);

            string url = "RegisterSuccess.aspx?Dataid=" + customer.DataID.ToString();
            if (Request["ReturnUrl"] != null)
            {
                url = Request["ReturnUrl"];
            }

            //同步注册
            if (WebUtility.isSyn())
            {
                //api/passports/ucenter/config.inc.php 
                //这个地方会有注册的用户是没有激活的。
                //define('DEFAULT_GROUP_ID', '10');
                //define('DEFAULT_MEMBERTYPE_ID', '2');
                var uc = new UcClient();
                //验证用户是否存在 
                var user = uc.UserInfo(tell);
                if (user.Uid == 0)
                {
                    var userret = uc.UserRegister(tell, TBpassword.Value.Trim(), "", 0, "");
                    Response.Write(uc.UserSynlogin(userret.Uid));
                }
                else//有用户，同步登录
                {
                    Response.Write(uc.UserSynlogin(user.Uid));
                }
                AlertScript.RegScript(Page, "timeoutlogin('" + url + "');");
            }
            else
            {
                Response.Redirect(url);
            }
        }


    }
}
