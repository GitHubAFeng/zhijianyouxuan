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
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 某一个程序提供者的收集器 包含一个list和相关的方法
/// </summary>
/// <typeparam name="T"></typeparam>
public class ProviderCollector<T>
{

    private List<T> list;

    public ProviderCollector()
    {
        list = new List<T>(3);
    }

    public void AddProvider(T provider)
    {
        lock (this)
        {
            list.Add(provider);
        }
    }

    public void RemoveProvider(T provider)
    {
        lock (this)
        {
            list.Remove(provider);
        }
    }

    public T[] AllProviders
    {
        get
        {
            lock (this)
            {
                return list.ToArray();
            }
        }
    }

    public T GetProvider()
    {
        lock (this)
        {
            return list[0];
        }
    }

}
