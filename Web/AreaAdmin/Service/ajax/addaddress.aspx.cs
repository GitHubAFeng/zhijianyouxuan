using System;
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
/// 添加地址
/// </summary>
public partial class qy_54tss_AreaAdmin_Ser1vice_ajax_addaddress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int DataId = Convert.ToInt32(WebUtility.InputText(Request["DataId"]));
        string Tel = WebUtility.InputText(Request["Tel"]);
        string UserName = Server.UrlDecode(WebUtility.InputText(Request["UserName"]));
        string Address = Server.UrlDecode(WebUtility.InputText(Request["Address"]));
        string lat = WebUtility.InputText(Request["lat"]);
        string lng = WebUtility.InputText(Request["lng"]);

        string crm_uid = WebUtility.FixgetCookie("crm_uid");
        int uid = 0;
        if (crm_uid != null && crm_uid != "")
        {
            uid = Convert.ToInt32(crm_uid);
        }
        else
        { 
            //添加用户

            ECustomerInfo model = new ECustomerInfo();
            string email = "";
            string password = WebUtility.GetMd5("123456");

            string points = SectionProxyData.GetSetValue(19);

            //添加地址
            model.EMAIL = email;
            model.Password = password;
            model.RegTime = DateTime.Now;
            model.Point = Convert.ToInt32(points);
            model.Name = Tel;
            model.IsActivate = 1;
            model.ActivateCode = WebUtility.RandStr(200);
            model.GroupID = 0;
            model.WebSite = "";
            model.RID = "0";
            model.TrueName = UserName;
            model.MSN = "";
            model.Sex = "";
            model.Tell = Tel;
            model.Phone = "";
            model.QQ = "";
            model.State = "0";

            uid = new ECustomer().Add(model);

            WebUtility.FixsetCookie("crm_uid", uid.ToString(), 1);
        }

        EAddressInfo info = new EAddressInfo();
        info.DataID = DataId;
        info.Receiver = UserName;
        info.Address = Address;
        info.BuildingID = 0;
        info.Phone =Tel;
        info.Mobilephone = Tel;
        info.UserID = uid;
        info.AddTime = DateTime.Now;
        info.Pri = 0;
        info.Lat = lat;
        info.Lng = lng;
        int rs = -1;

        if (DataId == 0)
        {
            rs = new EAddress().Add(info);
            info.DataID = rs;
        }
        else
        {
            rs = new EAddress().Update(info);
        }
        Response.Clear();
        //反回1表示正确，-1表示出错
        if (rs <=0)
        {
            Response.Write(rs);//出错
        }
        else
        {

            WebUtility.FixsetCookie("used_addressid",info.DataID+"",1);

            StringBuilder sb = new StringBuilder();
            string addsql = " userid =" + uid;
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
