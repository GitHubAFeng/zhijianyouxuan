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
using Hangjing.Model;
using Hangjing.Common;
using Hangjing.Cache;


public partial class EasyEatHome_MTogo_TogoLocal : AdminPageBase
{
    /// <summary>
    /// 是否已经定位,0表示未定位。1表示已经定位
    /// </summary>
    private string isloacal
    {
        get
        {
            object o = ViewState["isloacal"];
            return (o == null) ? "0" : Convert.ToString(o);
        }
        set
        {
            ViewState["isloacal"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("A");
        if (!Page.IsPostBack)
        {
            //获取商家
            ETogoLocalInfo info = new ETogoLocalInfo();
            ETogoLocal bll = new ETogoLocal();

            info = bll.GetInfoById(HjNetHelper.GetQueryString("tid"));
            int cid = HjNetHelper.GetQueryInt("cid", 0);

            CityInfo citymodel = new City().GetModel(cid);
            if (citymodel != null)
            {
                hfcity.Value = citymodel.cname;
            }

            //如无坐标点则默认
            if (info != null && info.Lat != null && info.Lng != null && info.Lat != "" && info.Lng != "")
            {
                hidLat.Value = info.Lat;
                this.hidLng.Value = info.Lng;
                isloacal = "1";
            }
            else
            {
                hidLat.Value = Map.DefautLocal.getlocalInfo().Lat;
                hidLng.Value = Map.DefautLocal.getlocalInfo().Lng;
                isloacal = "0";
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        //查看商家是否已经存在位置信息记录

        //获取商家
        ETogoLocalInfo info = new ETogoLocalInfo();

        ETogoLocal bll = new ETogoLocal();
        info = bll.GetInfoById(HjNetHelper.GetQueryString("tid"));
        info.Lat = hidLat.Value;
        info.Lng = hidLng.Value;
        info.TogoId = Convert.ToInt32(HjNetHelper.GetQueryString("tid"));
        info.Polygon = info.Polygon == null ? "" : info.Polygon;
        info.Radius = info.Radius == null ? (decimal)0.0 : info.Radius;

        try
        {

            bll.Add(info);
            EasyEatCache.GetCacheService().RemoveObject("/ShopLocallist");

            AlertScript.RegScript(this, "alert('保存成功!','250','150','true','1000','true','text');");
        }
        catch
        {
            AlertScript.RegScript(this, "tipsWindown('提示信息','text:保存失败!','250','150','true','1000','true','text');hideload_super();");
        }
    }

    protected void return_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetPolygonSuccess.aspx?tid=" + HjNetHelper.GetQueryString("tid") + "&cid=" + Request["cid"]);
    }
}

