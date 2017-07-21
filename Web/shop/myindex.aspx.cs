using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class shop_myindex : System.Web.UI.Page
{
    Points TogoDal = new Points();
    Custorder OrderDal = new Custorder();
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

    private void NoRecord()
    {
        if (rptCollect.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            GetData();
        }
    }
    protected void GetData()
    {
        PointsInfo model = UserHelp.GetUser_Togo();

        if (model != null)
        {

         
            SqlWhere = "TogoId=" + model.Unid+ "  and (paymode = 4 OR paystate = 1)";
            Info = model;
            BindDate();
        }
    }

    private PointsInfo Info
    {
        get
        {
            PointsInfo model = new PointsInfo();
            model.Unid = UserHelp.GetUser_Togo().Unid;
            return model;
        }
        set
        {
            this.LitUserName.Text = value.LoginName;
            this.litRegTime.Text = value.InTime.ToString();
            this.LitShopName.Text = value.Name.ToString();
            string sql = "TogoId=" + value.Unid + "  and (paymode = 4 OR paystate = 1)";
            this.LitOrderCount.Text = OrderDal.GetCount(sql)+"";
            this.LitAllCount.Text = value.reviewtimes.ToString();
            this.LitUserBalance.Text = value.money.ToString();
        }
    }

    private void BindDate()
    {
        SqlWhere += " and OrderDateTime >'" + DateTime.Now.ToShortDateString() + "' and OrderDateTime <'" + DateTime.Now.ToString() + "'";
        this.AspNetPager1.RecordCount = OrderDal.GetCount(SqlWhere);
        this.rptCollect.DataSource = OrderDal.GetListFix(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "custorder.Unid", 1);
        this.rptCollect.DataBind();
        NoRecord();
    }

    protected void rptproduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string s = e.CommandArgument.ToString();
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        string userid = UserHelp.GetUser().DataID.ToString();
        switch (e.CommandName)
        {
            case "del":
                /*if (dal.DelETogoCollect(id) == 1)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
                    BindDate();

                }
                else
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
                }*/
                break;
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindDate();
    }
}
