using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System.Data;
using System.Web;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 地图位置信息处理
    /// </summary>
    public class LocationHandler : BaseHandler
    {
        protected location model = null;
        public LocationHandler(BaseNotice _notice)
            : base(_notice)
        {

        }

        public override string HandleNotice(HttpContext context)
        {
            int count = 0;
            Points dal = new Points();
            model = (location)base.notice;

            string domain = CacheHelper.GetWeiXinAccount().revevar2;
           
            StringBuilder backmsg = new StringBuilder();
            HJlog.toLog("没转化过的坐标经度" + model.Location_X + "纬度" + model.Location_Y);
            LOCATION();
            HJlog.toLog("转化过的坐标经度" + model.Location_X + "纬度" + model.Location_Y);
            string url = domain + "/waimaijie.aspx?openid=" + model.FromUserName + "&lat=" + model.Location_X + "&lng=" + model.Location_Y + "&address=" + context.Server.UrlEncode(model.Label);
            string Sql = "";
            string strDistance = " distance <= Inve1 ";
            string sortword = " Status desc, havenew desc,sortnum";

            HJlog.toLog("url:" + url);
            if (model.Location_X == "" || model.Location_X == null)
            {
                model.Location_X = "0";
                model.Location_Y = "0";
            }
            else
            {
                Sql += "  1=1 and IsDelete = 0 and Star = 1 and InUse = 'Y'  ";
            }

            count = dal.GetCountWidthDistance(Sql, sortword, 1, model.Location_X, model.Location_Y, strDistance);

            //这里的图片用广告位的
            AdTableInfo model_ad = new AdTable().GetModel(3);
            string imgurl = model_ad.AdImageAdrees.Replace("~", CacheHelper.GetSetValue(1));

            backmsg.Append("<xml>");
            backmsg.Append("<ToUserName><![CDATA[" + model.FromUserName + "]]></ToUserName>");
            backmsg.Append("<FromUserName><![CDATA[" + model.ToUserName + "]]></FromUserName>");
            backmsg.Append("<CreateTime>" + DateTime.Now.Ticks + "</CreateTime>");
            backmsg.Append(" <MsgType><![CDATA[news]]></MsgType>");
            backmsg.Append(" <ArticleCount>" + (1) + "</ArticleCount>");
            backmsg.Append(" <Articles>");
            backmsg.Append(" <item>");
            backmsg.Append(" <Title><![CDATA[您附近有" + count + "个商家—点击查看]]></Title> ");
            backmsg.Append(" <Description><![CDATA[]]></Description>");
            backmsg.Append(" <PicUrl><![CDATA[" + imgurl + "]]></PicUrl>");
            backmsg.Append(" <Url><![CDATA[" + url + "]]></Url>");
            backmsg.Append(" </item>");


            HJlog.toLog(backmsg.ToString());

            backmsg.Append(" </Articles>");
            backmsg.Append(" </xml> ");

            return backmsg.ToString();
        }
        /// <summary>
        /// 获取用户地理位置   http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E7%94%A8%E6%88%B7%E5%9C%B0%E7%90%86%E4%BD%8D%E7%BD%AE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void LOCATION()
        {
            WeixinUserLocation dat = new WeixinUserLocation();

            string[] bdlatlng = Hangjing.Common.HjNetHelper.bd_encrypt(Convert.ToDouble(model.Location_X), Convert.ToDouble(model.Location_Y));
            model.Location_X = bdlatlng[0];
            model.Location_Y = bdlatlng[1];

            dat.Add(model.FromUserName, model.Location_X, model.Location_Y);
        }

    }
}
