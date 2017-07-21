using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Admin_packet_batpacketlist : System.Web.UI.Page
{
    userpacket dal = new userpacket();
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
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1=1";
            GetData();
        }
    }

    protected void GetData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);

        IList<userpacketInfo> list = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "id", 1);
        foreach (var item in list)
        {
            item.revevar1 = "满" + item.revevar1 + "元使用";

        }

        this.rtpUserlist.DataSource = list;
        this.rtpUserlist.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetData();
    }
    protected void rptUserList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                {
                    if (dal.Delete(id) > 0)
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
                break;
        }
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        if (dal.DeleteList(IdList) > 0)
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

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1= 1 ";
        if (this.tbckey.Text.Trim() != "")
        {
            SqlWhere += " AND pid LIKE '%" + WebUtility.InputText(this.tbckey.Text.Trim()) + "%' ";
        }
        if (this.tb_Start.Text != "")
        {
            SqlWhere += " AND validitytime >= '" + this.tb_Start.Text + "' ";
        }
        if (this.tb_End.Text != "")
        {
            SqlWhere += " AND validitytime <= '" + this.tb_End.Text + " 23:59:59' ";
        }
        GetData();
    }
}
