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

/// <summary>
/// 功能提供程序的收集器
/// </summary>
public static class Collectors
{
    /// <summary>
    /// The Cache Provider Collector instance.
    /// </summary>
    public static ProviderCollector<ICacheProvider> CacheProviderCollector;
}
