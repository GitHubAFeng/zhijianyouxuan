<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Html5.login" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2)%></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1" />

    <script src="javascript/jquery.js"></script>

</head>
<body>
    <div class="page">
        <div id="page_title">
            <%--<a href="#" id="back" class=" top_left"></a>--%>
            <h1>会员登录</h1>
            <a href="register.aspx" class="reg top_right" data-ajax="false">注册</a>
        </div>
        <form method="post" action="login.aspx?returnurl=<%= Request["returnurl"]%>" data-ajax="false">
            <div class="container">
                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>账号</strong>
                                <input name="tbuserName" type="text" runat="server" id="tbuserName" class="w_txt" placeholder="请输入会员名或手机号" />
                            </span>
                        </div>
                        <div class="order-tit" style="border-bottom:none;">
                            <span class="time"><strong>密码</strong>
                                <input name="tbpassword" type="password" id="tbpassword" class="w_txt" placeholder="请输入登录密码" />
                            </span>
                        </div>
                    </li>
                </ul>
                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>
                <div class="view_back_con">
                    <input type="submit" value="登录" onclick="return checkuser()" class="view_back_btn" data-ajax="false" />
                </div>

                <div style="padding:15px;">
                    <a style="float:right;font-size:14px;" href="ForgotPassword.aspx">忘记密码？</a>
                </div>
            </div>
        </form>
    </div>
</body>
</html>

<script type="text/javascript">

    //检查用户名称
    function checkuser() {
        var name = document.getElementById("tbuserName").value;
        var password = document.getElementById("tbpassword").value;
        if (name == "" || name == "请输入会员名或手机号") {
            alert("请输入会员名或手机号!");
            return false;
        }
        if (password == "" || password == "请输入登录密码") {
            alert("请输入登录密码!");
            return false;
        }
        return true;
    }

</script>

