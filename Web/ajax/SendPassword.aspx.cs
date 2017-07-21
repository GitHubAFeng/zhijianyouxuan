/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 异步发送邮件
 * Created by jijunjian at 2010-8-11 21:39:33.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
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

public partial class Themes_Default_Ajax_SendPassword : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string User_Emial = WebUtility.InputText(Request["email"]);
        string name = Server.UrlDecode( WebUtility.InputText(Request["name"]));

        string sql = "email = '"+User_Emial+"' and name = '"+name+"'";
        IList<ECustomerInfo> list = dal.GetList(1 , 1 , sql , "dataid" , 1);
        if (list.Count == 1)
        {
            string codebuildtime = DateTime.Now.AddDays(2).ToString();
            string ActivateCode = WebUtility.RandStr(25);
            sql = " update ECustomer set ActivateCode = '" + ActivateCode + "',buildtime = '" + codebuildtime + "' where dataid =" + list[0].DataID;
            WebUtility.excutesql(sql);

            string Title = "找回您的帐户密码";
            string Body = SectionProxyData.GetSetValue(35);
            Body = Body.Replace("{username}", list[0].Name);
            Body = Body.Replace("{activetime}", codebuildtime);

            string url = WebUtility.GetConfigsite() + "/updatepwd.aspx?mycode=" + ActivateCode + "&email=" + list[0].EMAIL;
            string urlhtml = "<a href='" + url + "'>" + url + "</a>";
            Body = Body.Replace("{url}", urlhtml);

            // Hangjing.AppLog.AppLog.Info("找回密码url=:" + url + "发送内容：" + Body);

            SysMailMessage mail = new SysMailMessage(User_Emial, Title, Body);
            bool res = mail.Send();
            if (res)
            {
                Response.Write("1");
            }
            else
            {
                Response.Write("-2");
            }
        }
        else
        {
            Response.Write("-1");//用户名不存在.
        }

        Response.End();
    }
}
