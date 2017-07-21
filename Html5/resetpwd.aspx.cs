using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

namespace Html5
{
    /// <summary>
    /// 从邮箱中链接，修改密码
    /// </summary>
    public partial class UpdatePassword : System.Web.UI.Page
    {
        ECustomer dal = new ECustomer();
        protected void Page_Load(object sender, EventArgs e)
        {
            //修改密码
            if (Request.HttpMethod.ToLower() == "post")
            {
                string pwd = WebUtility.InputText(Request["tbpassword"]);
                string sql = " update ECustomer set Password = '" + WebUtility.GetMd5(pwd) + "' where tell = '" + HjNetHelper.GetQueryString("tell") + "'";

                if (WebUtility.excutesql(sql) > 0)
                {
                    this.divError.InnerHtml = "修改密码成功！";
                    return;
                }
                else
                {
                    this.divError.InnerHtml = "服务器繁忙，请稍后再试！";
                    return;
                }
            }
        }





    }
}