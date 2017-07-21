using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 此类用于对应购物车中的信息
/// </summary>
[Serializable]
public class Cartinfo
{
    /// <summary>
    ///数据量
    /// </summary>
    public string totalNumber
    {
        set;
        get;
    }

    /// <summary>
    ///总金额
    /// </summary>
    public string totalAmount
    {
        set;
        get;
    }

    /// <summary>
    /// 商家列表
    /// </summary>
    public IList<CartProductinfo> productlist
    {
        set;
        get;
    }
}

