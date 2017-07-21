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
using Hangjing.Common;

/// <summary>
/// 提交评论
/// </summary>
public partial class AndroidAPI_submitreview : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Custorder dalorder = new Custorder();
        ETogoOpinion dalopinion = new ETogoOpinion();

        Response.Clear();
        string ret = "{\"state\":\"1\",\"msg\":\"\"}";
        string jsonstring = Request["ordermodel"];
        int id = HjNetHelper.GetPostParam("shopid", 0);//商家编号
        int userid = HjNetHelper.GetPostParam("userid", 0);//会员编号编号
        string username = HjNetHelper.GetPostParam("username");

        Hangjing.Common.HJlog.toLog("评论提交数据：" + jsonstring);

        ETogoOpinionInfo opinionmodel = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ETogoOpinionInfo>(jsonstring);
        if (opinionmodel != null)
        {
            int OrderID = dalorder.OrderDataID(userid, id);
            if (OrderID == 0)
            {
                if (dalorder.OrderDataIDstate(userid, id) > 0)
                {
                    ret = "{\"state\":\"0\",\"msg\":\"很抱歉，您已评论过!\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }
                else
                {
                    ret = "{\"state\":\"0\",\"msg\":\"很抱歉，您没有订餐无法评论!\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }
            }
            else
            {
                opinionmodel.UserID = userid;
                opinionmodel.UserName = username;
                opinionmodel.TogoID = id;
                opinionmodel.Point = 0;
                opinionmodel.PostTime = DateTime.Now;
                opinionmodel.Rcontent = "";
                opinionmodel.Rtype = 0;
                opinionmodel.Rtime = Convert.ToDateTime("1900-01-01");

                int dataid = dalopinion.Add(opinionmodel);

                if (dataid > 0)
                {
                    dalopinion.setReviewData(opinionmodel.TogoID);

                    string InveInt2 = SectionProxyData.GetSetValue(11);//0表示不审核，1表示要审核。
                    if (InveInt2 == "0")
                    {
                        ECustomer userBLL = new ECustomer();
                        userBLL.addpoint(userid, "评论获取积分", Convert.ToInt32(SectionProxyData.GetSetValue(23)));
                        dalorder.OrderUnid(OrderID);//设置些用户已评论
                    }

                    ret = "{\"state\":\"1\",\"msg\":\"\",\"dataid\":\"" + dataid + "\"}";
                    Response.Write(ret);
                    Response.End();
                    return;

                }
                else
                {
                    ret = "{\"state\":\"0\",\"msg\":\"服务器繁忙，请稍后再试\",\"dataid\":\"" + 0 + "\"}";
                    Response.Write(ret);
                    Response.End();
                    return;
                }
            }

        }
        else
        {
            ret = "{\"state\":\"0\",\"msg\":\"参数有错误\",\"dataid\":\"" + 0 + "\"}";
            Response.Write(ret);
            Response.End();
            return;
        }

        Response.Write(ret);
        Response.End();
    }
}

