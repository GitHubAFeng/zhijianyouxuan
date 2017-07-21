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

// 打印机列表管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 更新
// 2010-07-13

public partial class qy_54tss_Admin_Shop_AddPrinter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatorSet validator = new ValidatorSet("Admin");
        validator.SetValidator();
        if (!IsPostBack)
        {
            //首先需要判断是否存在该商店对应的打印机
            int TogoId = HjNetHelper.GetQueryInt("tid", 0);
            hidTogoId.Value = TogoId.ToString();

            if (TogoId > 0)
            {
                tbName.Text = new Hangjing.SQLServerDAL.Points().GetModel(TogoId).Name;

                TogoPrinterInfo info = new TogoPrinterInfo();
                info = bll.GetModelByTogoId(TogoId);
                if (info != null)
                {
                    hidDataId.Value = info.DataId.ToString();

                    BindData(info);

                    pageType.Text = "更新商店打印机信息";
                }
                else
                {
                    pageType.Text = "商店新增打印机信息";
                }
            }
            else
            {
                Response.Redirect("ShopList.aspx");
            }
        }
    }

    Hangjing.SQLServerDAL.TogoPrinter bll = new TogoPrinter();

    protected void BindData(TogoPrinterInfo info)
    {
        tbPrinterNum.Text = info.PrinterSn;
        hidPrinterSn.Value = info.PrinterSn;
    }

    //以下代码重要：更新系统中的打印机状态 更新商店是否有打印机
    protected void btSave_Click(object sender, EventArgs e)
    {

        TogoPrinterInfo info = new TogoPrinterInfo();
        info.PrinterSn = WebUtility.InputText(tbPrinterSn.Text);
        try
        {
            //删除商店的打印机
            if (!cbUsePrinter.Checked)
            {
                //首先检查商店是否存在打印机
                TogoPrinterInfo _info = new TogoPrinterInfo();
                _info = bll.GetModelByTogoId(Convert.ToInt32(hidTogoId.Value));

                if (_info == null)
                {
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('操作失败','text:该商店未使用打印机无需删除','250','150','true','','true','text')");
                }
                else
                {

                    //删除商店打印机关系记录
                    if (bll.DelTogoPrinter(Convert.ToInt32(hidDataId.Value)) > 0)
                    {
                        //TODO:此处考虑使用事务处理
                        //设置打印机状态为未使用状态
                        new PrinterSecret().UpdateValue("IsUse", 0, "  where PrinterSn='" + _info.PrinterSn + "'");
                        AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('删除成功','id:divShowContent','640','150','true','','true','text')");
                    }
                    else
                    {
                        AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('删除失败','text:删除失败','250','150','true','','true','text')");
                    }
                }

                return;
            }

            info.TogoId = Convert.ToInt32(hidTogoId.Value);
            info.TogoNum = "";//此字段未使用到
            info.PrinterSn = WebUtility.InputText(tbPrinterNum.Text);//tbPrinterNum
            info.PrintPage = 1;
            info.PrintTop = "";
            info.PrintFoot = "";
            info.IsUpdate = 1;
            info.TogoNum = "";

            if (pageType.Text == "商店新增打印机信息")
            {
                info.IsUpdate = 1;
                info.LastLoginDate = DateTime.Now.AddDays(-1);

                if (bll.Add(info) > 0)
                {  //判断权限
                   
                    pageType.Text = "更新商店打印机信息";
                    //设置打印机状态为使用中
                    new PrinterSecret().UpdateValue("IsUse", 1, " where PrinterSn='" + tbPrinterSn.Text.Trim() + "'");

                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增成功','id:divShowContent','640','150','true','','true','text')");
                }
                else
                {

                    //设置打印机状态为未使用
                    new PrinterSecret().UpdateValue("IsUse", 0, " where PrinterSn='" + info.PrinterSn + "'");

                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增失败','text:新增失败','250','150','true','','true','text')");
                }
            }
            else
            {
                info.DataId = Convert.ToInt32(hidDataId.Value);
                info.IsUpdate = 1;
                info.LastLoginDate = DateTime.Now.AddDays(-1);

                if (bll.Update(info) > 0)
                {
                   
                    //设置打印机状态为使用中
                    new PrinterSecret().UpdateValue("IsUse", 1, " where PrinterSn='" + info.PrinterSn + "'");
                    //更新此商店的是否有打印机字段
                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新成功','id:divShowContent','640','150','true','','true','text')");
                }
                else
                {
                    //设置打印机状态为未使用
                    new PrinterSecret().UpdateValue("IsUse", 0, " where PrinterSn='" + info.PrinterSn + "'");

                    AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新失败','text:更新失败','250','150','true','','true','text')");
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void cbUsePrinter_CheckedChanged(object sender, EventArgs e)
    {
        if (!cbUsePrinter.Checked)
        {
            tbPrinterNum.Text = "0";
            tbPrinterSn.Text = "0";
        }
    }
}
