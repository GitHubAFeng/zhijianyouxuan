using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Html5
{
    public partial class Merchantstous : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Action"]))
            {
                string action = Request.QueryString["Action"].ToString();
                // 处理异步请求
                switch (action)
                {
                    case "SaveShopInfo":
                        {

                            apiResultInfo rs = new apiResultInfo();

                            string mycode = Request.Form["tbadcode"];
                            string cookiecode = (string)Session["CheckCode"];
                            if (cookiecode == "" || cookiecode == null || mycode != cookiecode)
                            {
                                rs.state = 0;
                                rs.msg = "验证码错误，请重新输入";
                                Response.Write(JsonConvert.SerializeObject(rs));
                                Response.End();
                                return;
                            }


                            Points dal = new Points();
                            PointsInfo info = new PointsInfo();
                            info.Unid = 0;
                            info.ID = "0";
                            info.Name = WebUtility.InputText(Request.Form["tbName"]);
                            info.Comm = WebUtility.InputText(Request.Form["tbComm"]);
                            info.PType = 0;
                            info.RcvType = 0;
                            info.NamePy = "";
                            info.CommPerson = WebUtility.InputText(Request.Form["tbCommPerson"]);
                            info.SendLimit = 0;
                            info.LoginName = "";
                            info.SendFee = 0;
                            info.SN1 = "";
                            info.SN2 = "0";
                            info.Sn2Key = "";
                            info.PTimes = 0;
                            info.MgrCell = "";
                            info.PHead = "";
                            info.PEnd = "";
                            info.IsCallCenter = 0;
                            info.Address = WebUtility.InputText(Request.Form["tbAddress"]);
                            info.Introduce = "";
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
                            info.RefreshTime = "30";
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
                            info.cityid = 0;
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
                            info.Password = "";
                            info.InUse = "Y";
                            int togo_num = dal.Add(info);


                            string msg = "";

                            if (togo_num > 0)
                            {
                                rs.state = 1;
                                msg = "申请成功，请等待管理员审核";
                            }
                            else
                            {
                                rs.state = 0;
                                msg = "申请失败，请稍后再试";
                            }


                         
                            rs.msg = msg; ;
                            Response.Write(JsonConvert.SerializeObject(rs));
                            Response.End();
                            return;
                        }
                        break;
                }
            }
            else
            {
                // 正常的同步页面加载
            }
        }
    }
}