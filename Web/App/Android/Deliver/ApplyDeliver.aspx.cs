using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 骑士用户申请（注册）
/// </summary>
public partial class APP_Android_deliver_ApplyDeliver : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string state = "1";
        string msg = "";


        //输入参数：姓名、电话 、用户名、密码
        string name = WebUtility.InputText(Request["name"]);  //姓名
        string tel = WebUtility.InputText(Request["tel"]);
        string username = WebUtility.InputText(Request["username"]);  //会员名
        string password = WebUtility.InputText(Request["password"]);
        int cid = HjNetHelper.GetPostParam("cid", 0);

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || cid == 0)
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-3\",\"msg\":\"姓名、电话、会员名、密码和城市ID为必填项\"}");
            Response.End();
        }

        Deliver userBLL = new Deliver();
        DeliverInfo info = new DeliverInfo();

        string sql = "UserName = '" + username + "'";
        int count = userBLL.GetCount(sql);
        if (count > 0)
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-2\",\"msg\":\"该用户名已存在\"}");
            Response.End();
        }

        info.CodeId = "0";
        info.Name = name;
        info.UserName = username;
        info.Password = WebUtility.GetMd5(password);
        info.Phone = tel;
        info.Inve1 = cid; //城市编号
        info.Section = "0";
        info.Status = 0;
        info.GpsIMEI = "0";
        info.Inve2 = "";
        info.AddDate = DateTime.Now;
        info.OrderNum = 0;
        info.IsApproved = 1;
        info.pic1 = "";

        info.DataId = userBLL.Add(info);

        if (info.DataId > 0)
        {
            msg = "{\"userid\":\"" + info.DataId + "\",\"state\":\"" + state + "\",\"msg\":\"注册成功\"}";
        }
        else
        {
            msg = "{\"userid\":\"0\",\"state\":\"-1\",\"msg\":\"注册失败\"}";
        }

        Response.Write(msg);
        Response.End();
    }

}
