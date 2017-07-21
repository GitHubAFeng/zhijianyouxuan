#region license
/*****************************************
*CopyRight (c) 2009-2013 HangJing Teconology. All Rights Reserved.
*Function :
*Created by jijunjian at 2013/9/16 22:21:13.
*E-Mail: jijunjian@ihangjing.com
*****************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Newtonsoft.Json;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 用户定义菜单
    /// </summary>
    public class UserDefinedMenu
    {
        protected HttpContext context;
        protected string accesstoken = "";

        public UserDefinedMenu(HttpContext _context)
        {
            context = _context;

            accesstoken = new ACCESSTOKEN(context).getAccessTokern();
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        public string create()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token="+accesstoken;

            WeiXinAccountInfo weixinaccount = CacheHelper.GetWeiXinAccount();
            string domain = weixinaccount.revevar2;

            /**********************************************************************
             *创建菜单新实现方式 
             *第一个是只有单个菜单的
             *第二个是有子菜单的情况
             *其中 click 表示点击事件。参数为事件名称 
             * view 表示链接，参数为url
             * 其他类型为相关指定参数即可
             * 
             * *****************************************************************/


            //WeixinMenuInfo productInfo = new WeixinMenuInfo("开始订餐", ButtonType.location_select, "location_select");
            WeixinMenuInfo productInfo = new WeixinMenuInfo("开始订餐", ButtonType.view, domain + "/TogoList.aspx");
            WeixinMenuInfo frameworkInfo = new WeixinMenuInfo("我的订单", new WeixinMenuInfo[] { 
                new WeixinMenuInfo("今日订单", ButtonType.click, "cjfan_4"),
                new WeixinMenuInfo("所有订单", ButtonType.view,domain + "/myorderlist.aspx"),
            });
            WeixinMenuInfo relatedInfo = new WeixinMenuInfo("会员中心", new WeixinMenuInfo[] { 
                new WeixinMenuInfo("个人中心", ButtonType.click, "userCenter"),
                 //new WeixinMenuInfo("app下载", ButtonType.view, domain + "/app.aspx"),
                new WeixinMenuInfo("我的积分", ButtonType.click, "cjfan_6"),
                new WeixinMenuInfo("我的余额", ButtonType.click, "cjfan_5"),
                new WeixinMenuInfo("积分商城", ButtonType.view,  domain + "/GiftList.aspx"),
                new WeixinMenuInfo("收货地址薄", ButtonType.view,  domain + "/myAddresslist.aspx"),
            });

            MenuJson menuJson = new MenuJson();
            menuJson.button.AddRange(new WeixinMenuInfo[] { productInfo, frameworkInfo, relatedInfo });

            string postdata = JsonConvert.SerializeObject(menuJson);

            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "POST",
                UserAgent = context.Request.UserAgent,
                Postdata = postdata,
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.Common.HJlog.toLog("returnmsg=" + returnmsg + " postdata=" + postdata);

            return returnmsg;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        public string deletemenu()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + accesstoken;

            HttpHelper objhttp = new HttpHelper();
            objhttp.isToLower = false;
            HttpItem objHttpItem = new HttpItem()
            {
                URL = url,
                Encoding = "utf-8",
                Method = "GET",
                UserAgent = context.Request.UserAgent,
            };
            string returnmsg = objhttp.GetHtml(objHttpItem);

            Hangjing.Common.HJlog.toLog("菜单删除:returnmsg=" + returnmsg);

            return returnmsg;
        }
    }
}
