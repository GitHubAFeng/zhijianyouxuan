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

public partial class Themes_Default_Ajax_shopAjaxCheck : System.Web.UI.Page
{
    Points dal = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string User_Emial = WebUtility.InputText(Request["email"]);
        string name = Server.UrlDecode(WebUtility.InputText(Request["name"]));
        string NewPassword = WebUtility.RandStr(6);         //随机产生6位字符，作为新密码发送至用户注册的邮箱。
        string Password = WebUtility.GetMd5(NewPassword);

        string sql = "email = '" + User_Emial + "' and LoginName = '" + name + "'";
        IList<PointsInfo> list = dal.GetList(1, 1, sql, "unid", 1);
        if (list.Count == 1)
        {
            int id = dal.ResetPassword(list[0].Unid, Password);
            if (id == 1)
            {
                string Title = "尊敬的用户：您好";
                string Body = SectionProxyData.GetSetValue(10);
                string tempbody = Body.Replace("{url}", NewPassword);

                SysMailMessage mail = new SysMailMessage(User_Emial, Title, tempbody);
                bool res = mail.Send();
                if (res == true)
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
                Response.Write("0");
            }
        }
        else
        {
            Response.Write("-1");//用户名不存在.
        }

        Response.End();
    }
}
