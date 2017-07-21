using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 商家注册申请
/// </summary>
public partial class App_shop_ApplyShop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string name = HjNetHelper.GetPostParam("name");//联系人
        string tel = HjNetHelper.GetPostParam("tel");//联系电话
        string shopName = HjNetHelper.GetPostParam("shopName");//商家名称
        string address = HjNetHelper.GetPostParam("address");//商家地址
        string introduce = HjNetHelper.GetPostParam("introduce");//商家简介

        string loginName = HjNetHelper.GetPostParam("loginName");//登录名
        string passord = HjNetHelper.GetPostParam("passord");//密码

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(shopName) || string.IsNullOrEmpty(address))
        {
            Response.Write("{\"shopId\":\"0\",\"state\":\"-2\",\"msg\":\"联系人、电话、商家名称、商家地址为必填项！\"}");
            Response.End();
            return;
        }


        string lat = HjNetHelper.GetPostParam("lat");//纬度
        string lng = HjNetHelper.GetPostParam("lng");//经度


        Points dal = new Points();
        PointsInfo info = new PointsInfo();
        info.Unid = 0;
        info.ID = "0";
        info.Name = shopName;
        info.Comm = tel;
        info.PType = 0;
        info.RcvType = 0;
        info.NamePy = "";
        info.CommPerson = name;
        info.SendLimit = 0;
        info.LoginName = loginName;
        info.Password = WebUtility.GetMd5(passord);
        info.SendFee = 0;
        info.SN1 = "";
        info.SN2 = "0";
        info.Sn2Key = "";
        info.PTimes = 0;
        info.MgrCell = "";
        info.PHead = "";
        info.PEnd = "";
        info.RefreshTime = "30";
        info.IsCallCenter = 0;
        info.Address = address;
        info.Introduce = introduce;
        info.Status = 0;
        info.outnitice = "";
        info.OpenTime = "";
        info.Time1Start = DateTime.Now;
        info.Time1End = DateTime.Now;
        info.Time2Start = DateTime.Now;
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
        info.InUse = "Y";

        int shopId = dal.Add(info);

        if (shopId > 0)
        {
            Response.Write("{\"shopId\":\"" + shopId + "\",\"state\":\"1\",\"msg\":\"操作成功，请等待管理员审核\"}");
            #region 添加商家经纬度 2015-11-26 
            try
            {
                if (!string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lng))
                {
                    new ETogoLocal().Add(new ETogoLocalInfo()
                    {
                        TogoId = shopId,
                        Lat = lat,
                        Lng = lng,
                        Polygon="",
                        Radius = 0
                    });
                }
            }
            catch
            {
            } 
            #endregion
        }
        else
        {
            Response.Write("{\"shopId\":\"" + shopId + "\",\"state\":\"-1\",\"msg\":\"服务器错误，添加失败\"}");
        }

        Response.End();
    }


}