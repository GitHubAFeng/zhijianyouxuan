﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
/// 删除地址
/// </summary>
public partial class shopqy_54tss_Admin_Service_ajax_deladdress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        EAddress dal = new EAddress();
        int DataId = Convert.ToInt32(WebUtility.InputText(Request["DataId"]));
        string Tel = WebUtility.InputText(Request["Tel"]);

        int rs = -1;
        rs = dal.DelEAddress(DataId);
        Response.Clear();
        //反回1表示正确，-1表示出错
        if (rs != 1)
        {
            Response.Write(rs);//出错
        }
        else
        {
            string crm_uid = WebUtility.FixgetCookie("crm_uid");
            StringBuilder sb = new StringBuilder();
            string addsql = " userid =" + crm_uid;
            IList<EAddressInfo> addlist = new EAddress().GetList(5, 1, addsql, "pri", 1);
            if (addlist.Count > 0)
            {
                EAddressInfo addmodel = new EAddressInfo();
                addmodel.Receiver = "新地址";
                addmodel.Address = "";
                addmodel.BuildingName = "";
                addlist.Add(addmodel);
            }
            int i = 0;
            foreach (EAddressInfo item in addlist)
            {
                sb.Append("<li><input type=\"radio\" name=\"addressradio1\" id=\"addrlist_" + item.DataID + "\" ");
                if (i == 0)
                {
                    sb.Append("checked ");

                    sb.Append(" class=\"radio1 first_addr\"");
                }
                else
                {
                    sb.Append(" class=\"radio1 \"");
                }
                sb.Append(" onclick=\"setaddress(this); \"");
                sb.Append(" value=\"" + item.Receiver + "^" + item.Address + "^" + item.Lat + "^" + item.Lng + "^" + item.DataID + "^" + item.BuildingID + "\" />");

                sb.Append("<label for=\"addrlist_" + item.DataID + "\">" + item.Receiver + " " + item.Address + "</label>");
                sb.Append("</li>");
                i++;
            }
            Response.Write(sb.ToString());
        }

        Response.End();
    }
}
