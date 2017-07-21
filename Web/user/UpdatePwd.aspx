<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePwd.aspx.cs" Inherits="UserHome_UpdatePwd" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员中心-修改密码-<%= SectionProxyData.GetSetValue(3)%></title>
    <link rel="stylesheet" type="text/css" href="../css/sytle.css"></link>
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css"></link>
    <link href="../css/common.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkNull(oldpwd, newpwd, pwdagin, msgoldpwd, msgnewpwd, msgpwdagin) {
            if (document.getElementById(oldpwd).value.trim() == "") {
                document.getElementById(oldpwd).focus();

                if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) {
                    document.getElementById(msgoldpwd).innerText = "请输入原始密码!";
                }
                else if (navigator.userAgent.indexOf('Firefox') >= 0) {
                    document.getElementById(msgoldpwd).innerText = "请输入原始密码!";
                }
                return false;
            }
            if (document.getElementById(newpwd).value.trim() == "") {
                document.getElementById(newpwd).focus();

                if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) {
                    document.getElementById(msgnewpwd).innerText = "请输入新密码!";
                }
                else if (navigator.userAgent.indexOf('Firefox') >= 0) {
                    document.getElementById(msgnewpwd).innerText = "请输入新密码!";
                }
                return false;
            }
            if (document.getElementById(pwdagin).value.trim() == "") {
                document.getElementById(pwdagin).focus();

                if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) {
                    document.getElementById(msgpwdagin).innerText = "请再次输入新密码!";
                }
                else if (navigator.userAgent.indexOf('Firefox') >= 0) {
                    document.getElementById(msgpwdagin).innerText = "请再次输入新密码!";
                }
                return false;
            }
            return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <top:banner ID="Banner1" runat="server" />
            <uc1:banner ID="Banner2" runat="server" />
            <div class="warp">
                <uc2:LeftBanner runat="server" ID="Left" />
                <div class="rightmenu_cont">
                    <div class="rightmenu_cen">
                        <h1 class="topbg">
                            修改密码</h1>
                        <div class="usermima">
                            <ul>
                                <li><span class="left_span">原来密码：</span>
                                    <asp:TextBox ID="tbOldPwd" runat="server" TextMode="Password" class="text" ></asp:TextBox>
                                    <asp:Label ID="lbmsgOldpwd" runat="server"></asp:Label>
                                </li>
                                <li><span class="left_span">新密码：</span>
                                    <asp:TextBox ID="tbNewPwd" runat="server" TextMode="Password" class="text" ></asp:TextBox>
                                    <asp:Label ID="lbmsgNewpwd" runat="server"></asp:Label>
                                </li>
                                <li><span class="left_span">确认新密码：</span>
                                    <asp:TextBox ID="tbPwdagin" runat="server" TextMode="Password" class="text" ></asp:TextBox>
                                    <asp:Label ID="lbmsgPwdagin" runat="server"></asp:Label>
                                </li>
                                <li style="padding-left:100px;">
                                    <asp:Button ID="btUpdate" runat="server" Text="确认修改" class="subBtn" OnClick="btUpdate_Click" />
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <foot:foot ID="Foot1" runat="server" />
    </form>
</body>
</html>
