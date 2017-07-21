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

public partial class isactive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int id = HjNetHelper.GetQueryInt("id", 0);
        ECustomer bll = new ECustomer();
        ECustomerInfo info = bll.GetModel(id);
        if (info == null)
        {
            AlertScript.RegScript(Page, "alert('验证失败，请确认连接是否正确。');window.location = 'index.aspx';");
        }
        else
        {
            if (info.ActivateCode != HjNetHelper.GetQueryString("code"))
            {
                AlertScript.RegScript(Page, "alert('验证失败请确认连接是否正确。');window.location = 'index.aspx';");
                return;
            }
            if (info.IsActivate == 1)
            {
                AlertScript.RegScript(Page, "alert('您已经通过验证。');window.location = 'index.aspx';");
                return;
            }
            info.IsActivate = 1;
            if (bll.UpdateUserValue(info.DataID, "isActivate", 1) > 0)
            {
                int point = Convert.ToInt32( SectionProxyData.GetSetValue(22));

                bll.addpoint(info.DataID, "通过邮箱验证", point);
                //重新登录
                UserHelp.SetLogin(bll.GetModelByNameAPassword(info.Name, info.Password));

                AlertScript.RegScript(Page, "window.location = 'RegisterEmailSuccess.aspx?id=" + info.DataID + "';");
            }
        }
    }
}
