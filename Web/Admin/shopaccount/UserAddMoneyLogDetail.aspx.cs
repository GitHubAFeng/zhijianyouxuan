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

public partial class Admin_shopaccount_UserAddMoneyLogDetail : AdminPageBase
{
    UserAddMoneyLog dal = new UserAddMoneyLog();
    UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();
    ECustomer edal = new ECustomer();

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
        if(!Page.IsPostBack)
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
            pageType.Text = "添加用户记录信息";
        }
        else
        {
            pageType.Text = "编辑用户记录信息";
            UserAddMoneyLogInfo info = dal.GetModel(HjNetHelper.GetQueryInt("id",0));
            this.tbDataid.Text = HjNetHelper.GetQueryString("id");
            this.tbAddMoney.Text = info.AddMoney.ToString();
            this.ddlState.SelectedValue = Convert.ToString(info.State);
            this.ddlPayType.SelectedValue = Convert.ToString(info.PayType);
            this.tbAdddate.Text = Convert.ToString(info.PayDate);
            this.ddlPayState.SelectedValue = Convert.ToString(info.PayState);
            this.tbNewAdddate.Text = Convert.ToString(info.AddDate);
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("uid")))
            {
                int uid = HjNetHelper.GetQueryInt("uid", 0);
                ECustomerInfo model = edal.GetModel(uid);
                if (model != null)
                {
                    this.tbTogoName.Text = model.Name.ToString();
                }
            }
           
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        UserAddMoneyLogInfo userInfo = new UserAddMoneyLogInfo();
        userInfo.UserId = HjNetHelper.GetQueryInt("uid",0);
        userInfo.AddMoney = Convert.ToDecimal(WebUtility.InputText(tbAddMoney.Text.ToString()));
        userInfo.State = Convert.ToInt32(WebUtility.InputText(ddlState.SelectedValue));
        userInfo.PayType = Convert.ToInt32(WebUtility.InputText(ddlPayType.SelectedValue));
        userInfo.PayDate = Convert.ToDateTime(WebUtility.InputText(tbAdddate.Text));
        userInfo.PayState = Convert.ToInt32(WebUtility.InputText(ddlPayState.SelectedValue));
        userInfo.AddDate = Convert.ToDateTime(WebUtility.InputText(tbAdddate.Text).ToString());
        userInfo.Inve1 = 0;
        userInfo.Inve2 = "";

        if (pageType.Text == "添加用户记录信息")
        {
            tbTogoName.Visible = false;
            if (dal.Add(userInfo) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);");
            }
        }
        else 
        {
           // userInfo.TogoName = Convert.ToString(HjNetHelper.GetQueryString("name"));
            userInfo.DataId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));
            if (dal.Update(userInfo) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);");
            }
            else 
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);");
            }
        }
    }
}
