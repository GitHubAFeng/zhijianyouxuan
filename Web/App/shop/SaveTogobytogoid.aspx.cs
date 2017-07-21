using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class App_shop_SaveTogobytogoid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder json = new StringBuilder();
        Response.Clear();

        string jsonstring = Request["ordermodel"];

        string rs = "{\"state\":\"1\",\"msg\":\"更新成功\"}";

        Points togoBll = new Points();
        PointsInfo info = new PointsInfo();
        //商户版要加编辑资料的

        Hangjing.Common.HJlog.toLog("编辑资料数据：" + jsonstring);

        PointsInfo Togomodel = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PointsInfo>(jsonstring);

        info.Unid = Togomodel.Unid;
        info = togoBll.GetModel(info.Unid);
        info.LoginName = Togomodel.LoginName;
        string sqls = "LoginName='" + info.LoginName + "' and Unid <>" + info.Unid;
        if (togoBll.GetCount(sqls) > 0)
        {
            rs = "{\"state\":\"-1\",\"msg\":\"登陆名重复，请得新输入\"}";
            Response.Write(rs);
            Response.End();
            return;
        }
        info.Password = Togomodel.Password;
        info.Name = Togomodel.Name;
        info.Comm = Togomodel.Comm;
        info.CommPerson = Togomodel.CommPerson;
        info.Address = Togomodel.Address;
        info.senttime = Togomodel.senttime;
        info.special = Togomodel.special;

        //营业时间
        info.Opentimes1 = Togomodel.Opentimes1;
        info.Opentimes2 = Togomodel.Opentimes2;
        info.Closetimes1 = Togomodel.Closetimes1;
        info.Closetimes2 = Togomodel.Closetimes2;

        info.PTimes = Togomodel.PTimes;
        info.email = Togomodel.email;
        info.Status = Togomodel.Status;
        info.EBuilding = Togomodel.EBuilding;

        if (togoBll.Update(info) > 0)
        {
            rs = "{\"state\":\"1\",\"msg\":\"更新成功\"}";
        }
        else
        {
            rs = "{\"state\":\"-2\",\"msg\":\"服务器错误\"}";
        }

        Response.Write(rs);
        Response.End();


    }
}