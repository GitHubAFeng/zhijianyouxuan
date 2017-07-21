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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.DBUtility;
using System.Collections.Generic;

public partial class Admin_User_giveCardUsers : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();

    /// <summary>
    /// 用户条件
    /// </summary>
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

    /// <summary>
    /// 订单时间范围
    /// </summary>
    private string orderwhere
    {
        get
        {
            object o = ViewState["orderwhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["orderwhere"] = value;
        }
    }

    /// <summary>
    /// 订单次数范围
    /// </summary>
    private string otherwhere
    {
        get
        {
            object o = ViewState["otherwhere"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["otherwhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1";
            orderwhere = " OrderStatus = 3 ";
            otherwhere = " 1=1 ";


            WebUtility.BindList("dataid", "title", new batshopcard().GetList(5, 1, "1=1", "dataid", 1), ddlbatcard);

        }
    }

    protected void BindData()
    {
        IList<ECustomerInfo> usrs = dal.giveCardUsers(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, orderwhere, otherwhere);
        this.AspNetPager1.RecordCount = 0;
        if (usrs.Count > 0)
        {
            this.AspNetPager1.RecordCount = usrs[0].num;
        }
        this.rptCustomerList.DataSource = usrs;
        this.rptCustomerList.DataBind();
        AlertScript.RegScript(this.Page, UpdatePanel1, "init();$('#loading-mask').hide();");
    }




    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1= 1 ";

        if (this.tb_Name.Text.Trim() != "")
        {
            SqlWhere += " AND Name LIKE '%" + WebUtility.InputText(this.tb_Name.Text.Trim()) + "%' ";
        }
        if (this.tb_Point.Text.Trim() != "")
        {
            SqlWhere += " AND Point " + this.ddl_Operator.SelectedValue + " " + WebUtility.InputText(this.tb_Point.Text.Trim()) + " ";
        }
        if (this.tb_Email.Text.Trim() != "")
        {
            SqlWhere += " AND Email LIKE '%" + WebUtility.InputText(this.tb_Email.Text.Trim()) + "%' ";
        }
        if (this.tb_Start.Text != "")
        {
            SqlWhere += " AND RegTime >= '" + this.tb_Start.Text + "' ";
        }
        if (this.tb_End.Text != "")
        {
            SqlWhere += " AND RegTime <= '" + this.tb_End.Text + "' ";
        }
        if (this.tb_Phone.Text.Trim() != "")
        {
            SqlWhere += " AND tell LIKE '%" + WebUtility.InputText(this.tb_Phone.Text) + "%' ";
        }
        if (this.tb_UserName.Text.Trim() != "")
        {
            SqlWhere += " AND TrueName LIKE '%" + WebUtility.InputText(this.tb_UserName.Text.Trim()) + "%' ";
        }
        if (this.tbDataID.Text.Trim() != "")
        {
            SqlWhere += " AND DataID =" + tbDataID.Text;
        }
        if (ddlsex.SelectedValue != "-1")
        {
            SqlWhere += " AND sex = '" + ddlsex.SelectedValue + "'";
        }
        if (this.tb_userMoney.Text.Trim() != "")
        {
            SqlWhere += " AND userMoney " + this.ddl_usermoney.SelectedValue + " " + WebUtility.InputText(this.tb_userMoney.Text.Trim()) + " ";
        }
        if (ddlstate.SelectedValue != "-1")
        {
            SqlWhere += " AND state = '" + ddlstate.SelectedValue + "' ";
        }
        if (ddlPayPWDAnswer.SelectedValue != "-1")
        {
            SqlWhere += " AND PayPWDAnswer = '" + ddlPayPWDAnswer.SelectedValue + "' ";
        }

        orderwhere = " OrderStatus = 3 ";
        if (this.tborderStart.Text != "")
        {
            orderwhere += " AND OrderDateTime >= '" + this.tborderStart.Text + "' ";
        }
        if (this.tborderEnd.Text != "")
        {
            orderwhere += " AND OrderDateTime <= '" + this.tborderEnd.Text + "' ";
        }


        otherwhere = " 1=1 ";
        if (this.tbordertimes.Text.Trim() != "")
        {
            otherwhere += " AND ordercount " + this.ddlordercheck.SelectedValue + " " + WebUtility.InputText(this.tbordertimes.Text.Trim()) + " ";
        }



        BindData();

    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }


    protected void send_Click(object sender, EventArgs e)
    {
        if (Session["CheckCode"] == null || Session["CheckCode"].ToString().ToLower() != this.tbvcode.Text.ToLower())
        {
            WebUtility.RegScript(this, UpdatePanel1, "alert('验证码错误!','error');;hideload_super();$('#codepic').click();");
            return;
        }


        IList<ECustomerInfo> usrs = dal.giveCardUsers(9999, 1, SqlWhere, orderwhere, otherwhere);

        int adminid = UserHelp.GetAdmin().ID;
        batshopcardInfo cardconfig = new batshopcard().GetModel(Convert.ToInt32(ddlbatcard.SelectedValue));

        ShopCard dalcard = new ShopCard();

        IList<ShopCardInfo> cards = new List<ShopCardInfo>();
        foreach (var item in usrs)
        {


            ShopCardInfo model = new ShopCardInfo();
            model.AddDate = DateTime.Now;
            model.batid = cardconfig.DataID;
            model.cardnum = "";

            string ckey = item.DataID.ToString("0000")+ Guid.NewGuid().ToString().Substring(0,12);
            model.ckey = ckey.Substring(0, 4) + "-" + ckey.Substring(4, 4) + "-" + ckey.Substring(8, 4);
            model.cmoney = cardconfig.point;
            model.State = 1;
            model.canday = 0;
            model.Inve1 = adminid;
            model.Inve2 = "1";
            model.UserID = item.DataID;
            model.username = item.Name ;
            model.ReveInt = cardconfig.mtype;
            model.ReveVar = "";
            model.isbuy = 0;
            model.buyuid = 0;
            model.isused = 0;
            model.timelimity = cardconfig.timelimity;
            model.starttime = cardconfig.starttime;
            model.endtime = cardconfig.endtime;
            model.moneylimity = cardconfig.moneylimity;
            model.moneyline = cardconfig.moneyline;
            model.ReveInt1 = cardconfig.Inve1;
            model.Point = cardconfig.point;
            model.ReveVar1 = "";
            model.usergettime = DateTime.Now;

            dalcard.Add(model);

        }

        DataTable dt = CollectionHelper.ConvertTo(cards, "ShopCard");
        SQLHelper.SqlBulkCopyData(dt);

        WebUtility.RegScript(this, UpdatePanel1, "alert('发送完成!','error');hideload_super();$('#codepic').click();");

    }

}

