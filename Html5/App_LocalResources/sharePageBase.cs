using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using tenpayApp;
using Hangjing.Weixin;

/// <summary>
/// html5 分享父类
/// </summary>
public class sharePageBase : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// appid
    /// </summary>
    public string appId = TenpayUtil.appid;

    /// <summary>
    /// 时间戳
    /// </summary>
    public string timeStamp = "";

    /// <summary>
    /// 随机数
    /// </summary>
    public string nonceStr = "";

    /// <summary>
    /// 签名
    /// </summary>
    public string signature = "";

    /// <summary>
    /// PageBase类构造函数
    /// </summary>
    public sharePageBase()
    {
        this.PreRender += new System.EventHandler(Page_PreRender);
    }

    /// <summary>
    /// 初始化，这个时间生成session对像
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!WebUtility.isWeiXin())
        {
            return;
        }

        timeStamp = TenpayUtil.getTimestamp();
        nonceStr = TenpayUtil.getNoncestr();

        string jsapi_ticket = new jsapi_ticket().getTicket(Context);
        string url = Request.Url.ToString();

        //设置分享签名参数，
        RequestHandler paySignReqHandler = new RequestHandler(Context);
        paySignReqHandler.init();

        paySignReqHandler.setParameter("timestamp", timeStamp);
        paySignReqHandler.setParameter("noncestr", nonceStr);
        paySignReqHandler.setParameter("jsapi_ticket", jsapi_ticket);
        paySignReqHandler.setParameter("url", url);

        signature = paySignReqHandler.createSHA1ShareSign();

      //  HJlog.toLog("timeStamp=" + timeStamp + "\r\nnonceStr=" + nonceStr + "\r\njsapi_ticket=" + jsapi_ticket + "\r\nurl=" + url + "\r\nsignature=" + signature + "\r\nsetDebugInfo=" + paySignReqHandler.getDebugInfo());
    }

    void Page_PreRender(object sender, EventArgs e)
    {

    }

}

