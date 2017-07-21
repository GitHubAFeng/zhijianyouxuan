<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shopforgetpassword.aspx.cs"
    Inherits="shopforgetpassword" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>商家找回密码 -
        <%= SectionProxyData.GetSetValue(3)%></title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/logon_login.css" />

    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="javascript/shopFindPassword.js" type="text/javascript"></script>

    <style type="text/css">
        .text_style
        {
            line-height: 22px;
        }
    </style>
</head>
<body>
    <!--公共部分-->
    <form id="form1" runat="server">
    <uc3:Banner ID="Banner2" runat="server" />
    <uc1:Banner ID="Banner1" runat="server" />
    <!--主体部分-->
    <div class="wrap margin_b10">
        <div class="hplace_bg">
            <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 忘记密码</span>
        </div>
        <div class=" forget_code_info">
            <div class="f12 orange" style="margin-bottom: 15px;">
                提示：请输入帐号和邮箱，我们会把重设密码邮件发送到您的邮箱(发送失败请刷新页面再发送)！
                <div style="color: #0863C0; padding-left: 200px; display: none" id="loading">
                    <img src="images/loading.gif" />
                    <div>
                        正在发送...</div>
                </div>
            </div>
            <p>
                用户名<input name="" type="text" class="text_style" id="tbname" /><span class="text_heddin_style"
                    id="nikenamemsg" style="display: none; color: Red;">请输入用户名</span></p>
            <p class="forget_code_email">
                Email<input name="" type="text" class="text_style" id="tbemail1" /><span class="text_heddin_style"
                    id="emailmsg" style="display: none; color: Red;">请输入邮箱</span></p>
            <p>
                <input name="" type="button" class="sure_button" value="确 定" id="btOK" onclick="return  CheckForm();" />
            </p>
        </div>
    </div>
    <foot:foot ID="foot" runat="server" />
    </form>
</body>
</html>
