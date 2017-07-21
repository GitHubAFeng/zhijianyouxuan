<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPwd.aspx.cs" Inherits="Html5.PayPwd" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>
        <%= SectionProxyData.GetSetValue(2)%></title>

    <link type="text/css" rel="stylesheet" href="css/style.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=<%=(new Random()).Next(0000,9999) %>" />

    <script src="javascript/jquery.js"></script>
</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" id="back" data-ajax="false" class=" top_left"></a>
            <h1>设置支付密码</h1>
        </div>
        <form method="post" action="PayPwd.aspx" data-ajax="false">
            <div class="container">
                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>登录密码</strong>
                                <input name="tbOldPwd" type="password" id="tbOldPwd" class="w_txt" placeholder="请输入登录" />
                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>支付密码</strong>
                                <input name="tbNewPwd" type="password" id="tbNewPwd" class="w_txt" placeholder="请输入支付密码" />
                            </span>
                        </div>
                        <div class="order-tit" style="border-bottom:none;">
                            <span class="time"><strong>确认支付密码</strong>
                                <input name="tbPwdagin" type="password" id="tbPwdagin" class="w_txt" placeholder="请再次输入支付密码" />
                            </span>
                        </div>
                    </li>
                </ul>
                <div class="view_back_con" id="divError" runat="server"  >
                    <input type="submit" value="登录" onclick="return checkuser()" class="view_back_btn" data-ajax="false" />
                </div>
            </div>
        </form>
    </div>
</body>
</html>
<script src="javascript/jCommon.js?v=<%=(new Random()).Next(0000,9999) %>"></script>
<script type="text/javascript">
    //提交订单post错误信息
    //检查用户名称
    function checkuser() {
        var name = document.getElementById("tbOldPwd").value;
        var password = document.getElementById("tbNewPwd").value;
        var passwordagain = document.getElementById("tbPwdagin").value;
        if (name == "") {
            alert("请输入登录密码!");
            return false;
        }
        if (password == "") {
            alert("请输入支付密码!");
            return false;
        }
        if (password != passwordagain) {
            alert("请确认两次支付密码输入一致");
            return false;
        }
        return true;
    }

    $(document).ready(function () {
        var err = request("msg");
        switch (err) {
            case "1":
                alert("设置成功.");
                break;
            case "2":
                alert("设置失败.");
                break;
            case "3":
                alert("原密码输入错误.");
                break;

        }
    })

</script>

