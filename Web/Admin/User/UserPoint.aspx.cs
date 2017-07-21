/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 用户米粒操作
 * Created by jijunjian at 2009-7-24 15:49:47.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
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

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

public partial class qy_54tss_Admin_User_UserPoint : System.Web.UI.Page
{
    ECustomer dal_customer = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {
            //传来用户id
            if (Request["id"] != null)
            {
                GetUserData();
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        int uid = HjNetHelper.GetQueryInt("id", 0);
        ECustomerInfo _model = dal_customer.GetModel(Convert.ToInt32(uid));

        int addpoint = 0;


        switch (this.ddtOption.SelectedValue)
        {
            case "+":
                {
                    addpoint = Convert.ToInt32(tbHandlingPoint.Text.Trim());
                }
                break;
            case "-"://可能为负还要处理
                {
                    addpoint = -Convert.ToInt32(tbHandlingPoint.Text.Trim());
                }
                break;
            default:
                break;
        }

        //判断权限
        int _rs = WebUtility.checkOperator(3);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
            return;
        }
        if (dal_customer.addpoint(uid, WebUtility.InputText(tbevnt.Text), addpoint) > 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('更新成功！');");
            this.lbCurrentPoint.InnerText = (_model.Point + addpoint) + "";
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('更新失败！');");
        }
    }

    protected void GetUserData()
    {
        int id = HjNetHelper.GetQueryInt("id", 0);
        ECustomerInfo model = dal_customer.GetModel(id);
        if (model != null)
        {
            tbUsername.Text = model.Name;
            this.lbCurrentPoint.InnerHtml = model.Point.ToString();
        }
    }
}
