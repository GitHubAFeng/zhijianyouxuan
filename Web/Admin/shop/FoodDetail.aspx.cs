using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

using org.in2bits.MyXls;
using Hangjing.AppLog;
// 商家餐品信息管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 餐品信息

public partial class Admin_shop_FoodDetail :AdminPageBase
{
    Points daltogo = new Points();
    protected string otime
    {
        get
        {
            object o = ViewState["otime"];
            return (o == null) ? "" : o.ToString();
        }
        set
        {
            ViewState["otime"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        CheckRights("A");
        if (!IsPostBack)
        {
            GetFoodSort();
            //编辑
            int FPMaster = HjNetHelper.GetQueryInt("tid", 0);
            int Unid = HjNetHelper.GetQueryInt("id", 0);
            HdUnid.Value = Unid.ToString();
            this.hidTogoId.Value = FPMaster.ToString();

            PointsInfo info = daltogo.GetModel(FPMaster);
            this.lbtogo.Text = info.Name;
           

            if (Unid > 0)
            {
                BindFoodData(Unid);
                pageType.Text = "更新餐品";
                GetStyle();
                pricebox.Style["display"] = "none";
            }
            else
            {
                pageType.Text = "新增餐品";
                rblSpecial.Items[1].Selected = true;
                rblIsRecommend.Items[0].Selected = true;
                rblisauth.Items[1].Selected = true;
            }
           
        }
    }

    Foodinfo dal = new Foodinfo();
    EFoodSort dal_sort = new EFoodSort();

    FoodStyle dalsf = new FoodStyle();
    FoodAttributes dalfa = new FoodAttributes();


    protected void BindFoodData(int Unid)
    {
        FoodinfoInfo model = dal.GetModel(Unid);
        if (model != null)
        {
            this.tbNamefx.Text = model.FoodName;
            this.lbtogo.Text = model.TogoName.ToString();

            this.ddlFoodType.SelectedValue = model.FoodType.ToString();
            this.tbFoodNamePy.Text = model.FoodNamePy;
            this.tbtaste.Text = model.Taste;
            this.ImgUrl1.Value = model.Picture;
            ddlIsUse.SelectedValue = model.InUse.ToString();
            tbOrderNum.Text = model.OrderNum.ToString();
            rblIsRecommend.SelectedValue = model.IsRecommend.ToString();
           
            WebUtility.SelectValue(rblisauth, model.isauth.ToString());
            if (!string.IsNullOrEmpty(model.Picture))
            {
                ImgUrl.Src = WebUtility.ShowPic(model.Picture);
            }
            WebUtility.SelectValue(rblfoodno, model.FoodNo);
            WebUtility.SelectValue(rblSpecial, model.Special);
            hidDataId.Value = model.Unid.ToString();
            tbPrice.Text = model.FPrice.ToString();
            tbFullPrice.Text = model.FullPrice.ToString();
            tboldprice.Text = model.Oldprice.ToString();
            tbRemains.Text = model.Remains.ToString();
            tbMaxPerDay.Text = model.MaxPerDay.ToString();
            WebUtility.SelectValue(DropPerDay, model.DpPerDay.ToString());
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        if (IsRefresh)
        {
            return;
        }

        FoodinfoInfo model = new FoodinfoInfo();
        model.Unid = HjNetHelper.GetQueryInt("id", 0);
        if (model.Unid > 0)
        {
            model = dal.GetModel(model.Unid);
        }
        else
        {
            model.IsSpecial = 1;
            model.isauth =0;
        }
        model.InUse = ddlIsUse.SelectedValue.ToString();
        model.FoodName = WebUtility.InputText(this.tbNamefx.Text);
        model.FoodNo = rblfoodno.SelectedValue;
        model.Special = rblSpecial.SelectedValue;
        model.FPrice = Convert.ToDecimal(this.tbPrice.Text);
        model.FPInDate = DateTime.Now;
        model.FPActiveDate = DateTime.Now;
        model.FPMaster = HjNetHelper.GetQueryInt("tid", 0);
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
        model.FoodType = Convert.ToInt32(ddlFoodType.SelectedValue);
        model.OrderNum = Convert.ToInt32(tbOrderNum.Text);
        model.IsRecommend = Convert.ToInt32(rblIsRecommend.SelectedValue);
        model.Oldprice = Convert.ToDecimal(this.tboldprice.Text);
     
        model.OpenTime = "";
   
        if (pageType.Text == "新增餐品")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);init();");
                return;
            }

            int foodid = dal.Add(model);


            if (foodid > 0)
            {
                string url = "FoodDetail.aspx?tid="+model.FPMaster+"&id="+foodid;
                AlertScript.RegScript(this.Page, "alert('添加成功','success','true',5);gourl('"+url+"');");


                this.tbNamefx.Text = "";
                this.tbPrice.Text = "";
                tbtaste.Text = "";
                ImgUrl.Src = WebUtility.ShowPic(model.Picture);
            }
            else
            {
                AlertScript.RegScript(this, "showMessage('添加失败','error','true','8');");
            }
        }
        else
        {
            model.Unid = HjNetHelper.GetQueryInt("id", 0);
            if (model.Unid != 0)
            {
                //判断权限
                int _rs = WebUtility.checkOperator(3);
                if (_rs == 0)
                {
                    AlertScript.RegScript(this.Page, "alert('无操作权限','success','true',5);init();");
                    return;
                }
                if (dal.Update(model) == 1)
                {
                    AlertScript.RegScript(this, "showMessage('修改成功','success','true','5');");
                    ImgUrl.Src = WebUtility.ShowPic(model.Picture);
                }
                else
                {
                    AlertScript.RegScript(this, "showMessage('修改失败','error','true','8');");
                }
            }
        }
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("tid", 0) + "/food");
    }

    protected void GetFoodSort()
    {
        int tid = HjNetHelper.GetQueryInt("tid", 0);
        IList<EFoodSortInfo> list = dal_sort.GetListByTogoNum(tid);
        if (list.Count == 0)
        {
            string js = "alert('您还没有为此商家添加一个分类,点击确定添加分类');gourl('FoodSortDetail.aspx?tid=" + tid + "');";
            AlertScript.RegScript(Page, js);
            return;
        }
        WebUtility.BindList<EFoodSortInfo>("SortID", "SortName", list, ddlFoodType);
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

