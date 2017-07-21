using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class familyhonor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ECustomerInfo user = UserHelp.GetUser();
            if (user == null)
            {
                Response.Redirect("~/login.aspx?returnurl=" + Server.UrlEncode(Request.Url.ToString()));
            }
            else
            {
                user = new ECustomer().GetModel(user.DataID); 
                UserHelp.SetLogin(user);

                IList<ECustomerInfo> users = new List<ECustomerInfo>();

                ECustomerInfo item = new ECustomerInfo();
                item.Picture = user.Picture;
                item.GroupID = user.GroupID;
                item.distributemoney = user.distributemoney;

                distributorInfo model = new distributor().GetSuperiors(user.DataID);
                item.ActivateCode = model.oneName;

                item.DataID = -1;


                users.Add(item);

                WebUtility.BindRepeater(rptuser, users);

                IList<ShopDataInfo> childcountlist = new distributor().getChildCount(user.DataID);

                string[] numstr = {"一","二","三" };

                int allchildcount = 0;

                for (int i = 0; i < childcountlist.Count && i < 3; i++)
                {
                    childcountlist[i].classname = numstr[i];
                    allchildcount += childcountlist[i].ID;
                }

                WebUtility.BindRepeater(rptchild, childcountlist);

                allchild.InnerText = allchildcount.ToString();

            }
        }
    }
}