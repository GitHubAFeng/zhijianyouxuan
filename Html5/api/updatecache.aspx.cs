using Hangjing.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tenpayApp;

namespace Html5
{
    /// <summary>
    /// 根据key更新缓存（主站调用），曲线实现子站与主站红缓存同步
    /// </summary>
    public partial class updatecache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request["key"];
            if (key != null && key != "")
            {
                Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/"+key);
                if ("WeiXinAccount" == key)
                {
                    TenpayUtil s = new TenpayUtil(); //更新支付配置。
                }
            }
        }
    }
}