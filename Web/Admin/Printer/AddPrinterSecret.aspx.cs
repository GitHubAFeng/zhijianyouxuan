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

// 打印机信息管理
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com 更新
// 2010-07-13

public partial class qy_54tss_Admin_Printer_AddPrinterSecret : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidatorSet validator = new ValidatorSet("Admin");
            validator.SetValidator();
            //编辑
            int DataId = HjNetHelper.GetQueryInt("id", 0);
            if (DataId > 0)
            {
                hidDataId.Value = DataId.ToString();
                BindData();
                pageType.Text = "更新打印机信息";
            }
            else
            {
                pageType.Text = "新增打印机";
                hidDataId.Value = "0";
            }
        }
    }

    PrinterSecret bll = new PrinterSecret();

    protected void BindData()
    {
        PrinterSecretInfo info = new PrinterSecretInfo();

        info = bll.GetModel(HjNetHelper.GetQueryInt("id", 0));

        hidDataId.Value = info.DataId.ToString();
        tbPrinterNum.Text = info.PrinterNum;
        tbPrinterSn.Text = info.PrinterSn;
        tbPrinterKey.Text = info.PrinterKey;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        PrinterSecretInfo info = new PrinterSecretInfo();

        info.PrinterNum = WebUtility.InputText(tbPrinterNum.Text);
        info.PrinterSn = WebUtility.InputText(tbPrinterSn.Text);
        info.PrinterKey = WebUtility.InputText(tbPrinterKey.Text);
        info.FirstSn = "";
        info.IsUse = 0;

        if (pageType.Text == "新增打印机")
        {
          int i=  bll.GetCount(string.Format("PrinterNum='{0}' or PrinterSn='{1}' or PrinterKey='{2}'", WebUtility.InputText(tbPrinterNum.Text), WebUtility.InputText(tbPrinterSn.Text), WebUtility.InputText(tbPrinterKey.Text)));
          if (i==0)
          {
             
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
              AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('新增失败','text:新增失败，打印机已存在','250','150','true','','true','text')");
          }
        }
        else
        {
            info.DataId = HjNetHelper.GetQueryInt("id", 0);
            if (bll.Update(info) > 0)
            {

                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新成功','id:divShowContent','640','150','true','','true','text')");
            }
            else
            {
                AlertScript.RegScript(this.Page, UpdatePanel1, "tipsWindown('更新失败','text:更新失败','250','150','true','','true','text')");
            }
        }
    }
}
