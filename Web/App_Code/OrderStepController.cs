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
/// 订单过程相关
/// </summary>
public class OrderStepController : ApiController
{
    OrderStep dal = new OrderStep();

    // GET api/<controller>
    public HttpResponseMessage Get()
    {
        string orderid = HjNetHelper.GetQueryString("orderid");
        HJlog.toLog("orderid="+orderid);
        IList<OrderStepInfo> Stylelist = dal.GetOrderSteps(orderid);

        apiResultInfo res = new apiResultInfo()
        {
            msg = "",
            state = 1,
            data = Stylelist
        };

        return WebUtility.toJson(res);
    }

  
}
