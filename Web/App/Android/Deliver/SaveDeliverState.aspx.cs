using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
///  更新订单状态（当状态为3为配送完成时，订单状态也变成完成）
// 传入参数：订单编号 配送状态状态 
/// </summary>
public partial class App_Android_Deliver_SaveDeliverState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string userid = Request["userid"].ToString();
        string state = WebUtility.InputText(Request["state"]);//配送员状态
        
        Deliver dal = new Deliver();
        DeliverInfo info = dal.GetModel(Convert.ToInt32(userid));

        string sql = "update Deliver set IsWorking=" + state + " where DataId=" + userid;

        if (info != null)
        {
            if (WebUtility.excutesql(sql) > 0)
            {
                Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"1\"}");//修改状态成功
            }
            else
            {
                Response.Write("{\"userid\":\"" + userid + "\",\"state\":\"0\"}");////修改状态失败
            }
        }
        else
        {
            Response.Write("{\"userid\":\"-1\"}");
        }

        Response.End();
    }
}
