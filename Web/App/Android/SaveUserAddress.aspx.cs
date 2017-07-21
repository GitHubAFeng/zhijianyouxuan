using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

// CopyRight (c) 2009-2012 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2012-02-17
// 保存用户地址薄:保存、更新、删除

public partial class APP_Android_SaveUserAddressv : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder json = new StringBuilder();
        Response.Clear();

        //传入参数
        //?op=0/1/-1&
        EAddress dal = new EAddress();

        EAddressInfo info = new EAddressInfo();

        info.Receiver = Server.UrlDecode(WebUtility.InputText(Request["receiver"]));
        info.Address = Server.UrlDecode(WebUtility.InputText(Request["address"]));
        info.BuildingID = 0;
        info.Phone = "";
        info.Mobilephone = WebUtility.InputText(Request["mobilephone"]);
        info.UserID = Convert.ToInt32(Request["userid"]);
        info.DataID = Convert.ToInt32(Request["dataid"]);
        info.AddTime = DateTime.Now;
        info.Pri = 0;
        info.Lng = WebUtility.InputText(Request["lng"]);
        info.Lat = WebUtility.InputText(Request["lat"]);



        //op=1 新增地址 op=0 更新地址 op=-1 删除地址
        if (Request["op"] == "1")
        {
            int id = dal.Add(info);
            if (id > 0)
            {
                json.Append("{\"state\":\"" + id + "\"}");
            }
            else
            {
                json.Append("{\"state\":\"0\"}");
            }
        }
        else if (Request["op"] == "0")
        {
            if (dal.Update(info) > 0)
            {
                json.Append("{\"state\":\"1\"}");
            }
            else
            {
                json.Append("{\"state\":\"0\"}");
            }
        }
        else if (Request["op"] == "-1")
        {
            if (dal.DelEAddress(info.DataID) > 0)
            {
                json.Append("{\"state\":\"1\"}");
            }
            else
            {
                json.Append("{\"state\":\"0\"}");
            }
        }

        Response.Write(json.ToString());
        Response.End();
    }
}
