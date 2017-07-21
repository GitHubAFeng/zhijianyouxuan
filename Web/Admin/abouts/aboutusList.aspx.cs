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

public partial class Admin_abouts_aboutsdetail :AdminPageBase
{
    aboutus aboutsBLL = new aboutus();

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
       
        if (!IsPostBack)
        {
            SelRptaboutus();
        }
    }

    protected void SelRptaboutus()
    {
        AspNetPager1.RecordCount = aboutsBLL.GetCount(SqlWhere);
        this.rtpAboutus.DataSource = aboutsBLL.GetList(AspNetPager1.PageSize,AspNetPager1.CurrentPageIndex,SqlWhere,"DataId",1);
        this.rtpAboutus.DataBind();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hidDels.Value;

        string[] ids = IdList.Split(',');
        foreach (string item in ids)
        {
            int id = Convert.ToInt32(item);
            if (id <= 11)
            {
                AlertScript.RegScript(this, UpdatePanel1, "alert('此数据是系统默认数据，不能删除！');init();");
                return;
            }
        }
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (aboutsBLL.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            SelRptaboutus();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            SelRptaboutus();
        }
    }


    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        SelRptaboutus();
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (this.tbKeyword.Text.Trim() != "")
        {
            SqlWhere += " AND  Title LiKE '%" + WebUtility.InputText(this.tbKeyword.Text) + "%'";
        }
        SelRptaboutus();
    }

    protected void rtpAboutus_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            int id = Convert.ToInt32(e.CommandArgument);
            if (id <= 10)
            {
                AlertScript.RegScript(this, UpdatePanel1, "alert('此数据是系统默认数据，不能删除！');init();");
                return;
            }
            if (aboutsBLL.Delaboutus(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除成功','success','true',5);init();");
                SelRptaboutus();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('删除失败','error','true',5);init();");
            }
        }
    }
}
