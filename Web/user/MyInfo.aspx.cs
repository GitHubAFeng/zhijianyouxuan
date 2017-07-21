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
using System.IO;
using System.Text;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class UserHome_MyInfo : PageBase
{
    protected string usericon
    {
        set
        {
            ViewState["usericon"] = value;
        }
        get
        {
            return ViewState["usericon"] == null ? "" : ViewState["usericon"].ToString();
        }
    }


    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            GetData();
        }
    }


    private ECustomerInfo Info
    {
        get
        {
            ECustomerInfo model = UserHelp.GetUser();
            model.Name = WebUtility.InputText(this.tbname.Text);
            model.Tell = WebUtility.InputText(this.tbTel.Text);
            model.QQ = WebUtility.InputText(this.tbQQ.Text);
            model.EMAIL = WebUtility.InputText(this.tbemail.Text);
            model.TrueName = WebUtility.InputText(this.tbRealName.Text, 60);
            model.Sex = rblsex.SelectedValue;

            //注册保存生日
            string tbyear = WebUtility.InputText(this.tbbirthday.Text.Trim());
            string tbmonth = WebUtility.InputText(this.tbmonth.Text.Trim());
            string tbday = WebUtility.InputText(this.tbday.Text.Trim());
            model.MSN = tbyear + "-" + tbmonth + "-" + tbday;

            model.Picture = ImgUrl1.Value;

            return model;
        }
        set
        {
            this.tbRealName.Text = value.TrueName;
            this.tbname.Text = value.Name;
            this.tbTel.Text = value.Tell;
            this.tbQQ.Text = value.QQ;
            usericon = value.Picture;
            ImgUrl1.Value = value.Picture;
            ImgUrl.Src = WebUtility.ShowPic(value.Picture);
            tbemail.Text = value.EMAIL;
            WebUtility.SelectValue(rblsex,value.Sex);

            if (value.MSN.Contains('-'))
            {
                string[] strsex = value.MSN.Split('-');
                if (strsex.Length>0)
                {
                    tbbirthday.Text = strsex[0];
                    this.tbmonth.Text = strsex[1];
                    this.tbday.Text = strsex[2];
                }
            }

            //tbbirthday.Text = value.MSN;
        }
    }


    private void GetData()
    {
        ECustomerInfo model = UserHelp.GetUser();
        model = new ECustomer().GetModelByTellAPassword(model.Tell, model.Password);
        UserHelp.SetLogin(model);
        if (model.EMAIL != null)
        {
            Info = model;
        }
    }

    //保存
    protected void btSave_Click(object sender, EventArgs e)
    {
        if (UserHelp.IsLogin() && UserHelp.GetUser() != null)
        {
            ECustomerInfo info = new ECustomerInfo();
            info = Info;

            //判断昵称是否重复
            string sql = " [Name] = '" + info.Name + "' and DataID <> " + UserHelp.GetUser().DataID;
            int count = dal.GetCount(sql);
            if (count > 0)
            {
                AlertScript.RegScript(Page, UpdatePanel1, "alert('此昵称已存在，请重新输入！');hideload_super();");
                return;
            }

            //判断手机是否重复
            sql = "Tell = '" + info.Tell + "' and DataID <> " + UserHelp.GetUser().DataID;
            count = dal.GetCount(sql);
            if (count > 0)
            {
                AlertScript.RegScript(Page, UpdatePanel1, "alert('手机号码重复了，请重新输入！');hideload_super();");
                return;
            }

            if (dal.Update(info)>0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','2000','true','text');setIcon('" + WebUtility.ShowPic(info.Picture) + "')");
                UserHelp.SetLogin(dal.GetModel(info.DataID));
                GetData();
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','2000','true','text');");
            }
        }


    }

}
