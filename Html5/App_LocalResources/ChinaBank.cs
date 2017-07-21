using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
///ChinaBank 的摘要说明
/// </summary>
public class ChinaBank
{
    private static readonly string V_MID = System.Configuration.ConfigurationManager.AppSettings["v_mid"];
    private static readonly string KEY = System.Configuration.ConfigurationManager.AppSettings["chinabankkey"];

    public static string Pay(ChinaBankInfo payinfo)
    {
        string text = payinfo.Money + payinfo.MoneyType + payinfo.OrderId + V_MID + payinfo.ShowUrl + KEY; // 拼凑加密串 
        string v_md5info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5").ToUpper();

        //跳转到支付页
        //HttpContext.Current.Response.Redirect(pay_url);

        StringBuilder sbHtml = new StringBuilder();

        sbHtml.Append("<form action='https://pay3.chinabank.com.cn/PayGate' method='post' name='E_FORM'>");

        //<input type="hidden" name="v_md5info" value="<%=v_md5info%>" size="100" />
        //<input type="hidden" name="v_mid" value="<%=v_mid%>" />
        //<input type="hidden" name="v_oid" value="<%=v_oid%>" />
        //<input type="hidden" name="v_amount" value="<%=v_amount%>" />
        //<input type="hidden" name="v_moneytype" value="<%=v_moneytype%>" />
        //<input type="hidden" name="v_url" value="<%=v_url%>" />
        //<!--以下几项项为网上支付完成后，随支付反馈信息一同传给信息接收页-->
        //<input type="hidden" name="remark1" value="<%=remark1%>" />
        //<input type="hidden" name="remark2" value="<%=remark2%>" />

        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("v_md5info", v_md5info);
        sParaTemp.Add("v_mid", V_MID);
        sParaTemp.Add("v_oid", payinfo.OrderId);
        sParaTemp.Add("v_amount", payinfo.Money);
        sParaTemp.Add("v_moneytype", payinfo.MoneyType);
        sParaTemp.Add("v_url", payinfo.ShowUrl);
        sParaTemp.Add("remark1", "");
        sParaTemp.Add("remark2", "");

        foreach (KeyValuePair<string, string> temp in sParaTemp)
        {
            sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
        }

        //submit按钮控件请不要含有name属性
        //sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
        //document.E_FORM.submit()
        sbHtml.Append("<script>document.E_FORM.submit();</script>");
        //Response.Write(sHtmlText);
        return sbHtml.ToString();

    }
}
