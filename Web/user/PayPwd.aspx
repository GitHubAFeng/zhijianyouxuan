<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayPwd.aspx.cs" Inherits="UserHome_PayPwd" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="~/header.ascx" TagName="banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<%@ Register Src="~/user/Left.ascx" TagName="LeftBanner" TagPrefix="uc2" %>
<%@ Register TagPrefix="epc" Namespace="Hangjing.Control" Assembly="Hangjing.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置支付密码 - 会员中心 -
        <%= WebUtility.GetWebName() %></title>
    <link href="css/Common.css" rel="stylesheet" type="text/css" />
    <link href="css/userinfo.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" type="text/css" rel="Stylesheet" />
    <link href="../css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/divDialog.css" rel="stylesheet" type="text/css" />

    <script src="../javascript/jCommon.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JavaScript/ShowDivDialog.js" type="text/javascript"></script>

    <script type="text/javascript">
        function sentcode() {
            var input = document.getElementById('btsentcode');
            input.setAttribute('disabled', 'disabled');
            document.getElementById('btsentcode').value = '正在发送验证码';

            jQuery.ajax(
                {
                    type: "post",
                    url: "../Ajax/SendGmsCode.aspx",
                    data: "phone=" + $("#tbmobilephone").val() + "&type=active",
                    success: function (msg) {
                        if (msg != "-1") {
                            document.getElementById('btsentcode').value = '验证码发送成功';
                        }
                        else {
                            alert('验证码发送失败，请联系我们');
                            $("#btsentcode").removeAttr("disabled");
                        }
                    }
                })
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField runat="server" ID="tbmobilephone" />
        <top:banner ID="Banner1" runat="server" />
        <uc1:banner ID="Banner2" runat="server" />
        <div class="warp_con">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <uc2:LeftBanner runat="server" ID="Left" />
                    <div class="rightmenu_cont">
                        <div class="rightmenu_cen">
                            <h1 class="topbg">设置支付密码</h1>
                            <div class="usermima safe-sevi" id="divstep1" runat="server">
                                <div id="step1" class="step step01">
                                    <ul>
                                        <li class="fore1">1.验证身份<b></b></li>
                                        <li class="fore2">2.设置支付密码<b></b></li>
                                        <li class="fore3">3.完成</li>
                                    </ul>
                                </div>
                                <div class="form form01" style="display: none;">
                                    <div class="item item01">
                                        <span class="label">已验证手机：</span>
                                        <div class="fl">
                                            <strong id="mobileSpan" class="ftx-un" runat="server"></strong>
                                            <div class="clr">
                                            </div>
                                            <div class="msg-text">
                                                若该手机号无法接收验证短信，联系网站客服
                                            </div>
                                        </div>
                                        <div class="clr">
                                        </div>
                                    </div>
                                    <div class="item">
                                        <span class="label">&nbsp;</span>
                                        <div class="fl">
                                            <input type="button" value="获取手机验证码" id="btsentcode" onclick="sentcode();" />
                                            <div class="clr">
                                            </div>
                                            <div class="msg-text" style="display: none;" id="countDown">
                                                校验码已发出，请注意查收短信，如果没有收到，你可以在<strong class="ftx-01"></strong>秒要求系统重新发送
                                            </div>
                                            <div class="clr">
                                            </div>
                                            <div style="display: none;" id="sendCode_error" class="msg-error">
                                            </div>
                                        </div>
                                        <div class="clr">
                                        </div>
                                    </div>
                                </div>
                                <div class="paypwd_infor">
                                    <ul>

                                        <li style="display: none;"><span class="left_span">请填写手机校验码：</span>
                                            <epc:TextBox ID="tbphonevalid" runat="server" Style="width: 70px; float: left;"></epc:TextBox>
                                        </li>
                                        <li><span class="left_span">请填写用户密码：</span>
                                            <epc:TextBox ID="tbuserpwd" runat="server" TextMode="Password" Style="width: 120px; float: left;"></epc:TextBox>
                                        </li>
                                        <li><span class="left_span">验证码：</span>
                                            <epc:TextBox ID="tbmycode" runat="server" Style="width: 70px; float: left;"></epc:TextBox>
                                            <img src="../Admin/VCode.aspx?t='+new Date().getTime()" onclick="this.src = '../Admin/VCode.aspx?t='+new Date().getTime();"
                                                title="点击换一张" style="padding-left: 10px; cursor: pointer;" />
                                        </li>
                                        <li style="padding-left: 100px;">
                                            <asp:Button ID="btUpdate" runat="server" Text="确认修改" class="subBtn" OnClick="btUpdate_Click" />
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="usermima" style="display: none;" id="divstep2" runat="server">
                                <input type="checkbox" checked="checked" disabled="disabled" />
                                提交订单时，将启用支付密码
                                <asp:Button ID="Button2" runat="server" Text="确定" class="subBtn" OnClick="btnNext_Click" />
                            </div>
                            <div class="usermima" style="display: none" id="divstep3" runat="server">
                                <ul>
                                    <li><span class="left_span">支付密码：</span>
                                        <asp:TextBox ID="tbNewPwd" runat="server" TextMode="Password" CssClass="input_on"></asp:TextBox>
                                        <div class="mynotice">
                                            请输入新密码!
                                        </div>
                                    </li>
                                    <li><span class="left_span">确认新密码：</span>
                                        <asp:TextBox ID="tbPwdagin" runat="server" TextMode="Password" CssClass="input_on"></asp:TextBox>
                                        <div class="mynotice">
                                            请再次输入密码!
                                        </div>
                                    </li>
                                    <li style="padding-left:100px;">
                                        <asp:Button ID="Button1" runat="server" Text="确认修改" OnClientClick="return checkNull();"
                                            class="subBtn" OnClick="btpayUpdate_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div class="usermima" style="display: none" id="divstep4" runat="server">
                                <div class="email_verifi_con">
                                    密码设置成功
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <foot:foot runat="server" ID="foot" />
    </form>
</body>
</html>
<script type="text/javascript">
    function checkemail() {
        //        var tbmyemail = $("#tbmyemail").val() + "";
        //        if (tbmyemail == "") {
        //            alert("请输入邮箱");
        //            return false;
        //        }
        var tbmycode2 = $("#tbmycode2").val() + "";
        if (tbmycode2 == "") {
            alert("请输入验证码");
            return false;
        }
        return true;
    }
    function checkNull() {
        if ($("#tbNewPwd").val() == "") {
            alert("请输入支付密码！");
            return false;
        }
        if ($("#tbPwdagin").val() == "") {
            alert("请输入确认支付密码！");
            return false;
        }
    }
</script>

