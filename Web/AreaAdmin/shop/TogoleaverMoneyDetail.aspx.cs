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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class AreaAdmin_shop_TogoleaverMoney : AdminPageBase
{
    Points bll = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("A");
        if (!Page.IsPostBack)
        {
            if (HjNetHelper.GetQueryString("tid") != "")
            {
                this.tbDataId.Text = HjNetHelper.GetQueryInt("tid", 0).ToString();
                PointsInfo info = bll.GetModel(HjNetHelper.GetQueryInt("tid", 0));
                this.tboldmoney.Text = info.money.ToString();
            }
            else
            {
                Response.Redirect("ShopList.aspx");
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {

        if (ddlfunction.SelectedValue == "增加")
        {
            string money = (Convert.ToDecimal(this.tboldmoney.Text) + Convert.ToDecimal(this.tbmoney.Text)) + "";
          int DataID = HjNetHelper.GetQueryInt("tid", 0);
           if (bll.UpdateMoney("money ="+money, DataID) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作成功','250','150','true','','true','text');");
               // Response.Redirect("ShopList.aspx");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作失败','250','150','true','','true','text');");
            }
        }
        else
        {
            string money = (Convert.ToDecimal(this.tboldmoney.Text) - Convert.ToDecimal(this.tbmoney.Text)) + "";
            int DataID = HjNetHelper.GetQueryInt("tid", 0);
            if (bll.UpdateMoney("money =" + money, DataID) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作成功','250','150','true','','true','text');");
                //Response.Redirect("ShopList.aspx");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:操作失败','250','150','true','','true','text');");
            }
        }
    }
}
