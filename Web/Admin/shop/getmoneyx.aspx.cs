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

public partial class Admin_Shop_getmoneyx : System.Web.UI.Page
{
    Points dal = new Points();
    Custorder dalorder = new Custorder();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            h4title.InnerHtml = "网站交易额统计";
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        string sql = "1=1 ";
        if (tb_Start.Text.Trim() != "")
        {
            sql += " and OrderDateTime > '" + Convert.ToDateTime(tb_Start.Text) + "'";
        }
        if (tb_End.Text.Trim() != "")
        {
            sql += " and OrderDateTime < '" + tb_End.Text + " 23:59:59'";
        }
        if (ddlOrderState.SelectedValue != "-2")
        {
            sql += " and OrderStatus =  " + Convert.ToInt32(ddlOrderState.SelectedValue);
        }
        CustorderInfo model = dalorder.GetCountAndTotal1(sql);
        lborder.InnerText = model.OrderCount.ToString();
        lbcount.InnerText = model.OrderTotal.ToString();
    }

}
