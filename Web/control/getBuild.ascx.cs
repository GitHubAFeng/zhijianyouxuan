using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class control_getBuild : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //缓存
        rptSectinList.DataSource = SectionProxyData.GetSectionList();
        rptSectinList.DataBind();
    }
}
