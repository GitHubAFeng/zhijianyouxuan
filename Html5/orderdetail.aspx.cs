using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.WebCommon;
using Hangjing.Weixin;

namespace Html5
{
    public partial class orderdetai : PageBase
    {

        Custorder orderdal = new Custorder();
        TogoPrinter daltp = new TogoPrinter();
        EAddress addressdal = new EAddress();
        Points daltogo = new Points();
        public string AddressText = "";
        public string PhoneText = "";
        public string ReceiverText = "";
        public string remark = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = HjNetHelper.GetQueryInt("id", 0);
            PointsInfo master = new Points().GetModel(id);

            this.hftid.Value = id.ToString();

            OrderTime ot = new OrderTime(ddltime, master);
            //选择中
            DateTime temptimes = DateTime.Now.AddMinutes(45);
            int tempMinute = temptimes.Minute / 10 * 10;
            WebUtility.SelectValue(ddltime, temptimes.Hour + ":" + tempMinute.ToString("00"));

            List<string> datelist = new List<string>();
            datelist.Add(DateTime.Now.ToShortDateString());
            datelist.Add(DateTime.Now.AddDays(1).ToShortDateString());

            WebUtility.BindRepeater(rptstyle, CacheHelper.GetPayModelList().Where(a => a.Status != 1).ToList());

            gocart.HRef = "ShowTogo.aspx?id=" + id;
            //判断是否 登录
            ECustomerInfo model = UserHelp.GetUser();

            if (model == null)
            {
                Response.Redirect("login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            else
            {
                model = new ECustomer().GetModel(model.DataID);

                //填充地址
                EAddressInfo address = new EAddressInfo();
                IList<EAddressInfo> addresslist = new List<EAddressInfo>();
                addresslist = addressdal.GetList(1, 1, "userid=" + model.DataID + " and pri=1", "dataid", 1);
                if (addresslist != null && addresslist.Count > 0)
                {
                    address = addresslist[0];

                    tbaddress.InnerHtml = address.Address + address.Phone;
                    ReceiverText = address.Receiver;
                    PhoneText = address.Mobilephone;
                    AddressText = address.Address + address.Phone;
                    addnew.Visible = false;
                    nowres.Visible = true;
                    nowress.HRef = "myAddresslist.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString());
                }
                else
                {
                    addnews.HRef = "myAddressdetail.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString());
                    addnew.Visible = true;
                    nowres.Visible = false;

                }
                hfuid.Value = model.DataID.ToString();
                lbmymoney.InnerText = model.Usermoney + "元";

                mycard(model.DataID);
                mypackage(model.Tell);

                addcardllink.HRef = "addshopcard.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString());
            }

            remark = Server.UrlDecode(Server.UrlDecode(WebUtility.FixgetCookie("mark")));
            if (WebUtility.FixgetCookie("mark") != "")
            {
                WebUtility.FixsetCookie("mark", Server.UrlEncode(remark), 30);
            }
            else
            {
                remark = "点击添加备注";
            }

            string method = HjNetHelper.GetQueryString("method");
            if (method == "addorder")
            {
                add_Click();
            }

            //获取post信息，提交订单
            if (Request.HttpMethod.ToUpper() == "POST")
            {

            }
            else
            {
                IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();
                shoppromotions = Hangjing.WebCommon.WebHelper.getShopPromotions(master.Unid, master.PType, master.PEnd);
                WebUtility.BindRepeater(rptpromotion, shoppromotions);
            }
        }

        /// <summary>
        /// 我的优惠券(只绑定未使用的)
        /// </summary>
        protected void mycard(int userid)
        {
            IList<ShopCardInfo> list = new ShopCard().GetList(50, 1, "isused = 0 and userid= " + userid, "usergettime", 1);
            list.Insert(0, new ShopCardInfo() { ckey = "0", ReveVar1 = "不使用优惠券" });
            list.Insert(0, new ShopCardInfo() { ckey = "0", ReveVar1 = "选择优惠券" });


            WebUtility.BindList("ckey", "ReveVar1", list, dllcard);
        }

        /// <summary>
        /// 我的红包()
        /// </summary>
        protected void mypackage(string tel)
        {
            string sqlwhere = "1=1 and ReveVar='" + tel + "' and num=0 and validitytime>'" + DateTime.Now + "'";
            IList<msgpacketInfo> list = new msgpacket().GetList(999, 1, sqlwhere, "id", 1);

            WebUtility.BindRepeater(rptpackage, list);

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
                model.WebSite = "";
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
        /// 提交订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void add_Click()
        {
            int id = HjNetHelper.GetQueryInt("id", 0);
            ECustomerInfo user = UserHelp.GetUser();
            EAddressInfo infos = new EAddressInfo();
            string blat = WebUtility.FixgetCookie("mylat");
            string blng = WebUtility.FixgetCookie("mylng");

            infos.Receiver = ReceiverText;
            infos.Phone = PhoneText;
            infos.Mobilephone = PhoneText;
            if (WebUtility.FixgetCookie("mark") != "")
            {
                infos.Remark = Server.UrlDecode(Server.UrlDecode(WebUtility.FixgetCookie("mark")));
            }
            else
            {
                infos.Remark = "";
            }
            infos.Address = AddressText;
            infos.sendtime = WebUtility.InputText(Request.Form["ddltime"]);

            apiResultInfo rs = new apiResultInfo();
            rs.state = 0;

            if (infos.sendtime == "当前不配送")
            {
                rs.msg = "不在商家配送时间内";
                sendToClient(rs);

                return;
            }

            int uid = 0;
            //是否注册，如果有，提交成功后有提示
            int isreg = 0;
            if (user == null)
            {
                CheckUser(infos.Mobilephone, out isreg, out uid);
                user = UserHelp.GetUser();
            }
            else
            {
                uid = user.DataID;
            }
            infos.UserID = uid;

            //本地存储的信息，生成foods信息。
            string productjson = Request.Form["hfproductjson"];

            IList<Hangjing.Model.ETogoShoppingCart> Foods = new List<Hangjing.Model.ETogoShoppingCart>();
            decimal togolPrice = 0;//计算总金额（菜品的小计+配送费）
            decimal allcaipin = 0;
            int sendree = 0;//配送费
            Hangjing.Model.ETogoShoppingCart myshopcart = new Hangjing.Model.ETogoShoppingCart();
            myshopcart.TogoId = id;
            myshopcart.sendfree = 0;
            //满多少元免配送费未处理
            myshopcart.ItemList = null;

            Cartinfo cartlist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Cartinfo>(productjson);

            IList<ETogoShoppingCartInfo> foodlist = new List<ETogoShoppingCartInfo>();
            foreach (var item in cartlist.productlist)
            {
                ETogoShoppingCartInfo food = new ETogoShoppingCartInfo();


                food.addprice = Convert.ToDecimal(item.addprice);
                food.sid = Convert.ToInt32(item.sid);
                food.sname = item.sname;
                food.material = item.material;

                if (food.sname == null)
                {
                    food.sname = "";
                }
                if (food.material == null)
                {
                    food.material = "";
                }

                food.PId = Convert.ToInt32(item.id);
                food.PName = item.name;
                food.PNum = Convert.ToInt32(item.number);
                food.PPrice = Convert.ToDecimal(item.price);
                food.Currentprice = food.PPrice;
                food.Foodcurrentprice = food.PPrice;
                food.Remark = "";
                food.owername = item.packagefee;


                foodlist.Add(food);
            }
            myshopcart.ItemList = foodlist;

            Foods.Add(myshopcart);
            foreach (Hangjing.Model.ETogoShoppingCart item in Foods)
            {
                for (int i = 0; i < item.ItemList.Count; i++)
                {
                    allcaipin += Convert.ToDecimal(item.ItemList[i].PPrice + item.ItemList[i].addprice + item.ItemList[i].owername) * item.ItemList[i].PNum;
                }
            }
            IList<shopdeliveryInfo> deliveryrecord = new shopdelivery().GetList(" tid = " + id);
            IList<PointsInfo> master = new Points().GetDistanceListSuper(1, 1, " unid= " + id, "unid", 1, blat, blng, " 1=1 ");
            //根据配送段范围判断是否可以提交订单，设置配送费，起送价
            foreach (var item in master)
            {
                if (item.Inve1 < item.Distance)
                {
                    rs.msg = "超过" + item.Name + "配送距离" + item.Inve1 + "公里，请重新选择商家！";
                    sendToClient(rs);
                    return;
                }
                foreach (var record in deliveryrecord)
                {
                    if (record.distancestart <= item.Distance && record.distanceend > item.Distance)
                    {
                        item.SendFee = record.sendmoney;
                        item.SendLimit = record.minmoney;

                        foreach (var shopcart in Foods)
                        {
                            if (item.Unid == shopcart.TogoId)
                            {
                                decimal fillprice = Convert.ToDecimal(master[0].PTimes);
                                decimal oldpaymoney = allcaipin + item.SendFee;
                                decimal SendLimit = item.SendLimit;
                                shopcart.sendfree = Convert.ToInt32(item.SendFee);
                                if (fillprice > 0 && fillprice <= allcaipin)
                                {
                                    shopcart.sendfree = 0;
                                }
                                shopcart.Togoremark = Convert.ToInt32(item.SendLimit);
                                shopcart.Lat = item.Lat;
                                shopcart.Lng = item.Lng;
                                break;
                            }
                        }

                        break;
                    }
                }
            }

            IList<webPromotionConfigInfo> shoppromotions = new List<webPromotionConfigInfo>();
            foreach (Hangjing.Model.ETogoShoppingCart item in Foods)
            {
                shoppromotions = Hangjing.WebCommon.WebHelper.getShopPromotions(item.TogoId, -1, "");
                decimal oneshopprice = 0;
                for (int i = 0; i < item.ItemList.Count; i++)
                {
                    togolPrice += Convert.ToDecimal(item.ItemList[i].PPrice + item.ItemList[i].addprice) * item.ItemList[i].PNum;
                    oneshopprice += Convert.ToDecimal(item.ItemList[i].PPrice + item.ItemList[i].addprice) * item.ItemList[i].PNum;
                }
                if (item.Togoremark > oneshopprice)
                {
                    rs.msg = "低于" + item.TogoName + "起送价" + item.Togoremark + "元！";
                    sendToClient(rs);
                    return;
                }

                sendree += item.sendfree;
                togolPrice += item.sendfree;
                item.latlng = "{'ulat':'" + blat + "','ulng':'" + blng + "','slat':'" + item.Lat + "','slng':'" + item.Lng + "'}";

                infos.foodprice = oneshopprice;
                infos.senmoney = sendree;
            }

            infos.paymode = Convert.ToInt32(WebUtility.InputText(Request.Form["ddlpaymode"]));
            infos.Promotions = WebHelper.getOrderPromotions(shoppromotions, infos);
            foreach (var item in infos.Promotions)
            {
                togolPrice -= item.freeSendFee;
            }

            string uc = WebUtility.FixgetCookie("openid");
            if (uc == null)
            {
                uc = "";
            }
            infos.kefuid = "";
            infos.tempcode = uc;
            infos.fromweb = ((int)OrderSource.weixin).ToString();
            infos.Ordersource = ((int)OrderSource.weixin).ToString();
            infos.UserID = uid;


            infos.redpackage = new msgpacketInfo();
            if (true)
            {
                string ddlpackage = WebUtility.InputText(Request.Form["ddlpackage"]).Trim();
                if (ddlpackage != "0")
                {
                    infos.redpackage = new msgpacket().GetModel(Convert.ToInt32(ddlpackage));
                    if (infos.redpackage != null)
                    {
                        if (infos.redpackage.moneyline > infos.foodprice)
                        {
                            rs.msg = "此红包满" + infos.redpackage.moneyline + "元可用";

                            sendToClient(rs);
                            return;
                        }
                        else
                        {
                            togolPrice -= infos.redpackage.alltotal;
                        }
                    }
                }
            }


            //优惠券,判断券的正确，可用性，生成json兼容pc端的数据
            infos.CustomerName = user.Name;//使用优惠券时需要添加 2015-12-9 
            infos.shopcardjson = "";
            infos.isuercard = 0;
            string cardckey = WebUtility.InputText(Request.Form["dllcard"]).Trim();
            if (cardckey != "0")
            {
                resultinfo cardrs = Hangjing.WebCommon.WebHelper.checkCard(cardckey, allcaipin);
                if (cardrs.status == 0)
                {
                    infos.shopcardjson = cardrs.data;
                    infos.isuercard = 1;
                }
                else
                {
                    rs.msg = cardrs.message;
                    sendToClient(rs);
                    return;
                }
            }

            string PayPassword = WebUtility.InputText(Request.Form["tbpaypwd"]);

            decimal useroney = user.Usermoney;
            if (infos.paymode == 3)//3账户余额
            {
                if (useroney == 0)
                {
                    rs.msg = "余额不足，请选择其他支付方式";
                    sendToClient(rs);
                    return;

                }
                //可以支付部分金额
                if (useroney < togolPrice)
                {
                    rs.msg = "余额不足，请选择其他支付方式";
                    sendToClient(rs);
                    return;
                }

                if (WebUtility.GetMd5(PayPassword) != user.PayPassword)
                {
                    rs.msg = "支付密码错误，请重新输入";
                    sendToClient(rs);
                    return;
                }
            }

            ////这里进行所有的判断
            IList<ROrderinfo> mylist = new OrderManager().submitOrder(Foods, infos, Context);

            if (mylist != null)
            {
                Session["orderinfo"] = mylist;

                Custorder cdal = new Custorder();
                string orderid = mylist[0].Orderid;
                string Currentprice = mylist[0].Currentprice.ToString();

                rs.state = 1;
                rs.msg = "订单提交成功";

                CustorderInfo order = new Custorder().GetModel(orderid);

                int haspackage = Hangjing.WebCommon.sendPacket.packetHandle(order);
                if (haspackage > 0)
                {
                    rs.msg = "恭喜您订单提交成功，还获得了若干分享红包，分享红包自己也获取哦";
                }

             
                WechatPayInfo payinfo = new WechatPayInfo();

                if (infos.paymode == 5 && mylist[0].Currentprice > 0)
                {
                    //payinfo = new WechatPay(Context).BuildPayParam(orderid, mylist[0].Currentprice, infos.UserID, base.openid);
                }
                string url = "/OrderSuccess.aspx?isreg=" + isreg + "&tel=" + infos.Mobilephone + "&m=" + infos.paymode + "&orderid=" + orderid + "&price=" + Currentprice + "&haspackage=" + haspackage;

                rs.data = Newtonsoft.Json.JsonConvert.DeserializeObject("{'paydata':" + Newtonsoft.Json.JsonConvert.SerializeObject(payinfo) + ",'url':'"+ url + "'}");



                sendToClient(rs);
                return;

            }
            else
            {
                rs.msg = "服务器繁忙，请稍后再试";
                sendToClient(rs);
                return;
            }
        }

        /// <summary>
        /// 给客户端发送消息
        /// </summary>
        /// <param name="rs"></param>
        protected void sendToClient(apiResultInfo rs)
        {
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(rs));
            Response.End();
        }
    }
}
