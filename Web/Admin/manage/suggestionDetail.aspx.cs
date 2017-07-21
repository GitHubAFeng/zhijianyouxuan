using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class qy_54tss_Admin_suggestionDetail : System.Web.UI.Page
{
    EUserWord suggbll = new EUserWord();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            this.pageType.Text = "编辑";
            GetData();
        }
    }

    public void GetData()
    {
        if (HjNetHelper.GetQueryString("id") != "")
        {
            EUserWordInfo info = new EUserWordInfo();
            info = suggbll.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
            tbUserName.Text = info.UserName;
            tbdesc.InnerHtml = info.Word;
            WebUtility.SelectValue(ddlstate , info.State.ToString());

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        EUserWordInfo info = suggbll.GetModel(Convert.ToInt32(HjNetHelper.GetQueryString("id")));
        info.DataID = HjNetHelper.GetQueryInt("id", 0);
        info.Word = WebUtility.InputText(tbdesc.InnerHtml);
        info.State = Convert.ToInt32(ddlstate.SelectedValue);
        info.RTime = DateTime.Now;
        info.adminID = UserHelp.GetAdmin().ID.ToString();
        if (HjNetHelper.GetQueryString("id")!="")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (suggbll.Update(info) >0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','','true','text');");
            }
        }
    }
}
