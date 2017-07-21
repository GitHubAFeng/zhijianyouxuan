using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

using org.in2bits.MyXls;
using Hangjing.AppLog;

/// <summary>
/// 商家促销
/// </summary>
public partial class AreaAdmin_shop_shopPromotion : AdminPageBase
{
    Points daltogo = new Points();
    webPromotionConfig dal = new webPromotionConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        CheckRights("A");
        if (!IsPostBack)
        {
            BindData();

            int tid = HjNetHelper.GetQueryInt("tid", 0);
            PointsInfo shop = daltogo.GetModel(tid);

            WebUtility.BindList("pId", "revevar1", CacheHelper.GetWebPromotionConfig(), tbPEnd);
            WebUtility.SelectValue(rblshopptype, shop.PType.ToString());
            switch (shop.PType)
            {
                case 10:
                    webpromotionbox.Style["display"] = "none";
                    break;
                case 20:
                     promotionbox.Style["display"] = "none";
                     WebUtility.CheckValueS(tbPEnd, shop.PEnd);


                    break;
                default:
                    webpromotionbox.Style["display"] = "none";
                    promotionbox.Style["display"] = "none";

                    break;
            }
        }
    }


    protected void BindData()
    {
        int tid = HjNetHelper.GetQueryInt("tid", 0);
        this.rtpTogolist.DataSource = dal.GetList(10, 1, "shopid="+ tid, "pid", 1, 0);
        this.rtpTogolist.DataBind();
    }

    /// <summary>
    /// 类型促销分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Savetype_Click(object sender, EventArgs e)
    {
        int tid = HjNetHelper.GetQueryInt("tid", 0);

        string url = "shopPromotion.aspx?tid=" + tid;

        string PEnd = WebUtility.GetcheckStrFix(tbPEnd);
        string PType = rblshopptype.SelectedValue;
        if (PType != "20")
        {
            PEnd = "";
        }

        string sql = "UPDATE dbo.Points SET PType = '"+ PType + "' ,PEnd = '" + PEnd + "'  WHERE Unid = " + tid;
        if (WebUtility.excutesql(sql) > 0)
        {
            AlertScript.RegScript(this.Page,"alert('保存成功','text:批量显示成功!','250','150','true','1000','true','text');gourl('"+ url + "');");
        }
        else
        {
            AlertScript.RegScript(this.Page, "alert('保存失败','text:批量显示失败!','250','150','true','1000','true','text');init();");
        }
    }

    /// <summary>
    /// 事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptUserList_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            //判断权限
            int _rs = WebUtility.AreaAdmin_checkOperator(4);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page,  "alert('无操作权限','success','true',5);");
                return;
            }
            if (dal.DelList(e.CommandArgument.ToString()) > 0)
            {
                AlertScript.RegScript(this.Page,  "alert('删除成功','text:删除成功!','250','150','true','1000','true','text');");
                BindData();
            }
            else
            {
                AlertScript.RegScript(this.Page, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');");
            }
        }
    }

}

