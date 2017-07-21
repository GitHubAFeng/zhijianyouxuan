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

//zheng_jianfeng 
//2009-08-29 
//zjf@ihangjing.com
//验证控件提示信息气泡实体类

/// <summary>
///ValidatorInfo 的摘要说明
/// </summary>
public class ValidatorInfo
{
    private string m_tipname;
    private string m_tipfolder;
    private string m_tipcss;
    private string m_tipjs;

    public string TipName
    {
        set
        {
            m_tipname = value;
        }
        get
        {
            return m_tipname;
        }
    }

    public string TipFolder
    {
        set
        {
            m_tipfolder = value;
        }
        get
        {
            return m_tipfolder;
        }
    }

    public string TipCss
    {
        set
        {
            m_tipcss = value;
        }
        get
        {
            return m_tipcss;
        }
    }

    public string TipJs
    {
        set
        {
            m_tipjs = value;
        }
        get
        {
            return m_tipjs;
        }
    }
}
