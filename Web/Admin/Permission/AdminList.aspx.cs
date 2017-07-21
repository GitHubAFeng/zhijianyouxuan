using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Admin_AdminList : AdminPageBase
{
    EAdmin dal = new EAdmin();

    IList<RoleInfo> role_list = new List<RoleInfo>();

    Role rbll = new Role();

    protected string SqlWhere
    {
        set
        {
            ViewState["sqlwhere"] = value;
        }
        get
        {
            return ViewState["sqlwhere"] == null ? "" : ViewState["sqlwhere"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //权限判断


        SqlWhere = WebUtility.GetSql(SqlWhere);

        if (!Page.IsPostBack)
        {
            GetData();
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = WebUtility.GetSql(SqlWhere);      
        if (this.tbKeyword.Text != "")
        {
            if (ddlSearchType.SelectedValue == "1")
            {
                SqlWhere = "AdminName like '%" + WebUtility.InputText(this.tbKeyword.Text) + "%'";
            }
            else
            {
                SqlWhere = "RealName like '%" + WebUtility.InputText(this.tbKeyword.Text) + "%'";
            }
        }
        GetData();
    }

    protected string Permistion(object inpara)
    {
        int n = Convert.ToInt32(inpara);
        string outpara = "";
        if (n == 1)
        {
            outpara = "超級管理员";
        }
        else
        {
            outpara = "普通管理员";
        }
        return outpara;
    }

    protected string GetRoleName(string per)
    {
        //Todo 只读取一次
        string rolename = "客服";
        if (per.Trim() == "1")
        {
            rolename = "管理员";
        }
        return rolename;
    }

    protected void rptUserList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {

        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                {
                    //判断权限
                    int _rs = WebUtility.checkOperator(4);
                    if (_rs == 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                        return;
                    }
                    if (dal.Delete(id) > 0)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:刪除成功!','250','150','true','1000','true','text');init();");
                        GetData();
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:刪除失敗!','250','150','true','1000','true','text');init();");
                        GetData();
                    }
                }
                break;
        }
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hdDels.Value;
        if (dal.Delete(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            GetData();
        }
    }

    protected void GetData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);

        this.rtpUserlist.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere);
        this.rtpUserlist.DataBind();

        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void AspNetPager1_OnPageChanged(object sender, EventArgs e)
    {
        GetData();
    }
}
