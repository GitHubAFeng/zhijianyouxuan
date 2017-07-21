/*********************************************************************
 * CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
 * Function : 用户统计
 * Created by jijunjian at 2009-7-24 15:49:47.
 * E-Mail   : jijunjian@ihangjing.com
*********************************************************************/
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

public partial class Admin_User_UserCount :AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckRights("G");
        this.ScriptManager1.RegisterPostBackControl(this.bt_Count);
        if (Request.QueryString["type"] != null)
        {
            string type = Request.QueryString["type"].ToString();
            string str = "day,month,year,hour";
            if (str.Contains(type))
            {
                this.chart.DataFile = "CountUserData.aspx?type=" + type;
                this.chart.DataBind();
            }
        }
        else
        {
            this.chart.DataFile = "CountUserData.aspx?type=hour";
            this.chart.DataBind();
        }
    }

    protected void bt_Count_Click(object sender, EventArgs e)
    {
        if (this.tb_Start.Text != "" && this.tb_End.Text != "")
        {
            DateTime start = Convert.ToDateTime(this.tb_Start.Text);
            DateTime end = Convert.ToDateTime(this.tb_End.Text);
            TimeSpan span = end.Subtract(start);
            if (span.Days <= 0)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:结束时间不能小于开始时间!','250','150','true','3000','true','text');");
                return;
            }
            if (Request.QueryString["type"] != null)
            {
                string type = Request.QueryString["type"].ToString();
                if (CheckTime(type, start, end))
                {
                    this.chart.DataFile = "CountUserData.aspx?data=type" + type + "start" + start.ToShortDateString() + "end" + end.ToShortDateString();
                    this.chart.DataBind();
                }
            }
        }
        else
        {

            if (Request.QueryString["type"] != null)
            {
                string type = Request.QueryString["type"].ToString();
                this.chart.DataFile = "CountUserData.aspx?type=" + type;
                this.chart.DataBind();
            }
        }
    }

    private bool CheckTime(string type, DateTime start, DateTime end)
    {
        bool value = true;
        if (type == "year")
        {
            if (end.Year - start.Year >= 20)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:时间间隔必须要在20年内的!','250','150','true','3000','true','text');");
                value = false;
            }
        }
        else
        {
            if (start.Year != end.Year)
            {
                AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:统计时间必须在同一年内的!','250','150','true','3000','true','text');");
                value = false;
            }
            else
            {
                if (type == "day")
                {
                    TimeSpan span = end.Subtract(start);
                    if (span.Days >= 20)
                    {
                        AlertScript.RegScript(this.Page, this.UpdatePanel1, "tipsWindown('提示信息','text:时间间隔必须要在20天内的!','250','150','true','3000','true','text');");
                        value = false;
                    }
                }
            }
        }

        return value;
    }

}
