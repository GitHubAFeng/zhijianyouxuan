<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="uc2" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员登录-<%= WebUtility.GetWebName() %>
    </title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/logon_login.css" rel="stylesheet" type="text/css" />

    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checklogin() {
            var name = document.getElementById("tbemail").value;
            var password = document.getElementById("tbpassword").value;
            if (name == "") {
                tipsWindown('提示信息', 'text:请输入用户名!', '250', '150', 'true', '1000', 'true', 'text');
                return false;
            }
            if (password == "") {
                tipsWindown('提示信息', 'text:请输入密码!', '250', '150', 'true', '1000', 'true', 'text');
                return false;
            }
            return true;
        }

    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:header ID="header" runat="server" />
        <div class="wrap mgb15">
            <div class="hplace_bg">
                <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 用户登录</span>
            </div>
            <div class="logon_info" style="_height: 1%;">
                <div class="logon_left">
                    <h2 class="login_wel">登录</h2>
                    <div runat="server" id="divError" class="prompt_text">
                    </div>
                    <div class="logon_info_bg">
                        <p>
                            <span>账号</span><input runat="server" id="tbemail" name="" type="text" class="user_name"
                                onmouseout="this.className='user_name'" onmousemove="this.className='user_name_hover'" placeholder="请输入会员名或手机号" />
                        </p>
                        <p>
                            <span>密码</span><input runat="server" id="tbpassword" name="" type="password" class="user_name"
                                onmouseout="this.className='user_name'" onmousemove="this.className='user_name_hover'" placeholder="请输入登录密码" />
                        </p>
                    </div>
                    <span class="forget_code"><a href="forgetpassword.aspx">忘记密码？</a></span>
                    <asp:Button runat="server" ID="btLogin" class="login_button" Text="登录" OnClientClick="return checklogin();"
                        OnClick="Login_Click" />

                </div>
                <div class="logon_right">
                    <p class="reg_text">
                        新用户注册
                    </p>
                    <p>
                        还不是<%= SectionProxyData.GetSetValue(2) %> 用户吗？30秒完成注册！
                    </p>
                    <a href="<%= WebUtility.GetUrl("~/RegisterByEmail.aspx") %>">立即注册&gt;&gt;</a>
                </div>
            </div>

        </div>
        <uc2:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>
