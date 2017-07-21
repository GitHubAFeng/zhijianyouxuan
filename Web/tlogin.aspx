<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tlogin.aspx.cs" Inherits="tlogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>商家登录-<%= SectionProxyData.GetSetValue(3)%>
    </title>
    <link rel="stylesheet" type="text/css" href="css/sytle.css" />
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="css/togo_login.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />

    <script src="JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script src="javascript/jCommon.js" type="text/javascript"></script>

    <script type="text/javascript">

        function init() {

            $(document).ready(function () {
                location.href = "tlogin.aspx";

            });
        }

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

        function enterIn(event) {
            if (event.keyCode == 13 || event.which == 13) {
                if (navigator.userAgent.indexOf("MSIE") > 0) {
                    $("#btLogin").click(); event.returnValue = false;
                }
                if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
                    $("#btLogin").click();
                    return false;
                }
                return false;
            }
            else {

            }
        }

    </script>
</head>
<body>
    <form id="flogin" runat="server">
        <!-- top end-->
        <div class="wrap">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="hplace_bg">
                        <span class="hplace_house"><a href="index.aspx">首页</a> &gt;&gt; 商家登录</span>
                    </div>
                    <div class="tlogin_con">
                        <div class="user_t">
                            商家登录
                        </div>
                        <div class="user_in">
                            <div class="user_in_left">
                                <div class="user_in_left_t">
                                    商家登录
                                </div>
                                <div class="user_in_left_ul">
                                    <ul>
                                        <li><span>帐号</span>
                                            <input runat="server" id="tbemail" name="" type="text" class="user_in_left_ul_input"
                                                onmouseover="this.className='user_in_left_ul_inputh'" onmouseout="this.className='user_in_left_ul_input'" /></li>
                                        <li><span>密码</span>
                                            <input runat="server" id="tbpassword" name="" type="password"
                                                class="user_in_left_ul_input" onmouseover="this.className='user_in_left_ul_inputh'"
                                                onmouseout="this.className='user_in_left_ul_input'" onkeydown="return enterIn(event);" />
                                            <a href="shopforgetpassword.aspx">忘记密码？</a></li>
                                    </ul>
                                    <div>
                                        <asp:Button runat="server" ID="btLogin" CssClass="user_deng" OnClientClick="return checklogin();"
                                            OnClick="Login_Click" Text="登 录" />
                                    </div>
                                </div>
                            </div>
                            <div class="tlogo_line">
                            </div>
                            <div class="user_in_right">
                                <div class="user_in_right_t">
                                    商家加盟
                                </div>
                                <div style="margin-top: 30px; margin-left: 45px;">
                                    若您想加盟<%= SectionProxyData.GetSetValue(2) %>，请
                            <asp:Button ID="submitorder" runat="server" Text="联系我们" class="refer_buy" OnClientClick="window.location = 'applyshop.aspx'" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
