using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

namespace Html5
{
    public partial class myAddressdetail : System.Web.UI.Page
    {

        EAddress dal = new EAddress();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ECustomerInfo info = UserHelp.GetUser();
                if (info == null)
                {
                    Response.Redirect("login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
                }

                int id = HjNetHelper.GetQueryInt("id", 0);

                if (id == 0)
                {
                    this.updateaddress.InnerHtml = "新增地址";
                }
                else
                {
                    this.updateaddress.InnerHtml = "修改地址";
                    GetFirstAddress(id);
                }
                int aid = HjNetHelper.GetQueryInt("aid", 0);
                if (aid != 0)
                {
                    //重新选择了地址
                    BuildingInfo model = new EBuilding().GetModel(aid);
                }
                if (Request["returnurl"] != "" && Request["returnurl"] != null)
                {
                    back.HRef = Server.UrlDecode(Request["returnurl"]);
                }

                if (Request.HttpMethod.ToUpper() == "POST")
                {
                    add_Click();
                }

                //错误信息
                int err = HjNetHelper.GetQueryInt("err", 0);
                if (err == 1)
                {
                    hferrmsg.Value = Session["order_errinfo"].ToString();
                }
            }

            //获取post信息，提交订单
        }
        /// <summary>
        /// 获得默认地址
        /// </summary>
        public void GetFirstAddress(int id)
        {
            string SqlWhere = " DataID=" + id;

            IList<EAddressInfo> list = dal.GetList(1, 1, SqlWhere, "pri", 1);
            if (list.Count != 0)
            {
                EAddressInfo model = list[0];
                this.tbReceiveName.Value = model.Receiver;
                this.keyaddress.Value = model.Address;
                hfdefaultvalue.Value = model.Address;
                hidlat.Value = model.Lat;
                hidlng.Value = model.Lng;
                this.tbCellPhone.Value = model.Mobilephone;
                this.tbdoor.Value = model.Phone;
            }
        }
        /// <summary>
        /// 提交地址信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {
            string Address = WebUtility.InputText(Request.Form["keyaddress"]);
            string ReceiveName = WebUtility.InputText(Request.Form["tbReceiveName"]);
            string tbphone = WebUtility.InputText(Request.Form["tbCellPhone"]);
            string tbdoor = WebUtility.InputText(Request.Form["tbdoor"]);

            EAddressInfo info = new EAddressInfo();
            info.Receiver = ReceiveName;

            info.Phone = tbdoor;
            info.Mobilephone = tbphone;
            info.UserID = UserHelp.GetUser().DataID;
            info.AddTime = DateTime.Now;
            info.Pri = 1;
            info.Lat = WebUtility.InputText(Request.Form["hidlat"]);
            info.Lng = WebUtility.InputText(Request.Form["hidlng"]);
            info.Address = Address;

            int backnum = 0;

            int dataid = HjNetHelper.GetQueryInt("id", 0);

            if (dataid == 0)
            {

                backnum = dal.Add(info);

            }
            else
            {
                info.DataID = dataid;

                if (dal.Update(info) == 1)
                {
                    dal.UpdateDefaut(dataid, info.UserID);


                    if (Request["returnurl"] != "" && Request["returnurl"] != null)
                    {
                        WebUtility.FixsetCookie("mylat", info.Lat, 30);
                        WebUtility.FixsetCookie("mylng", info.Lng, 30);
                    }

                    Session["order_errinfo"] = "修改地址成功";
                    Response.Redirect("myAddressdetail.aspx?err=1&returnurl=" + Server.UrlEncode(Request["returnurl"]));



                    return;
                }
                else
                {
                    Session["order_errinfo"] = "修改失败";
                    Response.Redirect("myAddressdetail.aspx?err=1&returnurl=" + Server.UrlEncode(Request["returnurl"]));
                    return;
                }
            }

            if (backnum > 0)
            {
                dal.UpdateDefaut(backnum, info.UserID);

                Session["order_errinfo"] = "添加地址成功";
                if (Request["returnurl"] != "" && Request["returnurl"] != null)
                {

                    WebUtility.FixsetCookie("mylat", info.Lat, 30);
                    WebUtility.FixsetCookie("mylng", info.Lng, 30);


                    Response.Redirect(Server.UrlDecode(Request["returnurl"]));
                    return;
                }
                else
                {
                    Response.Redirect("myAddressdetail.aspx?err=1&returnurl=" + Server.UrlEncode(Request["returnurl"]));
                    return;
                }


            }
            else
            {
                Session["order_errinfo"] = "添加失败";
                Response.Redirect("myAddressdetail.aspx?err=1&returnurl=" + Server.UrlEncode(Request["returnurl"]));
                return;
            }
        }
    }
}
