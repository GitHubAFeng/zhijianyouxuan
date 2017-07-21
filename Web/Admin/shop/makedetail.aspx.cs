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

public partial class qy_54tss_Admin_aboutus_makedetail : System.Web.UI.Page
{
    Practice dal = new Practice();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {

            if (HjNetHelper.GetQueryString("id") == "")
            {
                pageType.Text = "添加口味信息";
            }
            else
            {
                pageType.Text = "编辑口味信息";
                PracticeInfo info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
  
                tbpname.Text = info.pname;
         
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        PracticeInfo info = new PracticeInfo();
        if (Request["id"] != null)
        {
            info = dal.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
        }
        else
        {
            info.cityid = 0;
        }
        info.pnum = "";
        info.pname = WebUtility.InputText(tbpname.Text);
        info.namepy = "";
        info.Inve1 = HjNetHelper.GetQueryInt("tid", 0);
        info.Inve2 = "";

        if (HjNetHelper.GetQueryString("id") == "")
        {
            //判断编号有没有重复
            string sql = "pname = '" + info.pname + "' and inve1 = " + info.Inve1;
            int count = dal.GetCount(sql);
            if (count > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "alert('相同编号的做法与要求已经存在，请检查');");
                return;
            }

            if (dal.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('新增失败','error','true',5);");
            }
        }
        else
        {
            //判断编号有没有重复
            string sql = "pname = '" + info.pname + "' and pid <> " + info.pId + " and inve1 = " + info.Inve1;
            int count = dal.GetCount(sql);
            if (count > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "alert('相同编号的做法与要求已经存在，请检查');");
                return;
            }
            if (dal.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑成功','success','true',5);");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "showMessage('编辑失败','error','true',5);");
            }
        }
    }
}
