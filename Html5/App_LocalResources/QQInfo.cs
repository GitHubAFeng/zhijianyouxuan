using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///QQInfo 登录登录后返回信息实体
/// </summary>
public class QQInfo
{
    private int m_ret;
    private string m_msg;
    private string m_nickname;
    private string m_figureurl;
    private string m_figureurl_1;
    private string m_figureurl_2;
    private string m_openid;
    private string _gender;
    private int _vip;
    private int _level;

    /// <summary>
    /// vip
    /// </summary>
    public int vip
    {
        get
        {
            return _vip;
        }

        set
        {
            this._vip = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public int level
    {
        get
        {
            return this._level;
        }

        set
        {
            this._level = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public int ret
    {
        get
        {
            return this.m_ret;
        }

        set
        {
            this.m_ret = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public string msg
    {
        get
        {
            return this.m_msg;
        }

        set
        {
            this.m_msg = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public string nickname
    {
        get
        {
            return this.m_nickname;
        }

        set
        {
            this.m_nickname = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public string figureurl
    {
        get
        {
            return this.m_figureurl;
        }

        set
        {
            this.m_figureurl = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public string figureurl_1
    {
        get
        {
            return this.m_figureurl_1;
        }

        set
        {
            this.m_figureurl_1 = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public string figureurl_2
    {
        get
        {
            return this.m_figureurl_2;
        }

        set
        {
            this.m_figureurl_2 = value;
        }
    }

    /// <summary>
    /// 获取或设置
    /// </summary>
    public string Openid
    {
        get
        {
            return this.m_openid;
        }

        set
        {
            this.m_openid = value;
        }
    }

    /// <summary>
    /// 性别
    /// </summary>
    public string gender
    {
        get
        {
            return _gender;
        }

        set
        {
            this._gender = value;
        }
    }
    
}
