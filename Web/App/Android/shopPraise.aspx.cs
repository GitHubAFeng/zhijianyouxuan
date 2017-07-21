using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// 商家点赞
/// </summary>
public partial class AndroidAPI_shopPraise : APIPageBase
{
    Custorder dal = new Custorder();
    CustorderInfo model = new CustorderInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        int shopid = HjNetHelper.GetQueryInt("shopid", 0);
        //PType  满意数;RcvType 一般数;IsCallCenter  较差数;
        string Field = HjNetHelper.GetQueryString("Field");
        int userid = HjNetHelper.GetQueryInt("userid", 0);

        string where = " 1=1 and TogoId=" + shopid + " AND UserId=" + userid + " AND (paymode=4 or paystate=1) and isreview IS NULL";
        model = dal.GetSqlWhere(where);
        if (model != null)
        {
            string sql = " UPDATE dbo.Points SET " + Field + " = " + Field + "+1 WHERE Unid=  " + shopid;
            if (WebUtility.excutesql(sql) > 0)
            {
                sql = "UPDATE dbo.Custorder SET isreview=1 WHERE unid=" + model.Unid;
                WebUtility.excutesql(sql);
                Response.Write("{\"msg\":\"点赞成功\",\"state\":\"" + 1 + "\"}");

            }
            else
            {
                Response.Write("{\"msg\":\"点赞失败\",\"state\":\"" + 0 + "\"}");
            }
        }
        else
        {
            Response.Write("{\"msg\":\"已点赞或未订餐\",\"state\":\"" + -1 + "\"}");
        }





        

      
        Response.End();
    }
}
