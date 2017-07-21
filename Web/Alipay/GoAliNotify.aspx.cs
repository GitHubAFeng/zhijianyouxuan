using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Net;
using System.Collections.Generic;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Hangjing.WebCommon;

public partial class Alipay_GoAliNotify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hangjing.Model.acountInfo mymodel = SectionProxyData.getOnlinePayType(1);
        string alipayNotifyURL = "https://mapi.alipay.com/gateway.do?service=notify_verify";
        string key = mymodel.Ali_Key; //partner 的对应交易安全校验码（必须填写）
        string partner = mymodel.Ali_Partner; 		//partner合作伙伴id（必须填写）
        string _input_charset = "utf-8";//编码类型，完全根据客户自身的项目的编码格式而定，千万不要填错。否则极其容易造成MD5加密错误。

        alipayNotifyURL = alipayNotifyURL + "&partner=" + partner + "&notify_id=" + Request.Form["notify_id"];

        //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的
        string responseTxt = Get_Http(alipayNotifyURL, 120000);
        int i;
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.Form;

        // Get names of all forms into a string array.
        String[] requestarr = coll.AllKeys;

        //进行排序；
        string[] Sortedstr = AliPay.BubbleSort(requestarr);

        //构造待md5摘要字符串 ；
        string prestr = "";
        for (i = 0; i < Sortedstr.Length; i++)
        {
            if (Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type")
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr = prestr + Sortedstr[i] + "=" + Request.Form[Sortedstr[i]];
                }
                else
                {
                    prestr = prestr + Sortedstr[i] + "=" + Request.Form[Sortedstr[i]] + "&";
                }
            }
        }
        prestr = prestr + key;

        string mysign = AliPay.GetMD5(prestr, _input_charset);

        string sign = Request.Form["sign"];

        if (mysign == sign && responseTxt == "true")   //验证支付发过来的消息，签名是否正确，只要成功进如这个判断里，则表示该页面已被支付宝服务器成功调用
        //但判断内出现自身编写的程序相关错误导致通知给支付宝并不是发送success的消息的情况或没有更新客户自身的数据库，请自身程序编写好应对措施，否则查明原因时困难之极
        {
            if (Request.Form["trade_status"] == "WAIT_BUYER_PAY")//   判断支付状态_等待买家付款（文档中有枚举表可以参考）            
            {
                //更新自己数据库的订单语句，请自己填写一下
                string strOrderNO = Request.Form["out_trade_no"];//订单号
                string strPrice = Request.Form["total_fee"];//金额

                Response.Write("success");
            }
            else if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")//高级即时到帐状态下 TRADE_SUCCESS
            //判断支付状态_TRADE_FINISHED交易成功（文档中有枚举表可以参考）
            //如果trade_status的值是TRADE_SUCCESSED，是因为您用的测试帐号是返回的这个结果，改成您自己的支付宝帐号和PID以及KEY，则返回的值才是TRADE_FINISHED            
            {
                //更新到数据库的订单
                string strOrderNO = Request.Form["out_trade_no"];//订单号
                string strPrice = Request.Form["total_fee"];     //金额

                Hangjing.AppLog.AppLog.Info("支付里面日志订单编号：" + strOrderNO);

                onlinepayCallback paycallback = new onlinepayCallback(strOrderNO, Convert.ToDecimal(strPrice), (int)OrderPayModel.alipay);
                paycallback.Handle();


                Response.Write("success");
            }
            else
            {
                //更新自己数据库的订单语句，请自己填写一下
                string strOrderNO = Request.Form["out_trade_no"];//订单号
                string strPrice = Request.Form["total_fee"];//金额
                Response.Write("faild");
            }
        }
        else
        {
            Response.Write("fail");

            //最好写TXT文件，以记录下是否异步返回记录。

            //写文本，纪录支付宝返回消息，比对md5计算结果（如网站不支持写txt文件，可改成写数据库）
            string TOEXCELLR = "MD5结果:mysign=" + mysign + ",sign=" + sign + ",responseTxt=" + responseTxt;
            StreamWriter fs = new StreamWriter(Server.MapPath("Log/" + DateTime.Now.ToString().Replace(":", "")) + ".txt", false, System.Text.Encoding.Default);
            fs.Write(TOEXCELLR);
            fs.Close();
        }
    }

    //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
    public String Get_Http(String a_strUrl, int timeout)
    {
        ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
        string strResult;
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
            myReq.Timeout = timeout;
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.Default);
            StringBuilder strBuilder = new StringBuilder();
            while (-1 != sr.Peek())
            {
                strBuilder.Append(sr.ReadLine());
            }

            strResult = strBuilder.ToString();
        }
        catch (Exception exp)
        {

            strResult = "错误：" + exp.Message;
        }

        return strResult;
    }


    private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }


}
