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
using Hangjing.Model;

public partial class Admin_User_Userleavermoney :AdminPageBase
{
    ECustomer bll = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("B");
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("id")))
            {
                int id = HjNetHelper.GetQueryInt("id", 0);
                ECustomerInfo info = bll.GetModel(id);
                this.tbDataId.Text = id.ToString();
                this.tboldtbmoney.Text = info.Usermoney.ToString();
            }
            else
            {
                Response.Redirect("~/Admin/User/UserList.aspx");
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        if (ddlfunction.SelectedValue == "增加")
        {           
            int dataid = HjNetHelper.GetQueryInt("id", 0);
            string usermoney = (Convert.ToDecimal(this.tboldtbmoney.Text) + Convert.ToDecimal(this.tbmoney.Text)) + "";
            if (bll.UpdateMoney("userMoney =" + usermoney, dataid) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作成功','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作失败','250','150','true','','true','text');");
            }
        }
        else
        {
            int dataid = HjNetHelper.GetQueryInt("id", 0);
            string usermoney = (Convert.ToDecimal(this.tboldtbmoney.Text) - Convert.ToDecimal(this.tbmoney.Text)) + "";
            if (bll.UpdateMoney("userMoney =" + usermoney, dataid) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作成功','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作失败','250','150','true','','true','text');");
            }
        }
     
    }
}
