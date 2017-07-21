using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class shop_SetStatus : System.Web.UI.Page
{
    Points dal = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);

            PointsInfo model = dal.GetModel(UserHelp.GetUser_Togo().Unid);

            ddlStatus.SelectedValue = model.Status.ToString();
            tbAlert.Text = model.outnitice;
        }

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        Points dal = new Points();

        if (dal.UpateStatus(UserHelp.GetUser_Togo().Unid, Convert.ToInt32(ddlStatus.SelectedValue), WebUtility.InputText(tbAlert.Text)) > 0)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息','text:更新成功!','250','150','true','2000','true','text');");
        }
        else
        {
            AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息','text:更新失败!','250','150','true','2000','true','text');");
        }
    }
}
