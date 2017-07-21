using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.WebCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderDetail : System.Web.UI.Page
{
    EAddress addressdal = new EAddress();
    Points daltogo = new Points();

    /// <summary>
    /// 商家编号
    /// </summary>
    private int togoId
    {
        get
        {
            object o = ViewState["TogoId"];
            return Convert.ToInt32(o);
        }
        set
        {
            ViewState["TogoId"] = value;
        }
    }

    /// <summary>
    /// 餐费
    /// </summary>
    protected decimal foodprice
    {
        get
        {
            return ViewState["foodprice"] == null ? 0 : Convert.ToDecimal(ViewState["foodprice"]);
        }
        set
        {
            ViewState["foodprice"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WebUtility.checkLocal();
            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                hidUid.Value = user.DataID.ToString();
                mycard(user.DataID);
                lbaccountmoney.InnerText = user.Usermoney.ToString();
            }
            else
            {
                historyAddr.Style["display"] = "none";
                rptusercard.Items[1].Enabled = false;
                rptusercard.Items[1].Text = rptusercard.Items[1].Text + "[会员特权]";
            }

            Bindingurl.HRef = "/user/myshopcard.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString());

            togoId = HjNetHelper.GetQueryInt("togoid", 0);

            BindOrder();
            Bindaddress();
            backurl.HRef = "shop.aspx?id=" + togoId; ;

            if (togoId == 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('您的购物是空的，点击确定点击开始订餐！');gourl('shoplist.aspx');");
                return;
            }

            PointsInfo togoinfo = daltogo.GetModel(togoId);

            OrderTime ot = new OrderTime(ddltime, togoinfo); 
            WebUtility.BindRepeater(rptfastremark, SectionProxyData.GetSTemplateList());


            WebUtility.BindList("status", "classname", CacheHelper.GetPayModelList(), rblpay);
            rblpay.Items[0].Selected = true;
        }
    }

    protected IList<Hangjing.Model.ETogoShoppingCart> BindOrder()
    {
        string tempcode = WebUtility.FixgetCookie("uc");
        string blat = WebUtility.FixgetCookie("mylat");
        string blng = WebUtility.FixgetCookie("mylng");

        Hangjing.SQLServerDAL.ETogoShoppingCart bll = new Hangjing.SQLServerDAL.ETogoShoppingCart();
        IList<Hangjing.Model.ETogoShoppingCart> list = new List<Hangjing.Model.ETogoShoppingCart>();
        IList<Hangjing.Model.ETogoShoppingCartInfo> listinfo = new List<Hangjing.Model.ETogoShoppingCartInfo>();

        list = bll.GetCart(tempcode);
        if (list.Count == 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('您的购物是空的，点击确定点击开始订餐！');gourl('shoplist.aspx');");
            WebUtility.BindRepeater(rptorder, listinfo);
            return list;
        }
        //如果没有传商家编号过来。默认第一个
        if (togoId == 0)
        {
            togoId = list[0].TogoId;
            Response.Redirect("OrderDetail.aspx?togoid=" + togoId);

        }


        list = list.Where(p => p.TogoId == togoId).ToList(); //获取该商家的购物车信息

        int all_num = 0;
        decimal all_price = 0,packagefee = 0;
        int sendree = 0;
    
        foodprice = 0;
        decimal oneshopprice = 0;

        IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + togoId);
        IList<PointsInfo> shoplist = daltogo.GetDistanceListSuper(1, 1, " unid= " + togoId, "unid", 1, blat, blng, " 1=1 ");

        IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();

        //根据配送段范围判断是否可以提交订单，设置配送费，起送价
        foreach (var item in shoplist)
        {
            if (item.Inve1 < item.Distance)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('超过" + item.Name + "配送距离" + item.Inve1 + "公里，点击确定，重新选择商家！');gourl('shoplist.aspx');");
                return null;
            }
            foreach (var record in deliveryrecord)
            {
                if (record.distancestart <= item.Distance && record.distanceend > item.Distance)
                {
                    item.SendFee = record.sendmoney;
                    item.SendLimit = record.minmoney;

                    foreach (var shopcart in list)
                    {
                        if (item.Unid == shopcart.TogoId)
                        {
                            shopcart.sendfree = Convert.ToInt32(item.SendFee);
                            shopcart.oldsendfree = Convert.ToInt32(item.SendFee);
                            shopcart.Togoremark = Convert.ToInt32(item.SendLimit);
                            break;
                        }
                    }

                    break;
                }
            }
            shoppromotions = Hangjing.WebCommon.WebHelper.getShopPromotions(item.Unid, item.PType, item.PEnd);



        }

        foreach (Hangjing.Model.ETogoShoppingCart item in list)
        {
            oneshopprice = 0;
            for (int i = 0; i < item.ItemList.Count; i++)
            {
                listinfo.Add(item.ItemList[i]);
                all_num += item.ItemList[i].PNum;
                all_price += item.ItemList[i].PPrice * item.ItemList[i].PNum;
                oneshopprice += item.ItemList[i].PPrice * item.ItemList[i].PNum;


                packagefee += item.ItemList[i].owername * item.ItemList[i].PNum;
            }
            lbtogominmoney.InnerText = "起送价：" + item.Togoremark;

            sendree += item.sendfree;
            all_price += item.sendfree;
            foodprice += oneshopprice;

        }

        all_price += packagefee;


        lbsendfee.InnerText = sendree.ToString();
        lbpackage.InnerText = packagefee.ToString();
        WebUtility.BindRepeater(rptorder, listinfo);
        this.count.InnerHtml = all_num.ToString();
        this.allprice.InnerHtml = all_price.ToString();
        hffoodprice.Value = foodprice.ToString();

        if (shoppromotions.Count == 0)
        {
            promotionbox.Style["display"] = "none";
        }
        else
        {
            WebUtility.BindRepeater(rptpromotion, shoppromotions);
        }

        return list;
    }

    /// <summary>
    /// 绑定地址，只对收货人，电话赋值
    /// </summary>
    protected void Bindaddress()
    {
        ECustomerInfo user = UserHelp.GetUser();
        if (user != null)
        {
            string sql = "userid = " + user.DataID;
            IList<EAddressInfo> list = addressdal.GetList(10, 1, sql, "pri", 1);
            if (list.Count > 0)
            {
                this.tbname.Value = list[0].Receiver;
                this.tbtel.Value = list[0].Mobilephone;
                tbdetailaddress.Value = list[0].Phone;
            }

            if (string.IsNullOrEmpty(user.PayPassword))
            {
                pwdmsg.Style["display"] = "";
                setpaypwd.HRef = "user/PayPwd.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString());
                tbpaypwd.Disabled = true;
            }
            else
            {
                pwdmsg.Style["display"] = "none";
            }

            WebUtility.BindRepeater(rptaddress, list);
        }

        this.tbaddress.Value = Server.UrlDecode(WebUtility.FixgetCookie("myaddress"));

    }

    protected void rptaddress_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
                if (new Hangjing.SQLServerDAL.EAddress().DelEAddress(Convert.ToInt32(e.CommandArgument)) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');");
                    Bindaddress();
                }
                break;
        }
    }

    protected void rptorder_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
                new Hangjing.SQLServerDAL.ETogoShoppingCart().DeleteCart(0, Convert.ToInt32(e.CommandArgument));
                BindOrder();
                break;
            case "cut":
                string[] Acut = e.CommandArgument.ToString().Split('|');
                int cnum = Convert.ToInt32(Acut[1]) - 1;
                new Hangjing.SQLServerDAL.ETogoShoppingCart().ModCart(0, Convert.ToInt32(Acut[0]), cnum, 0);

                BindOrder();

                break;
            case "add":
                string[] Aadd = e.CommandArgument.ToString().Split('|');
                int anum = Convert.ToInt32(Aadd[1]) + 1;
                new Hangjing.SQLServerDAL.ETogoShoppingCart().ModCart(0, Convert.ToInt32(Aadd[0]), anum, 0);
                BindOrder();
                break;
        }
    }

    //提交订单
    protected void btncheck_Click(object sender, EventArgs e)
    {
        string tempcode = WebUtility.FixgetCookie("uc");
        Custorder orderdal = new Custorder();
        ECustomerInfo user = UserHelp.GetUser();


        Hangjing.SQLServerDAL.ETogoShoppingCart dal = new Hangjing.SQLServerDAL.ETogoShoppingCart();
        EAddressInfo infoaddress = new EAddressInfo();
        infoaddress.Receiver = WebUtility.InputText(this.tbname.Value);
        infoaddress.Address = WebUtility.InputText(this.tbaddress.Value) + WebUtility.InputText(tbdetailaddress.Value);
        infoaddress.Phone = WebUtility.InputText(this.tbtel.Value.Trim());
        infoaddress.Mobilephone = WebUtility.InputText(this.tbtel.Value.Trim());
        infoaddress.Remark = this.tbremark.Value;
        infoaddress.paymode = Convert.ToInt32(rblpay.SelectedValue);
        infoaddress.sendtime = ddltime.SelectedValue;
        infoaddress.tempcode = tempcode;
        //是否注册，如果有，提交成功后有提示
        int isreg = 0;
        int userid = 0;

        if (user == null)
        {
            CheckUser(infoaddress.Mobilephone, out isreg, out userid);
            user = UserHelp.GetUser();
            infoaddress.CustomerName = infoaddress.Mobilephone;
        }
        else
        {
            userid = user.DataID;
            infoaddress.CustomerName = user.Name;
        }

        infoaddress.UserID = userid;

        infoaddress.kefuid = "";
        infoaddress.fromweb = ((int)OrderSource.web).ToString();
        string lat = WebUtility.FixgetCookie("mylat");
        string lng = WebUtility.FixgetCookie("mylng");

        IList<Hangjing.Model.ETogoShoppingCart> Foods = BindOrder();
        if (Foods.Count == 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('你的购物车为空，不能提交订单！');payusermoney(3);hideload_super();");
            return;
        }

        if (infoaddress.sendtime == "当前不配送")
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('商家当前不配送！');payusermoney(3);hideload_super();");
            return;
        }


        decimal togolPrice = 0;//计算总金额（菜品的小计+配送费）
        int sendree = 0;
        decimal oneshopprice = 0;

        IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();

        foreach (Hangjing.Model.ETogoShoppingCart item in Foods)
        {

            shoppromotions = Hangjing.WebCommon.WebHelper.getShopPromotions(item.TogoId, -1, "");

            oneshopprice = 0;
            for (int i = 0; i < item.ItemList.Count; i++)
            {
                togolPrice += Convert.ToDecimal(item.ItemList[i].PPrice+ item.ItemList[i].addprice+ item.ItemList[i].owername) * item.ItemList[i].PNum;
                oneshopprice += item.ItemList[i].PPrice * item.ItemList[i].PNum;
            }
            if (oneshopprice < item.Togoremark)  //item.oldsendfree == 0 && 
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('亲~本餐厅到您地址起送价格为" + item.Togoremark + "元噢！');hideload_super();");
                return;
            }

            sendree += item.sendfree;
            togolPrice += item.sendfree;
            item.latlng = "{'ulat':'" + lat + "','ulng':'" + lng + "','slat':'" + item.Lat + "','slng':'" + item.Lng + "'}";
        }

        infoaddress.foodprice = oneshopprice;
        infoaddress.senmoney = sendree;
        infoaddress.Promotions = WebHelper.getOrderPromotions(shoppromotions, infoaddress);
        foreach (var item in infoaddress.Promotions)
        {
            togolPrice -= item.freeSendFee;
        }


        if (user != null)
        {
            infoaddress.UserID = user.DataID;
            if (infoaddress.paymode == 3)//3账户余额
            {
                resultinfo paymentcheck = new UserTool(user.DataID).checkPayment(togolPrice, tbpaypwd.Value.Trim());
                if (paymentcheck.status != 1)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "alert('" + paymentcheck.message + "');hideload_super();");
                    return;
                }
            }
        }

        infoaddress.cityid = Convert.ToInt32(WebUtility.get_userCityid());

        //优惠券
        infoaddress.isuercard = Convert.ToInt32(this.rptusercard.SelectedValue);
        if (infoaddress.isuercard == 0)
        {
            infoaddress.shopcardjson = "";
        }
        else
        {
            //在dal层判断
            infoaddress.shopcardjson = hfshopcardinfo.Value;
        }

        IList<ROrderinfo> mylist =new  OrderManager().submitOrder(Foods, infoaddress,Context);
        if (mylist != null)
        {
            Session["orderinfo"] = mylist;
            Custorder cdal = new Custorder();
            dal.DeleteBytogo(tempcode, togoId);
          
            if (infoaddress.paymode == 5 && mylist[0].Currentprice > 0)
            {
                string url = CacheHelper.GetWeiXinAccount().revevar2 + "/weixinpay/nativepay.aspx?orderid=" + mylist[0].Orderid + "&price=" + mylist[0].Currentprice;
                Response.Redirect(url);
            }

            Response.Redirect("OrderSuccess.aspx?isreg=" + isreg + "&tel=" + infoaddress.Mobilephone + "&m=" + infoaddress.paymode);

        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('订单失败!');hideload_super();");
        }
    }

    /// <summary>
    /// 判断用户是否存在（根据电话），如果有返回用户编号，没有就添加用户 
    /// </summary>
    /// <returns></returns>
    protected void CheckUser(string tel, out int isreg, out int userid)
    {
        ECustomer dal = new ECustomer();
        ECustomerInfo model = new ECustomerInfo();
        isreg = 0;
        userid = 0;
        string sql = " (tell = '" + tel + "' or name = '" + tel + "') and 1=1 ";

        IList<ECustomerInfo> userlist = dal.GetList(1, 1, sql, "dataid", 1);
        if (userlist.Count > 0)
        {
            userid = userlist[0].DataID;
            UserHelp.SetLogin(userlist[0]);
        }
        else
        {
            int point = Convert.ToInt32(SectionProxyData.GetSetValue(19));
            model.EMAIL = "";
            model.Password = WebUtility.GetMd5("123456");
            model.RegTime = DateTime.Now;
            model.Point = point;
            model.Name = tel;
            model.IsActivate = -1;
            model.ActivateCode = WebUtility.RandStr(200);
            model.GroupID = 0;
            model.WebSite = ((int)OrderSource.web).ToString();
            model.RID = "";
            model.TrueName = "";
            model.MSN = "";
            model.Sex = "";
            model.Tell = tel;
            model.Phone = "";
            model.State = "0";
            model.Usermoney = 0;
            model.PhoneActivate = 0;

            if (dal.Add(model) > 0)
            {
                ECustomerInfo customer = dal.GetModelByNameAPassword(tel, model.Password);
                UserHelp.SetLogin(customer);
                EPointRecordInfo pointmodel = new EPointRecordInfo();
                pointmodel.UserID = customer.DataID;
                pointmodel.Point = point;
                pointmodel.Event = "新注册用户,获得积分" + point + "个";
                pointmodel.Time = DateTime.Now;
                new EPointRecord().Add(pointmodel);

                userid = customer.DataID;
                isreg = 1;
            }
        }
    }

    /// <summary>
    /// 我的优惠券(只绑定未使用的)
    /// </summary>
    protected void mycard(int userid)
    {
        IList<ShopCardInfo> list = new ShopCard().GetList(50, 1, "isused = 0 and userid= " + userid, "usergettime", 1);
        foreach (var item in list)
        {
            item.isbuy = 1;
        }
        this.rptcartlist.DataSource = list;
        this.rptcartlist.DataBind();
    }
}