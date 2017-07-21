using System;
using System.Collections.Generic;
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

public partial class header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AdTableInfo model_ad = new AdTable().GetModel(1);
            logolink.HRef = model_ad.AdAdrees;
            logolink.Title = model_ad.AdName;
            logoimg.Src = WebUtility.ShowPic(model_ad.AdImageAdrees);
            logoimg.Width = model_ad.AdWidth;
            logoimg.Height = model_ad.AdHeight;

            string[] keyword = SectionProxyData.GetSetValue(17).Split('|');
            
            IList<ShopDataInfo> list = new List<ShopDataInfo>();

            foreach (string item in keyword)
            {
                string[] list2=item.Split(',');
               
                    ShopDataInfo model = new ShopDataInfo();
                    model.value = list2[0].ToString();
                    model.classname = list2[1].ToString();
                    list.Add(model);
               
                
            }
            WebUtility.BindRepeater(rptqq, list);

            lbcityname.InnerText = WebUtility.get_userCityName();
        }
    }

}