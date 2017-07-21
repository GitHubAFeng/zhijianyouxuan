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

using Hangjing.Common;
using Hangjing.SQLServerDAL;
using Hangjing.Model;

/// <summary>
/// 商家信息
/// </summary>
public partial class Admin_Shop_ShopDetail : AdminPageBase
{
    protected string hasDiscount = "0";

    protected int togo_id
    {
        set
        {
            ViewState["togo_id"] = value;
        }
        get
        {
            return ViewState["togo_id"] == null ? 0 : Convert.ToInt32(ViewState["togo_id"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!IsPostBack)
        {
            WebUtility.SetDDLCity(DDLArea);
            WebUtility.BindList("id", "classname", SectionProxyData.GetSortList(), cblqq);

            WebUtility.BindList("IID", "togoname",CacheHelper.GetShopPicTag(), cbopentime);

            //编辑
            int TogoId = HjNetHelper.GetQueryInt("id", 0);

            if (TogoId > 0)
            {
                BindTogoData(TogoId);
                hidDataId.Value = TogoId.ToString();
                hidTogoId.Value = TogoId.ToString();
                pageType.InnerHtml = "<span>更新商家信息</span>";
            }
            else
            {
                pageType.InnerHtml = "<span>新增商家</span>";
                IList<SectionInfo> templists = new ESection().GetList(100, 1, " 1=1 AND cityid= " + this.DDLArea.SelectedValue, "pri", 1);
                hidDataId.Value = "0";
                cbIsdelete.Checked = false;
                ddlStatus.SelectedValue = "1";
                rblstart.Items[0].Selected = true;
                hfintime.Value = DateTime.Now.ToShortDateString();
                diagram_tab.Style["display"] = "none";
                btcopy.Style["display"] = "none";
                rblRcvType.Items[0].Selected = true;
            }
        }
    }

    Points togoBll = new Points();


    /// <summary>
    /// 获取商家的信息
    /// </summary>
    /// <param name="TogoId"></param>
    protected void BindTogoData(int TogoId)
    {
        PointsInfo info = new PointsInfo();

        info = togoBll.GetModel(TogoId);

        tbName204234.Text = info.Name;
        tbCommPerson.Text = info.CommPerson;
        tbAddress.Text = info.Address;
        tbComm.Text = info.Comm;
        tbRefresh.Text = info.RefreshTime;

        TextBoxtime.Text = info.Opentimes1.ToString("HH:mm");
        TtimeEnd.Text = info.Opentimes2.ToString("HH:mm");

        TextBoxtime2.Text = info.Closetimes1.ToString("HH:mm");
        TtimeEnd2.Text = info.Closetimes2.ToString("HH:mm");

        tbSortNum.Text = info.SortNum.ToString();
        tbIntroduce.Value = info.Introduce;
        hidInTime.Value = info.InTime.ToString();
        ImgUrl1.Value = info.Picture;
        if (!string.IsNullOrEmpty(info.Picture))
        {
            ImgUrl.Src = WebUtility.ShowPic(info.Picture);
        }
        ////登入帐号
        tbLoginName.Text = info.LoginName;
        hidPassword.Value = info.Password;
        hidIsDelete.Value = info.IsDelete.ToString();
        ddlStatus.SelectedValue = info.Status.ToString();//1 正常营业 0 暂停营业 －1 休息中
        cbIsdelete.Checked = info.IsDelete == 1 ? true : false;
        togo_id = TogoId;
        tbPoint.Text = info.point.ToString();
        tbPoint2.Text = info.point.ToString();

        tbSentTime.Text = info.senttime.ToString();

        WebUtility.SelectValue(ddlSentOrg, info.sentorg.Trim());

        tbViewTimes.Text = info.ViewTimes.ToString();
        tbMoney.Text = info.money.ToString();
        tbspecial.Text = info.special;
        WebUtility.SelectValue(rblstart, info.Star + "");
        tbPTimes.Text = info.PTimes.ToString();

        tbNamePy.Text = info.NamePy;
        tbInve1.Text = info.Inve1.ToString();
        WebUtility.SelectValue(ddlid, info.ID);
        tbemail.Text = info.email;

        WebUtility.SelectValue(DDLArea, info.cityid.ToString());
        WebUtility.SelectValue(ddlshowpicture, info.showpicture.ToString());
        WebUtility.CheckValueS(cblqq, info.category);
        WebUtility.CheckValueS(cbopentime, info.OpenTime);
        if (TogoId == 1)
        {
            picnotice.InnerText = "请上传591*308的图片";
        }
        WebUtility.SelectValue(ddlSN1, info.SN1.Trim());
        if (info.SN2.Length == 0)
        {
            info.SN2 = "0";
        }
        WebUtility.SelectValue(rblRcvType, info.RcvType.ToString());

        WebUtility.SelectValue(ddlIsCallCenter, info.IsCallCenter.ToString());
        tbSN2.Text = info.SN2;

    }

    /// <summary>
    /// 保存商家的信息 TODO:保存的标志建筑物那块功能是否可以变得更方便强大（删除建筑物）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click1(object sender, EventArgs e)
    {
        int id = HjNetHelper.GetQueryInt("id", 0);
        PointsInfo info = togoBll.GetModel(id);
        if (info == null)
        {
            info = new PointsInfo();
            info.Grade = 0;
            info.PType = 0;
            info.PEnd = "";
        }
        info.Unid = HjNetHelper.GetQueryInt("id", 0);
        info.ID = ddlid.SelectedValue;
        info.Name = WebUtility.InputText(tbName204234.Text);
        info.Comm = WebUtility.InputText(tbComm.Text);

        info.RcvType = Convert.ToInt32(rblRcvType.SelectedValue);
        info.NamePy = WebUtility.InputText(tbNamePy.Text);
        info.CommPerson = WebUtility.InputText(tbCommPerson.Text);
        info.SendLimit =0;
        info.LoginName = WebUtility.InputText(tbLoginName.Text);
        info.SendFee = 0;
        info.RefreshTime = tbRefresh.Text;
        info.SN1 = ddlSN1.SelectedValue.Trim();
        info.point = 0; 
        if (info.SN1 == "0")
        {
            info.point = Convert.ToInt32(WebUtility.InputText(tbPoint.Text));
        }
        else
        {
            info.point = Convert.ToInt32(WebUtility.InputText(tbPoint2.Text));
        }


        info.SN2 = WebUtility.InputText(tbSN2.Text);
        info.Sn2Key = "";
        info.PTimes = Convert.ToInt32(WebUtility.InputText(tbPTimes.Text));
        info.MgrCell = "";
        info.PHead = "";

        info.IsCallCenter = Convert.ToInt32(ddlIsCallCenter.SelectedValue);
        info.Address = WebUtility.InputText(tbAddress.Text);
        info.Introduce = tbIntroduce.Value;
        info.Status = Convert.ToInt32(ddlStatus.SelectedValue);
        info.outnitice = "";
        info.OpenTime = WebUtility.GetcheckStrFix(cbopentime);
        info.Time1Start = DateTime.Now;
        info.Time1End = DateTime.Now;
        info.Time2Start = DateTime.Now;
        info.Time2End = DateTime.Now;
        info.bisnessStart = DateTime.Now;
        info.bisnessend = DateTime.Now;
        info.bisnessStart2 = DateTime.Now;
        info.bisnessend2 = DateTime.Now;
        info.IsDelete = cbIsdelete.Checked ? 1 : 0;
        info.SortNum = Convert.ToInt32(WebUtility.InputText(tbSortNum.Text));
        info.EBuilding = "";
        info.Star = Convert.ToInt32(rblstart.SelectedValue);
        info.category = WebUtility.GetcheckStrFix(cblqq);
        info.senttime = Convert.ToInt32(tbSentTime.Text);
        info.sentorg = WebUtility.InputText(ddlSentOrg.SelectedValue);
        info.special = WebUtility.InputText(tbspecial.Text);
        info.Inve1 = Convert.ToInt32(tbInve1.Text);
        info.cityid = Convert.ToInt32(this.DDLArea.SelectedValue);
        info.Picture = ImgUrl1.Value;
        info.showpicture = Convert.ToInt32(ddlshowpicture.SelectedValue);
        info.foodupdatetime = DateTime.Now;
     
        info.ViewTimes = Convert.ToInt32(tbViewTimes.Text);
        //营业时间
        info.Opentimes1 = TextBoxtime.Text == "" ? DateTime.Now : Convert.ToDateTime(TextBoxtime.Text);
        info.Opentimes2 = TtimeEnd.Text == "" ? DateTime.Now : Convert.ToDateTime(TtimeEnd.Text);
        info.Closetimes1 = TextBoxtime2.Text == "" ? DateTime.Now : Convert.ToDateTime(TextBoxtime2.Text);
        info.Closetimes2 = TtimeEnd2.Text == "" ? DateTime.Now : Convert.ToDateTime(TtimeEnd2.Text);


        if (info.Closetimes1 < info.Opentimes1)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('营业时间段一必须早于营业时间段二');isPromotion();");
            return;
        }




        info.StopAM = DateTime.Now;
        info.StopPM = DateTime.Now;
        info.email = WebUtility.InputText(tbemail.Text);
        info.BigPicture = "";
        //新增
        if (hidDataId.Value == "0")
        {
            //判断权限
            int _rs = WebUtility.checkOperator(2);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);isPromotion();");
                return;
            }


            if (info.IsCallCenter == 1)
            {
                string sqls = "IsCallCenter=" + info.IsCallCenter + " and cityid=" + info.cityid;
                if (togoBll.GetCount(sqls) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('一个城市只能添加一个超市类型的商家','success','true',5);isPromotion();");
                    return;
                }
            }




            info.InTime = DateTime.Now;
            info.reviewtimes = 0;
            info.PosAddr = "";
            info.PosRoom = "";
            info.PosAttch = "";
            info.PosSrvAd = "";
            info.FlavorGrade = 1;
            info.ServiceGrade = 1;
            info.SpeedGrade = 1;
            info.InTime = DateTime.Now;
            info.ViewTimes = 0;
            info.pop = 0;
            info.Password = WebUtility.GetMd5(tbPassword.Text);
            info.InUse = "Y";
            int togo_num = togoBll.Add(info);

            if (togo_num > 0)
            {
                hidDataId.Value = togo_num.ToString();
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增商家成功','id:divShowContent','640','150','true','','true','text');isPromotion();");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增商家失败','text:新增商家失败','250','150','true','','true','text');isPromotion();");
            }
        }
        else
        {


            if (info.IsCallCenter == 1)
            {
                string sqls = "IsCallCenter=" + info.IsCallCenter + " and cityid=" + info.cityid + " and  unid <> "+info.Unid;
                if (togoBll.GetCount(sqls) > 0)
                {
                    AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('一个城市只能添加一个超市类型的商家','success','true',5);isPromotion();");
                    return;
                }
            }


            //判断权限
            int _rs = WebUtility.checkOperator(3);
            if (_rs == 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);init();");
                return;
            }
            if (tbPassword.Text != "")
            {
                info.Password = WebUtility.GetMd5(tbPassword.Text);
            }
            if (togoBll.Update(info) > 0)
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('您可以继续进行的操作','id:divShowContent','640','150','true','','true','text');isPromotion();");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新商家信息失败','text:更新商家信息失败','250','150','true','','true','text');isPromotion();");
            }
            new shopdelivery().GetModelbyShopId(info.Unid);
        }
        Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/shop" + HjNetHelper.GetQueryInt("id", 0) + "/info");
       
    }

    /// <summary>
    /// 复制店铺
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void copy_Click(object sender, EventArgs e)
    {
        //判断权限
        int _rs = WebUtility.checkOperator(2);
        if (_rs == 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('无操作权限','success','true',5);hideload_super();;init();");
            return;
        }


        int shopid = HjNetHelper.GetQueryInt("id", 0);
        int newshopid = togoBll.CopyShop(shopid);

        if (newshopid > 0)
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('复制成功，点击确定查看生成的店铺','250','150','true','1000','true','text');gourl('ShopDetail.aspx?id=" + newshopid + "');hideload_super();");

            Hangjing.Cache.EasyEatCache.GetCacheService().RemoveObject("/shop/");
        }
        else
        {
            AlertScript.RegScript(this.Page, this.UpdatePanel1, "alert('操作失败');hideload_super();");
        }
    }

}
