using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///用户中心订单状态图
///通过有参数的构造函数（参数表示状态）
/// </summary>
public class OrderStateProcess
{
    public string Processhtml = "";
    public OrderStateProcess()
    {

    }

    /// <summary>
    /// 根据是否是实物订单类型,返回html
    /// </summary>
    /// <param name="state">订单状态</param>
    /// <param name="isend">1表示是实物订单类型</param>
    public OrderStateProcess(int state)
    {

        string temphtml = "";

        //分别对应状态
        int[] statearray = new int[3] { 1, 2, 7 };
        //0表示已经完成,1表示未完成
        int flag = 0;
        foreach (int item in statearray)
        {
            if (item == state)
            {
                temphtml += state_node(WebUtility.TurnOrderState(item), flag);
                temphtml += state_proce(WebUtility.TurnOrderState(item), flag);
                flag = 1;

            }
            else
            {
                temphtml += state_node(WebUtility.TurnOrderState(item), flag);
                temphtml += state_proce(WebUtility.TurnOrderState(item), flag);
            }
        }
        if (state == 3)
        {
            temphtml += state_node(WebUtility.TurnOrderState(3), 0);
        }
        else
        {
            if (state >= 4 && state <= 6)
            {
                temphtml += state_node(WebUtility.TurnOrderState(5), 0);
            }
            else
            {
                temphtml += state_node(WebUtility.TurnOrderState(3), 1);
            }
           
        }


        Processhtml = temphtml;

    }

    /// <summary>
    /// 返回节点hmlt.iswait为了表示还要wait,不是1表示ready
    /// </summary>
    /// <param name="state"></param>
    /// <param name="iswait"></param>
    /// <returns></returns>
    public string state_node(string statestr, int iswait)
    {
        string temp = "";
        string classstr = "wait";
        if (iswait == 1)
        {
            classstr = "wait";
        }
        else
        {
            classstr = "ready";
        }
        temp = "<div class=\"node " + classstr + "\"><ul><li class=\"tx1\">&nbsp;</li><li class=\"tx2\">" + statestr + "</li><li class=\"tx3\" id=\"track_time_0\"></li></ul></div>";

        return temp;
    }

    /// <summary>
    /// 返回过程hmlt.iswait为了表示还要wait,不是1表示ready
    /// </summary>
    /// <param name="state"></param>
    /// <param name="iswait"></param>
    /// <returns></returns>
    public string state_proce(string statestr, int iswait)
    {
        string temp = "";
        string classstr = "wait";
        if (iswait == 1)
        {
            classstr = "wait";
        }
        else
        {
            classstr = "ready";
        }

        temp = "<div class=\"proce " + classstr + "\"><ul><li class=\"tx1\"></li></ul></div>";

        return temp;
    }
}
