
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
using Hangjing.Common;

/// <summary>
/// 商家加盟
/// </summary>
public partial class applyshop : System.Web.UI.Page
{
    Points dal = new Points();
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!Page.IsPostBack)
        {

        }
    }

    /// <summary>
    /// 提交推荐
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BTsave_Click(object sender, EventArgs e)
    {
        string mycode = WebUtility.InputText(tbadcode.Value).ToLower();
        string cookiecode = Session["CheckCode"].ToString();
        if (cookiecode == "" || cookiecode == null || mycode != cookiecode)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "alert('验证码错误，请重新输入','text:验证码错误，请重新输入','250','150','true','5000','true','text');hideload_super();");
            return;
        }


        Points dal = new Points();
        PointsInfo info = new PointsInfo();
        info.Unid = 0;
        info.ID = "0";
        info.Name = WebUtility.InputText(tbName.Value);
        info.Comm = WebUtility.InputText(tbComm.Value);
        info.PType = 0;
        info.RcvType = 0;
        info.NamePy = "";
        info.CommPerson = WebUtility.InputText(tbCommPerson.Value);
        info.SendLimit = 0;
        info.LoginName = "";
        info.SendFee = 0;
        info.SN1 = "";
        info.SN2 = "0";
        info.Sn2Key = "";
        info.PTimes = 0;
        info.MgrCell = "";
        info.PHead = "";
        info.PEnd = "";
        info.IsCallCenter = 0;
        info.Address = WebUtility.InputText(tbAddress.Value);
        info.Introduce = WebUtility.InputText(tbIntroduce.Value);
        info.Status = 0;
        info.outnitice = "";
        info.OpenTime = "";
        info.Time1Start = DateTime.Now;
        info.Time1End = DateTime.Now;
        info.Time2Start = DateTime.Now;
        info.Time2End = DateTime.Now;
        info.bisnessStart = DateTime.Now;
        info.bisnessend = DateTime.Now;
        info.bisnessStart2 = DateTime.Now;
        info.bisnessend2 = DateTime.Now;
        info.IsDelete = 0;
        info.SortNum = 0;
        info.EBuilding = "";
        info.Star = 0;
        info.category = "";
        info.senttime = 0;
        info.sentorg = "0";
        info.special = "";
        info.money = 0;
        info.RefreshTime = "30";
        info.Inve1 = 0;
        info.cityid = Convert.ToInt32(WebUtility.get_userCityid());
        info.Picture = "";
        info.showpicture = 0;
        info.foodupdatetime = DateTime.Now;
        info.point = 100;
        info.ViewTimes = 0;
        //营业时间
        info.Opentimes1 = DateTime.Now;
        info.Opentimes2 = DateTime.Now;
        info.Closetimes1 = DateTime.Now;
        info.Closetimes2 = DateTime.Now;
        info.StopAM = DateTime.Now;
        info.StopPM = DateTime.Now;
        info.email = "";
        info.BigPicture = "";
        //新增
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
        info.Password = "";
        info.InUse = "Y";
        int togo_num = dal.Add(info);
        if (togo_num > 0)
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增商家成功','text:操作成功，请等待管理员审核。','250','150','true','5000','true','text');");
        }
        else
        {
            AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增商家失败','text:新增商家失败','250','150','true','5000','true','text');");
        }

    }
}
