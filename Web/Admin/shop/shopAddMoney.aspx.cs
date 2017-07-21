using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 手动添加账号记录
/// </summary>
public partial class qy_54tss_Admin_User_shopAddMoney : System.Web.UI.Page
{
    TogoAddMoneyLog dal = new TogoAddMoneyLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            int TogoId = HjNetHelper.GetQueryInt("tid", 0);
            Points e_dal = new Points();
            PointsInfo e_info = e_dal.GetModel(TogoId);
            lbnikename.InnerHtml = e_info.Name;
            lbmoney.InnerHtml = e_info.money.ToString() + "元";
        }
    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        TogoAddMoneyLogInfo info = new TogoAddMoneyLogInfo();
        decimal point = Convert.ToDecimal(tbpoint.Text);
        info.AddMoney = point;
        info.Inve1 = 0;
        info.UserId = HjNetHelper.GetQueryInt("tid", 0);
        info.State = UserHelp.GetAdmin().ID;
        info.PayType = 0;
        info.PayDate = DateTime.Now;
        info.PayState = 1;
        info.AddDate = DateTime.Now;
        info.Inve2 = WebUtility.InputText(tbinve2.Text);

        if (dal.Add(info) > 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:操作成功','250','150','true','2000','true','text');");
            btSave.Visible = false;
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:操作失败','250','150','true','2000','true','text');");
        }

    }
}
