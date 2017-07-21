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

public partial class shop_FoodSortDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            if (Request.QueryString["sortid"] != null)
            {
                GetData();
            }
        }
    }

    EFoodSort dal = new EFoodSort();

    private EFoodSortInfo Info
    {
        get
        {
            EFoodSortInfo model = new EFoodSortInfo();
            model.SortID = HjNetHelper.GetQueryInt("sortid", 0);
            model.SortName = WebUtility.InputText(this.tbSortName.Text);
            model.TogoNum = UserHelp.GetUser_Togo().Unid;
            model.Jorder = Convert.ToInt32(tbJorder.Text);
            return model;
        }
        set
        {
            this.tbSortName.Text = value.SortName;
            tbJorder.Text = value.Jorder.ToString();
        }
    }

    void GetData()
    {
        int id = HjNetHelper.GetQueryInt("sortid", 0);
        EFoodSortInfo model = dal.GetModel(id);
        if (model != null)
        {
            Info = model;
            this.lbTitle.Text = "编辑餐品类别";
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        EFoodSortInfo info = new EFoodSortInfo();
        info = Info;

        if (info.SortID == 0)
        {
            if (dal.Add(info) >0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:添加成功!','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:添加失败!','250','150','true','','true','text');");
            }
        }
        else
        {
            if (dal.Update(info) == 1)
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:修改成功!','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:修改失败!','250','150','true','','true','text');");
            }
        }
    }
}
