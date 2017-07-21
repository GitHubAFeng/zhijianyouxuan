using System.Web;
using System.Text;
using System.Security.Cryptography;
/// <summary>
/// New Interface for AliPay
/// </summary>
public class AliPay
{
    private static readonly string ALI_SELLER_MAIL = System.Configuration.ConfigurationManager.AppSettings["Ali_Seller_Mail"];
    private static readonly string ALI_KEY = System.Configuration.ConfigurationManager.AppSettings["Ali_Key"];
    private static readonly string ALI_PARTNER = System.Configuration.ConfigurationManager.AppSettings["Ali_Partner"];

    private static readonly string ALI_NOTIFY_URL = WebUtility.GetConfigsite()+"/Alipay/GoAliNotify.aspx";//测试
    private static readonly string ALI_RETURN_URL = WebUtility.GetConfigsite()+"/Alipay/AliReturn.aspx";//测试
    
    public static void Pay(AliPayInfo alipayinfo)
    {
        //业务参数赋值；
        string gateway = "https://mapi.alipay.com/gateway.do?";	//支付接口
        string service = "create_direct_pay_by_user";                       //服务名称，这个是识别是何接口实现何功能的标识，请勿修改

        string seller_email = ALI_SELLER_MAIL;                              //商家签约时的支付宝帐号，即收款的支付宝帐号
        string sign_type = "MD5";                                           //加密类型,签名方式“不用改”
        string key = ALI_KEY;                                               //安全校验码，与partner是一组，获取方式是：用签约时支付宝帐号登陆支付宝网站www.alipay.com，在商家服务我的商家里即可查到。
        string partner = ALI_PARTNER;                                       //商户ID，合作身份者ID，合作伙伴ID
        string _input_charset = "utf-8";                                    //编码类型，完全根据客户自身的项目的编码格式而定，千万不要填错。否则极其容易造成MD5加密错误。

        string show_url = alipayinfo.ShowUrl;                               //展示地址，即在支付页面时，商品名称的链接地址。

        string out_trade_no = alipayinfo.OutTradeNo;                        //客户自己的订单号，（现取系统时间，可改成网站自己的变量），订单号必须在自身订单系统中保持唯一性
        string subject = alipayinfo.SubJect;                                //商品名称，也可称为订单名称，该接口并不是单一的只能买一样东西，可把一次支付当作一次下订单
        string body = alipayinfo.Body;                                      //商品描述，即备注
        string total_fee = alipayinfo.TotalFee;                             //商品价格，也可称为订单的总金额

        //服务器通知url（Alipay_Notify.asp文件所在路经），必须是完整的路径地址
        string notify_url = ALI_NOTIFY_URL;
        //服务器返回url（return_Alipay_Notify.asp文件所在路经），必须是完整的路径地址
        string return_url = ALI_RETURN_URL;

        //支付URL生成
        string aliay_url = AliPay.CreatUrl(
            gateway,
            service,
            partner,
            sign_type,
            out_trade_no,
            subject,
            body,
            total_fee,
            show_url,
            seller_email,
            key,
            return_url,
            _input_charset,
            notify_url
            );

        //跳转到支付宝支付页
        HttpContext.Current.Response.Redirect(aliay_url);
    }

    /// <summary>
    /// 与ASP兼容的MD5加密算法
    /// </summary>
    public static string GetMD5(string s, string _input_charset)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 冒泡排序法
    /// 按照字母序列从a到z的顺序排列
    /// </summary>
    public static string[] BubbleSort(string[] r)
    {
        int i, j; //交换标志 
        string temp;

        bool exchange;

        for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
        {
            exchange = false; //本趟排序开始前，交换标志应为假

            for (j = r.Length - 2; j >= i; j--)
            {
                if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                {
                    temp = r[j + 1];
                    r[j + 1] = r[j];
                    r[j] = temp;

                    exchange = true; //发生了交换，故将交换标志置为真 
                }
            }

            if (!exchange) //本趟排序未发生交换，提前终止算法 
            {
                break;
            }
        }
        return r;
    }

    /// <summary>
    /// URL链接生成
    /// </summary>
    /// <param name="gateway">网关</param>
    /// <param name="service">服务参数</param>
    /// <param name="partner">合作伙伴ID</param>
    /// <param name="sign_type">加密类型</param>
    /// <param name="out_trade_no">订单号</param>
    /// <param name="subject">商品名称或订单名称</param>
    /// <param name="body">描述与备注</param>
    /// <param name="total_fee">总金额</param>
    /// <param name="show_url">展示地址，“详情”的链接地址</param>
    /// <param name="seller_email">商家支付宝帐号，收款人支付宝帐号</param>
    /// <param name="key">安全校验码</param>
    /// <param name="return_url">跳转返回页</param>
    /// <param name="_input_charset">编码格式</param>
    /// <param name="notify_url">请求通知页</param>
    /// <returns>生成的URL链接字符串</returns>
    public static string CreatUrl(
        string gateway,
        string service,
        string partner,
        string sign_type,
        string out_trade_no,
        string subject,
        string body,
        string total_fee,
        string show_url,
        string seller_email,
        string key,
        string return_url,
        string _input_charset,
        string notify_url
        )
    {
        /// <summary>
        /// created by sunzhizhi 2006.5.21,sunzhizhi@msn.com。
        /// </summary>
        int i;

        //构造数组；
        //以下数组即是参与加密的参数，若参数的值不允许为空，若该参数为空，则不要成为该数组的元素
        string[] Oristr ={ 
                "service="+service, 
                "partner=" + partner, 
                "subject=" + subject, 
                "body=" + body, 
                "out_trade_no=" + out_trade_no, 
                "total_fee=" + total_fee, 
                "show_url=" + show_url,  
                "payment_type=1", 
                "seller_email=" + seller_email, 
                "notify_url=" + notify_url,
                "_input_charset="+_input_charset,          
                "return_url=" + return_url
                };

        //进行排序；
        string[] Sortedstr = BubbleSort(Oristr);


        //构造待md5摘要字符串 ；

        StringBuilder prestr = new StringBuilder();

        for (i = 0; i < Sortedstr.Length; i++)
        {
            if (i == Sortedstr.Length - 1)
            {
                prestr.Append(Sortedstr[i]);
            }
            else
            {
                prestr.Append(Sortedstr[i] + "&");
            }
        }

        prestr.Append(key);

        //生成Md5摘要；
        string sign = GetMD5(prestr.ToString(), _input_charset);

        //构造支付Url；
        StringBuilder parameter = new StringBuilder();
        parameter.Append(gateway);
        for (i = 0; i < Sortedstr.Length; i++)
        {
            parameter.Append(Sortedstr[i] + "&");
        }

        parameter.Append("sign=" + sign + "&sign_type=" + sign_type);

        //返回支付Url；
        return parameter.ToString();

    }

}