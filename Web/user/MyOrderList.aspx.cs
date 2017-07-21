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

public partial class UserHome_MyOrderList : PageBase
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["SqlWhere"] = value;
        }
    }

    public string ImagePath = WebUtility.GetMasterPicturePath();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            SqlWhere = "UserId=" + UserHelp.GetUser().DataID + " ";
            if (Request["state"] != null)
            {
                //今日订单
                SqlWhere += " and OrderDateTime between '" + DateTime.Now.ToShortDateString() + "' and  '" + DateTime.Now.ToString() + "'";
                hfstate.Value = Request["state"];
            }
            string sql = "OrderStatus in (0)";
            if (Request["states"] != null)
            {
                SqlWhere += "and  OrderStatus =" + Request["states"] + "";
            }
            WebUtility.FixsetCookie("ocount", dal.GetCount(sql) + "", 1);
            BindDate();

            this.tbuserids.Value = UserHelp.GetUser().DataID.ToString();
        }
    }

    Custorder dal = new Custorder();

    void BindDate()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptPointCount.DataSource = dal.GetListFix(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "OrderDateTime", 1);
        this.rptPointCount.DataBind();
        NoRecord();
    }

    private void NoRecord()
    {
        if (rptPointCount.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDate();
    }

    protected void btSearch_Click(object sender, EventArgs s)
    {
        SqlWhere = "UserId=" + UserHelp.GetUser().DataID + "";
        if (Request["state"] != null)
        {
            SqlWhere += " and OrderStatus = " + HjNetHelper.GetQueryString("state");
        }
        if (this.tbKeyword.Value != "")
        {
            SqlWhere += " and ( orderid like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Value.Trim())) + "%' ";
            SqlWhere += " or OrderComm like '%" + Utils.RegEsc(WebUtility.InputText(this.tbKeyword.Value.Trim())) + "%' )";
        }
        if (this.starttime.Value.Trim() != "")
        {
            SqlWhere += " and  OrderDateTime > '" + WebUtility.InputText(this.starttime.Value.Trim()) + "' ";
        }
        if (this.enttime.Value.Trim() != "")
        {
            SqlWhere += " and  OrderDateTime <  '" + WebUtility.InputText(this.enttime.Value.Trim()) + " 23:59:59'";
        }

        BindDate();
    }

    /// <summary>
    /// timeer event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        SqlWhere = "UserId=" + UserHelp.GetUser().DataID + " and TogoID <> 1";
        if (Request["state"] != null)
        {
            SqlWhere += " and OrderDateTime between '" + DateTime.Now.ToShortDateString() + "' and  '" + DateTime.Now.ToString() + "'";
            hfstate.Value = Request["state"];
        }



        BindDate();
        string sql = "UserId=" + UserHelp.GetUser().DataID + " and  OrderStatus in (3 ,4, 5,6)";
        int count = dal.GetCount(sql);
        int ocount = WebUtility.FixgetCookie("ocount") == null ? 0 : Convert.ToInt32(WebUtility.FixgetCookie("ocount"));
        if (count != ocount)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(1);");
            WebUtility.FixsetCookie("ocount", count + "", 1);
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "play(0);");
        }
    }

    protected void rptOrder_Command(object sender, RepeaterCommandEventArgs e)
    {
        string[] Cus = e.CommandArgument.ToString().Split(',');
        int id = Convert.ToInt32(Cus[0]);

        string s = id.ToString();
        #region 催单
        if (e.CommandName == "call")
        {
            // hotsetInfo hotset = new hotset().GetModel();
            CustorderInfo order = dal.GetModel(id);
            DateTime now = DateTime.Now;
            double Minutes = Convert.ToDouble(SectionProxyData.GetSetValue(20));//下订单后多少时间可以催单
            double Minutc = Convert.ToDouble(SectionProxyData.GetSetValue(21));//多少时间后不能催单
            if (order.OrderDateTime.AddMinutes(Minutes) < now && order.OrderDateTime.AddMinutes(Minutc) > now)
            {

            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:该订单目前不能催单!','250','150','true','2000','true','text');");
                return;
            }

            if (Session[s] == null)
            {
                Hangjing.SQLServerDAL.hurryorder dallho = new hurryorder();
                hurryorderInfo model = dallho.GetModel(Cus[1]);
                if (model.oid != "")//第一次
                {
                    model.oid = Cus[1];
                    model.Name = UserHelp.GetUser().Name;
                    model.addtime = DateTime.Now.ToString();
                    model.ReveInt = 0;
                    model.ReveVar = Cus[2];
                    model.Ccount = 1;


                    if (dallho.Add(model) > 0)
                    {
                        AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','2000','true','text');");
                        BindDate();
                        Session[s] = DateTime.Now.ToString();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作失败!','250','150','true','2000','true','text');");
                    }
                }
                else
                {
                    model.Ccount += 1;
                    model.addtime = DateTime.Now.ToString();
                    if (dallho.Update(model) > 0)
                    {
                        AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','2000','true','text');");
                        BindDate();
                        Session[s] = DateTime.Now.ToString();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作失败!','250','150','true','2000','true','text');");
                    }
                }

            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:您已经催过订单了，请耐心等待!','250','150','true','2000','true','text');");
            }
        }
        #endregion

        #region 再次支付
        if (e.CommandName == "pay")
        {
            CustorderInfo order = dal.GetModel(id);
            if (Hangjing.WebCommon.WebHelper.CanPayAgain(order))
            {
                decimal price = order.OrderSums - order.cardpay - order.promotionmoney;

                //根据原来的支付方式，进行相同支付方式重新支付。 or 可以新选择支付方式 ？
                switch (order.paymode)
                {
                    case 1:
                        {

                            PayOrderLog dalpaylog = new PayOrderLog();
                            string alipaypayorderid = dalpaylog.GetPayBatch(order.orderid);


                            /*********************准备去支付 添加支付日志********************************/
                            PayOrderLogInfo apyinfo = new PayOrderLogInfo();
                            apyinfo.OrderId = order.orderid;
                            apyinfo.AddTime = DateTime.Now;
                            apyinfo.Batch = alipaypayorderid;
                            apyinfo.Price = price;
                            apyinfo.PayType = 0;
                            apyinfo.PayTime = Convert.ToDateTime("1900-1-1");
                            apyinfo.State = 0;
                            apyinfo.PayCallTime = Convert.ToDateTime("1900-1-1");
                            apyinfo.Remark = "";
                            apyinfo.Reve1 = "1";
                            apyinfo.Reve2 = "";
                            dalpaylog.Add(apyinfo);
                            /*********************添加支付日志 over********************************/

                            string show_url = WebUtility.GetConfigsite() + "";
                            AliPayInfo alipa = new AliPayInfo(alipaypayorderid, "orderpay", "woydian", price.ToString(), show_url, "", "");
                            AliPay.Pay(alipa);
                        }

                        break;
                    case 5:
                        {
                            string url = CacheHelper.GetWeiXinAccount().revevar2 + "/weixinpay/nativepay.aspx?orderid=" + order.orderid + "&price=" + price;
                            //string url = "/weixinpay.aspx?orderid=" + order.orderid + "&price=" + price;
                            Response.Redirect(url);
                        }
                        break;
                    default:
                        break;
                }

            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:此订单已经不能继续支付了','250','150','true','2000','true','text');");
            }
        #endregion

        }
    }


    protected void order_bound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lb = (LinkButton)e.Item.FindControl("lbcui");
            LinkButton lbpayagain = (LinkButton)e.Item.FindControl("lbpayagain");
            CustorderInfo model = (CustorderInfo)e.Item.DataItem;
            if (model.OrderStatus == 1)
            {
                if (HjNetHelper.GetQueryInt("states", 0) > 0)
                {

                    lb.Visible = true;
                }
                else
                {
                    lb.Visible = false;
                }

            }
            else
            {
                lb.Visible = false;
            }

            //显示再次支付按钮
            if (Hangjing.WebCommon.WebHelper.CanPayAgain(model))
            {
                lbpayagain.Visible = true;
            }

        }
    }


}
