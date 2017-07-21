<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shoplogin.aspx.cs" Inherits="Html5.shoplogin" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>
        <%= SectionProxyData.GetSetValue(2)%></title>
    <link rel="stylesheet" href="css/jquery.mobile-1.2.0.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <link rel="stylesheet" href="css/wxdemo_mobile.css?v=<%=(new Random()).Next(0000,9999) %>">

    <script src="javascript/jquery.js"></script>

    <script src="javascript/jquery.mobile-1.2.0.js?v=<%=(new Random()).Next(0000,9999) %>"></script>

</head>
<body>
    <input type="hidden" runat="server" id="sendmsgcodestate" value="0" />
    <input type="hidden" runat="server" id="hfislogin" value="0" />
    <div data-role="page" data-theme="d" id="w_infor">
        <div data-role="header" data-theme="orange">
            <h1>商家登录</h1>

        </div>
        <!-- /header -->
        <div data-role="content" role="main" class="ui-content">
            <form method="post" action="shoplogin.aspx?openid=<%= Request["openid"]%>" data-ajax="false">
                <div class="wxdemo-gray-text">
                    <label for="tbname">
                        账号：</label>
                    <input name="tbuserName" type="text" runat="server" id="tbuserName" data-theme="d" placeholder="请输入您的商家帐号">
                    <label for="tbtel">
                        密码：</label>
                    <input name="tbpassword" type="password" id="tbpassword" data-theme="d" placeholder="请输入密码">
                    <div id="divError" runat="server" class="error_list" style="color: #FF6000">
                    </div>
                </div>
                <h3>
                    <input type="submit" value="登录" data-theme="green" onclick="return checkuser()" data-ajax="false" />
                </h3>
            </form>
        </div>
        <uc2:Foot runat="server" ID="footer" />
    </div>
</body>
</html>

<script type="text/javascript">

    //检查用户名称
    function checkuser() {
        var name = document.getElementById("tbuserName").value;
        var password = document.getElementById("tbpassword").value;
        if (name == "" || name == "请输入您的商家帐号") {
            alert("请输入您的商家帐号!");
            return false;
        }
        if (password == "" || password == "请输入密码") {
            alert("请输入密码!");
            return false;
        }
        return true;
    }



    var readyFunc = function onBridgeReady() {
        var curid;
        var curAudioId;
        var playStatus = 0;

        var hfislogin = $("#hfislogin").val();
        if (hfislogin == "1") {
            //验证验证码
            alert("登录成功，可实时接收订单了");
        }
        else {
            return;
        }

        // 关闭当前webview窗口 - closeWindow

        WeixinJSBridge.invoke(
            'closeWindow',
            {},
            function (res) {
            
        });


    }

    $(document).ready(function () {
        setTimeout("readyFunc()", 1000);
    })
</script>

