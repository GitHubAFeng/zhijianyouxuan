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

// 商家餐品信息管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息
// 2010-07-12

public partial class AreaAdmin_Shop_getmoney : System.Web.UI.Page
{
    Points dal = new Points();
    Custorder dalorder = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            PointsInfo model = dal.GetModel(HjNetHelper.GetQueryInt("tid", 0));
            h4title.InnerHtml = model.Name + " - 交易额统计";
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        string sql = " togoid =  " + HjNetHelper.GetQueryInt("tid", 0);
        string tid = HjNetHelper.GetQueryInt("tid", 0).ToString();
        DateTime time1 = Convert.ToDateTime("2000-01-01");
        if (tb_Start.Text.Trim() != "")
        {
            sql += " and ordertime > '" + Convert.ToDateTime(tb_Start.Text) + "'";
        }
        DateTime time2 = Convert.ToDateTime("2099-01-01");
        if (tb_End.Text.Trim() != "")
        {
            sql += " and ordertime < '" + tb_End.Text + " 23:59:59'";
        }
        if (ddlOrderState.SelectedValue != "-2")
        {
            sql += " and state =  " + Convert.ToInt32(ddlOrderState.SelectedValue);
        }
        CustorderInfo model = dalorder.GetCountAndTotal1(sql);
        lborder.InnerText = model.OrderCount.ToString();
        lbcount.InnerText = model.OrderTotal.ToString();
    }

    protected void goshop_Click(object seneer, EventArgs e)
    {
        Response.Redirect("ShopDetail.aspx?id=" + HjNetHelper.GetQueryInt("tid", 0));
    }
}
