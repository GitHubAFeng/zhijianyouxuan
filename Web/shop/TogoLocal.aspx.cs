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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class shop_TogoLocal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
        if (!Page.IsPostBack)
        {
            //获取商家
            ETogoLocalInfo info = new ETogoLocalInfo();

            ETogoLocal bll = new ETogoLocal();
            int id = UserHelp.GetUser_Togo().Unid;
            info = bll.GetInfoById(id + "");

            //如无坐标点则默认
            if (info != null && info.Lat != null && info.Lng != null && info.Lat != "" && info.Lng != "")
            {
                hidLat.Value = info.Lat;
                this.hidLng.Value = info.Lng;
            }
            else
            {

                hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
                hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;

            }

            hfcity.Value = SectionProxyData.GetSetValue(25);
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //查看商家是否已经存在位置信息记录

        //获取商家
        ETogoLocalInfo info = new ETogoLocalInfo();

        ETogoLocal bll = new ETogoLocal();

        info = bll.GetInfoById(UserHelp.GetUser_Togo().Unid.ToString());

        info.Lat = hidLat.Value;
        info.Lng = hidLng.Value;
        info.TogoId = UserHelp.GetUser_Togo().Unid;
        info.Polygon = info.Polygon == null ? "" : info.Polygon;
        info.Radius = info.Radius == null ? (decimal)0.0 : info.Radius;
        if (info.DataId == 0)
        {
            if (bll.Add(info) > 0)
            {
                AlertScript.RegScript(this, "alert('添加成功！');");
            }
            else
            {
                AlertScript.RegScript(this, "alert('添加失败！');hideload_super();");
            }
        }
        else
        {
            if (bll.Update(info) > 0)
            {
                AlertScript.RegScript(this, "alert('更新成功！');location.href='TogoLocal.aspx'");
            }
            else
            {
                AlertScript.RegScript(this, "alert('更新失败！');hideload_super();");
            }
        }

    }
}
