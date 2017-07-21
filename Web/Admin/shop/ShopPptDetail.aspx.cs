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

public partial class Admin_shop_ShopPptDetail : System.Web.UI.Page
{
    TogoPicture togopicBLL = new TogoPicture();
    private string togoid
    {
        get
        {
            object o = ViewState["togoid"];
            return (o == null) ? string.Empty : o.ToString();
        }
        set
        {
            ViewState["togoid"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelRptpic();
        }
    }

    protected void SelRptpic()
    {
        if (HjNetHelper.GetQueryString("id") == "")
        {
            pageType.Text = "添加幻灯片信息";
            tbTogoId.Text = HjNetHelper.GetQueryInt("tid", 0)+"";
            togoid = HjNetHelper.GetQueryInt("tid", 0)+"";

            PointsInfo model = new  Points().GetModel(Convert.ToInt32(togoid));
            lbtogoname.InnerHtml = model.Name;
        }
        else
        {
            pageType.Text = "编辑幻灯片信息";
            //控件绑定内容
            TogoPictureInfo info = togopicBLL.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
            this.tbTogoId.Text = info.TogoId.ToString();
            this.tbpri.Text = info.Pri.ToString();
            this.ImgUrl1.Value = info.Picture;
            this.ImgUrl.Src = WebUtility.ShowPic(info.Picture);
            togoid = info.TogoId.ToString();
            lbtogoname.InnerHtml = info.TogoName;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        TogoPictureInfo info = new TogoPictureInfo();
        info.TogoId = Convert.ToInt32(togoid);
        info.Pri = Convert.ToInt32(WebUtility.InputText(tbpri.Text));
        info.Picture = this.ImgUrl1.Value;
        info.Inve2 = "";
        if (pageType.Text == "添加幻灯片信息")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            TogoPictureInfo model = togopicBLL.GetPictureCount(HjNetHelper.GetQueryInt("tid",0));
            int Piccount = model.PictureCount;
            if (Piccount >= 4)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('不能新增','message','true',5);");

            }
            else
            {
                if (togopicBLL.Add(info) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增成功','success','true',5);");
                }
                else
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('新增失败','error','true',5);");
                }
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
            info.DataId = Convert.ToInt32(HjNetHelper.GetQueryString("id"));
            if (togopicBLL.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "showMessage('编辑失败','error','true',5);");
            }
        }
        this.ImgUrl.Src = WebUtility.ShowPic(info.Picture);
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + info.TogoId + "/ppt");
    }

    protected void btgo_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShopPptList.aspx?tid="+togoid);
    }
}
