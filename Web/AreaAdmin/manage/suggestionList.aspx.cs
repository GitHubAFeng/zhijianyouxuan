using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class AreaAdmin_suggestionList : System.Web.UI.Page
{
    EUserWord dal = new EUserWord();

    protected string SqlWhere
    {
        get
        {
            return (ViewState["sqlWhere"] == null) ? "" : ViewState["sqlWhere"].ToString();
        }
        set
        {
            ViewState["sqlWhere"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = "1=1";
            SqlWhere = WebUtility.GetSql(SqlWhere);
            GetData();
        }

    }

    public void GetData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        rtpNewsList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "DataID", 1);
        rtpNewsList.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetData();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        int i = dal.DelList(IdList);
        if (i > 0)
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

    protected void rtpNewsList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                //判断权限
                int _rs = WebUtility.AreaAdmin_checkOperator(4);
                if (_rs == 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                    return;
                }
                if (dal.DelEUserWord(Id) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
                    GetData();
                }
                else
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
                    GetData();
                }
                break;
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " and username like '%" + WebUtility.InputText(tbKeyword.Text) + "%'";
        }
        if (tbstarttime.Text != "")
        {
            SqlWhere += " and  time > '" + WebUtility.InputText(this.tbstarttime.Text) + "' ";
        }
        if (tbendtime.Text != "")
        {
            SqlWhere += " and  time < '" + WebUtility.InputText(this.tbendtime.Text) + "' ";
        }
        if (this.ddlstar.SelectedValue != "-1")
        {
            SqlWhere += " AND State = " + this.ddlstar.SelectedValue + " ";
        }
        GetData();
    }


    protected void NewsSort_Changed(object sender, EventArgs e)
    {
        GetData();
    }

    protected string getState(object s)
    {
        string rs = "";
        switch (s.ToString())
        {
            case "0":
                rs = "未审核";
                break;
            case "1":
                rs = "审核通过";
                break;
            case "2":
                rs = "审核未通过";
                break;
        }
        return rs;

    }
}
