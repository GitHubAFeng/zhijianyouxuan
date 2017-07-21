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

public partial class user_myindex : System.Web.UI.Page
{
    Custorder dal = new Custorder();
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

    public string OrderStatus(object os)
    {
        int Status = Convert.ToInt32(os);
        switch (Status)
        {
            case 1:
                return "初定";
                break;
            case 2:
                return "已定";
                break;
            case 3:
                return "已发";
                break;
            case 4:
                return "已收";
                break;
            case 5:
                return "已送达";
                break;
            default:
                return "失败";
                break;
        }
    }

    public string Pay(object p)
    {

        int pay = Convert.ToInt32(p);
        switch (pay)
        {
            case 1:
                return "支付宝";
                break;
            case 2:
                return "银联";
                break;
            case 3:
                return "账户余额";
                break;
            case 4:
                return "货到付款";
                break;
            default:
                return "失败";
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            ECustomerInfo user = UserHelp.GetUser();
            SqlWhere = "UserId=" + user.DataID.ToString();
            GetData();
            BindDate();
            imgmypic.Src = WebUtility.ShowPic(user.Picture);
        }
    }

    private ECustomerInfo Info
    {
        get
        {
            ECustomerInfo model = new ECustomerInfo();
            model.DataID = UserHelp.GetUser().DataID;
            return model;
        }
        set
        {
            this.LitUserName.Text = value.Name;
            this.litRegTime.Text = value.RegTime.ToString();
            this.LitLoginInt.Text = value.GroupID + "";
            string sql = "OrderStatus = 3 and UserId=" + value.DataID;
            this.LitOrder.Text = dal.GetCount(sql) + "";
            this.LitPoint.Text = value.Point.ToString();
            this.LitUserBalance.Text = value.Usermoney.ToString();

        }
    }

    private void BindDate()
    {
        SqlWhere += " and OrderDateTime >'" + DateTime.Now.ToShortDateString() + "' and OrderDateTime <'" + DateTime.Now.ToString() + "'";
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        this.rptCollect.DataSource = dal.GetListFix(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "custorder.Unid", 1);
        this.rptCollect.DataBind();
        NoRecord();
    }

    private void GetData()
    {
        ECustomer daluser = new ECustomer();
        ECustomerInfo model = UserHelp.GetUser();
        model = daluser.GetModelByNameAPassword(model.Name, model.Password);
        UserHelp.SetLogin(model);
        if (model.EMAIL != null)
        {
            Info = model;
        }
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
