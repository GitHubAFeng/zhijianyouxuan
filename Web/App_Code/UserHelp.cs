using System;
using System.Data;
using System.Configuration;
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
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Hangjing.Common;

/// <summary>
///UserHelp 的摘要说明
/// </summary>
public static class UserHelp
{
    private const string LONGIN_URL = "~/Login.aspx?returnurl={0}";
    private const string LONGIN_URL_TOGO = "~/tLogin.aspx?returnurl={0}";
    private const string LONGIN_DEFAULT = "~/Index.aspx";
    private const string SESSION_USER = "Hangjing_xcustomer";
    private const string SESSION_TOGO = "Hangjing_togo";
    private const string COOKIE_NAME = "Hangjing_xuser";
    private const string COOKIE_TOGO = "Hangjing_togo";
    private const string SECRET_KEY = "qwertvbnm";
    private const string COOKIE_ADMIN = "Hangjing_cadmin";

    private const string SESSION_ADMIN = "Hangjing_xadmin";

    private const string COOKIE_DELIVERCOMPANY_ADMIN = "COOKIE_DELIVERCOMPANY_ADMIN";

    #region 会员中心代码


    /// <summary>
    /// 登录后的设置
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    /// <param name="url">登录后跳转的URL</param>
    public static void SetLogin(Hangjing.Model.ECustomerInfo customer, string url)
    {
        SetSession(customer);
        SetCookieMemary(customer);
        if (string.IsNullOrEmpty(url))
        {
            HttpContext.Current.Response.Redirect(LONGIN_DEFAULT);
        }
        HttpContext.Current.Response.Redirect(url);
    }

    /// <summary>
    /// 登录后的设置
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    public static void SetLogin(Hangjing.Model.ECustomerInfo customer)
    {
        try
        {
            if (customer != null)
            {
                SetSession(customer);
                SetCookie(customer);
            }
        }
        catch (Exception ex)
        {
            //Hangjing.SQLServerDAL.GlobalManage.AddSystemLog(ex.Message, LogType.Error, "system");
        }
    }

    /// <summary>
    /// 不记住登录的登录设置
    /// </summary>
    /// <param name="customer"></param>
    public static void SetLoginMemmary(Hangjing.Model.ECustomerInfo customer)
    {
        try
        {
            if (customer != null)
            {
                SetSession(customer);
                SetCookie(customer);
            }
        }
        catch (Exception ex)
        {
            //Hangjing.SQLServerDAL.GlobalManage.AddSystemLog("SetLoginMemmary(Hangjing.Model.ECustomerInfo customer) catch"+ex.Message, LogType.Error, "system");

        }
    }

    /// <summary>
    /// 注销登陆
    /// </summary>
    public static void Logout()
    {
        HttpContext.Current.Session[SESSION_USER] = null;
        HttpCookie authCookie = new HttpCookie(COOKIE_NAME, "");
        authCookie.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(authCookie);

        HttpCookie cookie = new HttpCookie("Hangjing");
        cookie.Values.Clear();
        cookie.Expires = DateTime.Now.AddYears(-1);
        //  cookie.Domain = ConfigurationManager.AppSettings["domain"].ToString();
        cookie.Path = "/";
        cookie.Values["userid"] = "-1";
        cookie.Values["expires"] = "-1";//保存cookie 1 天
        HttpContext.Current.Response.AppendCookie(cookie);

    }

    /// <summary>
    /// 设置SESSION
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    public static void SetSession(Hangjing.Model.ECustomerInfo customer)
    {
        HttpContext.Current.Session[SESSION_USER] = customer;
    }

    /// <summary>
    /// 设置用户登录cookie
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    public static void SetCookie(Hangjing.Model.ECustomerInfo customer)
    {
        string useremail = WebUtility.Encrypt(customer.EMAIL);
        string password = WebUtility.Encrypt(customer.Password);
        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, useremail, DateTime.Now, DateTime.Now.AddDays(7), true, password);
        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        HttpCookie authCookie = new HttpCookie(COOKIE_NAME, encryptedTicket);
        authCookie.Expires = authTicket.Expiration;
        authCookie.Path = "/";
        //  authCookie.Domain = ConfigurationManager.AppSettings["domain"].ToString();
        HttpContext.Current.Response.Cookies.Add(authCookie);

        //记录用户ID
        HttpCookie cookie = new HttpCookie("Hangjing");
        cookie.Values["userid"] = customer.DataID.ToString();
        cookie.Values["expires"] = "43200";//保存cookie一个月
        //   cookie.Domain = ConfigurationManager.AppSettings["domain"].ToString();
        cookie.Path = "/";
        cookie.Expires = DateTime.Now.AddMinutes(43200);

        HttpContext.Current.Response.AppendCookie(cookie);
    }

    /// <summary>
    /// 当用户不选择记住登录时cookie只保存到关闭浏览器前
    /// </summary>
    /// <param name="customer"></param>
    public static void SetCookieMemary(Hangjing.Model.ECustomerInfo customer)
    {
        try
        {
            string useremail = WebUtility.Encrypt(customer.EMAIL);
            string password = WebUtility.Encrypt(customer.Password);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, useremail, DateTime.Now, DateTime.Now.AddDays(7), true, password);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(COOKIE_NAME, encryptedTicket);
            //       authCookie.Domain = ConfigurationManager.AppSettings["domain"].ToString();
            authCookie.Path = "/";

            HttpContext.Current.Response.Cookies.Add(authCookie);

            ////记录用户ID
            HttpCookie cookie = new HttpCookie("Hangjing");
            cookie.Values["userid"] = customer.DataID.ToString();
            cookie.Values["expires"] = "60";//保存cookie 1 天
            //    cookie.Domain = ConfigurationManager.AppSettings["domain"].ToString();
            cookie.Path = "/";
            cookie.Expires = DateTime.Now.AddMinutes(60);

            HttpContext.Current.Response.AppendCookie(cookie);
        }
        catch
        {
            //Hangjing.SQLServerDAL.GlobalManage.AddSystemLog("SetCookieMemary Catch", LogType.Error, "System");
        }
    }
    /// <summary>
    /// 读取用户cookie
    /// </summary>
    /// <returns>UserInfo</returns>
    public static Hangjing.Model.ECustomerInfo GetCookie()
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[COOKIE_NAME];
        if (authCookie == null)
            return null;
        try
        {
            FormsAuthenticationTicket decryptedTicket = FormsAuthentication.Decrypt(authCookie.Value);
            Hangjing.Model.ECustomerInfo customer = new Hangjing.Model.ECustomerInfo();
            customer.EMAIL = WebUtility.Decrypt(decryptedTicket.Name);
            customer.Password = WebUtility.Decrypt(decryptedTicket.UserData);
            return customer;
        }
        catch (Exception ex)
        {
            //Hangjing.SQLServerDAL.GlobalManage.AddSystemLog("SetCookieMemary Catch"+ex.Message, LogType.Error, "System");
            return null;
        }
    }

    /// <summary>
    /// 获取已登录用户信息
    /// </summary>
    /// <returns>UserInfo</returns>
    public static Hangjing.Model.ECustomerInfo GetUser()
    {
        try
        {
            if (HttpContext.Current.Session[SESSION_USER] == null)
            {
                Hangjing.SQLServerDAL.ECustomer customers = new Hangjing.SQLServerDAL.ECustomer();
                return new Hangjing.SQLServerDAL.ECustomer().GetModel(Convert.ToInt32(GetUserCookie("userid")));
            }
            else
            {
                return (Hangjing.Model.ECustomerInfo)HttpContext.Current.Session[SESSION_USER];
            }
        }
        catch (Exception ex)
        {
            //Hangjing.SQLServerDAL.GlobalManage.AddSystemLog("GetUser "+ex.Message+"", LogType.Error, "system");
            return null;
        }
    }

    ///zjf@ihangjing.com 2010-03-20 使用cookies判断用户是否登入,原使用session中保存的用户数据，会导致不同的IE实例需要重新登入的问题
    /// <summary>
    ///  用户是否登录
    /// </summary>
    /// <param name="url">重新登陆后跳转的页面</param>
    public static void IsLogin(string url)
    {
        if (GetUser() == null)
        {
            HttpContext.Current.Response.Redirect(GetLoginUrl(url));
        }

        if (GetUserCookie("userid") == "" || GetUserCookie("userid") == null)
        {
            HttpContext.Current.Response.Redirect(GetLoginUrl(url));
        }
        else
        {
            //如果cookies存在但是session中的model不存在则重新进行登入
            if (GetUser() == null)
            {
                SetLogin(new Hangjing.SQLServerDAL.ECustomer().GetModel(Convert.ToInt32(GetUserCookie("userid"))));
            }
        }

    }

    /// <summary>
    ///  用户是否登录,ECustomer
    /// </summary>
    public static bool IsLogin()
    {
        try
        {
            if (GetUserCookie("userid") == "" || GetUserCookie("userid") == null)
            {
                return false;
            }
            else
            {
                //如果cookies存在但是session中的model不存在则重新进行登入
                if (GetUser() == null)
                {
                    SetLogin(new Hangjing.SQLServerDAL.ECustomer().GetModel(Convert.ToInt32(GetUserCookie("userid"))));
                    if (GetUser() == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            //Hangjing.SQLServerDAL.GlobalManage.AddSystemLog("public static bool IsLogin()" + ex.Message, LogType.Error, "System");
            return false;
        }
    }

    /// <summary>
    /// 获取用户Cookies
    /// </summary>
    /// <param name="strName"></param>
    /// <returns></returns>
    public static string GetUserCookie(string strName)
    {
        if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["Hangjing"] != null && HttpContext.Current.Request.Cookies["Hangjing"][strName] != null)
        {
            return Utils.UrlDecode(HttpContext.Current.Request.Cookies["Hangjing"][strName].ToString());
        }

        return "";
    }

    /// <summary>
    /// 获取登录路径
    /// </summary>
    public static string GetLoginUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return LONGIN_URL;
        }
        return string.Format(LONGIN_URL, url);
    }


    /// <summary>
    /// 设置管理员登录信息
    /// </summary>
    /// <param name="users">UsersInfo</param>
    public static void AdminLogin(Hangjing.Model.EAdminInfo Eadmin)
    {
        //记录用户ID
        HttpCookie cookie = new HttpCookie(COOKIE_ADMIN);
        cookie.Values["userid"] = Eadmin.ID.ToString();
        cookie.Values["expires"] = "270";//保存cookie一个天
        //   cookie.Domain = ConfigurationManager.AppSettings["domain"].ToString();
        cookie.Expires = DateTime.Now.AddMinutes(60 * 6);
        HttpContext.Current.Response.AppendCookie(cookie);
    }

    /// <summary>
    /// 获取管理员登录信息
    /// </summary>
    /// <returns>UsersInfo</returns>
    public static Hangjing.Model.EAdminInfo GetAdmin()
    {
        //记录用户ID HttpContext.Current.Request.Cookies != null &&
        EAdmin dal = new EAdmin();
        EAdminInfo model = null;

        if ( HttpContext.Current.Request.Cookies[COOKIE_ADMIN] != null && HttpContext.Current.Request.Cookies[COOKIE_ADMIN]["userid"] != null)
        {
            int id = Convert.ToInt32(Utils.UrlDecode(HttpContext.Current.Request.Cookies[COOKIE_ADMIN]["userid"].ToString()));
            model = dal.GetModel(id);
        }
        return model;
    }



    /// <summary>
    /// 管理员退出
    /// </summary>
    public static void AdminLogout()
    {
        HttpCookie cookie = new HttpCookie(COOKIE_ADMIN);
        cookie.Values["userid"] = 0 + "";
        cookie.Values["expires"] = "-1";//保存cookie一个天
        //    cookie.Domain = ConfigurationManager.AppSettings["domain"].ToString();
        cookie.Path = "/";
        cookie.Expires = DateTime.Now.AddMinutes(-1);
        HttpContext.Current.Response.AppendCookie(cookie);
    }

    private static object SynObject = new object();

    /// <summary>
    /// 获取客户IP
    /// </summary>
    /// <returns></returns>
    public static string GetUserIP()
    {
        string userIP;
        if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null)
        {
            userIP = HttpContext.Current.Request.UserHostAddress;
        }
        else
        {
            userIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        return userIP;
    }

    #endregion  原始会员中心代码

    #region  商家

    /// <summary>
    /// 商家登录后的设置
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    /// <param name="url">登录后跳转的URL</param>
    public static void SetLogin_Togo(Hangjing.Model.PointsInfo loginInfo, string url)
    {
        SetSession_Togo(loginInfo);
        SetCookie_Togo(loginInfo);
        if (string.IsNullOrEmpty(url))
        {
            HttpContext.Current.Response.Redirect(LONGIN_URL_TOGO);
        }
        HttpContext.Current.Response.Redirect(url);
    }

    /// <summary>
    /// 商家登录后的设置
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    public static void SetLogin_Togo(Hangjing.Model.PointsInfo loginInfo)
    {
        SetSession_Togo(loginInfo);
        SetCookie_Togo(loginInfo);
    }

    /// <summary>
    /// 注销登陆的商家
    /// </summary>
    public static void Logout_Togo()
    {
        HttpContext.Current.Session[SESSION_TOGO] = null;
        HttpCookie authCookie = new HttpCookie(COOKIE_TOGO, "");
        authCookie.Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Response.Cookies.Add(authCookie);

        //保存cookies
        HttpCookie cookie = new HttpCookie("Hangjing_togo");
        cookie.Values.Clear();
        cookie.Expires = DateTime.Now.AddYears(-1);
        HttpContext.Current.Response.AppendCookie(cookie);
    }

    /// <summary>
    /// 设置商家SESSION
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    public static void SetSession_Togo(Hangjing.Model.PointsInfo loginInfo)
    {
        HttpContext.Current.Session[SESSION_TOGO] = loginInfo;
    }
    /// <summary>
    /// 设置用户登录商家cookie
    /// </summary>
    /// <param name="userinfo">UserInfo</param>
    public static void SetCookie_Togo(Hangjing.Model.PointsInfo loginInfo)
    {
        string username = WebUtility.Encrypt(loginInfo.LoginName);
        string password = WebUtility.Encrypt(loginInfo.Password);
        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddDays(7), true, password);
        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        HttpCookie authCookie = new HttpCookie(COOKIE_TOGO, encryptedTicket);
        authCookie.Expires = authTicket.Expiration;
        HttpContext.Current.Response.Cookies.Add(authCookie);

        //记录用户ID
        HttpCookie cookie = new HttpCookie("Hangjing_togo");
        cookie.Values["userid"] = loginInfo.Unid.ToString();
        cookie.Values["expires"] = "43200";//保存cookie一个月

        cookie.Expires = DateTime.Now.AddMinutes(43200);

        HttpContext.Current.Response.AppendCookie(cookie);
    }


    /// <summary>
    /// 读取用户cookie
    /// </summary>
    /// <returns>UserInfo</returns>
    public static Hangjing.Model.PointsInfo GetCookie_Togo()
    {
        HttpCookie authCookie = HttpContext.Current.Request.Cookies[COOKIE_TOGO];
        if (authCookie == null)
        {
            return null;
        }

        try
        {
            FormsAuthenticationTicket decryptedTicket = FormsAuthentication.Decrypt(authCookie.Value);

            Hangjing.Model.PointsInfo loginInfo = new PointsInfo();
            int dataid = Convert.ToInt32(GetTogoCookie("userid"));
            loginInfo.Unid = dataid;
            return loginInfo;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    /// <summary>
    /// 获取已登录用户信息,返回PointsInfo
    /// </summary>
    /// <returns>UserInfo</returns>
    public static Hangjing.Model.PointsInfo GetUser_Togo()
    {
        if (HttpContext.Current.Session[SESSION_TOGO] == null)
        {
            string shopid = GetTogoCookie("userid");
            if (shopid == null || shopid == "")
            {
                return null;
            }
            return new Points().GetModel(Convert.ToInt32(shopid));
        }
        else
        {
            return (Hangjing.Model.PointsInfo)HttpContext.Current.Session[SESSION_TOGO];
        }
    }

    /// <summary>
    ///  用户是否登录
    /// </summary>
    /// <param name="url">重新登陆后跳转的页面</param>
    public static void IsLogin_Togo(string url)
    {
        if (GetUser_Togo() == null)
        {
            HttpContext.Current.Response.Redirect(GetLoginUrl_Togo(url));
        }

    }

    /// <summary>
    /// 获取用户Cookies
    /// </summary>
    /// <param name="strName"></param>
    /// <returns></returns>
    public static string GetTogoCookie(string strName)
    {
        try
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["Hangjing_togo"] != null && HttpContext.Current.Request.Cookies["Hangjing_togo"][strName] != null)
            {
                return Utils.UrlDecode(HttpContext.Current.Request.Cookies["Hangjing_togo"][strName].ToString());
            }

            return "";
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    /// <summary>
    ///  用户是否登录，商家points
    /// </summary>
    public static bool islogin_togo()
    {
        try
        {
            if (GetTogoCookie("userid") == "")
            {
                return false;
            }
            else
            {
                //如果cookies存在但是session中的model不存在则重新进行登入
                if (GetUser_Togo() == null)
                {
                    // SetLogin_Togo(new TogoAd().GetTogoLoginModel(Convert.ToInt32(GetTogoCookie("userid"))));
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// 获取登录路径
    /// </summary>
    public static string GetLoginUrl_Togo(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return LONGIN_URL_TOGO;
        }
        return string.Format(LONGIN_URL_TOGO, url);
    }

    #endregion 商家

}
