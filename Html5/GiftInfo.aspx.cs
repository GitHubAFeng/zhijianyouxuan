using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

namespace Html5
{
    public partial class GiftInfo : System.Web.UI.Page
    {
        protected string msg;
        protected string goUrl;
        private int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserHelp.IsLogin(Server.UrlEncode(Request.Url.PathAndQuery));

            if (Request.HttpMethod.ToUpper() == "POST")
            {
                switch (Request.Form["hType"])
                {
                    case "card": GetGift_Click(); Card(); break;//兑换优惠券
                    case "other": btGet_Click(); Other(); break;//兑换其他
                    default: break;
                }
            }
            else
            {
                string type = Request.QueryString["type"];
                Id = HjNetHelper.GetQueryInt("id", 0);
                this.hType.Value = type;//存入隐藏域 以便在提交时判断
                this.hId.Value = Id.ToString(); //存入隐藏域 以便在提交时判断
                switch (type)
                {
                    case "card": Card(); break;//优惠券
                    case "other": Other(); break;//其他
                    default: break;
                }
            }
        }


        /// <summary>
        /// 优惠券信息
        /// </summary>
        private void Card()
        {
            batshopcard dal = new batshopcard();
            batshopcardInfo info = dal.GetModel(Id);
            this.title.InnerText = info.title;
            this.point.InnerText = info.mydiscount.ToString();
            this.point2.InnerText = info.mydiscount.ToString();
            this.address.Visible = false;//隐藏地址
        }

        /// <summary>
        /// 礼品信息
        /// </summary>
        private void Other()
        {
            Gifts bll = new Gifts();
            GiftsInfo info = bll.GetModel(Id);
            this.title.InnerText = info.Gname;
            this.point.InnerText = info.NeedIntegral;
            this.point2.InnerText = info.NeedIntegral;

            this.hStocks.Value = info.Stocks.ToString();//库存
            this.hGiftName.Value = info.Giftname;//礼品名称
            this.hPoint.Value = info.NeedIntegral;//所需积分

            this.IsNeedAddress.Value = "1";//需要收货地址

            ECustomerInfo user = UserHelp.GetUser();
            if (user != null)
            {
                string sql = "UserID =" + user.DataID;
                IList<EAddressInfo> list = new EAddress().GetList(1, 1, sql, "pri", 1);//读取用户默默地址
                if (list.Count > 0)
                {
                    this.tbAddress.InnerText = list[0].Address;
                    this.tbPerson.InnerText = list[0].Receiver;
                    this.tbPhone.InnerText = list[0].Mobilephone;

                    if (string.IsNullOrEmpty(list[0].Address) || string.IsNullOrEmpty(list[0].Receiver))
                    {
                        this.IsSHAddress.Value = "0";//标示为没有收货地址
                        this.noAddress.Style["display"] = "block";
                        this.addressInfo.Style["display"] = "none";
                        this.addressBackground.Style["background"] = "none";//没有下面的花条
                    }

                    //存入隐藏域
                    this.hAddress.Value = list[0].Address;
                    this.hPerson.Value = list[0].Receiver;
                    this.hPhone.Value = list[0].Mobilephone;
                }
                else
                {
                    this.IsSHAddress.Value = "0";//标示为没有收货地址
                    this.noAddress.Style["display"] = "block";
                    this.addressInfo.Style["display"] = "none";
                    this.addressBackground.Style["background"] = "none";//没有下面的花条

                    //WebUtility.FixsetCookie("MyAddressListReutrnUrl", Server.UrlEncode(Request.RawUrl), 1);
                    //this.address.Visible = false;//隐藏地址
                    //msg = "您还没有收货地址，请先添加地址！";
                    //goUrl = "/MyAddressDetail.aspx";
                    //return;
                }
            }
        }


        /// <summary>
        /// 兑换优惠券
        /// </summary>
        protected void GetGift_Click()
        {
            Id = HjNetHelper.GetFormInt("hId", 0);
            batshopcard dal = new batshopcard();
            batshopcardInfo info = dal.GetModel(Id);

            //1.判断积分是否足够进行兑换 积分需要从数据库中获取不能从cookie或者session中获取以免出现错误
            ECustomer cbll = new ECustomer();
            ECustomerInfo user = cbll.GetModel(UserHelp.GetUser().DataID);
            int sumNumber = Convert.ToInt32(Request.Form["sumNumber"]);

            goUrl = "/GiftList.aspx";
            if (user.Point < info.mydiscount * sumNumber)
            {
                msg = "积分不足，不能兑换！";
                return;
            }
            if (info.CardCount < sumNumber)
            {
                msg = "此券库存不足，不能兑换！";
                return;
            }

            //2.进行兑换 (减库存，减积分，生成券记录)
            int num = 0;
            for (int i = 0; i < sumNumber; i++)
            {
                int rs = dal.ExchangeVoucher(user.DataID, info.DataID, info.mydiscount);
                switch (rs)
                {
                    case -1:
                        msg += "兑换第" + (i + 1) + "张券时 积分不足！";
                        break;
                    case -2:
                        msg += "兑换第" + (i + 1) + "张券时 库存不足！";
                        break;
                    case 0:
                        //msg += "第" + (i + 1) + "张券兑换成功！";
                        num++;
                        break;
                    default:
                        break;
                }
            }

            msg = string.Format("成功兑换{0}张优惠券！", num) + msg;

            //更新用户资料 主要更新积分
            UserHelp.SetLogin(cbll.GetModel(UserHelp.GetUser().DataID));
        }

        /// <summary>
        /// 兑换礼品
        /// </summary>
        protected void btGet_Click()
        {
            Id = HjNetHelper.GetFormInt("hId", 0);
            //判断积分是否足够进行兑换 积分需要从数据库中获取不能从cookie或者session中获取以免出现错误
            ECustomer cbll = new ECustomer();
            ECustomerInfo user = cbll.GetModel(UserHelp.GetUser().DataID);
            int stocks = Convert.ToInt32(Request.Form["hStocks"]);//库存数量
            int sumNumber = Convert.ToInt32(Request.Form["sumNumber"]);//购买数量
            int Point = Convert.ToInt32(Request.Form["hPoint"]);//单个积分

            goUrl = "/GiftList.aspx";
            if (stocks < sumNumber)
            {
                msg = "库存不足，不能兑换！";
                return;
            }

            if (user.Point < Point * sumNumber)
            {
                msg = "积分不足，不能兑换！";
                return;
            }

            //增加兑换记录
            IntegralInfo iinfo = new IntegralInfo();
            #region 获取页面填写的
            //iinfo.Address = WebUtility.InputText(Request.Form["tbAddress"]);//收货地
            //iinfo.Person = WebUtility.InputText(Request.Form["tbPerson"]);   //收货人
            //iinfo.Phone = WebUtility.InputText(Request.Form["tbPhone"]);     //电话 
            #endregion
            //获取隐藏域的
            iinfo.Person = WebUtility.InputText(Request.Form["hPerson"]);
            iinfo.Phone = WebUtility.InputText(Request.Form["hPhone"]);
            iinfo.Address = WebUtility.InputText(Request.Form["hAddress"]);

            iinfo.CustId = user.DataID.ToString();
            iinfo.UserName = user.Name;
            iinfo.Cdate = DateTime.Now;
            iinfo.Date = "";//送货时间
            iinfo.DetailId = HjNetHelper.GetQueryInt("buy", 0);
            iinfo.GiftsId = Id;
            iinfo.PayIntegral = (Point * sumNumber).ToString();//所用积分
            iinfo.State = "0";//状态（0->未审核,1->审核通过,2->审核未通过,3->已发货）
            iinfo.GiftName = WebUtility.InputText(Request.Form["hGiftName"]);//礼品名称
            iinfo.Remark = "";//备注

            int num = 0;
            for (int i = 1; i <= sumNumber; i++)
            {
                if (new Integral().Add(iinfo) > 0)
                {
                    //msg += "第" + i + "个礼品兑换申请成功！";//window.location.href='../user/mychange.aspx'
                    num++;
                }
                else
                {
                    msg += "第" + i + "个礼品兑换申请失败！+";
                }
            }
            msg = string.Format("成功兑换{0}个礼品！", num) + msg;

        }



    }
}