using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;
using NetDimension.Json.Linq;

/// <summary>
/// 第三方用户名登录操作的地址
/// 到这个页面的参数：
/// uid 编号(保存openid字段)
/// uname 用户名（保存name字段）
/// utype第三方来来源(人人，QQ，新浪 )
/// </summary>
public partial class account_ThirdpartyLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string uid = HjNetHelper.GetPostParam("uid");
        string uname = WebUtility.UrlDecode(HjNetHelper.GetPostParam("uname"));
        string utype = HjNetHelper.GetPostParam("utype");
        string uway = HjNetHelper.GetPostParam("uway");

        //如果没有这个用户 openid = uid;就加一个用户，如果有，就登录
        string sql = "openid = '" + uid + "' and wtype = '" + utype + "'";
        ECustomer dal = new ECustomer();
        int count = dal.GetCount(sql);

        int state = 0;
        if (count == 0)//没用户，生成
        {
            ECustomerInfo model = new ECustomerInfo();
            int point = Convert.ToInt32(SectionProxyData.GetSetValue(19));
            //int point = 50;
            model.EMAIL = ""; ;
            model.Password = WebUtility.GetMd5(uid + "cxd");//加cxd只是不要让用户可以修改登录
            model.RegTime = DateTime.Now;
            model.IsActivate = model.Point = point;
            model.Name = uname;
            model.ActivateCode = WebUtility.RandStr(200);
            model.GroupID = 0;
            model.WebSite = "";
            model.RID = "";
            model.Picture = "";
            model.TrueName = "";
            model.MSN = "";
            model.Sex = "";
            model.Tell = "";
            model.Phone = "";
            model.QQ = "";
            model.Usermoney = 0;
            //model.PayPassword = "";
            model.State = "0";
            model.openid = uid;
            model.wtype = utype;

            if (dal.Add_Third(model) > 0)
            {
                state = 1;
                ECustomerInfo usermodel = dal.GetModelByThird(uid, utype);
                UserHelp.SetLogin(usermodel);
                //添加地址
                EPointRecordInfo pointmodel = new EPointRecordInfo();
                pointmodel.UserID = usermodel.DataID;
                pointmodel.Point = point;
                pointmodel.Event = "新注册用户,获得积分";
                pointmodel.Time = DateTime.Now;
                new EPointRecord().Add(pointmodel);

                if (uway == "1")
                {
                    Response.Write("{\"userid\":\"" + usermodel.DataID + "\",\"state\":\"" + state + "\"}");
                }
                else
                {
                    Response.Redirect("~/index.aspx");
                }

            }
            else
            {
                if (uway == "1")
                {
                    Response.Write("{\"userid\":\"" + model.DataID + "\",\"state\":\"" + state + "\"}");
                }
                else
                {
                    Response.Write("errorcode:1; 服务器繁忙，请稍后再试。");
                }

            }

        }
        else
        {
            ECustomerInfo amodel = dal.GetModelByThird(uid, utype); ;
            UserHelp.SetLogin(amodel);
            if (uway == "1")
            {
                state = 1;
                Response.Write("{\"userid\":\"" + amodel.DataID + "\",\"state\":\"" + state + "\"}");
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }
    }
}
