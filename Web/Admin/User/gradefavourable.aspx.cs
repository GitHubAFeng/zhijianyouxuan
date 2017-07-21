using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.Web.Script.Serialization;

public partial class Admin_Permission_gradefavourable : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            WebUtility.BindRepeater(rptlist, SectionProxyData.GetUserGradeList());
        }
    }


    protected void sava_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        User_Grade_R dalrp = new User_Grade_R();
        string json = hidhas.Value;
        IList<User_Grade_RInfo> rplist = new JavaScriptSerializer().Deserialize<IList<User_Grade_RInfo>>(json);
        foreach (User_Grade_RInfo item in rplist)
        {
            if (item.pid == 0)
            {
                dalrp.Add(item);
            }
            else
            {
                dalrp.Update(item);   
            }
        }
        AlertScript.RegScript(this.Page, UpdatePanel1, "hideload_super();alert('设置成功');gourl('gradefavourable.aspx')");
        SectionProxyData.ClearUserGradeList();
    }
}
