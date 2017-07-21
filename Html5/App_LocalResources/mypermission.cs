using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///权限(按文件夹)
/// </summary>
public class mypermission
{
    private string _foldername;
    private string _mycode;

    /// <summary>
    /// 文件夹名称
    /// </summary>
    public string foldername
    {
        set { _foldername = value; }
        get { return _foldername; }
    }

    /// <summary>
    /// 文件夹名称对应的编号{1},{2}...
    /// </summary>
    public string mycode
    {
        set { _mycode = value; }
        get { return _mycode; }
    }
}
