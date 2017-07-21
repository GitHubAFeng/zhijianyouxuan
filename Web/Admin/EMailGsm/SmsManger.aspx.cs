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

public partial class Admin_EMailGsm_SmsManger :AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /*
    * 注册企业信息Register（软件序列号,密码（6位）,企业名称(最多60字节)，联系人姓名(最多20字节),
    * 联系电话(最多20字节)，联系手机(最多15字节)，电子邮件(最多60字节)，联系传真(最多20字节)，
    * 公司地址(最多60字节)，邮政编码(最多6字节)）
    * 
    * 参数说明：
    * 注册需要的序列号,请通过亿美销售人员获取
    * 注册需要的密码,请通过亿美销售人员获取
    * 
    * 在注册序列号时，需注意不需要每次发送短信时都注册一遍，  一个序列号在一台机器上只需要注册一次就够了
    * 即便是关闭应用程序或重启电脑也不需要重新注册，除非重装电脑或注销序列号之后才需要重新注册。
    */
    protected void btSent_Click(object sender, EventArgs e)
    {
        //int result = EUCPComm.Register(this.txtCDKey.Text.Trim(), this.txtPassword.Text.Trim(), "1", "1", "1", "1", "1", "1", "1", "1");
        //string _Response = "";

        //if (result == 1)
        //    _Response = "注册成功";
        //else if (result == 101)
        //    _Response = "网络故障";
        //else if (result == 102)
        //    _Response = "其它故障";
        //else if (result == 0)
        //    _Response = "失败";
        //else if (result == 100)
        //    _Response = "序列号码为空或无效";
        //else if (result == 103)
        //    _Response = "注册企业基本信息失败，当软件注册号码注册成功,但整体还是失败，要重新注册";
        //else if (result == 104)
        //    _Response = "注册信息填写不完整";
        //else if (result == 114)
        //    _Response = "得到标识错误";
        //else
        //    _Response = "其他故障值：" + result.ToString();

        //AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:" + _Response + "!','250','150','true','1000','true','text');");
    }
}
