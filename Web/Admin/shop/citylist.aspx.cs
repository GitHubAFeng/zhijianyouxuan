#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-3-21 9:25:58.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class qy_54tss_Admin_manage_citylist : System.Web.UI.Page
{
    City dal = new City();

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

    /// <summary>
    /// 翻页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetData();
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    private void GetData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);
        rtpProductSortList.DataSource = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "cid", 1);
        rtpProductSortList.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = "1=1";
        if (tbKeyword.Text != "")
        {
            SqlWhere += " and ( cname like '%" + WebUtility.InputText(tbKeyword.Text) + "%' or site like '%" + WebUtility.InputText(tbKeyword.Text) + "%')";
        }
        GetData();
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        //判断权限
        int _rs = WebUtility.checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/city");
    }

}
