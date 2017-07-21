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
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class Admin_AdDetailotheraddetail : System.Web.UI.Page
{
    SortAd bll = new SortAd();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            if (Request["id"] != null)
            {
                GetAdData();
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {

        SortAdInfo model = new SortAdInfo();
        if (Request["id"] != null)
        {
            model.DataId = Request["id"];
        }
        model.cityid = 0;
        model.sortid = 0;
        model.Link = this.tbAddress.Text;
        model.Introduce = "";
        model.title = tbtitle.Text;
        model.isLink = Convert.ToInt32(islinked.SelectedValue);
        model.Servicefee =0;
        model.AdStartDate = Convert.ToDateTime("1970-1-1");
        model.AdEndDate = Convert.ToDateTime("1970-1-1");
        model.state = tbsortnum.Text;
        model.defautpic = "";
        model.Picture = ImgUrl1.Value;
        if (Request["id"] == null)
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "hideload_super();alert('无操作权限','success','true',5);init();");
                return;
            }
            if (bll.Add(model) > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "showMessage('添加成功','success','true','5');");
                
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "showMessage('添加失败','error','true','8');");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "hideload_super();alert('无操作权限','success','true',5);init();");
                return;
            }
            if (bll.Update(model) > 0)
            {

                AlertScript.RegScript(this, UpdatePanel1, "showMessage('编辑成功','success','true','5');");
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "showMessage('编辑失败','error','true','8');");
            }
        }
        SectionProxyData.ClearWordAdlist();
    }

    protected void GetAdData()
    {
        string id = HjNetHelper.GetQueryString("id");
        int sid = HjNetHelper.GetQueryInt("secid" , 0);
        SortAdInfo model = bll.GetModel(Convert.ToInt32(id), sid);
        if (model != null)
        {
            this.tbAddress.Text = model.Link;
            this.tbtitle.Text = model.title;
            WebUtility.SelectValue(islinked, model.isLink.ToString());
            this.tbsortnum.Text = model.state;
            this.ImgUrl1.Value = model.Picture;
            this.ImgUrl.Src = WebUtility.ShowPic(model.Picture);

            if (model.Width > 600)
            {
                ImgUrl.Height = model.Height / 2;
                ImgUrl.Width = model.Width / 2;
            }
            else
            {
                ImgUrl.Height = model.Height;
                ImgUrl.Width = model.Width;
            }
        }

    }
}
