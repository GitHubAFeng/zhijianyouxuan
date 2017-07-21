using System;
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
using Hangjing.DBUtility;
using System.IO;
using MVCNPOIHelper.Common;

/// <summary>
///代客下单统计
/// </summary>
public partial class qy_54tss_Admin_Sale_valetorder : System.Web.UI.Page
{
    /// <summary>
    /// 管理员条件
    /// </summary>
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

    /// <summary>
    /// 订单条件
    /// </summary>
    private string orderWhere
    {
        get
        {
            object o = ViewState["orderWhere"];
            return (o == null) ? "" : Convert.ToString(o);
        }
        set
        {
            ViewState["orderWhere"] = value;
        }
    }

    /// <summary>
    /// 排序字段
    /// </summary>
    protected string ordersort
    {
        get
        {
            object o = ViewState["ordersort"];
            return (o == null) ? "ordercount" : Convert.ToString(o);
        }
        set
        {
            ViewState["ordersort"] = value;
        }
    }

    Deliver bll = new Deliver();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "  1=1 ";
            orderWhere = "  1=1 and (paymode = 4 or paystate = 1) ";

            hfyestoday.Value = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            hftoday.Value = DateTime.Now.ToString("yyyy-MM-dd");

            tbStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tbEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");

            new OrderState().BindOrderState(rblstate);

            InitSqlwhere();
            BindData();
        }
    }

    /// <summary>
    /// 绑定指定的数据
    /// </summary>
    protected void BindData()
    {
        IList<TogoInfo> shops = bll.SendOrderStatistics(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, orderWhere, ordersort);
        if (shops.Count > 0)
        {
            this.AspNetPager1.RecordCount = shops[0].DataID;
        }

        string sql = orderWhere;
        if (tbshopname.Text != "")
        {
            sql += " and EXISTS(SELECT dataid FROM dbo.Deliver WHERE dataid = Custorder.deliverid AND Name LIKE  '%" + Utils.RegEsc(WebUtility.InputText(this.tbshopname.Text.Trim())) + "%'  )";
        }
        string ids = WebUtility.GetcheckStr(rblstate);
        if (ids != "")
        {
            sql += " and OrderStatus  in (" + ids + ")";
        }

        CustorderInfo count = new Custorder().GetCountAndTotal1(sql);

        lbordercount.InnerText = count.OrderCount.ToString();
        lballprice.InnerText = count.OrderTotal.ToString();

        this.rptsum.DataSource = shops;
        this.rptsum.DataBind();

        AlertScript.AjaxRegisterScript("init();", UpdatePanel1);
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        InitSqlwhere();
        BindData();
    }

    /// <summary>
    /// 生成查询条件
    /// </summary>
    protected void InitSqlwhere()
    {
        SqlWhere = "  1=1 ";

        orderWhere = " 1=1 and (paymode = 4 or paystate = 1) ";
        if (this.tbStartTime.Text != "")
        {
            orderWhere += " and  [OrderDateTime] > '" + WebUtility.InputText(this.tbStartTime.Text) + "' ";
        }
        if (this.tbEndTime.Text != "")
        {
            orderWhere += " and OrderDateTime < '" + WebUtility.InputText(this.tbEndTime.Text) + " 23:59:59'";
        }
        string ids = WebUtility.GetcheckStr(rblstate);
        if (ids != "")
        {
            orderWhere += " and OrderStatus  in (" + ids + ")";
        }

        if (tbshopname.Text != "")
        {
            SqlWhere += " and Name like '%" + Utils.RegEsc(WebUtility.InputText(this.tbshopname.Text.Trim())) + "%' ";
        }

       
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void out_Click(object sender, EventArgs e)
    {

        InitSqlwhere();
        var models = new List<TogoInfo>();
        IList<TogoInfo> shops = bll.SendOrderStatistics(999, 1, SqlWhere, orderWhere, ordersort);
        models.AddRange(shops);

        Stream stream = null;

        string dirurl = Hangjing.WebCommon.WebHelper.CreateDirectoryByMonth(Context);
        string filepath = dirurl + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls";

        var excelColumns = ExcelColumns.Create(Server.MapPath("~/App_Data/DeliverSettleImport.xml"), "root/export/row");
        if (models.Count < 36000)
        {
            stream = NpoiExcelHelper.RenderToExcelIList(models, excelColumns, NpoiExcelHelper.ExcelType.Excel03);
        }

        else
        {
            stream = NpoiExcelHelper.RenderToExcelIList(models, excelColumns, NpoiExcelHelper.ExcelType.Excel07);

        }

        StreamWriter sw = new StreamWriter(Server.MapPath(filepath));
        stream.CopyTo(sw.BaseStream);
        sw.Flush();
        sw.Close();

        Response.Redirect(filepath.Replace("~", ""));

    }


}
