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

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using System.IO;
using Hangjing.Model;
using System.Collections.Generic;

/// <summary>
/// 结算帐号信息
/// </summary>
public partial class shop_settlecount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!this.Page.IsPostBack)
        {
            UserHelp.IsLogin_Togo(Request.Url.PathAndQuery);
            GetData();
        }
    }

    shopsettleaccount dal = new shopsettleaccount();

    protected void GetData()    
    {
        int TogoId = UserHelp.GetUser_Togo().Unid;
        IList<shopsettleaccountInfo> list = dal.GetList(1, 1, " shopid =" + TogoId, "id", 1);
        if (list.Count > 0)
        {
            shopsettleaccountInfo info = list[0];
            tbbankname.Text = info.bankname;
            tbbankusername.Text = info.bankusername;
            tbaliaccount.Text = info.aliaccount;
            tbaliname.Text = info.aliname;
            tbrevevar1.Text = info.revevar1;
        }
    }
}
