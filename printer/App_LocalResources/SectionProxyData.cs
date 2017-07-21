/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 区域缓存代理类
 * Created by jijunjian at 2010-8-21 18:53:19.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Caching;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Cache;

/// <summary>
///SectionProxyData 的摘要说明
/// </summary>
public class SectionProxyData
{
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
            EasyEatCache.GetCacheService().AddObject("/syspara", list);
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
