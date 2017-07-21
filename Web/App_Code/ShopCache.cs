using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using Hangjing.Cache;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

/// <summary>
///ShopCache 的摘要说明
/// </summary>
public class ShopCache
{
    private static object lockHelper = new object();

    /// <summary>
    /// 获得推荐商家列表 使用:ShopCache.GetRecommendShopCache()
    /// </summary>
    /// <returns>推荐商家列表</returns>
    public static string GetRecommendShopCache()
    {
        EasyEatCache cache = EasyEatCache.GetCacheService();//目前通过此方法调用默认的缓存处理,位于Cache->DefaultCacheStrategy.cs

        string str = cache.RetrieveObject("/Shop/RecommendShopIdList") as string;

        if (Utils.StrIsNullOrEmpty(str))
        {
            StringBuilder sb = new StringBuilder();
            
            //到数据库读取，并存入缓存中

            sb.Append("1,2,3,4,5,6,7,8,9,10");

            str = sb.ToString();

            cache.AddObject("/Shop/RecommendShopIdList", str);

        }
        return str;
    }

    //使用: SortedList<int, string> topicTypeList = Caches.GetTopicTypeArray();

    ///// <summary>
    ///// 获得禁止的ip列表
    ///// </summary>
    ///// <returns>禁止列表</returns>
    //public static List<IpInfo> GetBannedIpList()
    //{
    //    List<IpInfo> list = DNTCache.GetCacheService().RetrieveObject("/Forum/BannedIp") as List<IpInfo>;

    //    if (list == null)
    //    {
    //        list = Ips.GetBannedIpList();
    //        DNTCache.GetCacheService().AddObject("/Forum/BannedIp", list);
    //    }
    //    return list;
    //}

    ///// <summary>
    ///// 获得主题类型数组
    ///// </summary>
    ///// <returns>主题类型数组</returns>
    //public static Hangjing.Common.SortedList<int, string> GetTopicTypeArray()
    //{
    //    EasyEatCache cache = EasyEatCache.GetCacheService();
    //    Discuz.Common.Generic.SortedList<int, string> topictypeList;
    //    topictypeList = cache.RetrieveObject("/Forum/TopicTypes") as Discuz.Common.Generic.SortedList<int, string>;

    //    if (topictypeList == null)
    //    {
    //        topictypeList = new Discuz.Common.Generic.SortedList<int, string>();
    //        DataTable dt = DatabaseProvider.GetInstance().GetTopicTypeList();
    //        if (dt.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                if (!Utils.StrIsNullOrEmpty(dr["typeid"].ToString()) && !Utils.StrIsNullOrEmpty(dr["name"].ToString()))
    //                {
    //                    topictypeList.Add(TypeConverter.ObjectToInt(dr["typeid"]), dr["name"].ToString());
    //                }
    //            }
    //        }
    //        cache.AddObject("/Forum/TopicTypes", topictypeList);
    //    }
    //    return topictypeList;
    //}

    ///// <summary>
    ///// 获取自带头像列表
    ///// </summary>
    ///// <returns>自带头像列表</returns>
    //public static DataTable GetAvatarList()
    //{
    //    DNTCache cache = DNTCache.GetCacheService();
    //    DataTable dt = cache.RetrieveObject("/Forum/CommonAvatarList") as DataTable;
    //    if (dt == null)
    //    {
    //        dt = new DataTable();
    //        dt.Columns.Add("filename", Type.GetType("System.String"));

    //        DirectoryInfo dirinfo = new DirectoryInfo(Utils.GetMapPath(BaseConfigs.GetForumPath + "avatars/common/"));
    //        string extname = "";
    //        foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
    //        {
    //            if (file != null)
    //            {
    //                extname = file.Extension.ToLower();
    //                if (extname.Equals(".jpg") || extname.Equals(".gif") || extname.Equals(".png"))
    //                {
    //                    DataRow dr = dt.NewRow();
    //                    dr["filename"] = @"avatars/common/" + file.Name;
    //                    dt.Rows.Add(dr);
    //                }
    //            }
    //        }
    //        cache.AddObject("/Forum/CommonAvatarList", dt);
    //    }
    //    return dt;
    //}


}
