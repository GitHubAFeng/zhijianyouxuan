using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Admin_packet_configpacketList : System.Web.UI.Page
{
    packetconfig dal = new packetconfig();
    protected string SqlWhere
    {
        set
        {
            ViewState["sqlwhere"] = value;
        }
        get
        {
            return ViewState["sqlwhere"] == null ? "" : ViewState["sqlwhere"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1=1 ";
            GetData();
        }
    }

    protected void GetData()
    {
        IList<packetconfigInfo> list = dal.GetList(2, 1, SqlWhere, "dataid", 0);
        foreach (var item in list)
        {
            item.revevar1 = "满" + item.revevar1 + "元使用";
        }
        this.rtpUserlist.DataSource = list;
        this.rtpUserlist.DataBind();
    }
}
