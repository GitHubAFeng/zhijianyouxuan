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
using System.Xml.Linq;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;
// StartupTools.cs:
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-04-08

/// <summary>
///StartupTools 的摘要说明
/// </summary>
public static class StartupTools
{

    /// <summary>
    /// 程序启动 初始化程序
    /// </summary>
    public static void Startup()
    {
        Collectors.CacheProviderCollector = new ProviderCollector<ICacheProvider>();

        //1. 初始化缓存提供程序
        //默认的cache提供程序
        CacheProvider cache = new CacheProvider();
        cache.Init();
        Collectors.CacheProviderCollector.AddProvider(cache);
        
    }
}
