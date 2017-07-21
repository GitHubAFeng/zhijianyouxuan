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

// 商家列表管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息
// 2010-07-12
public partial class AreaAdmin_Shop_FoodSortList : System.Web.UI.Page
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

    Hangjing.SQLServerDAL.EFoodSort bll = new EFoodSort();

    protected void BindData()
    {
        int togoid = HjNetHelper.GetQueryInt("tid", 0);//商家id
        hidTogoId.Value = togoid.ToString();
        rptSortlist.DataSource = bll.GetListByTogoNum(togoid);
        rptSortlist.DataBind();
        lbTogoName.Text = new Hangjing.SQLServerDAL.Points().GetModel(togoid).Name +"的餐品分类";
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
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
                    //TODO:同时删除对应类别下面的餐品
                  //  AlertScript.RegScript(this, "showMessage('删除成功','success','true','5');");
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
            //TODO:同时删除对应类别下面的餐品

            BindData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
        }
    }

}
