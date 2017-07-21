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

// 商家餐品分类信息管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-07-12

public partial class Admin_Shop_FoodSortDetail : System.Web.UI.Page
{
    EFoodSort dal = new EFoodSort();
    Points dal_togo = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            //编辑
            int TogoId = HjNetHelper.GetQueryInt("tid", 0);
            int SortId = HjNetHelper.GetQueryInt("id", 0);
            hidTogoId.Value = TogoId.ToString();
            hidDataId.Value = SortId.ToString();

            if (SortId > 0)
            {
                BindFoodSortData(TogoId, SortId);

                pageType.Text = "修改餐品类别";
            }
            else
            {
                BindFoodSortData(TogoId, 0);

                pageType.Text = "新增餐品类别";
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        Hangjing.Model.EFoodSortInfo model = new EFoodSortInfo();

        model.SortName = WebUtility.InputText(this.tbFoodsort.Text);
        model.TogoNum = Convert.ToInt32(Request.QueryString["tid"]);
        model.Jorder = Convert.ToInt32(this.tbJorder.Text);

        //编辑
        if (pageType.Text == "新增餐品类别")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Add(model) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增成功','id:divShowContent','640','150','true','','true','text')");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增失败','text:新增失败','250','150','true','','true','text')");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            model.SortID = HjNetHelper.GetQueryInt("id",0);
            if (dal.Update(model) == 1)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新成功','id:divShowContent','640','150','true','','true','text')");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新失败','text:更新失败','250','150','true','','true','text')");
            }
        }
    }

    protected void BindFoodSortData(int TogoId, int SortId)
    {
        PointsInfo togomodel = dal_togo.GetModel(TogoId);

        if (togomodel != null)
        {
            this.lbtogoname.InnerText = togomodel.Name;
        }

        //绑定分类名称
        if (SortId > 0)
        {
            EFoodSortInfo model = dal.GetModel(SortId);
            if (model != null)
            {
                this.tbFoodsort.Text = model.SortName;
                this.tbJorder.Text = model.Jorder+"";
            }
        }

    }
}
