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
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class addFoodStyle : System.Web.UI.Page
{
    FoodStyle dal = new FoodStyle();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("fid")))
            {
                int foodid = HjNetHelper.GetQueryInt("fid", 0);

                Foodinfo fooddal = new Foodinfo();
                FoodinfoInfo foodinfo = fooddal.GetModel(foodid);
                if (foodinfo != null)
                {
                    this.Lbfoodname.Text = foodinfo.FoodName.ToString();
                    this.Lbfoodtype.Text = foodinfo.SortName.ToString();
                }
                hffid.Value = foodid + "";
                hftid.Value = HjNetHelper.GetQueryString("tid");
            }
            GetFoodstyle();
        }
    }

    protected void GetFoodstyle()
    {
        if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("id")))
        {
            int dataid = HjNetHelper.GetQueryInt("id", 0);
            hfdid.Value = dataid.ToString();
            FoodStyleInfo info = dal.GetModel(dataid);
            this.tbtitle.Text = info.Title.ToString();
            this.tbPrice.Text = info.Price.ToString();
            this.tbMarkeyPrice.Text = info.MarkeyPrice.ToString();
            this.tbSaleSum.Text = info.SaleSum.ToString();
            //this.tbIntro.Value = info.Intro.ToString();
            this.Lbfoodname.Text = info.Name.ToString();
            this.DropIsUse.SelectedValue = info.InUser.ToString();
            this.hidDataId.Value = dataid.ToString();
           
        }
        else
        {

        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        int fid = Convert.ToInt32(hffid.Value);
        int tid = Convert.ToInt32(hftid.Value);
        int did = Convert.ToInt32(hfdid.Value);

        FoodStyleInfo info = new FoodStyleInfo();
        info.DataId = did;
        info.Title = this.tbtitle.Text;
        info.FoodtId = fid;
        info.SaleSum = Convert.ToInt32(this.tbSaleSum.Text);
        info.Price = Convert.ToDecimal(this.tbPrice.Text);
        info.MaxPerDay = Convert.ToInt32(this.tbMaxPerDay.Text);
        info.InUser = Convert.ToInt32(DropIsUse.SelectedValue);
        info.Intro = "";// this.tbIntro.Value;
        info.MarkeyPrice = Convert.ToDecimal(this.tbMarkeyPrice.Text);
        info.Inve1 = 0;
        info.Inve2 = "";

        string url = "FoodDetail.aspx?id=" + fid + "&tid=" + tid;

        if (did == 0)
        {
            if (dal.Add(info) > 0)
            {
                new Foodinfo().UpdateFooodPrice(fid);
                AlertScript.RegScript(this, "alert('添加成功');top.location.href  = top.location.href;");
            }
            else
            {
                AlertScript.RegScript(this, "alert('添加失败');top.location.href  = top.location.href;");
            }
        }
        else
        {
            if (dal.Update(info) > 0)
            {
                new Foodinfo().UpdateFooodPrice(fid);
                AlertScript.RegScript(this, "alert('编辑成功');top.location.href  = top.location.href;");
            }
            else
            {
                AlertScript.RegScript(this, "alert('编辑失败');top.location.href  = top.location.href;");
            }
        }
    }
}
