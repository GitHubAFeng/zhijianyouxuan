using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

/// <summary>
/// 根据城市编号，获取区域
/// </summary>
public partial class APP_Android_GetSectionListByCID : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int cityid = HjNetHelper.GetPostParam("cid", 0);
        IList<SectionInfo> list = SectionProxyData.GetSectionList().Where(p => p.cityid == cityid).ToList<SectionInfo>();

        Response.Clear();

        StringBuilder listjson = new StringBuilder();
        listjson.Append("{\"page\":\"1\",\"total\":\"" + list.Count.ToString() + "\", \"datalist\":[");

        SectionInfo info = new SectionInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = new SectionInfo();

            info = list[i];

            listjson.Append("{\"SectionID\":\"" + info.SectionID + "\",\"SectionName\":\"" + info.SectionName.ToString() + "\",\"cityid\":\"" + info.cityid + "\",\"Parentid\":\"" + info.Parentid.ToString() + "\"},");
        }

        listjson.Append("]}");

        Response.Write(listjson.ToString().Replace("},]}", "}]}"));
        Response.End();
    }
}
