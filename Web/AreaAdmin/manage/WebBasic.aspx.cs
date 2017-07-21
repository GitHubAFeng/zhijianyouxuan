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
using Hangjing.Cache;

/// <summary>
/// 系统参数设置 比如 积分 
/// </summary>
public partial class AreaAdmin_WebBasic : System.Web.UI.Page
{
    private WebBasic dal = new WebBasic();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string sql = "stype > 0";
        this.rptList.DataSource = dal.GetList(100 ,1 , sql ,"dataid" , 0);
        this.rptList.DataBind();
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        WebBasicInfo info = new WebBasicInfo();

        info = dal.GetModel(Convert.ToInt32(hidDataId.Value));

        info.Value = tbValue.Text;
        //判断权限
        int _rs = WebUtility.AreaAdmin_checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dal.Update(info) > 0)
        {
            BindData();
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑成功!','250','150','true','3000','true','text');");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:编辑失败!','250','150','true','3000','true','text');");
        }
        SectionProxyData.ClearWebSet();
    }

    protected string setLink(object key, object value, object dataid, object stype)
    {
        string rs = "";
        switch (stype.ToString())
        {
            case "1":
                rs = " <a onclick='return SetKeyValue(\"" + key + "\",\"" + value + "\",\"" + dataid + "\");' href=\"#\">编辑</a>";
                break;
            case "2":
                rs = " <a href=\"updateWebBasicfix.aspx?id=" + dataid + "\">编辑</a>";
                break;
            case "3":
                rs = " <a href=\"updateWebBasic.aspx?id=" + dataid + "\">编辑</a>";
                break;

        }
        return rs;
    }
}
