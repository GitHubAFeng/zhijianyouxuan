<%@ WebHandler Language="C#" Class="MyAjaxHandler" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Linq;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Newtonsoft.Json;

/// <summary>
/// 处理一般ajax请求:注意，所有方法是不能有参数的
/// </summary>
public class MyAjaxHandler : HandlerBase, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 实时监控界面统计信息
    /// </summary>
    public string businessmonitor()
    {
        int cityid = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["cityid"]));

        StringBuilder sb = new StringBuilder("{");

        businessmonitor monitor = new businessmonitor("rider", cityid);
        sb.Append("\"rider\":{\"quota\":" + monitor.quota.ToString("0") + ",\"quota1\":" + monitor.quota1 + "},");

        monitor = new businessmonitor("timeout", cityid);
        sb.Append("\"timeout\":{\"quota\":" + monitor.quota.ToString("0.0") + ",\"quota1\":" + monitor.quota1 + "},");
        monitor = new businessmonitor("loadCount", cityid);
        sb.Append("\"loadCount\":{\"quota\":" + monitor.quota.ToString("0.00") + ",\"quota1\":" + monitor.quota1 + "},");
        monitor = new businessmonitor("total", cityid);
        sb.Append("\"total\":{\"quota\":" + monitor.quota.ToString("0") + ",\"quota1\":" + monitor.quota1 + "}");



        sb.Append("}");


        return sb.ToString();
    }


    /// <summary>
    /// 配送员轨迹
    /// </summary>
    public string getdeliverpath()
    {
        string tid = WebUtility.InputText(base._httpReuqest["tid"]);
        string tbdate = WebUtility.InputText(base._httpReuqest["tbdate"]);

        IList<localInfo> paths = new GPS_Records().GetDeliverPath(Convert.ToInt32(tid), tbdate);
        return JsonConvert.SerializeObject(paths);
    }

    public string setRefuse()
    {
        int shopid = UserHelp.GetUser_Togo().Unid;

        string cid = WebUtility.InputText(base._httpReuqest["id"]);
        int state = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["state"]));
        string msg = Context.Server.UrlDecode(WebUtility.InputText(base._httpReuqest["msg"]));

        shopSetOrder set = new shopSetOrder(Context, shopid, cid, state, msg);
        int rs = set.HandleOrder();

        return rs.ToString();
    }
    public string setCancel()
    {
        int shopid = UserHelp.GetUser_Togo().Unid;

        string cid = WebUtility.InputText(base._httpReuqest["id"]);
        int state = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["state"]));
        string msg = Context.Server.UrlDecode(WebUtility.InputText(base._httpReuqest["msg"]));

        shopSetOrder set = new shopSetOrder(Context, shopid, cid, state, msg);
        int rs = set.CancelOrder();

        return rs.ToString();
    }


    /// <summary>
    /// 操作提出记录
    /// </summary>
    public string setCashOut()
    {
        string cid = WebUtility.InputText(base._httpReuqest["id"]);
        int state = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["state"]));
        string msg = Context.Server.UrlDecode(WebUtility.InputText(base._httpReuqest["msg"]));

        TogoAddMoneyLogInfo model = new TogoAddMoneyLog().GetModel(Convert.ToInt32(cid));
        CanCashOutInfo money = new TogoAddMoneyLog().GetCanCashOutmoney(model.UserId);

        if (state == 1)
        {
            if (money.AllMoney >= Math.Abs(model.AddMoney))
            {
                string sql = "UPDATE dbo.TogoAddMoneyLog SET State = '" + UserHelp.GetAdmin().ID + "',PayDate = GETDATE(),Inve2 = '" + msg + "',PayState = " + state + " WHERE dataid = " + cid + ";UPDATE dbo.Points SET money =money - " + (Math.Abs(model.AddMoney)) + " WHERE Unid = " + model.UserId;
                int rs = WebUtility.excutesql(sql);
                return "1";
            }
            else
            {
                return "0";
            }
        }
        else
        {
            string sql = "UPDATE dbo.TogoAddMoneyLog SET State = '" + UserHelp.GetAdmin().ID + "',PayDate = GETDATE(),Inve2 = '" + msg + "',PayState = " + state + " WHERE dataid = " + cid + ";";
            int rs = WebUtility.excutesql(sql);
            return "1";
        }
    }


    /// <summary>
    /// 操作提出记录
    /// </summary>
    public string uploadFoodSortPic()
    {
        string id = WebUtility.InputText(base._httpReuqest["id"]);
        string pic = WebUtility.InputText(base._httpReuqest["pic"]);

        string sql = "UPDATE dbo.EFoodSort SET pic =  '" + pic + "' WHERE SortID = " + id;
        int rs = WebUtility.excutesql(sql);
        return rs.ToString();
    }


    /// <summary>
    /// 查询商品
    /// </summary>
    public string searchFood()
    {
        string tid = base._httpReuqest["tid"];
        string key = base._httpReuqest["q"];

        StringBuilder sb = new StringBuilder(" ");
        string sql = " 1=1 and ( FoodName like'%" + key + "%' or  FoodNamePy like'%" + key + "%') and FPMaster =" + tid;
        IList<FoodinfoInfo> list = new Foodinfo().GetList(20, 1, sql, "Unid", 1);

        foreach (var item in list)
        {
            sb.Append(item.FoodName + "|" + item.Unid + "|" + item.FPrice + "\n");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 返回打印离线，及订单未打印的消息
    /// </summary>
    /// <returns></returns>
    public string printmsg()
    {
        int hasmsg = 0;
        StringBuilder sb = new StringBuilder();
        //1打印机离线情况
        FeYinPrinter printer = new FeYinPrinter();
        IList<deviceInfo> devlist = XmlHelper<deviceInfo>.XmlToEntityList(printer.ListDevice());
        IList<PointsInfo> printshop = new TogoPrinter().GetListWithSttus();

        StringBuilder printermsg = new StringBuilder();
        int hasbugprinter = 0;
        foreach (var item in printshop)
        {
            if (item.Status == 1 && item.isbisness == 1)
            {
                foreach (var dev in devlist)
                {
                    if (dev.id == item.Picture)
                    {
                        string msg = "";
                        if (dev.deviceStatus != null && dev.deviceStatus.IndexOf("离线") >= 0)
                        {
                            msg += "离线,";
                        }
                        if (dev.paperStatus != null && dev.paperStatus.IndexOf("缺纸") >= 0)
                        {
                            msg += "缺纸,";
                        }
                        msg = WebUtility.dellast(msg);
                        if (msg != "")
                        {
                            hasbugprinter = 1;
                            printermsg.Append("<li>" + item.Picture + "[" + item.Name + "] - " + msg + "</li>");
                            hasmsg = 1;
                        }


                        break;
                    }
                }
            }
        }
        if (hasbugprinter == 1)
        {
            sb.Append("<div class='clear'></div>");
            sb.Append("<div><span>有异常打印机列表</span>");
            sb.Append("<div  style=' width:337px' class='mymsg_notice'><ul>");
            sb.Append(printermsg);
            sb.Append("</ul></div>");
            sb.Append("</div>");
        }

        StringBuilder ordermsg = new StringBuilder();
        //2网站推送订单数据给打印机时，检测到打印机未打印时，声音及弹窗报警 飞印打印机有状态显示，就根据未打印状态报警
        int hasorders = 0;
        string sql = " printstate = 0 and  dateadd(minute,3,addtime) < getdate()  ";
        IList<printorderlogInfo> logs = new printorderlog().GetList(10, 1, sql, "pid", 1);
        foreach (var item in logs)
        {
            hasorders = 1;
            hasmsg = 1;
            ordermsg.Append("<li id='printorderlog_" + item.pid + "'>" + item.orderid + "[" + item.addtime + "]<img src='/images/del_btn.gif' style='margin-left:10px;' title='删除此提示' onclick='delprintorderlog(" + item.pid + ")' /></li>");
        }
        if (hasorders == 1)
        {
            sb.Append("<div class='clear'></div>");
            sb.Append("<div><span>未打印的订单</span>");
            sb.Append("<div  style=' width:337px' class='mymsg_notice'><ul>");
            sb.Append(ordermsg);
            sb.Append("</ul></div>");
            sb.Append("</div>");
        }


        sb.Append("<input id='hfhasmsg' value='" + hasmsg + "' type='hidden'>");


        return sb.ToString();
    }

    /// <summary>
    /// 查询商品
    /// </summary>
    public string delPrintOrderLog()
    {
        string pid = WebUtility.InputText(base._httpReuqest["id"]);

        string sql = " UPDATE printorderlog SET printstate = 222 WHERE pid =" + pid;

        WebUtility.excutesql(sql);

        return "1";
    }

    /// <summary>
    /// 根据城市筛选区域
    /// </summary>
    public string changgecity()
    {
        string cid = WebUtility.InputText(base._httpReuqest["id"]);

        ESection dal = new ESection();
        IList<SectionInfo> list = dal.GetList(100, 1, "cityid =" + cid, "SectionID", 1);

        StringBuilder listjson = new StringBuilder();
        listjson.Append("{\"datalist\":[");

        SectionInfo info = new SectionInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = new SectionInfo();

            info = list[i];

            listjson.Append("{\"id\":\"" + info.SectionID.ToString() + "\",\"name\":\"" + info.SectionName + "\"},");
        }

        listjson.Append("]}");

        HJlog.toLog("listjson=" + listjson);
        return listjson.ToString().Replace("},]}", "}]}");
    }
    /// <summary>
    /// 上传分类图片
    /// </summary>
    public string uploadpic()
    {
        string id = WebUtility.InputText(base._httpReuqest["id"]);
        string pic = WebUtility.InputText(base._httpReuqest["pic"]);
        string type = WebUtility.InputText(base._httpReuqest["type"]);
        string sql = "update " + type + " set pic = '" + pic + "' where id =" + id;
        int rat = WebUtility.excutesql(sql);
        return rat.ToString();

    }

    /// <summary>
    /// 操作用户提现记录
    /// </summary>
    public string setUserDrawCash()
    {
        string cid = WebUtility.InputText(base._httpReuqest["id"]);
        int state = Convert.ToInt32(WebUtility.InputText(base._httpReuqest["state"]));
        string msg = Context.Server.UrlDecode(WebUtility.InputText(base._httpReuqest["msg"]));

        string sql = "UPDATE dbo.UserAddMoneyLog SET Inve1 = '" + UserHelp.GetAdmin().ID + "',PayDate = GETDATE(),Inve2 = '" + msg + "',state = " + state + " WHERE dataid = " + cid + ";";
        int rs = WebUtility.excutesql(sql);
        return "1";

    }

}
