using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AreaAdmin_buildSelect : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //缓存
            rptSectinList.DataSource = SectionProxyData.GetSectionList();
            rptSectinList.DataBind();
        }
    }
}
