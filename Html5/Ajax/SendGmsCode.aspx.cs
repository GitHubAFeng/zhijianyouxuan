using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Hangjing.Common;
using System.Text.RegularExpressions;
using Hangjing.SQLServerDAL;

public partial class Ajax_SendGmsCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //如果是直接访问这个页面，就出错。
        if (Request.UrlReferrer == null || Request.UrlReferrer.AbsolutePath == null)
        {
            Response.Write("error1111");
            Response.End();
            return;
        }
        else
        {

        }

        string phone = Request["phone"].ToString().Trim();
        Regex rx = new Regex(@"^[0-9]*$");
        if (!rx.IsMatch(phone) || phone.Length != 11)
        {
            Response.Write("0");
            Response.End();
            return;
        }


        string code = WebUtility.GetRandomOnlyNum(6);


        if (!checkphone(phone))
        {
            Response.Write("0");
            Response.End();
            return;
        }


        string fuc = Request["fuc"];
        switch (fuc)
        {
            case "auth"://手机注册，要判处有没有认证过
                {
                    if (checkphoneIsExist(phone))//如果存在
                    {
                        Response.Write(-3);
                    }
                    else
                    {
                        WebUtility.FixsetCookie("gsmcode", code, 1);
                        int rs = Hangjing.WebCommon.SendMsg.SendValidCode(phone, code);
                        Response.Write(rs);
                    }
                }
                break;

            case "checkphone"://验证手机，如找回密码
                {
                    int rs = 0;
                    string mobile = phone;//手机
                    string s = checkUserphone(mobile);
                    if (s == "1")
                    {
                        WebUtility.FixsetCookie("gsmcode", code, 1);
                        Hangjing.WebCommon.SendMsg.SendValidCode(mobile, code);
                        rs = 1;
                    }
                    else
                    {
                        rs = -3;
                    }
                    Response.Write(rs);
                    break;
                }



        }

        Response.End();
    }

    protected string checkUserphone(string phone)
    {
        string rs = "0";
        string sql = string.Empty;
        sql = " Tell = '" + phone + "'";
        int count = new ECustomer().GetCount(sql);
        if (count > 0)
        {
            rs = "1";//存在
        }
        return rs;
    }



    /// <summary>
    /// 判断当前手机号在前5分钟内有没有发到5条短信。有表示用户是恶意,不发短信。
    /// </summary>
    /// <param name="phone"></param>
    /// <returns>true表示可继续发短信</returns>
    private bool checkphone(string phone)
    {
        bool issend = false;
        DateTime now = DateTime.Now;
        string sql = " OrderId = '" + phone + "' and AddDate  between '" + now.AddDays(-1) + "' and '" + now + "'";
        int maxcont = 30;

        string ip = UserHelp.GetUserIP();
        string ipsql = " Inve3 = '" + ip + "' and AddDate  between '" + now.AddDays(-1) + "' and '" + now + "'";

        int ipcount = 30;
        if (new msgRecord().GetCount(sql) >= maxcont || new msgRecord().GetCount(ipsql) >= ipcount)
        {
            issend = false;
        }
        else
        {
            issend = true;

        }
        return issend;
    }

    /// <summary>
    /// 判断该手机号是否存在
    /// </summary>
    /// <param name="phone">手机号码</param>
    /// <returns></returns>
    protected bool checkphoneIsExist(string phone)
    {
        string sql = string.Empty;
        sql = " Tell = '" + phone + "'";
        int count = new ECustomer().GetCount(sql);
        return count > 0 ? true : false;
    }


    public static string GetRandomOnlyNum(int len)
    {
        char[] s = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        string str = String.Empty;
        Random random = new Random(GetRandomSeed());
        for (int i = 0; i < len; i++)
        {
            str += s[random.Next(0, s.Length)].ToString();
        }
        return str;
    }
    public static int GetRandomSeed()
    {
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return BitConverter.ToInt32(bytes, 0);
    }



}
