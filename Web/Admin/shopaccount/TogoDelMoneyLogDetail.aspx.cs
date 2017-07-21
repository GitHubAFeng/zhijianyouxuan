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

public partial class Admin_shopaccount_TogoDelMoneyLogDetail :AdminPageBase
{
    TogoDelMoneyLog dal = new TogoDelMoneyLog();
    TogoDelMoneyLogInfo info = new TogoDelMoneyLogInfo();

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
        if (HjNetHelper.GetQueryString("id") == "")
        {
            pageType.Text = "商家消费记录信息";
        }
        else
        {
            pageType.Text = "编辑商家记录信息";
            int id = Convert.ToInt32(HjNetHelper.GetQueryInt("Id",0));
            info = dal.GetModel(id);
            info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
            this.tbDataid.Text = HjNetHelper.GetQueryInt("Id", 0).ToString();
            this.tbTogoName.Text = info.TogoName;
            this.tbDelMoney.Text = Convert.ToString(info.DelMoney);
            this.tbBuyItem.Text = Convert.ToString(info.BuyItem);
            this.tbNewAdddate.Text = Convert.ToString(info.AddDate);
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        TogoDelMoneyLogInfo userInfo = new TogoDelMoneyLogInfo();
        userInfo.UserId = Convert.ToInt32(WebUtility.InputText(tbDataid.Text));
        userInfo.TogoName = WebUtility.InputText(tbTogoName.Text);
        userInfo.DelMoney = Convert.ToDecimal(WebUtility.InputText(tbDelMoney.Text.ToString()));
        userInfo.BuyItem = WebUtility.InputText(tbBuyItem.Text);
        userInfo.AddDate = Convert.ToDateTime(WebUtility.InputText(tbNewAdddate.Text).ToString());

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
