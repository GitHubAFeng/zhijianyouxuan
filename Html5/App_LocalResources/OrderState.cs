using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Cache;
using System.Web.UI.WebControls;

/// <summary>
/// 此类主要处理订单状态相关操作。
/// </summary>
public class OrderState
{
    private string statestr = "";//订单状态对应的文字
    private IList<StateConfigInfo> statelist = null;

    public OrderState()
    {
        InitStateList();
    }

    /// <summary>
    /// 初始化订单状态列表
    /// </summary>
    private void InitStateList()
    {
        StateConfig dal = new StateConfig();
        statelist = (IList<StateConfigInfo>)EasyEatCache.GetCacheService().RetrieveObject("/StateConfig/order");
        if (statelist == null || statelist.Count == 0)
        {
            statelist = dal.GetsubList(1);
            EasyEatCache.GetCacheService().AddObject("/StateConfig/order", statelist);
        }
    }

    /// <summary>
    /// 获取状态对应的文字
    /// </summary>
    /// <param name="_state"></param>
    /// <returns></returns>
    public string TurnOrderState(object _state)
    {
        string ret = "";
        int state = Convert.ToInt32(_state);
        foreach (Hangjing.Model.StateConfigInfo item in statelist)
        {
            if (state == item.Status)
            {
                ret = item.classname;
                break;
            }
        }
        return ret;
    }

    /// <summary>
    /// 订单状态绑定到DropDownList中
    /// </summary>
    /// <param name="ddl"></param>
    public void BindOrderState(DropDownList ddl)
    {
        WebUtility.BindList("status", "classname", statelist, ddl);
    }

    /// <summary>
    /// 订单状态绑定到DropDownList中
    /// </summary>
    /// <param name="ddl"></param>
    public  void BindOrderState(CheckBoxList ddl)
    {
        WebUtility.BindList("status", "classname", statelist, ddl);
    }

    
}
