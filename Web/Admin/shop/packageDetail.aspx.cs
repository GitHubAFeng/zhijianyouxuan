using System;
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
using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;
using System.Web.Script.Serialization;
using System.Text;

/// <summary>
/// 套餐信息
/// </summary>
public partial class qy_54tss_Admin_Shop_packageDetail : System.Web.UI.Page
{
    FoodPackag dal = new FoodPackag();
    PackFoodlist dalpfl = new PackFoodlist();
    Points dal_togo = new Points();

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();

        if (!IsPostBack)
        {
            //编辑
            int TogoId = HjNetHelper.GetQueryInt("tid", 0);
            int SortId = HjNetHelper.GetQueryInt("id", 0);
            hidTogoId.Value = TogoId.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("当前位置:<a href='ShopList.aspx'>商家管理</a>");
            PointsInfo togomodel = dal_togo.GetModel(TogoId);
            sb.Append(" >  <a href='ShopDetail.aspx?id=" + TogoId + "'>" + togomodel.Name + "</a>");
            sb.Append(" >  <a href='packagelist.aspx?tid=" + TogoId + "'>套餐管理</a>");

            if (SortId > 0)
            {
                pageType.Text = "修改套餐";
                sb.Append(" > 修改套餐");
                BindFoodSortData(SortId);
            }
            else
            {
                pageType.Text = "新增套餐";
                sb.Append(" > 新增套餐");
            }
            dangqian.InnerHtml = sb.ToString();

        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        Hangjing.Model.FoodPackagInfo info = new FoodPackagInfo();
        info.PID = HjNetHelper.GetQueryInt("id", 0);
        if (info.PID > 0)
        {
            info = dal.GetModel(info.PID);
        }
        else
        {
            info.cnum = 0; ;
            info.state = 0;
            info.sortnum = 0;
        }

        info.Code = "";
        info.ShopId = HjNetHelper.GetQueryInt("tid", 0);
        info.foodids = "";
        info.Num = WebUtility.InputText(tbNum.Text, true);
        info.Price = WebUtility.InputText(tbPrice.Text, 'c');
        info.Unit = "";
        info.Tag = 0;
        info.SectionId = 0;
        info.Inve1 = 0;
        info.Inve2 = "";
        info.title = WebUtility.InputText(tbtitle.Text);
        info.startdate = DateTime.Now;
        info.enddate = DateTime.Now;
        info.starttime = Convert.ToDateTime(tbstarttime.Text);
        info.endtime = Convert.ToDateTime(tbendtime.Text);
        info.oldprice = WebUtility.InputText(tboldprice.Text, 'c');
        info.ReveFloat1 = 0;
        info.ReveFloat2 = 0;
        info.ReveVar1 = "";
        info.ReveVar2 = "";

        if (info.PID == 0)
        {
            int pid = dal.Add(info);
            if (pid > 0)
            {
                //添加菜品PackFoodlist
                IList<PackFoodlistInfo> styles = new JavaScriptSerializer().Deserialize<IList<PackFoodlistInfo>>(this.hidStyle.Value);
                foreach (PackFoodlistInfo item in styles)
                {
                    item.shopid = info.ShopId;
                    item.pid = pid;
                    item.sortnum = 0;
                    item.sid = 0;
                    item.ReveVar1 = "";
                }

                DataTable dt = CollectionHelper.ConvertTo(styles, "PackFoodlist");
                dalpfl.AddWidhDataTable(dt);

                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:操作成功','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:新增失败','250','150','true','','true','text');");
            }
        }
        else
        {
            if (dal.Update(info) > 0)
            {
                //删除所有再批量添加
                string sql = "DELETE FROM dbo.PackFoodlist WHERE pid = " + info.PID;
                WebUtility.excutesql(sql);
                //添加菜品PackFoodlist
                IList<PackFoodlistInfo> styles = new JavaScriptSerializer().Deserialize<IList<PackFoodlistInfo>>(this.hidStyle.Value);
                foreach (PackFoodlistInfo item in styles)
                {
                    item.shopid = info.ShopId;
                    item.pid = info.PID;
                    item.sortnum = 0;
                    item.sid = 0;
                    item.ReveVar1 = "";
                }

                DataTable dt = CollectionHelper.ConvertTo(styles, "PackFoodlist");
                dalpfl.AddWidhDataTable(dt);

                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:操作成功','250','150','true','','true','text');");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('提示','text:更新失败','250','150','true','','true','text');");
            }
        }
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    /// <param name="id"></param>
    protected void BindFoodSortData(int id)
    {
        FoodPackagInfo info = dal.GetModel(id);
        if (info != null)
        {
            tbNum.Text = info.Num.ToString();
            tbPrice.Text = info.Price.ToString();
            tbtitle.Text = info.title;
            tbstarttime.Text = info.starttime.ToShortTimeString();
            tbendtime.Text = info.endtime.ToShortTimeString();
            tboldprice.Text = info.oldprice.ToString();

            IList<PackFoodlistInfo> foodlist = dalpfl.GetList(100, 1, "pid=" + id, "mid", 0);
            WebUtility.BindRepeater(rptfood, foodlist);
        }
    }
}
