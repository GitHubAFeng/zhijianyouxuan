using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

using Com.Alipay;
using System.Text;
using Hangjing.Common;

namespace Html5
{
    public partial class OrderSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int isreg = Convert.ToInt32(Request["isreg"]);
                if (isreg == 1)
                {
                    string msg = "系统已经自动为您注册了帐号（手机号）：<span class=\"f24 orange\">" + Request["tel"] + "</span>，默认密码：<span class=\"f24 orange\">123456。</span>，请登录后及时修改";
                    regnotice.InnerHtml = msg;
                }
                else
                {
                    reghid.Visible = false;
                }
                IList<ROrderinfo> list = (IList<ROrderinfo>)Session["orderinfo"];
                if (list != null && list.Count > 0)
                {
                    string orders = "";
                    decimal p = 0;
                    decimal accountpay = 0;
                    decimal allprice = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        orders += "  " + list[i].Orderid;
                        p += list[i].Currentprice;
                        allprice += list[i].allprice;
                        accountpay += list[i].accountpay;
                    }

                    //只提交一家的订单
                    lborderid.InnerHtml = orders;
                    Custorder dalorder = new Custorder();
                    CustorderInfo infoorder = dalorder.GetModel(list[0].Orderid);
                    payway.InnerHtml = WebUtility.TurnPayModel(infoorder.paymode);
                    lbprice.InnerHtml = (allprice).ToString();

                    Points dal = new Points();
                    PointsInfo info = dal.GetModel(Convert.ToInt32(list[0].togoid));
                    sendtime.InnerHtml = info.senttime.ToString();

                    if (Request["m"] != null && Request["m"].ToString() == "1" && p > 0)
                    {
                        string show_url = WebUtility.GetConfigsite() + "/PaySuccess.aspx";
                        AliPayInfo alipa = new AliPayInfo(infoorder.orderid, "payorder", "支付", p.ToString(), show_url);
                        Pay(alipa);

                    }
                    //微信支付
                    if (Request["m"].ToString() == "5" && p > 0)
                    {
                        string url = "/wxpay.aspx?orderid=" + infoorder.orderid + "&showwxpaytitle=1";
                        url += "&price=" + p;

                        string ispaytest = WebUtility.GetConfigKey("ispaytest");
                        if (ispaytest == "1")
                        {
                            url = "/weixinpaytest/" + url;
                        }
                        else
                        {
                            url = "/weixinpay/" + url;
                        }

                        Response.Redirect(url);

                    }

                }


            }

        }

        /// <summary>
        /// 支付宝wap接口
        /// </summary>
        /// <param name="alipayinfo"></param>
        public void Pay(AliPayInfo alipayinfo)
        {
            //支付宝网关地址
            string GATEWAY_NEW = "http://wappaygw.alipay.com/service/rest.htm?";

            ////////////////////////////////////////////调用授权接口alipay.wap.trade.create.direct获取授权码token////////////////////////////////////////////

            //返回格式
            string format = "xml";
            //必填，不需要修改

            //返回格式
            string v = "2.0";
            //必填，不需要修改

            //请求号
            string req_id = DateTime.Now.ToString("yyyyMMddHHmmss");
            //必填，须保证每次请求都是唯一


            //服务器异步通知页面路径
            string notify_url = WebUtility.GetConfigsite() + "/Alipay/notify_url.aspx";
            //需http://格式的完整路径，不允许加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string call_back_url = WebUtility.GetConfigsite() + "/Alipay/wappayreturn.aspx";
            //需http://格式的完整路径，不允许加?id=123这类自定义参数

            //操作中断返回地址
            string merchant_url = WebUtility.GetConfigsite() + "/index.aspx";
            //用户付款中途退出返回商户的地址。需http://格式的完整路径，不允许加?id=123这类自定义参数

            //支付宝信息

            Hangjing.Model.acountInfo mymodel = CacheHelper.getOnlinePayType(1);
            if (mymodel == null)
            {
                return;
            }

            //卖家支付宝帐户
            string seller_email = mymodel.Ali_Seller_Mail;
            //必填

            //商户订单号
            string out_trade_no = alipayinfo.OutTradeNo;
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = alipayinfo.SubJect;
            //必填

            //付款金额
            string total_fee = alipayinfo.TotalFee;
            //必填

            //请求业务参数详细
            string req_dataToken = "<direct_trade_create_req><notify_url>" + notify_url + "</notify_url><call_back_url>" + call_back_url + "</call_back_url><seller_account_name>" + seller_email + "</seller_account_name><out_trade_no>" + out_trade_no + "</out_trade_no><subject>" + subject + "</subject><total_fee>" + total_fee + "</total_fee><merchant_url>" + merchant_url + "</merchant_url></direct_trade_create_req>";
            //必填

            //把请求参数打包成数组
            Dictionary<string, string> sParaTempToken = new Dictionary<string, string>();
            sParaTempToken.Add("partner", Config.Partner);
            sParaTempToken.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTempToken.Add("sec_id", Config.Sign_type.ToUpper());
            sParaTempToken.Add("service", "alipay.wap.trade.create.direct");
            sParaTempToken.Add("format", format);
            sParaTempToken.Add("v", v);
            sParaTempToken.Add("req_id", req_id);
            sParaTempToken.Add("req_data", req_dataToken);

            //建立请求
            string sHtmlTextToken = Submit.BuildRequest(GATEWAY_NEW, sParaTempToken);
            //URLDECODE返回的信息
            Encoding code = Encoding.GetEncoding(Config.Input_charset);
            sHtmlTextToken = HttpUtility.UrlDecode(sHtmlTextToken, code);

            //解析远程模拟提交后返回的信息
            Dictionary<string, string> dicHtmlTextToken = Submit.ParseResponse(sHtmlTextToken);

            //获取token
            string request_token = dicHtmlTextToken["request_token"];

            ////////////////////////////////////////////根据授权码token调用交易接口alipay.wap.auth.authAndExecute////////////////////////////////////////////


            //业务详细
            string req_data = "<auth_and_execute_req><request_token>" + request_token + "</request_token></auth_and_execute_req>";
            //必填

            //把请求参数打包成数组
            Dictionary<string, string> sParaTemp = new Dictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("sec_id", Config.Sign_type.ToUpper());
            sParaTemp.Add("service", "alipay.wap.auth.authAndExecute");
            sParaTemp.Add("format", format);
            sParaTemp.Add("v", v);
            sParaTemp.Add("req_data", req_data);

            //建立请求
            string sHtmlText = Submit.BuildRequest(GATEWAY_NEW, sParaTemp, "get", "确认");
            HJlog.toLog("===sHtmlText=" + sHtmlText);
            Response.Write(sHtmlText);
        }
    }
}
