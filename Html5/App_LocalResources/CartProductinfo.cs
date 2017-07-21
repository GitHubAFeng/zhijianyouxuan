using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 此类用于对应购物车中商品信息
/// </summary>
[Serializable]
public class CartProductinfo
{
    /// <summary>
    ///编号
    /// </summary>
    public string id
    {
        set;
        get;
    }

    /// <summary>
    ///名称
    /// </summary>
    public string name
    {
        set;
        get;
    }

    /// <summary>
    ///数量
    /// </summary>
    public string number
    {
        set;
        get;
    }

    /// <summary>
    ///单价
    /// </summary>
    public string price
    {
        set;
        get;
    }

    /// <summary>
    ///规格名称
    /// </summary>
    public string sname
    {
        set;
        get;
    }

    /// <summary>
    /// 加价
    /// </summary>
    public string addprice
    {
        set;
        get;
    }

    /// <summary>
    /// 打包费
    /// </summary>
    public decimal packagefee
    {
        set;
        get;
    }

    /// <summary>
    //规格编号
    /// </summary>
    public string sid
    {
        set;
        get;
    }

    /// <summary>
    /// 加菜详情
    /// </summary>
    public string material
    {
        set;
        get;
    }


}
