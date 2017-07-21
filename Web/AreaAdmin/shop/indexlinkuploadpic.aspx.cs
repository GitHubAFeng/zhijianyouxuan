using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hangjing.Common;

/// <summary>
/// 首页链接上传图片
/// </summary>
public partial class qy_54tss_AreaAdmin_uploadpic_brandindexlinkuploadpic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hfsid.Value = HjNetHelper.GetQueryInt("id", 0).ToString();
    }
}
