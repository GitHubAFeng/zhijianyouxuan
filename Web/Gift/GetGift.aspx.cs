#region license
/**********************************************
* CopyRight (c) 2009-2010 HangJing Teconology. All Rights Re* Function :
* Created by jijunjian at 2011-4-25 14:11:47.
* E-Mail: jijunjian@ihangjing.com
*********************************************** */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

public partial class Gift_getGift1 : System.Web.UI.Page
{
    ECustomer dal = new ECustomer();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserHelp.IsLogin(Request.Url.PathAndQuery);
        if (!Page.IsPostBack)
        {
            int Id = HjNetHelper.GetQueryInt("id", 0);
            Gifts bll = new Gifts();
            InitUser();
            GiftsInfo info = new GiftsInfo();
            info = bll.GetModel(Id);

            lbGiftName.Text = info.Gname;
            lbprice.Text = ""+info.GiftsPrice.ToString("#0.0");
            lbPoint.Text = info.NeedIntegral.ToString();
            lbstocks.Text = info.Stocks.ToString();
            ECustomerInfo user1 = UserHelp.GetUser();
            if (user1 != null)
            {
                string sql = "UserID =" + user1.DataID;
                IList<EAddressInfo> list = new EAddress().GetList(1, 1, sql, "pri", 1);
                if (list.Count > 0)
                {
                    
                    string phone = list[0].Phone == "" ? "" : "/" + list[0].Phone;
                    tbPerson.Text = list[0].Receiver + phone;
                    tbPhone.Text = list[0].Mobilephone  ;
                    tbAddress.Text = list[0].Address;
                }
            }
            if (Request["buy"] != null && Request["buy"].ToString() == "1")
            {
                lipoint.Style["display"] = "none";
            }
            else
            {
                liprice.Style["display"] = "none";
            }
        }
    }

    protected void btGet_Click(object sender, EventArgs e)
    {
        //判断积分是否足够进行兑换 积分需要从数据库中获取不能从cookie或者session中获取以免出现错误
        ECustomer cbll = new ECustomer();
        ECustomerInfo user = new ECustomerInfo();
        user = cbll.GetModel(UserHelp.GetUser().DataID);
        if (lbstocks.Text == "0")
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:库存不足，不能兑换.','250','150','true','2000','true','text');inithead('" + user.Point + "', '" + user.Name + "');");
            btGet.Enabled = true;
            return;
        }

        //进行兑换
        if (tbAddress.Text == "" || tbPerson.Text == "" || tbPhone.Text == "")
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:信息输入不完整!','250','150','true','2000','true','text');inithead(" + user.Point + ", '" + user.Name + "');");
            return;
        }
        if (user.Point < Convert.ToInt32(lbPoint.Text))
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:对不起，您的积分还不足以兑换此礼品!','250','150','true','2000','true','text');inithead(" + user.Point + ", '" + user.Name + "');");
            return;
        }


        //增加兑换记录
        IntegralInfo iinfo = new IntegralInfo();
        Integral bll = new Integral();

        iinfo.Address = WebUtility.InputText(tbAddress.Text);
        iinfo.Cdate = DateTime.Now;
        iinfo.CustId = UserHelp.GetUser().DataID.ToString();
        iinfo.Date = ddlDate.SelectedValue;
        iinfo.DetailId = HjNetHelper.GetQueryInt("buy", 0);
        iinfo.GiftsId = HjNetHelper.GetQueryInt("id", 0);
        if (Request["buy"].ToString() == "0")
        {
            iinfo.PayIntegral = lbPoint.Text;
        }
        else
        {
            iinfo.PayIntegral = lbprice.Text;
        }
        iinfo.Person = WebUtility.InputText(tbPerson.Text);
        iinfo.Phone = WebUtility.InputText(tbPhone.Text);
        iinfo.State = "0";
        iinfo.GiftName = WebUtility.InputText(lbGiftName.Text);
        iinfo.UserName = user.Name;
        iinfo.Remark = WebUtility.InputText(tbremark.Text);

        if (bll.Add(iinfo) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('礼品兑换申请成功，我们会尽快处理您的礼品兑换申请!');window.location.href='../user/mychange.aspx'");
        }
        else
        {

            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:对不起，礼品兑换失败，请与我们客服联系!','250','150','true','2000','true','text');inithead(" + user.Point + ", '" + user.Name + "');");
        }
    }

    /// <summary>
    /// 需要登入的才能执行的操作则进行判断是否已经登入
    /// </summary>
    /// <returns></returns>
    private void InitUser()
    {
        ECustomerInfo model = UserHelp.GetUser();
        if (model != null)
        {
            AlertScript.RegScript(Page, UpdatePanel1, "inithead(" + model.Point + ", '" + model.Name + "', '" + WebUtility.ShowPic(model.Picture) + "');");
        }
        else
        {

        }
    }
}
