using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class qy_54tss_Admin_User_UserDistributionDetail : System.Web.UI.Page
{
    UserDistributionLog dal = new UserDistributionLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            ECustomer e_dal = new ECustomer();
            ECustomerInfo e_info = e_dal.GetModel(Convert.ToInt32(Request["uid"]));
            lbnikename.InnerHtml = e_info.Name;
            lbmoney.InnerHtml = e_info.GroupID.ToString() + "元";
            if (Request["id"] != null)
            {
                pageType.Text = "查看充值记录";
                GetData();
            }
            else
            {
                pageType.Text = "新增充值记录";
            }
        }
    }

    protected void GetData()
    {
        int id = HjNetHelper.GetQueryInt("id", 0);
        UserDistributionLogInfo model = null;
        model = dal.GetModel(id);
        if (model != null)
        {
            this.lbnikename.InnerHtml = model.UserName;
            this.tbpoint.Text = model.AddMoney.ToString();
            tbpoint.ReadOnly = true;

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        UserDistributionLogInfo info = new UserDistributionLogInfo();
        decimal point = Convert.ToDecimal(tbpoint.Text);
        info.AddMoney = point;
        info.Inve1 = UserHelp.GetAdmin().ID;
        info.UserId = HjNetHelper.GetQueryInt("uid", 0);
        info.State = 0;
        info.PayType = 0;
        info.PayDate = DateTime.Now;
        info.PayState = 1;
        info.AddDate = DateTime.Now;
        info.Inve2 = WebUtility.InputText(tbspecial.Text);

        dal.AddMoney(info);
        AlertScript.RegScript(this.Page, UpdatePanel1, "alert('充值成功','text:','250','150','true','2000','true','text');gourl('UserDistributionList.aspx?uid=" + info.UserId + "')");



    }
}
