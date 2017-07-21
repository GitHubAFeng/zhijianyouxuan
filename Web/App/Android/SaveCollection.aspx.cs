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

/// <summary>
/// 用户收藏
/// </summary>
public partial class APP_Android_SaveCollection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        //传入参数
        ETogoCollect dal = new ETogoCollect();

        ETogoCollectInfo info = new ETogoCollectInfo();

        info.togoid = HjNetHelper.GetPostParam("togoid", 0);
        info.userid = HjNetHelper.GetPostParam("userid", 0);
        info.ctime = DateTime.Now;
        info.inve1 = 0;
        info.inve2 = "";

        StringBuilder json = new StringBuilder();
        int op = Convert.ToInt32(Request["op"]);
        if (op == 1)
        {
            //检查昵称是否有重复的
            if (dal.GetTogoCount("userid=" + info.userid + " and togoid=" + info.togoid + "") > 0)
            {
                json.Append("{\"state\":\"2\"}");
            }
            else
            {
                if (dal.AddTogo(info) > 0)
                {
                    json.Append("{\"state\":\"1\"}");
                }
                else
                {
                    json.Append("{\"state\":\"0\"}");
                }
            }
        }
        if (op == -1)
        {
            if (dal.GetTogoCount("userid=" + info.userid + " and togoid=" + info.togoid + "") > 0)
            {

                if (dal.DelETogoCollect(info.userid, info.togoid) > 0)
                {
                    json.Append("{\"state\":\"1\"}");
                }
                else
                {
                    json.Append("{\"state\":\"0\"}");
                }
            }
            else
            {
                json.Append("{\"state\":\"3\"}");
            }
        }
        

        Response.Write(json.ToString());
        Response.End();
    }
}
