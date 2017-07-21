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
using Hangjing.Model;
using Hangjing.Common;

public partial class Admin_shopaccount_TogoAddMoneyLogDetail :AdminPageBase
{
    TogoAddMoneyLog dal = new TogoAddMoneyLog();
    TogoAddMoneyLogInfo info = new TogoAddMoneyLogInfo();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("D");
        if (!Page.IsPostBack)
        {
            BindDate();
        }
    }
    private void BindDate() 
    {
        SqlWhere = " 1=1 ";
        this.ddlState.DataSource = dal.GetList(100, 1, SqlWhere, "UserId", 1);
        this.ddlState.DataTextField = "State";
        this.ddlState.DataValueField = "UserId";
        //this.ddlState.DataBind();
        this.ddlPayType.DataTextField = "PayType";
        this.ddlPayType.DataValueField = "UserId";
        this.ddlPayType.DataBind();
        this.ddlPayState.DataTextField = "PayState";
        this.ddlPayState.DataValueField = "UserId";
        this.ddlPayState.DataBind();
        if (HjNetHelper.GetQueryString("id") == "")
        {
            pageType.Text = "添加商家记录信息";
        }
        else
        {
            pageType.Text = "编辑商家记录信息";
            int id = HjNetHelper.GetQueryInt("Id", 0);
            info = dal.GetModel(id);
            this.tbDataid.Text = HjNetHelper.GetQueryInt("Id", 0).ToString();
            this.tbTogoName.Text = info.TogoName;
            this.tbAddMoney.Text = Convert.ToString(info.AddMoney);
            this.ddlState.SelectedValue = Convert.ToString(info.State);
            this.ddlPayType.SelectedValue = Convert.ToString(info.PayType);
            this.tbAdddate.Text = Convert.ToString(info.PayDate);
            this.ddlPayState.SelectedValue = Convert.ToString(info.PayState);
            this.tbNewAdddate.Text = Convert.ToString(info.AddDate);
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        TogoAddMoneyLogInfo userInfo = new TogoAddMoneyLogInfo();
        userInfo.UserId = Convert.ToInt32(WebUtility.InputText(tbDataid.Text));
        userInfo.TogoName = WebUtility.InputText(tbTogoName.Text);
        userInfo.AddMoney = Convert.ToDecimal(WebUtility.InputText(tbAddMoney.Text.ToString()));
        userInfo.State = Convert.ToInt32(WebUtility.InputText(ddlState.SelectedValue));
        userInfo.PayType = Convert.ToInt32(WebUtility.InputText(ddlPayType.SelectedValue));
        userInfo.PayDate = Convert.ToDateTime(WebUtility.InputText(tbAdddate.Text));
        userInfo.PayState = Convert.ToInt32(WebUtility.InputText(ddlPayState.SelectedValue));
        userInfo.AddDate = Convert.ToDateTime(WebUtility.InputText(tbAdddate.Text).ToString());

        if (pageType.Text == "添加商家记录信息")
        {
            if (dal.Add(userInfo) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);init();");
            }
        }
        else
        {
            userInfo.TogoName = Convert.ToString(HjNetHelper.GetQueryString("name"));
            userInfo.UserId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));
            if (dal.Update(userInfo) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);init();");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);init();");
            }
        }
    }
}
