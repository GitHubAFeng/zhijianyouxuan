﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

using Hangjing.Model;
using Hangjing.SQLServerDAL;
// CopyRight (c) 2009-2012 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2012-02-17
// 检查是否有更新

public partial class APP_Android_deliver_CheckUpdate : System.Web.UI.Page
{
    //数据库中保存最新的版本号
    //传入参数:当前客户端使用的程序版本号
    //版本号进行比较，返回结果：{\"state\":\"1\",\"ulr\":\"http:\\/\\/www.dianyifen.com\\/upload\\/togopicture\\/1.apk\"}
    //state： 1有更新 0 无更新
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string mobile_version = Request["version"];
        string lastest_version = "";

        //TODO：app版本记录表 
        //目前直接在web.config中指定当前的版本号
        lastest_version = System.Configuration.ConfigurationManager.AppSettings["AndroidAppDeliverVersion"].ToString();// "1.0";

        StringBuilder json = new StringBuilder();

        if (mobile_version.Equals(lastest_version))
        {
            json.Append("{\"state\":\"0\",\"url\":\"\"}");
        }
        else
        {
            json.Append("{\"state\":\"1\",\"url\":\"\"}");
        }

        Response.Write(json.ToString());
        Response.End();
    }
}
