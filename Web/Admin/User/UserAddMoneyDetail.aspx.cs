using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class qy_54tss_Admin_User_UserAddMoneyDetail : System.Web.UI.Page
{
    UserAddMoneyLog dal = new UserAddMoneyLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            ECustomer e_dal = new ECustomer();
            ECustomerInfo e_info = e_dal.GetModel(Convert.ToInt32(Request["uid"]));
            lbnikename.InnerHtml = e_info.Name;
            lbmoney.InnerHtml = e_info.Usermoney.ToString() + "元";
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
        UserAddMoneyLogInfo model = null;
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
        UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();
        decimal point = Convert.ToDecimal(tbpoint.Text);
        info.AddMoney = point;
        info.Inve1 = UserHelp.GetAdmin().ID;
        info.UserId = HjNetHelper.GetQueryInt("uid", 0);
        info.State = 0;
        info.PayType = 1;
        info.PayDate = DateTime.Now;
        info.PayState = 1;
        info.AddDate = DateTime.Now;
        info.Inve2 = WebUtility.InputText(tbspecial.Text);


        if (pageType.Text == "新增充值记录")
        {
            if (dal.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:充值成功','250','150','true','2000','true','text');");
                string sql = "update ecustomer set userMoney = userMoney +" + info.AddMoney + " where dataid=" + info.UserId;
                WebUtility.excutesql(sql);
                btSave.Visible = false;
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:充值失败','250','150','true','2000','true','text');");
            }
        }
    }
}
