using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///ChinaBankInfo 的摘要说明
/// </summary>
public class ChinaBankInfo
{
    private string _orderid;
    private string _moneytype;
    private string _money;
    private string _show_url;

    /// <summary>
    /// 初始化AliPayInfo
    /// </summary>
    /// <param name="out_trade_no">订单号</param>
    /// <param name="subject">订单总金额</param>
    /// <param name="body">币种</param>
    /// <param name="show_url">商品展示地址</param>
    public ChinaBankInfo(string orderid, string money, string moneytype, string show_url)
    {
        _orderid = orderid;
        _money = money;
        _moneytype = moneytype;
        _show_url = show_url;
    }

    /// <summary>
    /// 订单号
    /// </summary>
    public string OrderId
    {
        get { return _orderid; }
        set { _orderid = value; }
    }

    /// <summary>
    /// 商品总价
    /// </summary>
    public string Money
    {
        get { return _money; }
        set { _money = value; }
    }

    /// <summary>
    /// 币种
    /// </summary>
    public string MoneyType
    {
        get { return _moneytype; }
        set { _moneytype = value; }
    }

    /// <summary>
    /// 商品展示地址
    /// </summary>
    public string ShowUrl
    {
        get { return _show_url; }
        set { _show_url = value; }
    }
}
