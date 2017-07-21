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

public partial class Admin_shop_DistancePayDetail : System.Web.UI.Page
{
    shopdelivery dal = new shopdelivery();
    Points dal_togo = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            //编辑
            int TogoId = HjNetHelper.GetQueryInt("tid", 0);//商家id
            int SortId = HjNetHelper.GetQueryInt("id", 0);
            hidTogoId.Value = TogoId.ToString();
            hidDataId.Value = SortId.ToString();

            if (SortId > 0)
            {
                BindFoodSortData(TogoId, SortId);

                pageType.Text = "修改配送范围";
            }
            else
            {
                BindFoodSortData(TogoId, 0);

                pageType.Text = "新增配送范围";
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        Hangjing.Model.shopdeliveryInfo model = new shopdeliveryInfo();
        model.tid = Convert.ToInt32(Request.QueryString["tid"]);
        model.distancestart = Convert.ToDecimal(WebUtility.InputText(this.tbdistancestart.Text));
        model.distanceend = Convert.ToDecimal(WebUtility.InputText(this.tbdistanceend.Text));
        model.minmoney = Convert.ToInt32(WebUtility.InputText(this.tbminmoney.Text));
        model.sendmoney = Convert.ToDecimal(WebUtility.InputText(this.tbsendmoney.Text));
        model.ReveVar2 = "";
        model.ReveVar1 = "";
        model.ReveInt2 = 0;
        model.ReveInt1 = 0;
        model.ReveFloat2 = 0;
        model.ReveFloat1 = 0;
        model.AddTime = DateTime.Now;

        //编辑
        if (pageType.Text == "新增配送范围")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (model.distancestart >= model.distanceend)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('结束公里必须比初始公里大','success','true',5);init();");
                return;
            }
            if (dal.Add(model) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增成功','id:divShowContent','640','150','true','','true','text')");
                BindData();
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
            model.rId = HjNetHelper.GetQueryInt("id", 0);
            if (model.distancestart >= model.distanceend)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('结束公里必须比初始公里大','success','true',5);init();");
                return;
            }
            if (dal.Update(model) > 0)
            {

                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新成功','id:divShowContent','640','150','true','','true','text')");
                BindData();
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
            shopdeliveryInfo model = dal.GetModel(SortId);
            if (model != null)
            {
                this.tbdistancestart.Text = Convert.ToString(model.distancestart);
                this.tbdistanceend.Text = Convert.ToString(model.distanceend);
                this.tbminmoney.Text = Convert.ToString(model.minmoney);
                this.tbsendmoney.Text = Convert.ToString(model.sendmoney);
            }
        }

    }
    
    protected void BindData()
    {
        int togoid = HjNetHelper.GetQueryInt("tid", 0);//商家id
        dal.GetModelbyShopId(togoid);
    }
}