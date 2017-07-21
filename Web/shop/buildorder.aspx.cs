using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.IO;
using Hangjing.Common;

public partial class TogoHome_FoodDetailbuildorder : System.Web.UI.Page
{
    Custorder orderdal = new Custorder();
    protected int userid
    {
        get
        {
            object o = ViewState["userid"];
            return (o == null) ? 0 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["userid"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            PointsInfo togoinfo = UserHelp.GetUser_Togo();
            OrderTime ot = new OrderTime(ddltime, togoinfo);
            hidLat.Value = SectionProxyData.GetSetValue(4);
            hidlocalflag.Value = SectionProxyData.GetSetValue(4);
            hidLng.Value = SectionProxyData.GetSetValue(5);

            intiAddress();
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        PointsInfo shop = UserHelp.GetUser_Togo();
        shop = new Points().GetModel(shop.Unid);
        ETogoLocalInfo info = new ETogoLocalInfo();
        ETogoLocal bll = new ETogoLocal();

        info = bll.GetInfoById(shop.Unid.ToString());

        CustorderInfo infoaddress = new CustorderInfo();
        infoaddress.OrderRcver = WebUtility.InputText(this.tbuname.Text);
        infoaddress.AddressText = WebUtility.InputText(this.tbaddress.Text) + WebUtility.InputText(tbaddressdetail.Text);
        infoaddress.OrderComm = WebUtility.InputText(this.tbtel.Text.Trim());
        infoaddress.OrderAttach = "[" + tbfoodcount.Text +"份餐]" + WebUtility.InputText(this.tbremark.Text.Trim());
        infoaddress.paymode = 4;
        infoaddress.SendTime = Convert.ToDateTime(ddltime.SelectedValue);
        infoaddress.tempcode = "";
        infoaddress.CustomerName = "";
        infoaddress.UserId = 0;
        infoaddress.fromweb = ((int)OrderSource.ShopCenter).ToString();
        infoaddress.ReveVar1 = shop.sentorg;

        string ulat = hidLat.Value;
        string ulng = hidLng.Value;

        if (ulat == SectionProxyData.GetSetValue(4))
        {
            ulat = "0";
            ulng = "0";
        }

        infoaddress.ReveVar2 = "{'ulat':'" + ulat + "','ulng':'" + ulng + "','slat':'" + info.Lat + "','slng':'" + info.Lng + "'}";
        infoaddress.OrderSums = Convert.ToDecimal(tbPrice.Text);
        infoaddress.shopdiscountmoney = infoaddress.OrderSums - Convert.ToDecimal(shop.SN2);
        infoaddress.OldPrice = infoaddress.OrderSums;
        infoaddress.cityid = shop.cityid;
        infoaddress.TogoId = shop.Unid;
        infoaddress.P2Sign = "";
        infoaddress.paytime = Convert.ToDateTime("1970-1-1");
        infoaddress.IsShopSet = 1;
        infoaddress.ReveDate1 = DateTime.Now;

        if (orderdal.AddTBOrder(infoaddress) > 0)
        {
            AlertScript.RegScript(this.Page, "alert('订单提交成功!');hideload_super();;gourl('buildorder.aspx')");
            if (add_dataid.Value == "0")
            {
                EAddressInfo addrmodel = new EAddressInfo();
                addrmodel.Receiver = infoaddress.OrderRcver;
                addrmodel.Address = infoaddress.AddressText;
                addrmodel.BuildingID = 0;
                addrmodel.Phone = infoaddress.OrderComm;
                addrmodel.Mobilephone = infoaddress.OrderComm;
                addrmodel.UserID = 0;
                addrmodel.AddTime = DateTime.Now;
                addrmodel.Pri = 0;
                addrmodel.Lat = WebUtility.InputText(hidLat.Value);
                addrmodel.Lng = WebUtility.InputText(this.hidLng.Value);

                int userid = new EAddress().SaveUserAndAddress(addrmodel);
            }
        }
        else
        {
            AlertScript.RegScript(this.Page, "alert('订单失败!');hideload_super();;");
        }

    }

    public void intiAddress()
    {
        string tel = HjNetHelper.GetQueryString("tel");
        //2用户地址
        if (tel != null && tel != "")
        {
            tbtel.Text = tel;
            ECustomerInfo user = new ECustomer().GetListByTel(tel);
            if (user != null)
            {
                userid = user.DataID;
                WebUtility.FixsetCookie("crm_uid", user.DataID.ToString(), 1);
                string addsql = " userid =" + user.DataID;
                IList<EAddressInfo> addlist = new EAddress().GetList(5, 1, addsql, "pri", 1);
                if (addlist.Count > 0)
                {
                    tbuname.Text = addlist[0].Receiver;
                    tbtel.Text = addlist[0].Mobilephone;
                    tbaddressdetail.Text = addlist[0].Address;
                    add_dataid.Value = addlist[0].DataID.ToString();
                    EAddressInfo addmodel = new EAddressInfo();
                    addmodel.Receiver = "新地址";
                    addmodel.Address = "";
                    addlist.Add(addmodel);


                    hidLat.Value = addlist[0].Lat;
                    hidlocalflag.Value = addlist[0].Lat;
                    hidLng.Value = addlist[0].Lng;
                }
                WebUtility.BindRepeater(rptaddress, addlist);
                lbtelmsg.InnerText = "(会员)";
            }
            else
            {
                lbtelmsg.InnerText = "(非会员)";
            }
        }

    }

}
