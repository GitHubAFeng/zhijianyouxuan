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

public partial class AreaAdmin_updateWebBasic : System.Web.UI.Page
{
    WebBasic dal = new WebBasic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            WebBasicInfo info = dal.GetModel(HjNetHelper.GetQueryInt("id", 0));
            FCKContent.Value = info.Value;
            tbKey.Text = info.Key;
            lbinve.InnerText = info.Inve1;
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        WebBasicInfo info = dal.GetModel(HjNetHelper.GetQueryInt("id" , 0));
        info.Value = FCKContent.Value;
        if (dal.Update(info) > 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','3000','true','text');");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','3000','true','text');");
        }
        SectionProxyData.ClearWebSet();
    }
}
