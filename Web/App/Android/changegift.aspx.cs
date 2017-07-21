using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;

/// <summary>
/// 兑换礼品提交订单。
/// </summary>
public partial class Android_changegift : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Integral dal = new Integral();
        //加测试参数
        string jsonstring = Request["ordermodel"];
        //state=-1 表示失败，1成功
        string ret = "{\"state\":\"1\",\"msg\":\"\"}";

        IList<IntegralInfo> orderlist = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<IList<IntegralInfo>>(jsonstring);

        if (orderlist.Count > 0)
        {
            IntegralInfo iinfo = orderlist[0];
            iinfo.Cdate = DateTime.Now;

            /*****************************兑换判断 **********************/
            ECustomer daluser = new ECustomer();
            ECustomerInfo user = daluser.GetModel(Convert.ToInt32(iinfo.CustId));
            if (user == null)
            {
                ret = "{\"state\":\"-1\",\"msg\":\"此用户不存在\"}";
                Response.Write(ret);
                Response.End();
                return;
            }
            if (user.State == "1")
            {
                ret = "{\"state\":\"-1\",\"msg\":\"此用户已经加入黑名单，不能兑换礼品\"}";
                Response.Write(ret);
                Response.End();
                return;
            }

            int subpoint = Convert.ToInt32(Convert.ToDecimal(iinfo.PayIntegral));
            if (user.Point < subpoint)
            {
                ret = "{\"state\":\"-1\",\"msg\":\"用户积分不足，不能兑换礼品\"}";
                Response.Write(ret);
                Response.End();
                return;
            }
            GiftsInfo gift = new Gifts().GetModel(iinfo.GiftsId);
            if (gift.Stocks <= 0)
            {
                ret = "{\"state\":\"-1\",\"msg\":\"此礼品库存不足，不能兑换\"}";
                Response.Write(ret);
                Response.End();
                return;
            }

            int rs = dal.Add(iinfo);
            if (rs > 0)
            {
                ret = "{\"state\":\"1\",\"msg\":\"\"}";
            }
            else
            {
                ret = "{\"state\":\"-1\",\"msg\":\"服务器错误\"}";
            }
        }
        else
        {
            ret = "{\"state\":\"-1\",\"msg\":\"参数错误，请检查\"}";
        }

        Response.Write(ret);
        Response.End();

    }
}
