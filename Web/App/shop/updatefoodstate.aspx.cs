using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 上架下架商品 
/// </summary>
public partial class App_shop_updatefoodstate : System.Web.UI.Page
{
    Foodinfo dal = new Foodinfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string ids = WebUtility.InputText(Request["id"]); //多个用逗号分分开
        string state = HjNetHelper.GetQueryString("state");//y表示上架，n表示下架 


        string rs = "{\"state\":\"1\",\"msg\":\"操作成功\"}";
        if (ids == "")
        {
            rs = "{\"state\":\"-2\",\"msg\":\"参数不完整\"}";
            Response.Write(rs);
            Response.End();
            return;
        }

        if (dal.UpdateValue("InUse", state, "where unid in (" + WebUtility.dellast(ids) + ")") > 0)
        {
            rs = "{\"state\":\"1\",\"msg\":\"操作成功\"}";
        }
        else
        {
            HJlog.toLog("上架下架商品失败：ids" + ids);
            rs = "{\"state\":\"-1\",\"msg\":\"操作失败\"}";
        }
        Response.Write(rs);
        Response.End();
    }
}