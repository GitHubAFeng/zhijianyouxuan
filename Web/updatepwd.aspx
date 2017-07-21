<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updatepwd.aspx.cs" Inherits="updatepwd" %>


<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="uc2" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>重置密码 - <%= WebUtility.GetWebName() %>
    </title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/logon_login.css" rel="stylesheet" type="text/css" />

    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckForm() {
            var tbnewpwd = $("#tbnewpwd").val();

            if (tbnewpwd == "") {
                alert("请输入新密码");
                return false;
            }

            var tbnagainpwd = $("#tbnagainpwd").val();
            if (tbnewpwd != tbnagainpwd) {
                alert("两次密码不一至，请重新输入");
                return false;
            }

            showloadfix();
            return true;
        }

    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:header ID="header" runat="server" />
        <div class="wrap margin_b10">
            <div class="hplace_bg">
                <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 重置密码</span>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="logon_info" style="_height: 1%;">
                        <div runat="server" id="divError">
                        </div>
                        <p>
                            <span>&nbsp;&nbsp;&nbsp;新密码：</span><input runat="server" id="tbnewpwd" name="" type="password" class="user_name"
                                onmouseout="this.className='user_name'" onmousemove="this.className='user_name_hover'" />
                        </p>
                        <p>
                            <span>确认密码：</span><input runat="server" id="tbnagainpwd" name="" type="password" class="user_name"
                                onmouseout="this.className='user_name'" onmousemove="this.className='user_name_hover'" />
                        </p>
                        <p style="padding-left:60px;">
                            <asp:Button runat="server" ID="btLogin" CssClass="common_button" OnClientClick="return CheckForm();"
                                OnClick="Login_Click" Text="确 定" />

                        </p>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <uc2:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>
