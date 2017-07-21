#region license
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// Function :礼品信息详细
// Created by tuhui at 2010-6-24 16:28:14.
// E-Mail: tuhui@ihangjing.com
#endregion
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
using Hangjing.Model;
using Hangjing.Common;
using Hangjing.Cache;
using System.IO;
using org.in2bits.MyXls;
using System.ComponentModel;

/// <summary>
/// 生成优惠券
/// </summary>
public partial class qy_54tss_Admin_Gifts_importshopcart : System.Web.UI.Page
{
    BackgroundWorker bgw;
    batshopcard dal = new batshopcard();
    ShopCard dalc = new ShopCard();
    public int dataid = 0;

    protected EAdminInfo admin
    {
        set
        {
            ViewState["admin"] = value;
        }
        get
        {
            return (EAdminInfo)ViewState["admin"];
        }
    }

    int count = 0;
    string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        dataid = HjNetHelper.GetQueryInt("id", 0);
        if (!IsPostBack)
        {
            admin = UserHelp.GetAdmin();

            if (dataid > 0)
            {
                batshopcardInfo info = dal.GetModel(dataid);
                law1.Disabled = true;
                law2.Disabled = true;
                law3.Disabled = true;
                switch (info.Inve1)
                {
                    case 1:
                        tbpoint1.Text = info.point.ToString();
                        tbmoneyline1.Text = info.moneyline.ToString();
                        law1.Checked = true;
                        tbpoint1.Enabled = false;
                        tbmoneyline1.Enabled = false;
                        break;
                    case 2:
                        tbpoint2.Text = info.point.ToString();
                        tbmoneyline2.Text = info.moneyline.ToString();
                        law2.Checked = true;
                        tbpoint2.Enabled = false;
                        tbmoneyline2.Enabled = false;
                        break;
                    case 3:
                        tbpoint3.Text = info.point.ToString();
                        tbmoneyline3.Text = info.moneyline.ToString();
                        law3.Checked = true;
                        tbpoint3.Enabled = false;
                        tbmoneyline3.Enabled = false;
                        break;
                    default:
                        break;
                }

                tbtitle.Text = info.title;
                tbtitle.Text = info.title;
                fcContent.Value = info.Contents;
                tbsortnum.Text = info.sortnum.ToString();
                ImgUrl1.Value = info.Inve2;
                ImgUrl.Src = WebUtility.ShowPic(info.Inve2);
                WebUtility.SelectValue(ddlReveInt, info.mtype.ToString());
                ddlReveInt.Enabled = false;
                tbCardCount.Text = info.CardCount.ToString();
                trexcel.Style["display"] = "none";
                tbmydiscount.Text = info.mydiscount.ToString();
            }
            else
            {
                law1.Checked = true;
            }
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        bgw = new BackgroundWorker();
        bgw.WorkerSupportsCancellation = true;
        bgw.WorkerReportsProgress = true;

        bgw.DoWork += new DoWorkEventHandler(DoWork);
        bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);

        bgw.RunWorkerAsync();
    }


    /// <summary>
    /// 耗时操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void DoWork(object sender, DoWorkEventArgs e)
    {
        batshopcardInfo bm = new batshopcardInfo();
        bm.AddDate = DateTime.Now;
        bm.batnum = "";
        bm.CardCount = Convert.ToInt32(tbCardCount.Text);
        bm.cantimes = bm.CardCount;
        bm.DataID = dataid;
        bm.title = WebUtility.InputText(tbtitle.Text);
        bm.Inve2 = ImgUrl1.Value;
        bm.Contents = fcContent.Value;
        bm.AdminName = admin.AdminName;
        bm.sortnum = Convert.ToInt32(this.tbsortnum.Text);
        bm.mtype = Convert.ToInt32(ddlReveInt.SelectedValue);
        string filepath = "~/upload/excel/";
        string suffix = "";
        if (bm.mtype == 2 && bm.DataID == 0)
        {
            if (this.fuFoodExcel.HasFile)
            {
                suffix = Path.GetExtension(this.fuFoodExcel.PostedFile.FileName);
                if (suffix == ".xls" || suffix == ".XLS")
                {

                }
                else
                {
                    msg = "文件格式不正确，请上传excel文件";
                    return;
                }
            }
            else
            {
                msg = "请上传excel文件";
                return;
            }

            if (!System.IO.Directory.Exists(Server.MapPath(filepath)))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(filepath));
            }
        }


        bm.mydiscount = Convert.ToInt32(tbmydiscount.Text);
        bm.foossortids = "";

        bm.starttime = DateTime.Now;
        bm.endtime = DateTime.Now;
        bm.timelimity = 1;
        bm.moneylimity = 0;
        bm.Inve1 = 0;

        if (law1.Checked == true)
        {
            bm.Inve1 = 1;
            bm.point = Convert.ToDecimal(tbpoint1.Text);
            bm.moneyline = Convert.ToInt32(tbmoneyline1.Text);
        }
        if (law2.Checked == true)
        {
            bm.Inve1 = 2;
            bm.point = Convert.ToDecimal(tbpoint2.Text);
            bm.moneyline = Convert.ToInt32(tbmoneyline2.Text);
        }
        if (law3.Checked == true)
        {
            bm.Inve1 = 3;
            bm.point = Convert.ToDecimal(tbpoint3.Text);
            bm.moneyline = Convert.ToInt32(tbmoneyline3.Text);
        }

        if (bm.DataID > 0)
        {   
            dal.Update(bm);
            msg = "操作成功";
        }
        else
        {
            int rs = dal.Add(bm);
            if (rs > 0)
            {
                if (bm.mtype == 2 && bm.DataID == 0)
                {
                    string sFile = filepath + System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + suffix;  //上传后文件的新名
                    //保存文件
                    try
                    {
                        this.fuFoodExcel.PostedFile.SaveAs(Server.MapPath(sFile));
                    }
                    catch (Exception ex)
                    {
                        HJlog.toLog(ex.ToString());
                        return;
                    }
                    //必要的判断以防止excel格式的问题导致程序错误
                    try
                    {
                        //解析文件并保存数据到数据库
                        XlsDocument xls = new XlsDocument(Server.MapPath(sFile));
                        Worksheet sheet = xls.Workbook.Worksheets[0];

                        //excel格式:第一行是说明，第二行是标题,第三行开始是数据
                        for (int i = 3; i < sheet.Rows.Count; i++) //
                        {
                            if (sheet.Rows[ushort.Parse(i.ToString())] != null && sheet.Rows[ushort.Parse(i.ToString())].CellCount >= 1 && sheet.Rows[ushort.Parse(i.ToString())].GetCell(ushort.Parse("1")) != null && sheet.Rows[ushort.Parse(i.ToString())].GetCell(ushort.Parse("1")).Value != null)
                            {
                                ShopCardInfo model = new ShopCardInfo();
                                model.AddDate = DateTime.Now;
                                model.batid = rs;
                                model.cardnum = "";
                                model.ckey = sheet.Rows[ushort.Parse(i.ToString())].GetCell(ushort.Parse("1")).Value.ToString();
                                model.cmoney = bm.point;
                                model.State = 0;
                                model.canday = 0;
                                model.Inve1 = admin.ID;
                                model.Inve2 = "0";
                                model.UserID = 0;
                                model.username = "";
                                model.ReveInt = bm.mtype;
                                model.ReveVar = "";
                                model.isbuy = 0;
                                model.buyuid = 0;
                                model.isused = 0;
                                model.timelimity = bm.timelimity;
                                model.starttime = bm.starttime;
                                model.endtime = bm.endtime;
                                model.moneylimity = bm.moneylimity;
                                model.moneyline = bm.moneyline;
                                model.ReveInt1 = bm.Inve1;
                                model.Point = bm.point;
                                model.ReveVar1 = "";
                                dalc.Add(model);
                                count++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = "导入出错，请检查excel";
                        HJlog.toLog(ex.ToString());
                        return;
                    }
                    string sql = " update batshopcard set CardCount = " + count + " where DataID =" + rs;
                    WebUtility.excutesql(sql);
                    WebUtility.FixdelCookie("gsmcode_admin");

                    msg = "操作成功，已经生成充值卡" + count + "张。";
                }
                else
                {
                    msg = "操作成功";
                }
            }
            else
            {
                msg = "操作失败";
            }
        }
        btSave.Visible = true;


    }

    /// <summary>
    /// 完成
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        string tipmsg = "";
        if (count == 0)
        {
            tipmsg = msg;
        }
        else
        {
            tipmsg = msg;
        }

        AlertScript.RegScript(this.Page, "alert('" + tipmsg + "');hideload_super();");
    }

}
