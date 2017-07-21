using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///RenrenInfo 登录登录后返回信息实体
/// </summary>
[Serializable]
public class RenrenInfo
{
    private int _uid;
    private string _tinyurl;
    private int _vip;
    private int _sex;
    private string _name;
    private int _star;
    private string _headurl;
    private int _zidou;

    /// <summary>
    /// 会员编号
    /// </summary>
    public int uid
    {
        set { _uid = value; }
        get { return _uid; }
    }
    /// <summary>
    /// 相片
    /// </summary>
    public string tinyurl
    {
        set { _tinyurl = value; }
        get { return _tinyurl; }
    }

    /// <summary>
    /// 未用
    /// </summary>
    public int vip
    {
        set { _vip = value; }
        get { return _vip; }
    }
    /// <summary>
    /// 性别 1是男
    /// </summary>
    public int sex
    {
        set { _sex = value; }
        get { return _sex; }
    }

    /// <summary>
    /// 姓名
    /// </summary>
    public string name
    {
        set { _name = value; }
        get { return _name; }
    }
    /// <summary>
    /// 星级
    /// </summary>
    public int star
    {
        set { _star = value; }
        get { return _star; }
    }

    /// <summary>
    /// 头像
    /// </summary>
    public string headurl
    {
        set { _headurl = value; }
        get { return _headurl; }
    }
    /// <summary>
    /// 未用
    /// </summary>
    public int zidou
    {
        set { _zidou = value; }
        get { return _zidou; }
    }
}
