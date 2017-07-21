using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class UserHome_MyPointRecharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!this.Page.IsPostBack)
        {
            lbMoney.Text = UserHelp.GetUser().Point.ToString();
            lbUserName.Text = UserHelp.GetUser().Name;

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {

        ECustomerInfo user = UserHelp.GetUser();

        int inputpoint = Convert.ToInt32(this.tbRealName.Text.Trim());

        decimal usermoney = inputpoint * Convert.ToDecimal(SectionProxyData.GetSetValue(31));

        if (user.Point < inputpoint && usermoney > 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:输入的积分应该小于用户积分!','250','150','true','1000','true','text');");
            return;
        }
        else
        {
            //修改用户账户积分
            string userpointstrsql = "update dbo.ECustomer set Point=Point-" + inputpoint + ",userMoney=userMoney+" + usermoney + " where DataID=" + user.DataID;

            if (WebUtility.excutesql(userpointstrsql) > 0)
            {

                UserAddMoneyLogInfo info = new UserAddMoneyLogInfo();
                UserAddMoneyLog dal = new UserAddMoneyLog();
                string orderid = "";
                string tnum = user.DataID.ToString("00000");
                orderid = "r" + tnum + DateTime.Now.ToString("yyMMddHHmmss");

                info.AddDate = DateTime.Now;
                info.AddMoney = usermoney;
                info.Inve1 = 0;
                info.Inve2 = orderid;
                info.PayDate = Convert.ToDateTime("1900-01-01 00:00:00");
                info.PayState = 1;
                info.PayType = 4;
                info.State = 0;
                info.TogoName = "";
                info.UserId = user.DataID;

                dal.Add(info);

                //AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示信息','text:积分充值成功!','250','150','true','1000','true','text');");
                string js = "loadpage()";
                AlertScript.RegScript(this.Page, this.UpdatePanel1, js);
            }
        }

    }
}
