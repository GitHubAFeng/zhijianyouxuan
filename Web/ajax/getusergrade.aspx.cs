using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;

public partial class Ajax_getusergrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            VipGrade dal = new VipGrade();
            User_Grade_R rdal = new User_Grade_R();
            string vip = WebUtility.InputText(Request["vip"]);
            VipGradeInfo info = dal.GetModel(Convert.ToInt32(vip));
            User_Grade_RInfo rinfo = rdal.GetModelByGid(Convert.ToInt32(vip));
            string rs = info.GradeName + "享受订购" + rinfo.foodmoneyDiscount + "折，获得积分"+rinfo.pointrat+"倍。";

            Response.Write(rs);
        }
        catch
        {
            Response.Write("-1");
        }
        Response.End();
    }
}
