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
// 2012-02-18
// 获取用户的地址列表

public partial class APP_Android_GetUserAddressListv : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<EAddressInfo> list = new List<EAddressInfo>();
        EAddress dal = new EAddress();

        int userid = HjNetHelper.GetPostParam("userid", 0);

        list = dal.GetList(100, 1, "userid=" + userid + "", "dataid", 1);

        Response.Clear();

        StringBuilder listjson = new StringBuilder();
        listjson.Append("{\"page\":\"1\",\"total\":\"" + list.Count.ToString() + "\", \"list\":[");

        EAddressInfo info = new EAddressInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = new EAddressInfo();

            info = list[i];

            listjson.Append("{\"dataid\":\"" + info.DataID.ToString().ToString() + "\",\"receiver\":\"" + info.Receiver.ToString() + "\",\"lat\":\"" + info.Lat.ToString() + "\",\"lng\":\"" + info.Lng.ToString() + "\",\"buildingid\":\"" + info.BuildingID.ToString() + "\",\"phone\":\"" + info.Phone.ToString() + "\",\"mobilephone\":\"" + info.Mobilephone.ToString() + "\",\"address\":\"" + info.Address.ToString() + "\",\"isdefault\":\"" + info.Pri.ToString() + "\"},");
        }

        listjson.Append("]}");

        Response.Write(listjson.ToString().Replace("},]}", "}]}"));
        Response.End();
    }
}
