using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

public partial class Ajax_getvipgrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            VipGrade dal = new VipGrade();
            string vip = WebUtility.InputText(Request["vip"]);
            VipGradeInfo info = dal.GetModel(Convert.ToInt32(vip));
            string rs = "兑换" + info.GradeName + "所需的积分为" + info.GaiPoint + "";

            Response.Write(rs);
        }
        catch
        {
            Response.Write("-1");
        }
        Response.End();
        
    }
}
