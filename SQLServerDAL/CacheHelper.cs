/*********************************************************************
 * CopyRight (c) 2009-2014 HangJing Teconology. All Rights Reserved.
 * FileName : 
 * Function : 
 * Created by jijunjian at 2014-08-12 21:08:44.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Cache;
using System.Net;
using System.IO;

namespace Hangjing.SQLServerDAL
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 缓存充值优惠
        /// </summary>
        /// <returns></returns>
        public static IList<ordersourcesInfo> GetOrderSourceList()
        {
            ordersources dal = new ordersources();
            IList<ordersourcesInfo> list = (IList<ordersourcesInfo>)EasyEatCache.GetCacheService().RetrieveObject("/OrderSourceList");
            if (list == null || list.Count == 0)
            {
                list = dal.GetsubList(0);
                EasyEatCache.GetCacheService().AddObject("/OrderSourceList", list);
            }
            return list;
        }

        /// <summary>
        /// 清除缓存缓存充值优惠
        /// </summary>
        public static void OrderSourceListClear()
        {
            EasyEatCache.GetCacheService().RemoveObject("/OrderSourceList");
            CacheHelper.UpdateCacheByKey("OrderSourceList");
        }


        /// <summary>
        /// 缓存分销配置
        /// </summary>
        /// <returns></returns>
        public static IList<distributeRatioInfo> GetDistributeRatConfigs()
        {
            distributeRatio dal = new distributeRatio();
            IList<distributeRatioInfo> list = (IList<distributeRatioInfo>)EasyEatCache.GetCacheService().RetrieveObject("/DistributeRatConfigs");
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(100, 1, "", "drid", 1);
                EasyEatCache.GetCacheService().AddObject("/DistributeRatConfigs", list);
            }
            return list;
        }

        /// <summary>
        /// 清除缓存分销配置
        /// </summary>
        public static void ClearDistributeRatConfigs()
        {
            EasyEatCache.GetCacheService().RemoveObject("/DistributeRatConfigs");
            CacheHelper.UpdateCacheByKey("syspara");
        }

        /// <summary>
        /// 缓存群组管理
        /// </summary>
        /// <returns></returns>
        public static IList<DeliverInfo> GetDeliverList()
        {
            Deliver dal = new Deliver();
            IList<DeliverInfo> list = (IList<DeliverInfo>)EasyEatCache.GetCacheService().RetrieveObject("/DeliverInfo");
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(1000, 1, "IsWorking = 1", "dataid", 1);
                EasyEatCache.GetCacheService().AddObject("/DeliverInfo", list);
            }
            return list;
        }


        /// <summary>
        /// 缓存平台促销
        /// </summary>
        /// <returns></returns>
        public static IList<webPromotionConfigInfo> GetWebPromotionConfig()
        {
            webPromotionConfig dal = new webPromotionConfig();
            IList<webPromotionConfigInfo> list = (IList<webPromotionConfigInfo>)EasyEatCache.GetCacheService().RetrieveObject("/WebPromotionConfig");
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(1000, 1, "shopid=0 and isopen = 1 and startdate <= '"+DateTime.Now.ToString("yyyy-MM-dd")+ "' AND enddate >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "'", "pid", 1,0);
                EasyEatCache.GetCacheService().AddObject("/WebPromotionConfig", list);
            }
            return list;
        }

        /// <summary>
        /// 清除缓存平台促销
        /// </summary>
        public static void ClearWebPromotionConfig()
        {
            UpdateCacheByKey("WebPromotionConfig");
            EasyEatCache.GetCacheService().RemoveObject("/WebPromotionConfig");
        }


        /// <summary>
        /// 缓存支付方式
        /// </summary>
        /// <returns></returns>
        public static IList<StateConfigInfo> GetPayModelList()
        {
            StateConfig dal = new StateConfig();
            IList<StateConfigInfo> list = (IList<StateConfigInfo>)EasyEatCache.GetCacheService().RetrieveObject("/PayModelList");
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(999, 1, "isdel=0 and Parentid =" + Constant.PaymentMethodPrrentID, "Priority", 1);
                EasyEatCache.GetCacheService().AddObject("/PayModelList", list);
            }
            return list;
        }

        /// <summary>
        /// 缓存订单状态
        /// </summary>
        /// <returns></returns>
        public static IList<StateConfigInfo> GetOrderState()
        {
            return GetOrderBaseData().Where(a => a.Parentid == 1).ToList();
        }

        /// <summary>
        /// 缓存一些基础数据（订单状态，就餐方式，支付方式，订单来源（用户来源）等）
        /// 注：这个表中的数据含中英文。
        /// </summary>
        /// <returns></returns>
        public static IList<StateConfigInfo> GetOrderBaseData()
        {
            StateConfig dal = new StateConfig();
            IList<StateConfigInfo> list = (IList<StateConfigInfo>)EasyEatCache.GetCacheService().RetrieveObject("/StateConfig");
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(999, 1, "isdel=0", "Priority", 1);
                //EasyEatCache.GetCacheService().AddObject("/StateConfig", list);
            }
            return list;
        }

        /// <summary>
        /// 根据key更新缓存（主要用来更新同步微信等站点）
        /// </summary>
        /// <param name="key"></param>
        public static void UpdateCacheByKey(string key)
        {
            //todo:这里本应该用数据库保存各子系统
            List<string> subsystems = new List<string>();
            string apipage = "/api/updatecache.aspx";

            subsystems.Add(GetWeiXinAccount().revevar2 + apipage + "?key=" + key);

            foreach (var item in subsystems)
            {
                Query(item);
            }

        }


        /// <summary>
        /// 公共的HTTP查询方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Query(string url)
        {
            if (url.IndexOf("localhost") >= 0)
            {
                return "";
            }
            string data = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();

                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding)))
                {
                    data = reader.ReadToEnd();
                    response.Close();
                }

            }
            catch (Exception ex)
            {
                HJlog.toLog("远程服务器出错：url=" + url + "\r\n error:" + ex);
                data = "";
            }

            return data;

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

        /// <summary>
        /// 缓存网站城市
        /// </summary>
        /// <returns></returns>
        public static IList<CityInfo> GetCityList()
        {
            City dal = new City();
            IList<CityInfo> list = (IList<CityInfo>)EasyEatCache.GetCacheService().RetrieveObject("/city");
            if (list == null || list.Count == 0)
            {
                list = dal.GetAllCity();
                EasyEatCache.GetCacheService().AddObject("/city", list);
            }
            return list;
        }

        /// <summary>
        /// 缓存商家标签
        /// </summary>
        /// <returns></returns>
        public static IList<ShopFoodPictureInfo> GetShopPicTag()
        {
            ShopFoodPicture dal = new ShopFoodPicture();
            IList<ShopFoodPictureInfo> list = (IList<ShopFoodPictureInfo>)EasyEatCache.GetCacheService().RetrieveObject("/ShopPicTag");
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(1000, 1, "1=1", "IID", 1);
                foreach (var item in list)
                {
                    item.togoname = "<img src='" + item.Picture.Replace("~", "") + "' /> " + item.Title;
                }
                EasyEatCache.GetCacheService().AddObject("/ShopPicTag", list);
            }
            return list;
        }

        /// <summary>
        /// 清除缓存商家标签
        /// </summary>
        public static void ClearShopPicTag()
        {
            EasyEatCache.GetCacheService().RemoveObject("/ShopPicTag");
        }

        /// <summary>
        /// 缓存支付信息
        /// </summary>
        /// <returns></returns>
        public static IList<acountInfo> GetOnlinePayTypeList()
        {
            acount dal = new acount();
            IList<acountInfo> list = (IList<acountInfo>)EasyEatCache.GetCacheService().RetrieveObject("/OnlinePayTypeList");
            if (list == null || list.Count == 0)
            {
                list = dal.GetList(10, 1, "Reve1 = '0'", "dataid", 1);
                EasyEatCache.GetCacheService().AddObject("/OnlinePayTypeList", list);
            }
            return list;
        }

        /// <summary>
        /// 获取一个在线支付方式信息
        /// </summary>
        /// <param name="dataid"></param>
        /// <returns></returns>
        public static acountInfo getOnlinePayType(int dataid)
        {
            acountInfo model = null;
            IList<acountInfo> list = GetOnlinePayTypeList();
            foreach (acountInfo item in list)
            {
                if (item.DataID == dataid)
                {
                    return item;
                    break;
                }
            }
            return model;
        }

        /// <summary>
        /// 缓存微信配置
        /// </summary>
        /// <returns></returns>
        public static WeiXinAccountInfo GetWeiXinAccount()
        {
            WeiXinAccountInfo account = (WeiXinAccountInfo)EasyEatCache.GetCacheService().RetrieveObject("/WeiXinAccount");
            if (account == null)
            {
                account = new WeiXinAccount().GetModel(1);
                EasyEatCache.GetCacheService().AddObject("/WeiXinAccount", account);
            }
            return account;
        }

        /// <summary>
        /// 缓存自动调度配置
        /// </summary>
        /// <returns></returns>
        public static IList<autodispatchconfigInfo> Getautodispatchconfig()
        {
            IList<autodispatchconfigInfo> account = (IList<autodispatchconfigInfo>)EasyEatCache.GetCacheService().RetrieveObject("/Autodispatchconfiglist");
            if (account == null)
            {
                account = new autodispatchconfig().GetList(1000, 1, "1=1", "dataid", 1);
                EasyEatCache.GetCacheService().AddObject("/Autodispatchconfiglist", account);
            }
            return account;
        }

        /// <summary>
        /// 缓存建筑物
        /// </summary>
        /// <returns></returns>
        public static IList<BuildingInfo> GetBuildingList()
        {
            EBuilding dal = new EBuilding();
            IList<BuildingInfo> list = (IList<BuildingInfo>)EasyEatCache.GetCacheService().RetrieveObject("/BuildingList");
            if (list == null || list.Count == 0)
            {
                list = dal.GetAll();
                EasyEatCache.GetCacheService().AddObject("/BuildingList", list);
            }
            return list;
        }

        /// <summary>
        /// 清除缓存pp
        /// </summary>
        public static void ClearBuildingListList()
        {
            EasyEatCache.GetCacheService().RemoveObject("/BuildingList");
        }

    }
}
