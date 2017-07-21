using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using System.Text;
using Hangjing.Common;

/// <summary>
/// 意见反馈
/// </summary>
public partial class App_Android_Feedback : System.Web.UI.Page
{
    EUserWord suggbll = new EUserWord();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string state = "1";

        string content = WebUtility.InputText(Request["content"]);
        string username = WebUtility.InputText(Request["username"]);
        string contact = WebUtility.InputText(Request["contact"]);
        int userid = HjNetHelper.GetPostParam("userid", 0);

        EUserWordInfo model = new EUserWordInfo();

        model.UserName = username;
        model.UserID = Convert.ToInt32(userid);
        model.Word = content;
        model.State = 0;
        model.Time = DateTime.Now;
        model.Rremark = "";
        model.RTime = DateTime.Now;
        model.adminID = "";

        model.DataID = suggbll.Add(model);

        if (model.DataID > 0)
        {
            Response.Write("{\"state\":\"" + state + "\",\"msg\":\"\"}");

            Response.End();

        }
    }
}
