using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Model;
using Hangjing.Common;
using System.Data;

public partial class citys : MasterPageBase
{
    City dalcity = new City();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            IList<CityInfo> citylist = SectionProxyData.GetCityList();

            IList<citygroup> group = new List<citygroup>();

            IEnumerable<IGrouping<string, CityInfo>> result = citylist.Cast<CityInfo>().GroupBy<CityInfo, string>(dr => dr.ReveVar);//按字母母分组
            foreach (IGrouping<string, CityInfo> ig in result)
            {
                citygroup model = new citygroup();
                model.firstletter = ig.Key.ToUpper();
                model.citylist = new List<CityInfo>();

                foreach (var dr in ig)
                {
                    CityInfo t = new CityInfo();
                    t.cid = Convert.ToInt32(dr.cid);
                    t.cname = dr.cname.ToString();
                    model.citylist.Add(t);
                }
                group.Add(model);

            }

            WebUtility.BindRepeater(rptcity, group);
        }
    }

}
