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

public partial class Admin_abouts_aboutusdetail :AdminPageBase
{
    aboutus dal = new aboutus();
    aboutClass gdal = new aboutClass();

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
       
        if (!Page.IsPostBack)
        {
            SqlWhere = "parentid = 0";
            this.ddlGclass.DataSource = gdal.GetList(100, 1, SqlWhere, "id", 1);
            this.ddlGclass.DataTextField = "name";
            this.ddlGclass.DataValueField = "id";
            this.ddlGclass.DataBind();

            if (HjNetHelper.GetQueryString("id") == "")
            {
                pageType.Text = "添加信息";
            }
            else
            {
                pageType.Text = "编辑信息";
                //控件绑定内容
                aboutusInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
                this.tbtitle.Text = info.Title;
                this.fcContent.Value = info.HelpContent;
                this.ddlGclass.SelectedValue = info.SortId + "";
                tbordernum.Text = info.OrderNum + "";
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        aboutusInfo info = new aboutusInfo();
        info.SortId = Convert.ToInt32(ddlGclass.SelectedValue);
        info.Title = WebUtility.InputText(this.tbtitle.Text);
        info.DataId = HjNetHelper.GetQueryInt("id", 0);
        info.SortId = Convert.ToInt32(ddlGclass.SelectedValue);
        info.HelpContent = fcContent.Value;
        info.AddTime = DateTime.Now;
        info.ViewTimes = 0;
        info.OrderNum = Convert.ToInt32(tbordernum.Text);
        info.KeyWord = 0 + "";
        info.IsVisiableAtHome = true;
        info.IsVisiablePictureAtHome = true;

        if (pageType.Text == "添加信息")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Add(info) > 0)
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
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Update(info) > 0)
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
