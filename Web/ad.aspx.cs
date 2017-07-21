using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Web.Script.Serialization;
using Hangjing.Common;
using Hangjing.DBUtility;

/// <summary>
/// 生成所有js广告
/// </summary>
public partial class Admin_jsad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<AdTableInfo> list = new AdTable().GetList(1000, 1, "1=1", "tid", 0);
        for (int i = 0; i < list.Count; i++)
        {
            CreatJs js = new CreatJs();
            js.CreatJsFile(list[i]);
        }
        Response.Write("生成成功");
    }
}
