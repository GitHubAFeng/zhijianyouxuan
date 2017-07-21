/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 用户详细信息
 * Created by jijunjian at 2010-7-31 19:07:31.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.Model;
using Hangjing.Common;
using Hangjing.SQLServerDAL;

public partial class Admin_User_UserDetail : AdminPageBase
{
    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["id"] != null)
            {
                GetData();
            }
        }
    }

    protected void GetData()
    {
        int id = HjNetHelper.GetQueryInt("id", 0);
        ECustomerInfo model = null;
        model = dal.GetModel(id);
        if (model != null)
        {
            lbnikename.InnerText = model.Name;
            lbTruename.InnerText = model.TrueName;
            lbPhone.InnerText = model.Tell;
            lbQQ.InnerText = model.QQ;
            lbEmail.InnerText = model.EMAIL;
            tbpoint.InnerText = model.Point + "";
            this.lbRegtime.InnerHtml = model.RegTime.ToString();
            lbbirthday.InnerText = model.MSN;
            lbsex.InnerText = model.Sex == "0" ? "男" : "女";
        }

        EAddress daladdress = new EAddress();
        string sql = "userid = " + id;
        IList<EAddressInfo> list = daladdress.GetList(10, 1, sql, "pri", 1);
        this.rtpAddressList.DataSource = list;
        this.rtpAddressList.DataBind();
    }

    /// <summary>
    /// 修改备注，份数,删除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void orderfood_Command(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "update")//修改数量s
        {
            //判断权限
            int _rs = WebUtility.checkOperator(7);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            EAddressInfo model = new EAddressInfo();
            model.DataID = Convert.ToInt32(e.CommandArgument);
            TextBox tbaddress = e.Item.FindControl("tbaddress") as TextBox;
            TextBox tbmobilephone = e.Item.FindControl("tbmobilephone") as TextBox;
            model.Address = WebUtility.InputText(tbaddress.Text);
            model.Mobilephone =WebUtility.InputText( tbmobilephone.Text.Trim());
            EAddress daladdress = new EAddress();
            if (daladdress.Update_tem(model) > 0)
            {
                AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息','text:修改成功!','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(Page, UpdatePanel1, "tipsWindown('提示信息','text:修改失败!','250','150','true','','true','text');");
            }
        }
      
        GetData();
    }

}
