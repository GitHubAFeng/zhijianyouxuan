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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
public partial class Admin_shop_NoticenewsDetail :AdminPageBase
{
    Points daltogo = new Points();
    Noticenews dal = new Noticenews();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("tid")))
            {
                int togoid = HjNetHelper.GetQueryInt("tid", 0);
                PointsInfo togomodel = daltogo.GetModel(togoid);
                this.lbtname.Text = togomodel.Name;
            }
            GetNoticenews();
        }
    }
    protected void GetNoticenews()
    {
        if (!string.IsNullOrEmpty(HjNetHelper.GetQueryString("id")))
        {
            int dataid = HjNetHelper.GetQueryInt("id", 0);
            NoticenewsInfo info = dal.GetModel(dataid);
            this.tbAddDate.Text = info.Adddate.ToShortDateString();
            this.lbtname.Text = info.TogoName.ToString();
            this.tbproducer.Value = info.producer.ToString();
            this.Dropstatus.SelectedValue = info.status.ToString();
            this.tbName.Text= info.Title.ToString();
            WebUtility.SelectValue(ddlstate, info.inve2);
        }
        else
        {
            tbAddDate.Text = DateTime.Now.ToString();
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        NoticenewsInfo info = new NoticenewsInfo();
        info.DataId = HjNetHelper.GetQueryInt("id", 0);
        info.Title = this.tbName.Text.ToString();
        info.producer = this.tbproducer.Value;
        info.status = Convert.ToInt32(Dropstatus.SelectedValue);
        this.tbAddDate.Text = DateTime.Now.ToShortDateString();
        info.Adddate = DateTime.Now;
        info.inve1 = HjNetHelper.GetQueryInt("tid", 0);
        info.inve2 = ddlstate.SelectedValue;

        if (Request["id"] != null)
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Update(info) > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','2000','true','text');");
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','2000','true','text');");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (dal.Add(info) > 0)
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:添加成功!','250','150','true','2000','true','text');");
                this.tbName.Text = "";
                this.tbproducer.Value = "";
            }
            else
            {
                AlertScript.RegScript(this, UpdatePanel1, "tipsWindown('提示信息','text:添加失败!','250','150','true','2000','true','text');");

            }
        }
    }

    protected void btgo_Click(object sender, EventArgs e)
    {
        Response.Redirect("NoticenewsList.aspx?tid=" + Request["tid"]);
    }
}
