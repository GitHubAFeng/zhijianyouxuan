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
using System.Collections;

/// <summary>
///NameService 的摘要说明
/// </summary>
public class NameService
{
    private ArrayList m_names;

    public NameService(ArrayList list_names)
    {
        this.m_names = list_names;
    }
    public static NameService getInstance(ArrayList list_names)
    {
        return new  NameService(list_names);
    }

    public ArrayList findNames(string prefix , int type)
    {
        string prefix_upper = "";
        if (type == 1)
        {
            prefix_upper = prefix.ToUpper();
        }
        prefix_upper = prefix;
        ArrayList maches = new ArrayList();
        foreach (string name in m_names)
        {
            string name_upper = "";
            if (type == 1)
            {
                name_upper = name.ToUpper();
            }
            name_upper = name;
            if (name_upper.StartsWith(prefix_upper))
            {
                maches.Add(name);
            }     
        }
        return maches;
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public ArrayList Names
    {
        get
        {
            return this.m_names;
        }

        set
        {
            this.m_names = value;
        }
    }
}
