using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.DBUtility;

public partial class qy_54tss_AreaAdmin_Service_submitorder : System.Web.UI.Page
{
    Points daltogo = new Points();
    Hangjing.SQLServerDAL.ETogoShoppingCart dal = new Hangjing.SQLServerDAL.ETogoShoppingCart();

    protected IList<Hangjing.Model.ETogoShoppingCart> Foods
    {
        set
        {
            ViewState["Foods"] = value;
        }
        get
        {
            return ViewState["Foods"] == null ? null : (IList<Hangjing.Model.ETogoShoppingCart>)ViewState["Foods"];
        }
    }

    /// <summary>
    /// 经度
    /// </summary>
    protected string blng
    {
        set
        {
            ViewState["blng"] = value;
        }
        get
        {
            return ViewState["blng"] == null ? "" : ViewState["blng"].ToString();
        }
    }

    /// <summary>
    /// 纬度
    /// </summary>
    protected string blat
    {
        set
        {
            ViewState["blat"] = value;
        }
        get
        {
            return ViewState["blat"] == null ? "" : ViewState["blat"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int id = HjNetHelper.GetQueryInt("id", 0);

            string uid = WebUtility.FixgetCookie("crm_uid");
            if (uid == null || uid == "")
            {
                AlertScript.RegScript(Page, UpdatePanel1, "alert('请先选择用户');gourl('OrderCrm.aspx');");
                return;
            }

            string usercode = WebUtility.FixgetCookie("uc");

            Foods = dal.GetCart(usercode);
            //计算配送费

            //计算配送费
            blat = WebUtility.FixgetCookie("mylat");
            blng = WebUtility.FixgetCookie("mylng");


            IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + id);

            string strDistance = " 1=1 ";

            IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, "Points.unid=" + id, "unid", 1, blat, blng, strDistance);
            foreach (var item in shoplist)
            {
                foreach (var record in deliveryrecord)
                {
                    if (record.distancestart <= item.Distance && record.distanceend > item.Distance)
                    {
                        item.SendFee = record.sendmoney;
                        item.SendLimit = record.minmoney;
                        break;
                    }
                }
            }

            PointsInfo model = shoplist[0];
            hftid.Value = id.ToString();
            hidTogoName.Value = model.Name;


            int m_sendmoney = 0;
            decimal m_totalmoney = 0;
            int m_allnum = 0;
            decimal oneshopprice = 0;


            foreach (Hangjing.Model.ETogoShoppingCart item in Foods)
            {
                foreach (var shop in shoplist)
                {
                    if (item.TogoId == shop.Unid)
                    {
                        item.sendfree = Convert.ToInt32(shop.SendFee);
                    }
                }
                oneshopprice = 0;
                for (int i = 0; i < item.ItemList.Count; i++)
                {
                    m_totalmoney += item.ItemList[i].PPrice * item.ItemList[i].PNum;
                    m_allnum += item.ItemList[i].PNum;

                    oneshopprice += item.ItemList[i].PPrice * item.ItemList[i].PNum;
                }
                if (item.ptimes > 0 && oneshopprice >= item.ptimes)
                {
                    item.sendfree = 0;
                }

                m_sendmoney += item.sendfree;
                m_totalmoney += item.sendfree;
            }
            lballmoney.InnerText = m_totalmoney.ToString();
            lbnum.InnerText = m_allnum.ToString();
            lbsendmony.InnerText = m_sendmoney.ToString();

            rptFoodSort.DataSource = Foods;
            rptFoodSort.DataBind();

            string myaddrid = WebUtility.FixgetCookie("used_addressid");
            if (myaddrid != null && myaddrid != "")
            {
                int myid = Convert.ToInt32(myaddrid);
                EAddressInfo addrmodel = new EAddress().GetModel(myid);
                tbuname.Text = addrmodel.Receiver;
                tbaddress.Text = addrmodel.Address;
                tbtel.Text = WebUtility.FixgetCookie("crm_tell");
                tbbuildingname.Text = addrmodel.BuildingName;

            }

            OrderTime ot = new OrderTime(ddltime, model);

        }
    }

    protected void add_click(object sender, EventArgs e)
    {
        //反馈添加 2015-8-12 
        if (ddltime.SelectedValue == "当前不配送")
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('该商家当前不配送！')");
            return;
        }

        string usercode = WebUtility.FixgetCookie("uc");
        ECustomerInfo user = new ECustomer().GetListByTel(HjNetHelper.GetQueryString("tel"));
        int uid = user.DataID;
        Custorder orderdal = new Custorder();
        EAddressInfo infos = new EAddressInfo();
        infos.Receiver = WebUtility.InputText(this.tbuname.Text);
        string maddress = WebUtility.InputText(this.tbaddress.Text);
        infos.Address = maddress;
        infos.Phone = WebUtility.InputText(this.tbtel.Text.Trim());
        infos.Mobilephone = WebUtility.InputText(this.tbtel.Text.Trim());
        infos.UserID = uid;
        infos.Remark = WebUtility.InputText(this.tbremark.Value);
        infos.DataID = 2;
        infos.sendtime = ddltime.SelectedValue;  
        infos.cityid = 0;
        infos.paymode = 4;
        infos.kefuid = UserHelp.GetAdmin().AdminName;
        infos.ReveInt1 = Convert.ToInt32(tbpeople.Text);
        infos.ReveInt2 = 0;
        infos.fromweb = ((int)OrderSource.CallCenter).ToString();
        infos.tempcode = usercode;

        IList<Hangjing.Model.ETogoShoppingCart> list = Foods;

        decimal togolPrice = 0;//计算总金额（菜品的小计+配送费）
        decimal oneshopprice = 0;
        int sendree = 0;

        foreach (Hangjing.Model.ETogoShoppingCart item in Foods)
        {
            oneshopprice = 0;
            for (int i = 0; i < item.ItemList.Count; i++)
            {
                togolPrice += Convert.ToDecimal(item.ItemList[i].PPrice) * item.ItemList[i].PNum;
                oneshopprice += item.ItemList[i].PPrice * item.ItemList[i].PNum; ;
            }
            if (item.ptimes > 0 && oneshopprice >= item.ptimes)
            {
                item.sendfree = 0;
            }

            sendree += item.sendfree;
            togolPrice += item.sendfree;
            item.latlng = "{'ulat':'" + blat + "','ulng':'" + blng + "','slat':'" + item.Lat + "','slng':'" + item.Lng + "'}";
        }


        IList<Hangjing.Model.ROrderinfo> mylist = orderdal.SubmitOrder(Foods, infos);
        if (mylist != null)
        {
            dal.DelAllCartItem(usercode);
            WebUtility.FixdelCookie("crm_tell");
            WebUtility.FixdelCookie("crm_uid");
            WebUtility.FixdelCookie("used_addressid");

            foreach (var item in mylist)
            {
                NoticeHelper notice = new NoticeHelper(Context, item.togoid.ToString());
                notice.send2ShopByShopid();

                if (item.WeiXxinOpenID != "" && SectionProxyData.GetSetValue(58) == "1")
                {
                    new Hangjing.Weixin.SendMsg(Context).sendText(item.WeiXxinOpenID, "您有新订单：" + item.Orderid + "，请注意查收");
                }
            }

            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('订单提交成功!');hideload_super();gourl('OrderCrm.aspx')");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('订单失败!')");
        }
    }

}
