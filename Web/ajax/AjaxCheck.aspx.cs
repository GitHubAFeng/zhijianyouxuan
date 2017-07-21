using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Data;

using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class Themes_Default_Ajax_AjaxCheck : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();
    protected string return_content = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string type = Request["type"];
        string value = Request["value"];

        switch (type)
        {
            case "nike":              //检测会员名是否已注册
                registerConrim("nike", value);
                break;
            case "email":                    //检测邮箱是否已被存在
                registerConrim("email", value);
                break;
            case "code":
                if (Session["CheckCode"] == null)
                {
                    Response.Write("0");        //验证码已过期
                }
                else if (Session["CheckCode"].ToString().ToLower() == value.ToLower())
                {
                    Response.Write("1");
                }
                else
                {
                    Response.Write("2");        //验证码错误
                }
                break;
            case "phone":                    //检测邮箱是否已被存在
                registerConrim("phone", value);
                break;
        }
      
        Response.End();
    }

    /// <summary>
    /// 远程注册验证
    /// </summary>
    /// <returns></returns>
    public void registerConrim(string type, string value)
    {
       
        switch (type)
        {
            case "nike":              //检测会员名是否已注册
                if (dal.IsExistNike(Server.UrlDecode( value)))
                {
                    Response.Write("0");//已经注册
                }
                else
                {
                    Response.Write("1");
                }
                break;
            case "email":                    //检测邮箱是否已被存在
                if (dal.exists(value))
                {
                    Response.Write("0");//已经注册
                }
                else
                {
                    Response.Write("1");
                }
                break;
            case "phone":
                {//检测邮箱是否已被存在
                    string sql = "Tell = '"+value+"'";
                    int count = dal.GetCount(sql);
                    if (count > 0)
                    {
                        Response.Write("0");//已经注册
                    }
                    else
                    {
                        Response.Write("1");
                    }
                    break;
                }
        }
    }
}
