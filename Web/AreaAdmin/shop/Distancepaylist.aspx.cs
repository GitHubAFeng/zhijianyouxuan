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
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

// 配送距离管理
public partial class AreaAdmin_shop_Distancepaylist : System.Web.UI.Page
{
    private string SqlWhere
    {
        get
        {
            object o = ViewState["SqlWhere"];
            return (o == null) ? "" : Convert.ToString(o);
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
            BindData();
        }
    }
    Hangjing.SQLServerDAL.shopdelivery bll = new shopdelivery();

    protected void BindData()
    {
        Points dal = new Points();
        int togoid = HjNetHelper.GetQueryInt("tid", 0);//商家id
        hidTogoId.Value = togoid.ToString();
        rptSortlist.DataSource = bll.GetListByTogoNum(togoid);
        rptSortlist.DataBind();
        lbTogoName.Text = new Hangjing.SQLServerDAL.Points().GetModel(togoid).Name + "的配送范围";
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");

        new shopdelivery().GetModelbyShopId(togoid);
    }

    protected void rptSortlist_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
                //判断权限
                int _rs = WebUtility.AreaAdmin_checkOperator(4);
                if (_rs == 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                    return;
                }
                if (bll.Delete(Convert.ToInt32(e.CommandArgument)) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('删除成功!');init();");
                    BindData();
                }
                else
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
                }
                break;
        }
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(4);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        string ids = hdDels.Value;
        if (bll.DelList(ids) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
    }

}
