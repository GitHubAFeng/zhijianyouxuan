using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

// CopyRight (c) 2009-2012 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2012-02-18
public partial class APP_Android_GetUserAddressDetailv2 : APIPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<EAddressInfo> list = new List<EAddressInfo>();
        EAddress dal = new EAddress();

        int dataid = HjNetHelper.GetPostParam("dataid", 0);

        list = dal.GetList(1, 1, "dataid=" + dataid + "", "dataid", 1);

        Response.Clear();

        StringBuilder listjson = new StringBuilder();
        listjson.Append("");

        EAddressInfo info = new EAddressInfo();

        for (int i = 0; i < list.Count; i++)
        {
            info = new EAddressInfo();

            info = list[i];

            listjson.Append("{\"dataid\":\"" + info.DataID.ToString().ToString() + "\",\"receiver\":\"" + info.Receiver.ToString() + "\",\"buildingid\":\"" + info.BuildingID.ToString() + "\",\"sex\":\"" + info.Phone.ToString() + "\",\"mobilephone\":\"" + info.Mobilephone.ToString() + "\",\"address\":\"" + info.Address.ToString() + "\",\"isdefault\":\"" + info.Pri.ToString() + "\",");

            listjson.Append("\"lat\":\"" + info.Lat + "\",");
            listjson.Append("\"lng\":\"" + info.Lng + "\",");

            listjson.Append("\"BuildingName\":\"" + info.BuildingName + "\",");
            listjson.Append("\"Floor\":\"\",");
            listjson.Append("\"Room\":\"\"");

            listjson.Append("}");
        }

        Response.Write(listjson.ToString());
        Response.End();
    }
}
