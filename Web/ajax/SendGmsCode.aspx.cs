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
using Hangjing.Model;

public partial class Ajax_SendGmsCode : System.Web.UI.Page
{

    ECustomer userBLL = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        string phone = Request["phone"].ToString().Trim();

        Regex rx = new Regex(@"^[0-9]*$");
        if (!rx.IsMatch(phone) || phone.Length != 11)
        {
            Response.Write("0");
            Response.End();
            return;
        }

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

        string code = WebUtility.GetRandomOnlyNum(6);

        try
        {
            string fuc = Request["fuc"];
            switch (fuc)
            {
                case "auth"://注册
                    {
                        int rs = 0;
                        bool b = checkphone(phone);
                        if (b)//已注册
                        {
                            rs = -3;
                        }
                        else
                        {

                            WebUtility.FixsetCookie("gsmcode", code, 1);
                            rs = Hangjing.WebCommon.SendMsg.SendValidCode(phone, code);
                        }
                        Response.Write(rs);
                    }
                    break;

                case "authcode"://手机提交订单验证
                    {
                        int rs = 0;
                        bool b= checkphone(phone);
                        if (!b)//没有注册
                        {
                            Response.Write(-3);
                        }
                        else
                        {
                            WebUtility.FixsetCookie("gsmcode", code, 1);
                            rs = Hangjing.WebCommon.SendMsg.SendValidCode(phone, code);
                        }
                        Response.Write(rs);
                    }
                    break;
            }

        }
        catch
        {
            Response.Write("0");
        }
        Response.End();
    }


    /// <summary>
    /// 判断该手机号是否存在
    /// </summary>
    /// <param name="phone">手机号码</param>
    /// <returns></returns>
    protected bool checkphone(string phone)
    {
        string sql = string.Empty;
        sql = " Tell = '" + phone + "'";
        int count = new ECustomer().GetCount(sql);
        return count > 0 ? true : false;
    }


}
