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
/// 会员注册 
/// </summary>
public partial class AndroidAPI_NewRegeditv : APIPageBase
{
    ECustomer userBLL = new ECustomer();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string state = "1";

        //输入参数：手机号、密码
        string tel = WebUtility.InputText(Request["tel"]);
        string password = WebUtility.GetMd5(WebUtility.InputText(Request["password"]));


        #region 相关验证
        if (string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(password))
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-1\",\"msg\":\"手机号或密码为空\"}");
            Response.End();
            return;
        }

        string sql = "name = '" + tel + "'";
        int count = userBLL.GetCount(sql);
        if (count > 0)
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-2\",\"msg\":\"该手机号已注册\"}");
            Response.End();
            return;
        }

        sql = "tell = '" + tel + "'";
        count = userBLL.GetCount(sql);
        if (count > 0)
        {
            Response.Write("{\"userid\":\"0\",\"state\":\"-3\",\"msg\":\"手机号码重复\"}");
            Response.End();
            return;
        }
        #endregion


        ECustomerInfo model = new ECustomerInfo();
        int point = Convert.ToInt32(SectionProxyData.GetSetValue(19));
        model.Name = tel;
        model.Password = password;
        model.EMAIL = "";
        model.RegTime = DateTime.Now;
        model.Point = point;
        model.IsActivate = 0;
        model.Sex = "";
        model.ActivateCode = WebUtility.RandStr(200);
        model.GroupID = 0;
        model.RID = "0";
        model.TrueName = "";
        model.QQ = "";
        model.MSN = "";
        model.Phone = "0";
        model.Tell = tel;
        model.WebSite = ((int)OrderSource.web).ToString();
        model.Usermoney = 0;
        model.PhoneActivate = 0;

        model.DataID = userBLL.Add(model);

        if (model.DataID > 0)
        {
            EPointRecordInfo info = new EPointRecordInfo();
            info.UserID = model.DataID;
            info.Point = point;
            info.Event = "手机版新注册用户,获得积分" + point + "个";
            info.Time = DateTime.Now;
            EPointRecord bll = new EPointRecord();
            bll.Add(info);

            Response.Write("{\"userid\":\"" + model.DataID.ToString() + "\",\"state\":\"" + state + "\",\"msg\":\"\"}");
        }

        Response.End();
    }
}
