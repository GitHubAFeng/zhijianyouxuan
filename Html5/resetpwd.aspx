<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resetpwd.aspx.cs" Inherits="Html5.UpdatePassword" %>

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

    <style type="text/css">
        .my_order_list li .order-tit .time strong {
            min-width: 60px;
            display: inline-block;
        }
    </style>

</head>
<body>
    <div class="page">
        <div id="page_title">
            <a href="ForgotPassword.aspx" id="back" class=" top_left"></a>
            <h1>设置新密码</h1>
             <a href="login.aspx" class="reg top_right" data-ajax="false">登录</a>
        </div>

        <form runat="server" data-ajax="false">
            <div class="container">
                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>新密码</strong>
                                <input name="tbpassword" id="tbpassword" runat="server" type="password" class="w_txt" placeholder="请输入新密码" />
                            </span>
                        </div>
                        <div class="order-tit" style="border-bottom: none;">
                            <span class="time"><strong>确认密码</strong>
                                <input name="tbpassword_2" id="tbpassword_2" runat="server" type="password" class="w_txt" placeholder="请输入确认密码" />
                            </span>
                        </div>
                    </li>
                </ul>

                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>
                <div class="view_back_con">
                    <input type="submit" value="确定" onclick="return checkuser()" class="view_back_btn" data-ajax="false" />
                </div>

            </div>
        </form>
    </div>
</body>
</html>

<script type="text/javascript">

    //检查用户名称
    function checkuser() {
        var tbpassword = document.getElementById("tbpassword").value;

        if ($.trim(tbpassword) == "") {
            alert("请输入新密码!");
            return false;
        }

        var tbpassword_2 = document.getElementById("tbpassword_2").value;
        if ($.trim(tbpassword_2) == "") {
            alert("请输入确认密码!");
            return false;
        }

        if (tbpassword != tbpassword_2) {
            alert("两次密码不一致！");
            return false;
        }

        return true;
    }

</script>
