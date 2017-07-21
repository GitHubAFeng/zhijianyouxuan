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
using System.IO;
using Hangjing.Model;

/// <summary>
/// 提现申请
/// </summary>
public partial class shop_cachoutdetail : System.Web.UI.Page
{
    public string Picture
    {
        get
        {
            return ViewState["Picture"] == null ? "" : ViewState["Picture"].ToString();
        }
        set
        {
            ViewState["Picture"] = (object)value;
        }
    }

    protected string oldname
    {
        get
        {
            object o = ViewState["oldname"];
            return (o == null) ? "" : o.ToString();
        }
        set
        {
            ViewState["oldname"] = value;
        }
    }

    protected string oldpic
    {
        get
        {
            object o = ViewState["oldpic"];
            return (o == null) ? "" : o.ToString();
        }
        set
        {
            ViewState["oldpic"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            initMoney();

        }
    }

    public decimal initMoney()
    {
        CanCashOutInfo model = dal.GetCanCashOutmoney(UserHelp.GetUser_Togo().Unid);
        LitUserBalance.Text = model.AllMoney.ToString();

        lbnouse.Text = model.nousemoney.ToString();
        lbcanuse.Text = model.usemoney.ToString();

        if (model.usemoney <= 0)
        {
            AlertScript.RegScript(this, UpdatePanel1, "alert('当前没有可提现的金额','text:添加成功!','250','150','true','3000','true','text');");
            btSave.Visible = false;
        }

        return model.usemoney;
    }

    TogoAddMoneyLog dal = new TogoAddMoneyLog();


    protected void btSave_Click(object sender, EventArgs e)
    {
        int tid = UserHelp.GetUser_Togo().Unid;


        TogoAddMoneyLogInfo info = new TogoAddMoneyLogInfo();
        decimal point = -Convert.ToDecimal(tbwantmoney.Text);
        info.AddMoney = point;
        info.Inve1 = 0;
        info.UserId = tid;
        info.State = 0;
        info.PayType = 1;
        info.PayDate = DateTime.Now;
        info.PayState = 0;
        info.AddDate = DateTime.Now;
        info.Inve2 = "商户提现";


        if (Math.Abs(info.AddMoney) > initMoney())
        {
            AlertScript.RegScript(this, UpdatePanel1, "alert('当前最多可以提现" + initMoney() + "元','text:添加成功!','250','150','true','3000','true','text');hideload_super();");
            return;
        }




        if (dal.AddModel(info) > 0)
        {
            AlertScript.RegScript(this, UpdatePanel1, "alert('申请成功，请等待管理员处理','text:添加成功!','250','150','true','3000','true','text');gourl('cachoutlist.aspx');");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:操作失败','250','150','true','2000','true','text');");
        }

    }
}
