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

public partial class Admin_AdminDetail : System.Web.UI.Page
{
    EAdmin dal = new EAdmin();
    protected int adminStatus
    {
        set
        {
            ViewState["adminStatus"] = value;
        }
        get
        {
            return ViewState["adminStatus"] == null ? 0 : Convert.ToInt32(ViewState["adminStatus"]);
        }
    }

    protected DateTime lastAccess
    {
        set
        {
            ViewState["lastAccess"] = value;
        }
        get
        {
            return ViewState["lastAccess"] == null ? DateTime.Now : Convert.ToDateTime(ViewState["lastAccess"]);
        }
    }

    protected string password
    {
        set
        {
            ViewState["password"] = value;
        }
        get
        {
            return ViewState["password"] == null ? "" : ViewState["password"].ToString();
        }
    }

    protected EAdminInfo info
    {
        set
        {
            this.tbName.Text = value.AdminName;
            WebUtility.SelectValue(stpermition, value.Permission.Trim());
            WebUtility.SelectValue(ddlroot, value.root.ToString());
            this.tbRealname.Text = value.RealName;
            lbNotice.InnerHtml = "<font color='red'>不修改密码输入框留空即可.</font>";
            adminStatus = value.AdminStatus;
            lastAccess = value.LastAccess;
            password = value.AdminPassword;
            WebUtility.SelectValue(ddlRem, value.Rem);
            WebUtility.SelectValue(DDLArea, value.CityID.ToString());
        }
        get
        {
            EAdminInfo model = new EAdminInfo();
            model.ID = HjNetHelper.GetQueryInt("id", 0);
            model.AdminName = WebUtility.InputText(this.tbName.Text, 20);
            model.RealName = WebUtility.InputText(this.tbRealname.Text, 20);
            model.Rem = ddlRem.SelectedValue;
            model.AdminStatus = adminStatus;
            model.LastAccess = lastAccess;
            model.Permission = stpermition.SelectedValue;
            model.CityID = Convert.ToInt32(DDLArea.SelectedValue);
            //model.root = Convert.ToInt32(ddlroot.SelectedValue);
            model.root = 0;
            if (this.tbPassword.Text != "")
            {
                model.AdminPassword = WebUtility.GetMd5(WebUtility.InputText(this.tbPassword.Text));
            }
            else
            {
                model.AdminPassword = password;
            }
            return model;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            //角色
            WebUtility.BindList("RoleID", "R_RoleName", new sys_Roles().getall("1=1"), ddlRem);
            WebUtility.BindList("cid", "cname", SectionProxyData.GetCityList(), DDLArea);
            if (Request["id"] != null)
            {
                getAdminData();
                pageType.Text = "编辑管理员";
            }
            else
            {
                pageType.Text = "添加管理员";
            }
        }
    }

    protected void getAdminData()
    {
        EAdminInfo model = null;
        int id = HjNetHelper.GetQueryInt("id", 0);
        model = dal.GetModel(id);
        if (model != null)
        {
            info = model;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        if (this.tbPassword.Text.Trim() != this.tbConfirm.Text.Trim())
        {
            AlertScript.RegScript(this.Page,UpdatePanel1, "tipsWindown('提示信息','text:密码两次输入不一致!','250','150','true','1000','true','text');");
            return;
        }
        EAdminInfo model = info;
        if (Request["id"] == null)
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Add(model)>0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:保存成功!','250','150','true','1000','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:保存失败!','250','150','true','1000','true','text');");
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
            if (dal.Update(model) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:保存成功!','250','150','true','1000','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:保存失败!','250','150','true','1000','true','text');");
            }
        }
    }
}
