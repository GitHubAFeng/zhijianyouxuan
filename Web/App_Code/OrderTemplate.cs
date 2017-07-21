using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
/// 订单打印模版
/// </summary>
public class OrderTemplate
{
    private int PrintTimes = 2;//默认打印2联

    /// <summary>
    /// 订单编号(自)
    /// </summary>
    private string orderid;

    public OrderTemplate(string orderid)
    {
        this.orderid = orderid;
    }

    /// <summary>
    /// 生成打印内容
    /// </summary>
    /// <returns></returns>
    public string getPrintStr()
    {
        CustorderInfo info = new Custorder().GetModel(orderid);

        StringBuilder sb = new StringBuilder();
        sb.Append("<Font# Bold=0 Width=2 Height=2>" + buildCenterLine(SectionProxyData.GetSetValue(2), 2) + "</Font#>");
        sb.Append("\r\n" + buildCenterLine(SectionProxyData.GetSetValue(1).Replace("http://", ""), 1) + "\r\n");

        //订单信息
        sb.Append("\r\n订单单号：" + info.orderid + "");
        sb.Append("\r\n下单时间：" + info.OrderDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "");
        sb.Append("\r\n" + buildLine());

        sb.Append("\r\n姓名：" + info.OrderRcver + "");
        sb.Append("\r\n电话：" + info.OrderComm + "");
        sb.Append("\r\n地址：" + info.AddressText + "");
        sb.Append("\r\n到送时间：" + info.SendTime + "");
        sb.Append("\r\n备注：" + info.OrderAttach + "");
        sb.Append("\r\n" + buildLine());

        //菜品信息
        sb.Append("  菜式            数量    单价  \r");
        IList<FoodlistInfo> foodlist = new Foodlist().GetAllByOrderID(info.orderid);

        foreach (var food in foodlist)
        {
            string other = "";
            sb.Append("\r\n");
            sb.Append("" + food.FoodName + other);
            sb.Append("\r\n");
            sb.Append("                  " + food.FCounts + "    " + (food.FoodPrice));
        }
        sb.Append("\r\n" + buildLine());
        sb.Append("\r\n配送费：" + info.SendFee + "\r\n");
        sb.Append("<Font# Bold=0 Width=2 Height=2>" + buildRightLine("总额：" + info.OrderSums, 2) + "</Font#>\r\n");
        sb.Append("\r\n" + buildCenterLine(SectionProxyData.GetSetValue(52), 1) + "");

        StringBuilder body = new StringBuilder();
        for (int i = 0; i < PrintTimes-1; i++)
        {
            body.Append(sb);
            body.Append("\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
        }
        body.Append(sb);

        return body.ToString();
    }

    /// <summary>
    /// 生成一条虚线
    /// </summary>
    /// <returns></returns>
    public string buildLine()
    {
        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < 32; i++)
        {
            sb.Append("-");
        }
        return sb.ToString();
    }

    /// <summary>
    /// 打印一行右对齐的数据
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="rat">字体倍数</param>
    /// <returns></returns>
    public string buildRightLine(string data, int rat)
    {
        int max = 32;

        int dataLength = getLength(data) * rat;
        if (dataLength >= max)
        {
            return data;
        }

        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < (max - dataLength)/rat; i++)
        {
            sb.Append(" ");
        }

        return sb.ToString() + data;
    }

    /// <summary>
    /// 打印一行居中的数据
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="rat">字体倍数</param>
    /// <returns></returns>
    public string buildCenterLine(string data, int rat)
    {
        int max = 32;

        int dataLength = getLength(data) * rat;
        if (dataLength >= max)
        {
            return data;
        }

        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < (max - dataLength) / 2 / rat; i++)
        {
            sb.Append(" ");
        }

        return sb.ToString() + data;
    }

    /// <summary>
    /// 计算字符串字符个数（汉字算2个）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int getLength(string str)
    {
        char[] temp = str.ToCharArray();

        int length = 0;

        for (int i = 0; i < str.Length; i++)
        {
            int code = Convert.ToInt32(temp[i]);

            if (code > 1000)//汉字加2个
            {
                length++;
                length++;
            }
            else
            {
                length++;
            }

        }
        return length;

    }
}