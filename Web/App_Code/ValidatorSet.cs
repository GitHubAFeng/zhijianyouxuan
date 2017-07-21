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
using System.Text;
using System.IO;

//zheng_jianfeng 
//2009-08-29 
//zjf@ihangjing.com
//验证控件提示信息气泡显示设置,改编自网络

/// <summary>
///ValidatorSet 的摘要说明
/// </summary>
public class ValidatorSet
{
    private bool m_isAutoClose = false;        //自动关闭
    private bool m_isfilterClose = true;       //是否淡出效果关闭
    private int m_closeTime = 2500;            //关闭等待时间,单位ms
    private string m_filesFolder = "HomeDemo"; //带参构造函数
    private bool m_isValidatorSet = true;      //是否验证

    public ValidatorSet(string filesFolder)
    {
        m_filesFolder = filesFolder;
    }

    public ValidatorSet(string filesFolder, bool isValidatorSet)
    {
        m_filesFolder = filesFolder;
        m_isValidatorSet = isValidatorSet;
    }

    /// <summary>
    /// 是否自动关闭
    /// </summary>
    public bool IsAutoClose
    {
        set
        {
            m_isAutoClose = value;
        }
        get
        {
            return m_isAutoClose;
        }
    }

    /// <summary>
    /// 是否渐变关闭
    /// </summary>
    public bool IsFilterClose
    {
        set
        {
            m_isfilterClose = value;
        }
        get
        {
            return m_isfilterClose;
        }
    }

    /// <summary>
    /// 自动关闭等待的时间
    /// </summary>
    public int CloseTime
    {
        set
        {
            m_closeTime = value;
        }
        get
        {
            return m_closeTime;
        }
    }


    /// <summary>
    /// 控件资源文件夹的名称
    /// </summary>
    public string FilesFolder
    {
        set
        {
            m_filesFolder = value;
        }
        get
        {
            return m_filesFolder;
        }
    }

    /// <summary>
    /// 当前控件的版本号
    /// </summary>
    public string Version
    {
        get
        {
            return "ihangjing version 2.0";
        }
    }

    /// <summary>
    /// 设置气泡验证控件，设置完之后才起作用
    /// </summary>
    public void SetValidator()
    {
        if (m_isValidatorSet)
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath != "/")
            {
                applicationPath = applicationPath + "/";
            }

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(applicationPath + m_filesFolder)))
            {
                ValidatorDeal.JsAlert("加载“" + m_filesFolder + "”文件夹失败，请确定文件夹是否存在。", false);
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("\n");
            sb.Append("var __Win__IsAutoClose = " + m_isAutoClose.ToString().ToLower() + ";\n");
            sb.Append("var __Win__IsFilterClose = " + m_isfilterClose.ToString().ToLower() + ";\n");
            sb.Append("var __Win__CloseWaitTime = " + m_closeTime.ToString().ToLower() + ";\n");
            sb.Append("\n");
            ValidatorDeal.ExecuteJs(sb.ToString(), false);

            ValidatorInfo info = new ValidatorInfo();
            info.TipFolder = "css";
            info.TipCss = "Validator.css";
            info.TipFolder = "JavaScript";
            info.TipJs = "ValidatorLib.js";

            ValidatorDeal.IncludeCssFile(applicationPath + m_filesFolder + "/css/" + info.TipCss);
            ValidatorDeal.IncludeJsFile(applicationPath + m_filesFolder + "/" + info.TipFolder + "/" + info.TipJs, false);
            
        }
    }
}