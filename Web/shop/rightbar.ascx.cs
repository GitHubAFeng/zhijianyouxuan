using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class shop_rightbar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PointsInfo model = UserHelp.GetUser_Togo();
        Custorder OrderDal = new Custorder();
        IList<PointsInfo> list = new Points().GetDistanceListSuper(1, 1, "Unid=" + model.Unid, "unid", 1, "0", "0", "1=1");
        this.rptTogo.DataSource = list;
        this.rptTogo.DataBind();


        string sql = "TogoId=" + model.Unid + "  and (paymode = 4 OR paystate = 1)";
        sql += " and DATEDIFF(day,OrderDateTime,GETDATE()) =0 ";

        CustorderInfo ordercount = new Custorder().SiteIncomeStatistics(sql);

        noworder.InnerText = ordercount.OrderCount.ToString();
        salemoney.InnerText = ordercount.OrderSums.ToString();
        nowmoney.InnerText = ordercount.shopdiscountmoney.ToString();

        model = new Points().GetModel(model.Unid);
        mymoney.InnerText = model.money.ToString();


    }
    public string shopdate(string time)
    {
        string rs = "<span class=\"current\">营业中</span><span>休息</span>";
        if (time == "0")
        {
            rs = "<span>营业中</span><span class=\"current\">休息</span>";
        }
        return rs;
    }
}