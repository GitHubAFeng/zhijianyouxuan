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
using Hangjing.Model;
using Hangjing.Common;
using System.IO;

public partial class qy_54tss_AreaAdmin_aboutus_addpic : System.Web.UI.Page
{
    ShopFoodPicture dal = new ShopFoodPicture();
    private string bigpic
    {
        get
        {
            object o = ViewState["bigpic"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["bigpic"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
           
            if (HjNetHelper.GetQueryString("id") == "")
            {
                pageType.Text = "添加标签图片";
            }
            else
            {
                pageType.Text = "编辑标签图片";
                ShopFoodPictureInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
                ImgUrl.Src = WebUtility.ShowPic(info.Picture);
                ImgUrl1.Value = info.Picture;
                tbTitle.Text = info.Title;
                bigpic = info.Inve2;
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        ShopFoodPictureInfo info = new ShopFoodPictureInfo();
        if (Request["id"] != null)
        {
            info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));

        }
        else
        {
            info.cityid = 0;
            info.Inve1 = 0;
        }
        info.ShopId = 0;
        info.Url = "";
        info.Title = WebUtility.InputText(tbTitle.Text);
        info.Inve2 = "";

        info.Picture = WebUtility.InputText(ImgUrl1.Value);

        if (HjNetHelper.GetQueryString("id") == "")
        {
            if (dal.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1,  "showMessage('新增成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增失败','error','true',5);");
            }
        }
        else
        {
            if (dal.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑失败','error','true',5);");
            }
        }
        ImgUrl.Src = WebUtility.ShowPic(info.Picture);
        CacheHelper.ClearShopPicTag();
    }
}
