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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.DBUtility;
using System.Collections.Generic;

/// <summary>
/// 订单调度系统
/// </summary>
public partial class qy_54tss_AreaAdmin_GpsSet_OrderDelive : System.Web.UI.Page
{

    Deliver daldel = new Deliver();
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }
    private string ExpressSqlWhere
    {
        get
        {
            object o = ViewState["ExpressSqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["ExpressSqlWhere"] = value;
        }
    }

    protected string ordersort
    {
        get
        {
            object o = ViewState["ordersort"];
            return (o == null) ? "orderTime" : Convert.ToString(o);
        }
        set
        {
            ViewState["ordersort"] = value;
        }
    }

    /// <summary>
    /// 城市编号
    /// </summary>
    protected int cityid
    {
        get
        {
            object o = ViewState["cityid"];
            return (o == null) ? 0 : Convert.ToInt32(o);
        }
        set
        {
            ViewState["cityid"] = value;
        }
    }

    Custorder dal = new Custorder();
    ExpressOrder expressdal = new ExpressOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (UserHelp.GetAdmin() == null)
            {
                Response.Redirect("../login.aspx");
                return;
            }

            hidLat.Value = SectionProxyData.GetSetValue(4);
            hidLng.Value = SectionProxyData.GetSetValue(5);

            string cityname = "";

            cityid = HjNetHelper.GetQueryInt("cid", 0);
            if (cityid == 0)
            {
                string cityid_cookie = WebUtility.FixgetCookie("d_cityid");
                if (cityid_cookie == null || cityid_cookie == "")
                {
                    hfflag.Value = "1";
                    return;
                }
                else
                {
                    cityid = Convert.ToInt32(cityid_cookie);
                    cityname = Server.UrlDecode(WebUtility.FixgetCookie("d_cityname"));
                }
            }
            else
            {
                WebUtility.FixsetCookie("d_cityid", cityid.ToString(), 30);
                cityname = HjNetHelper.GetQueryString("cname");
                WebUtility.FixsetCookie("d_cityname", Server.UrlEncode(cityname), 30);
            }
            hfcityname.Value = cityname;
            hfcityid.Value = cityid.ToString();


            SqlWhere = "  OrderStatus in (2,7) and ReveVar1 = '0' and ReveInt2 = 0 and (paymode = 4 or paystate = 1)";
            if (HjNetHelper.GetQueryString("oid") != "")
            {
                string oid = HjNetHelper.GetQueryString("oid");
                SqlWhere += " and ( Custorder.OrderID like '%" + oid + "%')";
            }
            //SqlWhere += " and Custorder.cityid = " + cityid;


            ExpressSqlWhere = "State in(0,1,2,4)";
            if (HjNetHelper.GetQueryString("eid") != "")
            {
                string eid = HjNetHelper.GetQueryString("eid");
                ExpressSqlWhere += " and (ExpressOrder.OrderID like '%" + eid + "%')";
            }
            //ExpressSqlWhere += " and ExpressOrder.cityid = " + cityid;
            //大部分数据需要采用ajax方式获取


            EAdminInfo model = UserHelp.GetAdmin();
            if (model.CityID != 0)
            {
                SqlWhere += " and Custorder.cityid=" + model.CityID;
                ExpressSqlWhere += " and Cityid=" + model.CityID;
            }

            //注意事项：
            //1、增加订单智能提醒功能，未处理订单永远自动排列在最上面
            //2、配送员情况统计功能，在配送员名称下面需提示目前未回复完成订单的数值，在颜色上面要有所区别；没有订单（呈现绿色），有订单（呈现红色）
            //3、订单列表中的状态共设计分为五种：新增订单（呈现红色），下单成功（呈现绿色），正在调度，订单完成，订单失败
            //
            //
            //1、骑士在通过骑士手机端接到订单的时候必须要做一个时间选定回复，设定默认4级：30分钟，40分钟，50分钟，60分钟；(OrderDeliver 表字段 Inve1)
            //系统根据他回复的时间进行记录，超出选定时间5分钟以上未进行订单状态修改的（未进入手机端调整订单完成状态修改的），系统最好能做到弹窗提醒，不需要太复杂的内容，就右下角跳出一个小弹窗，显示订单号即可
            //2、订单配送时间超过XX分钟未送达，系统自动提醒。
            //3、订单超过xxx分钟未被任何处理，那么系统应当自动提醒。



            //获取配送员列表 ajax/GetDeliverList.aspx

            //获取配送员有几个订单在处理的方法 
            //配送员繁忙应该是通过查看有几个正在配送的订单来判定

            //获取订单列表 需要做分页
            BindOrderData();
            BindExpressData();

            lbadminname.InnerText = UserHelp.GetAdmin().AdminName;
            //this.snDate.InnerHtml = cityname + "[<a href=\"javascript:show_citytable();\" class='orange'>切换城市</a>]";

            WebUtility.BindList("Sectionid", "SectionName", SectionProxyData.GetSectionList().Where(a => a.cityid == cityid).ToList(), ddlmysecion);
            WebUtility.BindList("dataid", "name", SectionProxyData.GetDeliverList().Where(a => a.Inve1 == cityid).ToList(), ddldeliverfix);
            WebUtility.BindList("id", "classname", SectionProxyData.GetEdelivergroupList().Where(a => a.Status == cityid).ToList(), ddlgroup);

            new OrderState().BindOrderState(ddlstate);

        }

    }

    private void BindOrderData()
    {
        AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        IList<CustorderInfo> xist = dal.GetListForDelive(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "OrderDateTime", 1);
        this.rptOrderList.DataSource = xist;
        rptOrderList.DataBind();
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
    }


    private void BindExpressData()
    {       
        //绑定跑腿订单
        AspNetPager2.RecordCount = expressdal.GetCount(ExpressSqlWhere);
        IList<ExpressOrderInfo> expresslist = expressdal.GetListForDelive(AspNetPager2.PageSize, AspNetPager2.CurrentPageIndex, ExpressSqlWhere, "orderTime", 1);
        this.rptExpressList.DataSource = expresslist;
        rptExpressList.DataBind();
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindOrderData();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        BindExpressData();
    }


    /// <summary>
    /// 确认开始分配配送员
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void deliver_click(object sender, EventArgs e)
    {
        int ordertype = Convert.ToInt32(hfexpressororder.Value);//0外卖订单 1跑腿订单

        int did = Convert.ToInt32(hfdid_hh.Value);
        DeliverInfo model = daldel.GetModel(did);
        if (model != null)
        {
            OrderDeliver dalod = new OrderDeliver();

            string oid = WebUtility.InputText(hforderid.Value);

            OrderDeliverInfo odmodel = dalod.GetModel(oid);
            if (odmodel == null)
            {
                odmodel = new OrderDeliverInfo();
            }
            odmodel.Orderid = oid;
            odmodel.DeliverId = did;
            odmodel.DeliverName = model.Name;
            odmodel.Dispatcher = UserHelp.GetAdmin().AdminName;
            odmodel.DispatchTime = DateTime.Now;
            odmodel.DeliveryTime = Convert.ToDateTime("1900-01-01");
            odmodel.Inve1 = 0;
            odmodel.Inve2 = "";
            odmodel.Section = "";

            int id = 0;
            if (odmodel.DataId > 0)
            {
                id = dalod.Update(odmodel);
            }
            else
            {
                id = dalod.Add(odmodel);
            }

            if (id > 0)
            {
                CustorderInfo order = dal.GetModel(oid);
                if (ordertype == 0)
                {
                    //记录
                    dal.AddOrderRecord(odmodel.Orderid, 7, UserHelp.GetAdmin().AdminName, "管理员修改订单信息:调度页面（调度）");
                    {
                        NoticeHelper notice = new NoticeHelper(Context, did.ToString(), hforderid.Value.Trim());
                        notice.sendOrderByDeliveryid();
                    }

                    //给商家消息
                    {
                        NoticeHelper notice = new NoticeHelper(Context);
                        notice.sendNotice2Shop(order.TogoId, "订单" + oid + "已经分配配送员", oid);
                    }


                    AlertScript.RegScript(Page, UpdatePanel1, "alert('操作成功,订单已经调度');pageinit();");
                    BindOrderData();
                }
                else//跑腿订单
                {
                    //记录
                    dal.AddOrderRecord(odmodel.Orderid, 2, UserHelp.GetAdmin().AdminName, "管理员修改订单信息:调度页面（调度）");

                    //通知骑士客户端。
                    NoticeHelper notic = new NoticeHelper(HttpContext.Current, did.ToString(), hforderid.Value.Trim());
                    notic.sendExpressOrderToDeliver();

                    AlertScript.RegScript(Page, UpdatePanel1, "alert('操作成功,订单已经调度');pageinit();");
                    BindExpressData();
                }


            }
            else
            {
                AlertScript.RegScript(Page, UpdatePanel1, "alert('操作失败');pageinit();");
            }
            hfdid_hh.Value = "0";
        }
        else
        {
            //
        }
        //OrderDeliver 添加记录
    }


    /// <summary>
    /// 订单分配给群组
    /// </summary>
    protected void sendgroup_click(object sender, EventArgs e)
    {
        int ordertype = Convert.ToInt32(hfexpressororder.Value);//0外卖订单 1跑腿订单

        string oid = WebUtility.InputText(hforderid.Value);
        int gid = Convert.ToInt32(ddlgroup.SelectedValue);
        //记录
        dal.AddOrderRecord(oid, 7, UserHelp.GetAdmin().AdminName, "管理员修改订单信息:调度页面（发群）");

        if (ordertype == 0)
        {
            //记录
            dal.AddOrderRecord(oid, 7, UserHelp.GetAdmin().AdminName, "管理员修改订单信息:调度页面（发群）");


            //订单表中也保存配送员编号，便于统计,修改订单状态:
            string sql = "update Custorder set deliverheaderid=" + gid + ",OrderStatus=7,deliverid=0,sendstate=0  where OrderID='" + hforderid.Value.Trim() + "'";
            WebUtility.excutesql(sql);
            AlertScript.RegScript(Page, UpdatePanel1, "alert('操作成功,订单已经调度');pageinit();");

            BindOrderData();
            hfdid_hh.Value = "0";
            tborderinfo.Value = "";
        }
        else
        {
            //记录
            dal.AddOrderRecord(oid, 1, UserHelp.GetAdmin().AdminName, "管理员修改订单信息:调度页面（发群）");

            //订单表中也保存配送员编号，便于统计,修改订单状态:
            string sql = "update ExpressOrder set sid=" + gid + ",state=1  where OrderID='" + hforderid.Value.Trim() + "'";
            WebUtility.excutesql(sql);

            AlertScript.RegScript(Page, UpdatePanel1, "alert('操作成功,订单已经调度');pageinit();");

            BindExpressData();
            hfdid_hh.Value = "0";
            tborderinfo.Value = "";
        }




        NoticeHelper notice = new NoticeHelper(Context, gid.ToString());
        notice.send2Group();

    }


    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void search_click(object sender, EventArgs e)
    {
        
        SqlWhere = "  OrderStatus in (2,7) and ReveVar1 = '0' and ReveInt2 = 0 and (paymode = 4 or paystate = 1)";

        string key = WebUtility.InputText(tbkeyword.Value);
        if (key != "" && key != "可按照电话、姓名、订单号查询")
        {
            SqlWhere += " and (OrderComm like '%" + key + "%' or OrderRcver like '%" + key + "%' or Custorder.OrderID like '%" + key + "%')";
        }
        if (ddlstate.SelectedValue != "0")
        {
            SqlWhere += " and OrderStatus = " + ddlstate.SelectedValue + "";
        }
        if (this.ddldeliverfix.SelectedValue != "0")
        {
            SqlWhere += " and Custorder.deliverid = " + ddldeliverfix.SelectedValue + "";
        }
        SqlWhere += " and Custorder.cityid = " + cityid;
        BindOrderData();
    }

    /// <summary>
    /// 时间差
    /// </summary>
    /// <param name="time1"></param>
    /// <param name="time2"></param>
    /// <returns></returns>
    public string getdiffdate(object time1, object time2)
    {
        DateTime t1 = Convert.ToDateTime(time1);
        DateTime t2 = Convert.ToDateTime(time2);

        if (t1 < Convert.ToDateTime("2012-01-01") || t2 < Convert.ToDateTime("2012-01-01"))
        {
            return "";
        }

        TimeSpan ts1 = new TimeSpan(t1.Ticks);
        TimeSpan ts2 = new TimeSpan(t2.Ticks);
        TimeSpan ts = ts1.Subtract(ts2).Duration();
        double minu = Math.Abs(ts.TotalMinutes);
        return minu.ToString("0.0");
    }

    /// <summary>
    /// timeer event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        string sql = " ReveVar1 = '0' and ReveInt2 = 0 and OrderStatus in (2) and (paymode = 4 or paystate = 1)";
        sql += " and Custorder.cityid = " + cityid;
        int count = dal.GetCount(sql);
        if (count != 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(" + count + ");");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(0);");
        }
        BindOrderData();
    }
}
