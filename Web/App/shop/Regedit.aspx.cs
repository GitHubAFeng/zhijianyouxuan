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

public partial class App_shop_Regedit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string state = "1";
        Points dal = new Points();
        PointsInfo info = new PointsInfo();
        //输入参数：用户名、密码
        string Name = WebUtility.InputText(Request["name"]);
        string Comm = WebUtility.InputText(Request["comm"]);
        string CommPerson = WebUtility.InputText(Request["commperson"]);
        string Address = WebUtility.InputText(Request["address"]);
        string Introduce = WebUtility.InputText(Request["introduce"]);

        string loginname = WebUtility.InputText(Request["loginname"]);
        string password = WebUtility.GetMd5(WebUtility.InputText(Request["password"]));
        if (Name == "")
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-1\",\"msg\":\"商家名称不能为空\"}");
            Response.End();
            return;
        }
        if (Comm == "")
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-2\",\"msg\":\"联系电话不能为空\"}");
            Response.End();
            return;
        }
        if (CommPerson == "")
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-3\",\"msg\":\"联系人不能为空\"}");
            Response.End();
            return;
        }
        if (Address == "")
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-4\",\"msg\":\"商家地址不能为空\"}");
            Response.End();
            return;
        }
        if (Request["password"] == "" || Request["password"]==null)
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-6\",\"msg\":\"密码不能为空\"}");
            Response.End();
            return;
        }

        string sql = "loginname = '" + loginname + "'";
        int count = dal.GetCount(sql);
        if (count > 0)
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-7\",\"msg\":\"用户名重复\"}");
            Response.End();
            return;
        }

        info.Unid = 0;
        info.ID = "0";
        info.Name = Name;
        info.Comm = Comm;
        info.PType = 0;
        info.RcvType = 0;
        info.NamePy = "";
        info.CommPerson = CommPerson;
        info.SendLimit = 0;
        info.LoginName = loginname;
        info.SendFee = 0;
        info.SN1 = "";
        info.SN2 = "";
        info.Sn2Key = "";
        info.PTimes = 0;
        info.MgrCell = "";
        info.PHead = "";
        info.PEnd = "";
        info.IsCallCenter = 0;
        info.Address = Address;
        info.Introduce = Introduce;
        info.Status = 0;
        info.outnitice = "";
        info.OpenTime = "";
        info.Time1Start = Convert.ToDateTime("1990-09-08 08:00:00.000");
        info.Time1End = Convert.ToDateTime("1990-09-08 08:00:00.000");
        info.Time2Start = Convert.ToDateTime("1990-09-08 08:00:00.000");
        info.Time2End = DateTime.Now;
        info.bisnessStart = DateTime.Now;
        info.bisnessend = DateTime.Now;
        info.bisnessStart2 = DateTime.Now;
        info.bisnessend2 = DateTime.Now;
        info.IsDelete = 0;
        info.SortNum = 0;
        info.EBuilding = "";
        info.Star = 0;
        info.category = "";
        info.senttime = 0;
        info.sentorg = "0";
        info.special = "";
        info.money = 0;
        info.Inve1 = 0;
        info.cityid = Convert.ToInt32(WebUtility.get_userCityid());
        info.Picture = "";
        info.showpicture = 0;
        info.foodupdatetime = DateTime.Now;
        info.point = 100;
        info.ViewTimes = 0;
        //营业时间
        info.Opentimes1 = DateTime.Now;
        info.Opentimes2 = DateTime.Now;
        info.Closetimes1 = DateTime.Now;
        info.Closetimes2 = DateTime.Now;
        info.StopAM = DateTime.Now;
        info.StopPM = DateTime.Now;
        info.email = "";
        info.BigPicture = "";
        //新增
        info.InTime = DateTime.Now;
        info.reviewtimes = 0;
        info.PosAddr = "";
        info.PosRoom = "";
        info.PosAttch = "";
        info.PosSrvAd = "";
        info.FlavorGrade = 1;
        info.ServiceGrade = 1;
        info.SpeedGrade = 1;
        info.InTime = DateTime.Now;
        info.ViewTimes = 0;
        info.pop = 0;
        info.Password = password;
        info.InUse = "Y";
        info.RefreshTime = "30";
        int togo_num = dal.Add(info);
        if (togo_num > 0)
        {
            //string Body = SectionProxyData.GetSetValue(10);
            //Body = Body.Replace("{url}", Request["password"]);

            //int rat = Hangjing.WebCommon.SendMsg.send(Comm, Body);
            Response.Write("{\"userid\":\"" + togo_num + "\",\"state\":\"" + state + "\",\"msg\":\"新增商家成功\"}");

            Response.End();
        }
        else
        {
            Response.Write("{\"userid\":\"" + togo_num + "\",\"state\":\"-7\",\"msg\":\"新增商家失败\"}");
            Response.End();
        }
    }
}