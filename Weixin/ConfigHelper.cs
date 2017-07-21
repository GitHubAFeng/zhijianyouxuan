using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Hangjing.Cache;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

namespace Hangjing.Weixin
{
    /// <summary>
    /// 此类用于获取配置信息
    /// </summary>
    public static class ConfigHelper
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static string GetConfigBackMsg()
        {
            string msg = "";
            WeiXinAccountInfo weixin = new WeiXinAccount().GetModel(1);
            if (weixin.revevar1 != "")
            {
                msg = weixin.revevar1;
            }
            return msg.Trim();
        }

        /// <summary>
        /// 返回订单状态的中文显示
        /// 状态(1新增订单  2下单成功  3已经调度(已经删除此状态)  4 正在配送  5处理成功  0已经取消)
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public static string TurnOrderState(object _State)
        {
            string ret = "";
            switch (Convert.ToInt32(_State))
            {
                case -1:
                    ret = "<font color='red'>客服通知</font>"; break;
                case 0:
                    ret = "等待审核"; break;
                case 1:
                    ret = "未审核"; break;
                case 2:
                    ret = "审核通过"; break;
                case 3:
                    ret = "处理成功"; break;
                case 4:
                    ret = "处理失败"; break;
                case 5:
                    ret = "订单取消"; break;
                case 6:
                    ret = "订单失效"; break;
                case 7:
                    ret = "已经调度"; break;

            }
            return ret;
        }

        /// <summary>
        /// 缓存系统参数
        /// </summary>
        /// <returns></returns>
        public static string GetSetValue(int id)
        {
            string rs = "";
            IList<WebBasicInfo> list = (IList<WebBasicInfo>)EasyEatCache.GetCacheService().RetrieveObject("/syspara");
            if (list == null || list.Count == 0)
            {
                WebBasic dal = new WebBasic();
                list = dal.GetAllData("");
                EasyEatCache.GetCacheService().AddObject("/syspara", list, 600);
            }
            foreach (WebBasicInfo i in list)
            {
                if (i.DataId == id)
                {
                    rs = i.Value;
                }
            }
            return rs;
        }

    }
}
