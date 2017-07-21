<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterSuccess.aspx.cs"
    Inherits="RegisterSuccess" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="uc2" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>手机注册成功 -  <%= SectionProxyData.GetSetValue(3)%>
    </title>
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />
    <link href="css/common.css" rel="stylesheet" />
    <link href="css/logon_login.css" rel="stylesheet" />
    <link href="css/Regitser.css" rel="stylesheet" />

    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="javascript/jCommon.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:Banner ID="Banner1" runat="server" />
        <!--主体部分-->
        <div class="warp">
            <div class="hplace_bg" style="margin-top: 15px;">
                <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 注册成功</span>
            </div>
            <div class="register_info">
                <div class="register_bg">
                    <ul>
                        <li>1.填写会员信息</li>
                        <li class="reg_02_hover">2.注册成功</li>
                    </ul>
                </div>
                <div class="register_under">
                    <div class="register_left_img">
                        <img src="images/photos_04.jpg" />
                    </div>
                    <div class="register_right_text">
                        <p>
                            恭喜，您已注册成功！
                        </p>
                        <p>
                            您的帐号为 <span class="orange padding_l10 padding_r10 fb f16">
                                <asp:Label ID="LabPhone" runat="server"></asp:Label></span> 或 <span class="orange padding_l10 padding_r10 fb f16">
                                    <asp:Label
                                        ID="LabName" runat="server"></asp:Label></span> 都可以用来直接登录哦！
                        </p>
                        <p>
                            <a href="index.aspx">立即订餐</a> 或 <a href="user/myindex.aspx">完善我的个人信息</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        </div>
    <!--页脚-->
        <uc2:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>
