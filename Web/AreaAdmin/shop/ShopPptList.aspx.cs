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

public partial class AreaAdmin_shop_ShopPptList : System.Web.UI.Page
{
    TogoPicture topicBLL = new TogoPicture();
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
            if (HjNetHelper.GetQueryString("tid")!="")
            {
                SqlWhere = "TogoId =" + HjNetHelper.GetQueryInt("tid", 0);
                SelRptshoppic();
            }
        }
    }

    protected void SelRptshoppic()
    {
        AspNetPager1.RecordCount = topicBLL.GetCount(SqlWhere);
        Rptshoppic.DataSource = topicBLL.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "Pri", 1);
        Rptshoppic.DataBind();
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        SelRptshoppic();
    }

    
    protected void  btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere="1=1";
        if (this.tbTogoName.Text != "")
        {
            SqlWhere += " and TogoId in (select dataid from etogo where  `TogoName` like '%" + WebUtility.InputText(this.tbTogoName.Text) + "%')";
        }
        this.AspNetPager1.RecordCount = topicBLL.GetCount(SqlWhere);
        if (AspNetPager1.RecordCount == 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('搜索结果为0','success','true',5)");
        }
        SelRptshoppic();
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShopPptDetail.aspx?tid=" + HjNetHelper.GetQueryInt("tid", 0));
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string IdList = this.hidDels.Value;
        if (topicBLL.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','2000','true','text');init();");
            SelRptshoppic();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','2000','true','text');init();");
        }
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("tid" , 0) + "/ppt");
    }

    protected void rptToGoAd_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            //判断权限
            int _rs = WebUtility.AreaAdmin_checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (topicBLL.DelTogoPicture(Convert.ToInt32(e.CommandArgument)) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','2000','true','text');init();");
                SelRptshoppic();
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','2000','true','text');init();");
                
            }
        }
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("tid", 0) + "/ppt");
    }
}
