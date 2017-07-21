using System;
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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;
using System.IO;

public partial class shop_myshop : System.Web.UI.Page
{
    Points dal = new Points();
    EBuilding dal_build = new EBuilding();
    TogoPrinter dal_printer = new TogoPrinter();
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
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            ShopData dal = new ShopData();
            IList<ShopDataInfo> list = dal.GetsubList(0);
            cblCategory.DataSource = list;
            cblCategory.DataTextField = "classname";
            cblCategory.DataValueField = "id";
            cblCategory.DataBind();
            GetData();

        }
    }

    protected void GetData()
    {
        PointsInfo model = dal.GetModel(UserHelp.GetUser_Togo().Unid);

        if (model != null)
        {
            Info = model;
        }
    }

    private PointsInfo Info
    {
        get
        {
            PointsInfo model = dal.GetModel(UserHelp.GetUser_Togo().Unid);
           

            model.Name = WebUtility.InputText(tbName.Text);
            model.Comm = WebUtility.InputText(tbComm.Text);
            model.NamePy = WebUtility.InputText(tbNamePy.Text);
            model.CommPerson = WebUtility.InputText(tbCommPerson.Text);
            model.SendLimit = Convert.ToDecimal(WebUtility.InputText(tbSendLimit.Text));
            model.LoginName = WebUtility.InputText(tbLoginName.Text);
            if (tbPassword.Text == "")
            {

            }
            else
            {
                model.Password = WebUtility.GetMd5( WebUtility.InputText(tbPassword.Text));
            }
            model.Address = WebUtility.InputText(tbAddress.Text);
            model.Introduce = tbIntroduce.Value;
            model.bisnessStart = DateTime.Now;
            model.bisnessend = DateTime.Now;
            model.Time1Start = Convert.ToDateTime(tbTime1Start.Text);
            model.Time1End = Convert.ToDateTime(tbTime1End.Text);
            model.Time2Start = Convert.ToDateTime(tbTime2Start.Text);
            model.Time2End = Convert.ToDateTime(tbTime2End.Text);
            model.category = WebUtility.GetcheckStrFix(cblCategory);
            model.senttime = Convert.ToInt32(tbSentTime.Text);
            model.special = WebUtility.InputText(tbspecial.Text);
            model.RefreshTime = WebUtility.InputText(tbtbRefresh.Text);
           
            model.bisnessStart = tbbisnessStart1.Text == "" ? DateTime.Now : Convert.ToDateTime(tbbisnessStart1.Text);
            model.bisnessend = tbbisnessend1.Text == "" ? DateTime.Now : Convert.ToDateTime(tbbisnessend1.Text);
            model.bisnessStart2 = tbbisnessStart2.Text == "" ? DateTime.Now : Convert.ToDateTime(tbbisnessStart2.Text);
            model.bisnessend2 = tbbisnessend2.Text == "" ? DateTime.Now : Convert.ToDateTime(tbbisnessend2.Text);

            //营业时间
            model.Opentimes1 = TextBoxtime.Text == "" ? DateTime.Now : Convert.ToDateTime(TextBoxtime.Text);
            model.Opentimes2 = TtimeEnd.Text == "" ? DateTime.Now : Convert.ToDateTime(TtimeEnd.Text);
            model.Closetimes1 = TextBoxtime2.Text == "" ? DateTime.Now : Convert.ToDateTime(TextBoxtime2.Text);
            model.Closetimes2 = TtimeEnd2.Text == "" ? DateTime.Now : Convert.ToDateTime(TtimeEnd2.Text);

           

            model.PTimes = Convert.ToInt32(tbPTimes.Text);
            model.Picture = ImgUrl1.Value;
            model.email = WebUtility.InputText(tbemail.Text);

            return model;
        }
        set
        {
            oldname = value.Name;
            oldpic = value.Picture;
            tbName.Text = value.Name;
            tbCommPerson.Text = value.CommPerson;
            tbAddress.Text = value.Address;
            tbComm.Text = value.Comm;
            tbtbRefresh.Text = value.RefreshTime;

            tbbisnessStart1.Text = value.bisnessStart.ToString("HH:mm");
            tbbisnessend1.Text = value.bisnessend.ToString("HH:mm");
            tbbisnessStart2.Text = value.bisnessStart2.ToString("HH:mm");
            tbbisnessend2.Text = value.bisnessend2.ToString("HH:mm");

            tbTime1End.Text = value.Time1End.ToString("HH:mm");
            tbTime1Start.Text = value.Time1Start.ToString("HH:mm");
            tbTime2End.Text = value.Time2End.ToString("HH:mm");
            tbTime2Start.Text = value.Time2Start.ToString("HH:mm");

            TextBoxtime.Text = value.Opentimes1.ToString("HH:mm");
            TtimeEnd.Text = value.Opentimes2.ToString("HH:mm");

            TextBoxtime2.Text = value.Closetimes1.ToString("HH:mm");
            TtimeEnd2.Text = value.Closetimes2.ToString("HH:mm");
          
            tbIntroduce.Value = value.Introduce;
            ImgUrl1.Value = value.Picture;
            if (!string.IsNullOrEmpty(value.Picture))
            {
                ImgUrl.Src = WebUtility.ShowPic(value.Picture);
            }
            ////登入帐号
            tbLoginName.Text = value.LoginName;

            ////配送费用 送货时间  分类  类别
            tbSentTime.Text = value.senttime.ToString();
            tbSendLimit.Text = value.SendLimit + "";


            //数据库保存为{1},{2},...获取出来进把"{" , "}",替换
            string tempcat = value.category.Replace("{", "").Replace("}", "");
            WebUtility.CheckValueS(cblCategory, tempcat);
            tbspecial.Text = value.special;
            tbNamePy.Text = value.NamePy;
            tbemail.Text = value.email;
            tbPTimes.Text = value.PTimes.ToString();
            
        }
      
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        PointsInfo info = new PointsInfo();
        info = Info;

        if (info.Closetimes1 < info.Opentimes1)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('营业时间段一必须早于营业时间段二');hideload_super();");
            return;
        }
       
        if (dal.Update(info) > 0)
        {
            AlertScript.RegScript(Page,UpdatePanel1,  "tipsWindown('提示信息','text:编辑成功!','250','150','true','2000','true','text');");
            ImgUrl.Src = WebUtility.ShowPic(info.Picture);
        }
        else
        {
            AlertScript.RegScript(Page,UpdatePanel1, "tipsWindown('提示信息','text:修改失败!','250','150','true','2000','true','text');");
        }
    }
}
