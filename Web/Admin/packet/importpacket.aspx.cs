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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

public partial class Admin_packet_importpacket : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidatorSet validator = new ValidatorSet("Admin");
            validator.SetValidator();
            int id = HjNetHelper.GetQueryInt("id",0);
            if (id == 0)
            {
                pageType.Text = "生成红包批次";
            }
            else
            {
                pageType.Text = "编辑红包批次";
                //控件绑定内容
                userpacketInfo info = bll.GetModel(id);
                if (info != null)
                {
                    tbtel.Text = info.pulltel;
                    tbNum.Text = Convert.ToString(info.reveint);
                    WebUtility.SelectValue(tbstyle, info.reveint1.ToString());
                    tbsum.Text = Convert.ToString(info.money);
                    tbKey.Text = info.revevar1;
                    tbValidity.Text = Convert.ToString(info.state);
                }
            }
        }
    }

    userpacket bll = new userpacket();
    userpacketInfo info = new userpacketInfo();

    protected void btSave_Click(object sender, EventArgs e)
    {

        info.id = HjNetHelper.GetQueryInt("id", 0);
        if (info.id > 0)
        {
            info =bll.GetModel(info.id);
        }

        info.pid = "2";
        info.userid = 0;
        info.exptime = DateTime.Now;
        info.pulltime = DateTime.Now;
        info.pulltel = WebUtility.InputText(tbtel.Text);
        info.reveint = Convert.ToInt32(WebUtility.InputText(tbNum.Text));
        info.reveint1 = Convert.ToInt32(tbstyle.SelectedValue);
        info.money = Convert.ToDecimal(WebUtility.InputText(tbsum.Text));
        info.revevar1 = WebUtility.InputText(tbKey.Text);
        info.state = Convert.ToInt32(WebUtility.InputText(tbValidity.Text));
        info.moneyline = 0;
        info.datetime1 = DateTime.Now;
        info.datetime2 = DateTime.Now;

        if (info.id == 0)
        {
            info.revevar = Guid.NewGuid().ToString();
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (bll.Add(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增成功','id:divShowContent','640','150','true','','true','text')");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增失败','text:新增失败','250','150','true','','true','text')");
            }
        }
        else
        {
            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (bll.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增成功','id:divShowContent','640','150','true','','true','text')");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增失败','text:新增失败','250','150','true','','true','text')");
            }
        }

        
    }
}
