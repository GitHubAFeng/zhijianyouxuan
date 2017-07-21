using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Text.RegularExpressions;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class SendWebSocketMSG : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        //返回结果：rscode表示代码，msg表示说明;  1表示成功，msg为空
        string rs = "{\"rscode\":\"1\",\"msg\":\"\"}";
        int id;//商家或者骑士编号

        //HJlog.toLog("id=" + Request["id"] + " se=" + Request["se"] + "ty=" + Request["ty"] + " msg=" + Request["msg"] + " autotype=" + Request["autotype"] + " ids=" + Request["ids"]);

        int autotype = HjNetHelper.GetQueryInt("autotype", 0);
        switch (autotype)
        {
            case 1:  //发所有人
                {
                    int cityid = HjNetHelper.GetQueryInt("cityid", 0);
                    NoticeHelper notice = new NoticeHelper(Context);
                    notice.send2All(cityid);
                }
                break;
            case 2:  //发给商家一定范围内的骑士

                string ids = Request["ids"];
                if (ids != null && ids != "")
                {
                    NoticeHelper notice = new NoticeHelper(Context);
                    notice.send2IDs(ids);
                }

                break;
            default:
                if (int.TryParse(Request["id"], out id))
                {
                    int type = 0;//消息类型：0表示订单，1表示纯消息。
                    int se = 1;//1表示骑士，2表示商家


                    int.TryParse(Request["se"], out se);
                    int.TryParse(Request["ty"], out type);

                    /**********************************************
                      msg（通知内容）
                           新订单通知 {\"state\":"\"1\,\"count\":\"1\",\"ExpressOrdercount\":\"1\"}  count 表示外卖订单数量,ExpressOrdercount表示跑腿订单数量
                           消息通知   {\"state\":"\"1\,\"msg\":\"XX订单取消配送\"} msg 表示消息内容
                      id  商家或者骑士编号
                      se  1表示骑士，2表示商家
                      type  消息类型：0表示订单，1表示纯消息。
                    ************************************************/
                    String msg = Request["msg"];//消息内容：json格式


                    if (WebUtility.GetConfigKey("socketmodel") == "0") //SuperWebSocket
                    {

                    }
                    else//supersocket
                    {
                        wcfnotice.UserNoticeServiceClient unsc = new wcfnotice.UserNoticeServiceClient();
                        ///发订单
                        unsc.AddMessage(id, se, type, msg);
                    }

                }
                else
                {
                    rs = "{\"rscode\":\"-2\",\"msg\":\"参数错误\"}";
                    Response.Write(rs);
                    Response.End();
                }
                break;
        }



    }
}
