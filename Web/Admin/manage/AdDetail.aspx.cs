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
using Hangjing.Cache;

public partial class Admin_AdDetail : AdminPageBase
{
    AdTable bll = new AdTable();

    AdTableInfo info
    {
        set
        {
            this.tbname.Text = value.AdName;
            tbAdPage.Text = value.AdPage;
            this.tbAddress.Text = value.AdAdrees;
            this.tbAdAddDate.Text = value.AdAddDate.ToString();
            this.tbAdStartDate.Text = value.AdStartDate.ToString();
            this.tb_Width.Text = value.AdWidth.ToString();
            this.tb_Length.Text = value.AdHeight.ToString();
            this.ImgUrl1.Value = value.AdImageAdrees;
            this.ImgUrl.Src = WebUtility.ShowPic(value.AdImageAdrees);

            if (value.AdWidth > 600)
            {
                ImgUrl.Height = value.AdHeight / 2;
                ImgUrl.Width = value.AdWidth / 2;
            }
            else
            {
                ImgUrl.Height = value.AdHeight;
                ImgUrl.Width = value.AdWidth;
            }
        }
        get
        {
            AdTableInfo model = new AdTableInfo();

            model.TID = Convert.ToInt32(HjNetHelper.GetQueryString("id"));
            model.AdName = this.tbname.Text.ToString();
            model.AdType = "";
            model.AdPage = tbAdPage.Text;
            model.UserID = 0;
            model.DayMoney = 0;
            model.AdAdrees = this.tbAddress.Text;
            model.MID = 0;
            model.DayMode = 0;
            model.AdStartDate = Convert.ToDateTime(this.tbAdStartDate.Text);
            model.AdAddDate = DateTime.Now;
            model.AdWidth = Convert.ToInt32(tb_Width.Text);
            model.AdHeight = Convert.ToInt32(tb_Length.Text);
            model.AdImageAdrees = ImgUrl1.Value;
            return model;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            GetAdData();
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        AdTableInfo model = info;
        if (bll.Update(model) > 0)
        {
            AlertScript.RegScript(this, UpdatePanel1, "showMessage('编辑成功','success','true','5');");

            //生成js.

            CreatJs js = new CreatJs();
            js.CreatJsFile(model);
            this.ImgUrl.Src = WebUtility.ShowPic(model.AdImageAdrees);

        }
        else
        {

            AlertScript.RegScript(this, UpdatePanel1, "showMessage('添加失败','error','true','8');");
        }
        SectionProxyData.ClearWebSet();
    }

    protected void GetAdData()
    {
        AdTableInfo model = bll.GetModel(Convert.ToInt32(Request.QueryString["id"]));
        if (model != null)
        {
            info = model;
            admsg.InnerHtml = "请上传大小为" + model.AdWidth + "*" + model.AdHeight + "的图片，flash";
        }
    }
}
