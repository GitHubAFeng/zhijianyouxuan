﻿using System;
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
/// 商品规格相关操作
/// </summary>
public class FoodStyleController : ApiController
{
    FoodStyle dal = new FoodStyle();

    // GET api/<controller>
    public HttpResponseMessage Get()
    {
        int foodid = HjNetHelper.GetQueryInt("foodid",0);
        IList<FoodStyleInfo> Stylelist = dal.GetList(20, 1, "FoodtId = " + foodid, "dataid", 1);

        apiResultInfo res = new apiResultInfo()
        {
            msg = "",
            state = 1,
            data = Stylelist
        };

        return WebUtility.toJson(res);
    }

    // GET api/<controller>/5
    public HttpResponseMessage Get(int id)
    {
        FoodStyleInfo model = dal.GetModel(id);
        apiResultInfo res = new apiResultInfo()
        {
            msg = "",
            state = 1,
            data = model
        };

        return WebUtility.toJson(res);
    }

    // POST api/<controller>
    [HttpPost]
    public HttpResponseMessage Post([FromBody]string value)
    {
        apiResultInfo res = new apiResultInfo()
        {
            msg = "",
            state = 1,
        };
        FoodStyleInfo postmodel = JsonConvert.DeserializeObject<FoodStyleInfo>(value);
        if (postmodel.DataId > 0)
        {
            FoodStyleInfo model = dal.GetModel(postmodel.DataId);
            model.Title = postmodel.Title;
            model.Price = postmodel.Price;

            dal.Update(postmodel);
            res.data = postmodel.DataId;
        }
        else
        {
            postmodel.SaleSum =0;
            postmodel.MaxPerDay =0;
            postmodel.InUser =0;
            postmodel.Intro = "";
            postmodel.MarkeyPrice = 0;
            postmodel.Inve1 = 0;
            postmodel.Inve2 = "";

            postmodel.DataId = dal.Add(postmodel);
            res.data = postmodel.DataId;
        }

        return WebUtility.toJson(res);
    }


    // DELETE api/<controller>/5
    public HttpResponseMessage Delete(int id)
    {
        apiResultInfo res = new apiResultInfo()
        {
            msg = "",
            state = 1,
        };

        if (dal.DelProductStyle(id) <= 0)
        {
            res.state = 0;
            res.msg = "删除失败，请联系管理";
        }

        return WebUtility.toJson(res);
    }


}
