using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class App_Android_GetHurry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Custorder dal = new Custorder();

        string ret = "{\"orderid\":\"" + 0 + "\",\"state\":\"" + 0 + "\",\"msg\":\"" + 0 + "\"}";

        string orderid = HjNetHelper.GetQueryString("orderid");
        string s = orderid;
        CustorderInfo order = dal.GetModel(orderid);
        DateTime now = DateTime.Now;
        double Minutes = Convert.ToDouble(SectionProxyData.GetSetValue(20));//下订单后多少时间可以催单
        double Minutc = Convert.ToDouble(SectionProxyData.GetSetValue(21));//多少时间后不能催单
        if (order.OrderDateTime.AddMinutes(Minutes) < now && order.OrderDateTime.AddMinutes(Minutc) > now)
        {

        }
        else
        {
            ret = "{\"orderid\":\"" + orderid + "\",\"state\":\"" + -1 + "\",\"msg\":\"该订单目前不能催单!\"}";
            Response.Write(ret);
            Response.End();
            return;
        }

        if (Session[s] == null)
        {
            Hangjing.SQLServerDAL.hurryorder dallho = new hurryorder();
            hurryorderInfo model = dallho.GetModel(orderid);
            if (model.oid != "")//第一次
            {
                model.oid = orderid;
                model.Name = order.CustomerName;
                model.addtime = DateTime.Now.ToString();
                model.ReveInt = 0;
                model.ReveVar = order.TogoId.ToString();
                model.Ccount = 1;


                if (dallho.Add(model) > 0)
                {
                    NoticeHelper notice = new NoticeHelper(null);
                    notice.sendNotice2Shop(order.TogoId, "您有一个催单请求", orderid);

                    ret = "{\"orderid\":\"" + orderid + "\",\"state\":\"" + 0 + "\",\"msg\":\"操作成功!\"}";
                    Session[s] = DateTime.Now.ToString();
                    Response.Write(ret);
                    Response.End();
                    return;
                }
                else
                {
                    ret = "{\"orderid\":\"" + orderid + "\",\"state\":\"" + -2 + "\",\"msg\":\"操作失败!\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }
            }
            else
            {
                model.Ccount += 1;
                model.addtime = DateTime.Now.ToString();
                if (dallho.Update(model) > 0)
                {

                    ret = "{\"orderid\":\"" + orderid + "\",\"state\":\"" + 0 + "\",\"msg\":\"操作成功!\"}";
                    Session[s] = DateTime.Now.ToString();
                    Response.Write(ret);
                    Response.End();
                    return;
                }
                else
                {
                    ret = "{\"orderid\":\"" + orderid + "\",\"state\":\"" + -2 + "\",\"msg\":\"操作失败!\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }
            }

        }
        else
        {
            ret = "{\"orderid\":\"" + orderid + "\",\"state\":\"" + -2 + "\",\"msg\":\"您已经催过订单了，请耐心等待!\"}";
            Response.Write(ret);
            Response.End();
            return;
        }
    }
}