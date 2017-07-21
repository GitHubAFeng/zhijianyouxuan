//----------------------------------------------------------------------
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :
// Created by zhangxiaoliang at 2011-4-13 9:57:13.
// E-Mail: zhangxiaoliang@Ihangjing.com
//----------------------------------------------------------------------
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
using System.Collections.Generic;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;

public partial class Index_bak : MasterPageBase
{
    EAddress daladdress = new EAddress();
    protected string Business = string.Empty, Servicefee = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string cityid = WebUtility.get_userCityid();
            if (cityid == "0")
            {
                string url = "citys.aspx";
                Response.Redirect(url);
            }
            hfcityname.Value = WebUtility.get_userCityName();
            lbcityname.InnerText = WebUtility.get_userCityName();

            CityInfo city = new City().GetModel(Convert.ToInt32(cityid));
            hidLat.Value = city.Lat;
            hidLng.Value = city.Lng;

            //历史地址
            int unid=-1;
            if (UserHelp.IsLogin())
            {
                unid = UserHelp.GetUser().DataID;
            }
            if (unid > 0)
            {
                string SqlWhere = string.Format("UserId={0}",unid);
                IList<EAddressInfo> list = new List<EAddressInfo>();
                //int count = daladdress.GetCount(SqlWhere);
                list = daladdress.GetList(15, 1, SqlWhere, "pri", 1);
                WebUtility.BindRepeater(rptaddress, list);      
            }

            WebUtility.BindRepeater(rpthotbuild, CacheHelper.GetBuildingList().Where(a => a.cityid == city.cid && a.IsShow == 1).ToList());
            WebUtility.BindRepeater(rptbuild, CacheHelper.GetBuildingList().Where(a => a.cityid == city.cid).ToList());

            hfshowtype.Value = SectionProxyData.GetSetValue(43);

            HtmlMeta desc = new HtmlMeta();
            desc.Name = "Description";
            desc.Content = WebUtility.GetDescription();
            Page.Header.Controls.Add(desc);
            HtmlMeta keywords = new HtmlMeta();
            keywords.Name = "keywords";
            keywords.Content = WebUtility.GetKeywords();
            Page.Header.Controls.Add(keywords);

        }

    }

}
