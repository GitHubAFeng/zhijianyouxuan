using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;


namespace Html5
{
    public partial class myuserinfo : System.Web.UI.Page
    {
        ECustomer dal = new ECustomer();
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }

            //保存信息
            if (Request.HttpMethod.ToLower() == "post")
            {
                btSave_Click();
            }

            GetData();

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

        private ECustomerInfo Info
        {
            get
            {
                ECustomerInfo model = UserHelp.GetUser();
                //model.Tell = WebUtility.InputText(this.tbTel.Text);
                model.Name = WebUtility.InputText(this.tbname.Value);
                model.EMAIL = WebUtility.InputText(this.tbemail.Value);
                model.QQ = WebUtility.InputText(this.tbQQ.Value);
                model.TrueName = WebUtility.InputText(this.tbRealName.Value, 60);
                //model.Picture = ImgUrl1.Value;

                return model;
            }
            set
            {
                lbpont.InnerText = value.Point + "分";
                lbmoney.InnerText = value.Usermoney + "元";

                this.tbTel.InnerText = value.Tell;
                this.tbname.Value = value.Name;
                this.tbQQ.Value = value.QQ;
                this.tbemail.Value = value.EMAIL;
                this.tbRealName.Value = value.TrueName;

                //usericon = value.Picture;
                //ImgUrl1.Value = value.Picture;
                //ImgUrl.Src = WebUtility.ShowPic(value.Picture);
            }
        }


        //保存
        protected void btSave_Click()
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
                    this.divError.InnerHtml = "此昵称已存在，请重新输入！";
                    return;
                }

                //判断手机是否重复
                sql = "Tell = '" + info.Tell + "' and DataID <> " + UserHelp.GetUser().DataID;
                count = dal.GetCount(sql);
                if (count > 0)
                {
                    this.divError.InnerHtml = "手机号码重复了，请重新输入！";
                    return;
                }

                if (dal.Update(info) > 0)
                {
                    this.divError.InnerHtml = "编辑成功！";
                    UserHelp.SetLogin(dal.GetModel(info.DataID));
                    GetData();
                }
                else
                {
                    this.divError.InnerHtml = "编辑失败！";
                }
            }


        }




    }
}
