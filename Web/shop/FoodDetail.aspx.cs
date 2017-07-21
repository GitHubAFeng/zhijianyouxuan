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
using System.IO;
using Hangjing.Model;

public partial class shop_FoodDetail : System.Web.UI.Page
{
    public string Picture
    {
        get
        {
            return ViewState["Picture"] == null ? "" : ViewState["Picture"].ToString();
        }
        set
        {
            ViewState["Picture"] = (object)value;
        }
    }

    protected string oldname
    {
        get
        {
            object o = ViewState["oldname"];
            return (o == null) ? "" : o.ToString();
        }
        set
        {
            ViewState["oldname"] = value;
        }
    }

    protected string oldpic
    {
        get
        {
            object o = ViewState["oldpic"];
            return (o == null) ? "" : o.ToString();
        }
        set
        {
            ViewState["oldpic"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            BindFoodType();

            int tid = UserHelp.GetUser_Togo().Unid;


            if (Request["id"] != null)
            {
                GetData();
                pricebox.Style["display"] = "none";
            }
            else
            {
                rblIsRecommend.Items[0].Selected = true;

            }

            this.hidTogoId.Value = tid.ToString();

        }
    }

    Foodinfo dal = new Foodinfo();
    EFoodSort dal_sort = new EFoodSort();
    Points togodal = new Points();

    FoodStyle dalsf = new FoodStyle();
    FoodAttributes dalfa = new FoodAttributes();

    protected void BindFoodType()
    {
        int togonum = UserHelp.GetUser_Togo().Unid;
        WebUtility.BindList<EFoodSortInfo>("SortID", "SortName", dal_sort.GetListByTogoNum(togonum), DropFoodType);
    }

    FoodinfoInfo Info
    {
        get
        {
            FoodinfoInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id", 0));
            if (model == null)
            {
                model = new FoodinfoInfo();
                model.IsSpecial = 1;
                model.isauth = 0;
                model.Special = "0";
            }
            model.Unid = HjNetHelper.GetQueryInt("id", 0);
            model.FoodName = WebUtility.InputText(this.tbName.Text);
            model.FoodNo = this.HdFoodNO.Value;
            model.FPrice = Convert.ToDecimal(this.tbPrice.Text);
            model.FPInDate = DateTime.Now;
            model.FPActiveDate = DateTime.Now;
            model.FPMaster = UserHelp.GetUser_Togo().Unid;
            model.FoodNamePy = WebUtility.InputText(this.tbFoodNamePy.Text);
            model.FullPrice = Convert.ToDecimal(this.tbFullPrice.Text);

            model.Remains = Convert.ToInt32(this.tbRemains.Text);
            model.DpPerDay = Convert.ToInt32(DropPerDay.SelectedValue);
            if (model.DpPerDay == 0)
            {
                model.MaxPerDay = 100000;
            }
            else
            {
                model.MaxPerDay = Convert.ToInt32(this.tbMaxPerDay.Text);
            }
            
            model.Taste = this.tbtaste.Text;
            model.Picture = ImgUrl1.Value;
            model.FoodType = Convert.ToInt32(this.DropFoodType.SelectedValue);
            model.OrderNum = Convert.ToInt32(tbOrderNum.Text);
            if (!string.IsNullOrEmpty(rblIsRecommend.SelectedValue))
            {
                model.IsRecommend = Convert.ToInt32(rblIsRecommend.SelectedValue);
            }
            else
            {
                model.IsRecommend = 0;
            }
            model.OpenTime = "";
            return model;
        }
        set
        {
            this.tbName.Text = value.FoodName;
            WebUtility.SelectValue(DropFoodType, value.FoodType.ToString());
            this.tbPrice.Text = value.FPrice.ToString();
            this.tbFoodNamePy.Text = value.FoodNamePy;
            this.tbMaxPerDay.Text = value.MaxPerDay.ToString();
            this.tbtaste.Text = value.Taste;
            this.ImgUrl1.Value = value.Picture;
            this.ImgUrl.Src = WebUtility.ShowPic(value.Picture);
            tbOrderNum.Text = value.OrderNum.ToString();
            rblIsRecommend.SelectedValue = value.IsRecommend.ToString();
            oldname = value.FoodName;
            oldpic = value.Picture;
            HdFoodNO.Value = value.FoodNo;
            hidDataId.Value = value.Unid.ToString();
            tbFullPrice.Text = value.FullPrice.ToString();
            WebUtility.SelectValue(DropPerDay, value.DpPerDay.ToString());
        }

    }


    protected void GetData()
    {
        FoodinfoInfo model = dal.GetModel(HjNetHelper.GetQueryInt("id", 0));
        if (model != null)
        {
            Info = model;
            this.lbTitle.Text = "修改餐品信息";

            GetStyle();
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        FoodinfoInfo info = new FoodinfoInfo();
        info = Info;

        if (Request["id"] == null)
        {
            info.InUse = "y";
            int foodid = dal.Add(info);


            if (foodid > 0)
            {
                string url = "FoodDetail.aspx?tid=" + info.FPMaster + "&id=" + foodid;
                AlertScript.RegScript(this.Page, UpdatePanel1, "alert('添加成功','success','true',5);gourl('" + url + "');");
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:添加失败!','250','150','true','3000','true','text');");
            }
        }
        else
        {
            if (dal.Update(info) == 1)
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:修改成功!','250','150','true','3000','true','text');");
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:修改失败!','250','150','true','3000','true','text');");
            }
        }
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + info.FPMaster + "/food");
    }


    protected void GetStyle()
    {
        int FoodId = HjNetHelper.GetQueryInt("id", 0);
        rptFoodlist.DataSource = dalsf.GetList(10, 1, "FoodtId =" + FoodId, "dataid", 0);
        rptFoodlist.DataBind();

        rptattr.DataSource = dalfa.GetList(20, 1, "FoodtId =" + FoodId, "dataid", 0);
        rptattr.DataBind();
    }

    protected void rptFoodlist_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
                if (dalsf.DelProductStyle(Convert.ToInt32(e.CommandArgument)) > 0)
                {
                    AlertScript.RegScript(this.Page, "alert('删除成功','text:删除成功!','250','150','true','1000','true','text');init();");
                    //TODO:同时删除对应类别下面的商品
                    GetStyle();
                    new Foodinfo().UpdateFooodPrice(HjNetHelper.GetQueryInt("id", 0));
                }
                else
                {
                    AlertScript.RegScript(this.Page, "alert('删除失败','text:删除失败!','250','150','true','1000','true','text');init();");
                }
                break;
        }
    }

    protected void rptattr_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        string type = e.CommandName;
        switch (type)
        {
            case "del":
                if (dalfa.DelProductAttributes(Convert.ToInt32(e.CommandArgument)) > 0)
                {
                    AlertScript.RegScript(this.Page, "alert('删除成功','text:删除成功!','250','150','true','1000','true','text');init();");
                    //TODO:同时删除对应类别下面的商品
                    GetStyle();
                    new Foodinfo().UpdateFooodAttr(HjNetHelper.GetQueryInt("id", 0));
                }
                else
                {
                    AlertScript.RegScript(this.Page, "alert('删除失败','text:删除失败!','250','150','true','1000','true','text');init();");
                }
                break;
        }
    }
}
