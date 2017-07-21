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

public partial class Admin_NewsList :AdminPageBase
{
    News dal = new News();

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
            GetData();
        }

    }

    public void GetData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        rtpNewsList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "dataid", 1);
        rtpNewsList.DataBind();
        NoRecord();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    public void NoRecord()
    {
        if (rtpNewsList.Items.Count == 0)
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "block");
        }
        else
        {
            noRecord.Style.Add(HtmlTextWriterStyle.Display, "none");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetData();
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
        SectionProxyData.ClearIndexNewsList();
    }

    protected void rtpNewsList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int Id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                //判断权限
                int _rs = WebUtility.checkOperator(4);
                if (_rs == 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                    return;
                }
                if (dal.DelNews(Id) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
                    GetData();
                }
                else
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
                    GetData();
                }
                SectionProxyData.ClearIndexNewsList();
                break;
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " and  Title like '%" + WebUtility.InputText(tbKeyword.Text) + "%'";
        }
        if (tbstarttime.Text != "")
        {
            SqlWhere += " and  Posttime > '" + WebUtility.InputText(this.tbstarttime.Text) + "' ";
        }
        if (tbendtime.Text != "")
        {
            SqlWhere += " and  Posttime < '" + WebUtility.InputText(this.tbendtime.Text) + "' ";
        }
        GetData();
    }


    protected void NewsSort_Changed(object sender, EventArgs e)
    {
        GetData();
    }
}
