using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hangjing.Common;
using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Newtonsoft.Json;
using System.Web;

/// <summary>
/// 商家结算
/// </summary>
public class ShopSettleController : ApiController
{
    Points bll = new Points();

    // GET api/<controller>
    public HttpResponseMessage Get()
    {
        int shopid = HjNetHelper.GetQueryInt("shopid", 0);

        string deliversql = " unid =" + shopid;
        string basesql = " OrderStatus = 3 " ;

        IList<TogoInfo> datas = new List<TogoInfo>();

        {
            string time = "今天";
            IList<TogoInfo> shops = bll.GetListWithOrderStatistics(1, 1, deliversql, basesql + " and DATEDIFF(day,OrderDateTime,GETDATE()) =0", "ordercount");
            if (shops.Count == 0)
            {
                shops.Add(new TogoInfo() { LinkMan = time });
            }
            else
            {
                shops[0].LinkMan = time;
            }

            datas.Add(shops[0]);

        }

        {
            string time = "近7天";
            IList<TogoInfo> shops = bll.GetListWithOrderStatistics(1, 1, deliversql, basesql + " and OrderDateTime > '"+ DateTime.Now.AddDays(-7).ToFormString()+"'  ", "ordercount");
            if (shops.Count == 0)
            {
                shops.Add(new TogoInfo() { LinkMan = time });
            }
            else
            {
                shops[0].LinkMan = time;
            }

            datas.Add(shops[0]);
        }

        {
            string time = "近30天";
            IList<TogoInfo> shops = bll.GetListWithOrderStatistics(1, 1, deliversql, basesql + " and OrderDateTime > '" + DateTime.Now.AddDays(-30).ToFormString() + "'  ", "ordercount");
            if (shops.Count == 0)
            {
                shops.Add(new TogoInfo() { LinkMan = time });
            }
            else
            {
                shops[0].LinkMan = time;
            }
            datas.Add(shops[0]);
        }


        apiResultInfo res = new apiResultInfo()
        {
            msg = "",
            state = 1,
            data = datas
        };

        return WebUtility.toJson(res);
    }


}
