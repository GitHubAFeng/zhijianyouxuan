using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// 获取验证码,通过手机号验证码
/// </summary>
public partial class AndroidAPI_sendcode : APIPageBase
{
    //参数sendcode.aspx?tel=1875850307
    //
    ECustomer dal = new ECustomer();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        //输入参数：手机号
        string tel = WebUtility.InputText(Request["tel"]);
        string code = WebUtility.GetRandomOnlyNum(6);
        int type = HjNetHelper.GetQueryInt("type", 0);//0表示注册（手机号不能重复），1表示验证手机号是否存在 

        string rs = "";

        if (type == 0)
        {
            if (dal.IsExistPhone(tel))
            {
                Hangjing.WebCommon.SendMsg.SendValidCode(tel, code);
                Response.Write("{\"pwd\":\"" + code + "\",\"state\":\"" + 1 + "\",\"auto\":\"" + CacheHelper.GetSetValue(39) + "\"}");
            }
            else
            {
                //手机号已经存在
                Response.Write("{\"pwd\":\"" + "" + "\",\"state\":\"" + -1 + "\",\"auto\":\"" + 0 + "\"}");
            }
        }
        else
        {
            if (!dal.IsExistPhone(tel))
            {
                Hangjing.WebCommon.SendMsg.SendValidCode(tel, code);
                Response.Write("{\"pwd\":\"" + code + "\",\"state\":\"" + 1 + "\",\"auto\":\"" + CacheHelper.GetSetValue(39) + "\"}");
            }
            else
            {
                //手机号不存在
                Response.Write("{\"pwd\":\"" + "" + "\",\"state\":\"" + -2 + "\",\"auto\":\"" + 0 + "\"}");
            }
        }


        Response.Write(rs);

        Response.End();
    }
}
