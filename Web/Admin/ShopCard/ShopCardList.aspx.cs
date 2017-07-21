/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * FileName : Admin_card_cardlist
 * Function : 
 * Created by jijunjian at 2010-12-26 15:04:37.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Hangjing.SQLServerDAL;
using Hangjing.Common;
using Hangjing.Model;

/// <summary>
/// 店铺券
/// </summary>
public partial class Admin_card_cardlistShopCardList : System.Web.UI.Page
{
    ShopCard dal = new ShopCard();
    protected string SqlWhere
    {
        set
        {
            ViewState["sqlwhere"] = value;
        }
        get
        {
            return ViewState["sqlwhere"] == null ? "" : ViewState["sqlwhere"].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlWhere = " 1=1";
            int id = HjNetHelper.GetQueryInt("id", 0);
            if (id > 0)
            {
                SqlWhere += " and batid = '" + id + "'";
            }
            int state =  HjNetHelper.GetQueryInt("state", 0);
            if (state != 0)
            {
                SqlWhere += " and  state = " + state;
            }
            //管理员
            IList<EAdminInfo> adminlist = new EAdmin().GetList(1000, 1, "1=1");
            WebUtility.BindList("id", "adminname", adminlist, ddladmin);
            GetData();
        }
    }

    protected void GetData()
    {
        this.AspNetPager1.RecordCount = dal.GetCount(SqlWhere);

        IList<ShopCardInfo> list = dal.GetList(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, SqlWhere, "cid", 1);
        foreach (var item in list)
        {
            item.cardnum = "满" + item.moneyline + "元";
            switch (item.ReveInt1)
            {
                case 1:
                    item.cardnum += "减" + item.Point + "元";
                    break;
                case 2:
                    item.cardnum += "享" + item.Point + "折优惠";
                    break;
                case 3:
                    item.cardnum += "享" + item.Point + "倍积分";
                    break;
                default:
                    break;
            }

        }

        this.rtpUserlist.DataSource = list;
        this.rtpUserlist.DataBind();
        AlertScript.RegScript(this.Page, this.UpdatePanel1, "init();");
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetData();
    }

    protected void DelList_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        if (dal.DelList(IdList) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除成功!','250','150','true','1000','true','text');init();");
            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:删除失败!','250','150','true','1000','true','text');init();");
            GetData();
        }
    }

    protected void btSearch_Click(object sender, EventArgs e)
    {
        SqlWhere = " 1= 1 ";
        if (this.tbckey.Text.Trim() != "")
        {
            SqlWhere += " AND ckey LIKE '%" + WebUtility.InputText(this.tbckey.Text.Trim()) + "%' ";
        }
        if (this.tb_Start.Text != "")
        {
            SqlWhere += " AND adddate >= '" + this.tb_Start.Text + "' ";
        }
        if (this.tb_End.Text != "")
        {
            SqlWhere += " AND adddate <= '" + this.tb_End.Text + " 23:59:59' ";
        }
        //绑定
        if (ddl_Operator.SelectedValue != "-1")
        {
            SqlWhere += " AND State = " + this.ddl_Operator.SelectedValue + " ";
        }
        //激活
        if (ddlinve2.SelectedValue != "-1")
        {
            SqlWhere += " AND Inve2 = " + this.ddlinve2.SelectedValue + " ";
        }
        //使用
        if (this.ddlisused.SelectedValue != "-1")
        {
            SqlWhere += " AND isused = " + this.ddlisused.SelectedValue + " ";
        }
        //管理员
        if (this.ddladmin.SelectedValue != "-1")
        {
            SqlWhere += " AND Inve1 = " + this.ddladmin.SelectedValue + " ";
        }
        GetData();
    }

    //激活部分
    protected void part_Click(object sender, EventArgs e)
    {

        string IdList = this.hdDels.Value;
        string sql = "update ShopCard set Inve2 = 1 where CID in (" + IdList + ")";

        if (WebUtility.excutesql(sql) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','1000','true','text');init();");
            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作失败!','250','150','true','1000','true','text');init();");
            GetData();
        }
        WebUtility.FixdelCookie("gsmcode_admin");
    }

    //激活全部
    protected void all_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        string sql = "update ShopCard set Inve2 = 1 where " + SqlWhere;

        if (WebUtility.excutesql(sql) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','1000','true','text');init();");
            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作失败!','250','150','true','1000','true','text');init();");
            GetData();
        }
        WebUtility.FixdelCookie("gsmcode_admin");
    }

    //绑定选中用户
    protected void bd_Click(object sender, EventArgs e)
    {
        string IdList = this.hdDels.Value;
        //判断这些卡中是否有没有激活或者已经绑定的。
        string sql = " Inve2 = 0 and CID in (" + IdList + ")";
        if (dal.GetCount(sql) > 0)
        {
            AlertScript.RegScript(this, this.UpdatePanel1, "alert('选择的优惠券中有部分未激活，请检查，确认选中的都是已经激活的!');init('tbinfood');");
            return;
        }
        sql = " state = 1 and CID in (" + IdList + ")";
        if (dal.GetCount(sql) > 0)
        {
            AlertScript.RegScript(this, this.UpdatePanel1, "alert('选择的优惠券中有部分已绑定，请检查，确认选中的都是未绑定的!');init('tbinfood');");
            return;
        }

        string _tbuid = tbuid.Text;
        sql = " dataid like  '" + _tbuid + "' ";
        IList<ECustomerInfo> userlist = new ECustomer().GetList(1, 1, sql, "dataid", 1);
        if (userlist.Count == 0)
        {
            AlertScript.RegScript(this, this.UpdatePanel1, "alert('用户编号不存在,请重新输入!');init('tbinfood');");
            return;
        }


        sql = "update ShopCard set usergettime = '" + DateTime.Now.ToString() + "' , state =1 , userid = " + _tbuid + ",username = '" + userlist[0].Name + "' where CID in (" + IdList + ")";

        if (WebUtility.excutesql(sql) > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作成功!','250','150','true','1000','true','text');init();");
            GetData();
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:操作失败!','250','150','true','1000','true','text');init();");
            GetData();
        }
        //WebUtility.FixdelCookie("gsmcode_admin");
    }
}
