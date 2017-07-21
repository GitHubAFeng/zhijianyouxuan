using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// 店铺券使用记录
/// </summary>
public partial class Admin_card_ShopCardUserRecord : System.Web.UI.Page
{
    ShopCardUserRecord dal = new ShopCardUserRecord();
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
        IList<ShopCardUserRecordInfo> list = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "cid", 1);
        foreach (var item in list)
        {
            item.cardnum = "";
            switch (item.ReveInt)
            {
                case 1:
                case 2:
                    item.cardnum += "减" + item.Point + "元";
                    break;
                case 3:
                    item.cardnum += "" + item.Point + "倍积分";
                    break;
                default:
                    break;
            }
        }

        this.rtpUserlist.DataSource = list;
        this.rtpUserlist.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetData();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:刪除成功!','250','150','true','1000','true','text');init();");
            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:刪除失败!','250','150','true','1000','true','text');init();");
            GetData();
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1= 1 ";
        if (this.tbusername.Text.Trim() != "")
        {
            SqlWhere += " AND username like '%" + WebUtility.InputText(this.tbusername.Text.Trim()) + "%'";
        }
        if (this.tbcardkey.Text.Trim() != "")
        {
            SqlWhere += " and ckey like '%" + WebUtility.InputText(this.tbcardkey.Text.Trim()) + "%'";     
        }
        if (this.tb_Start.Text != "")
        {
            SqlWhere += " AND AddDate >= '" + this.tb_Start.Text + "' ";
        }
        if (this.tb_End.Text != "")
        {
            SqlWhere += " AND AddDate <= '" + this.tb_End.Text + " 23:59:59' ";
        }
        GetData();
    }
}
