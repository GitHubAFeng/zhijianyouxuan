<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Html5.registerdetail" %>

<%@ Register Src="~/baseControl.ascx" TagPrefix="uc1" TagName="baseControl" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=2016073052" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=2016073052" />
    <link href="css/sweetalert.css" rel="stylesheet" />


    <link href="css/swiper.min.css" rel="stylesheet" />
    <script src="javascript/jquery.js"></script>
</head>


<body>
    <div class="page">
        <div id="page_title">
            <a href="#" data-ajax="false" class=" top_left" runat="server" id="gocart"></a>
            <h1>会员注册</h1>
            <a href="login.aspx" class="reg top_right" data-ajax="false">登录</a>
        </div>

        <input id="isofficial" type="hidden" value="<%= SectionProxyData.GetSetValue(39) %>" />

        <div class="container">
            <form method="post" action="/ajaxHandler.ashx" id="addform">

                <input type="hidden" id="method" name="method" value="userreg" />

                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>手机号</strong>
                                <input name="tbmobile" type="text" id="tbmobile" class="w_txt" placeholder="请输入手机号" runat="server" />
                            </span>
                        </div>

                        <div class="order-tit" id="div_gsmCode" runat="server">
                            <span class="time"><strong>验证码</strong>
                                <input type="text" style="width: 120px;" name="tbGsmCode" id="tbGsmCode" class="w_txt" placeholder="请输入验证码" />
                                <input type="button" value="获取验证码" id="btsentcode" onclick="sentcode();" class="code_style" style="padding: 4px 2px;" />
                            </span>
                        </div>

                        <div class="order-tit">
                            <span class="time"><strong>设置密码</strong>
                                <input name="TBpassword" type="password" id="tbpassword" class="w_txt" placeholder="请输入密码" />
                            </span>
                        </div>
                        <div class="order-tit" style="border-bottom: none;">
                            <span class="time"><strong>确认密码</strong>
                                <input name="TBpasswordagain" type="password" id="tbpasswordagain" class="w_txt" placeholder="请输入确认密码" />
                            </span>
                        </div>
                    </li>
                </ul>

                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>
                <div class="view_back_con" id="div1" runat="server">
                    <input type="submit" value="提交" class="view_back_btn" onclick="return checkuserregin()" data-ajax="false" />
                </div>
            </form>
        </div>

    </div>

    <uc1:baseControl runat="server" ID="baseControl" />
</body>
</html>

<script src="javascript/jquery.form.js"></script>
<script src="javascript/CommonJs.js"></script>
<script src="javascript/jCommon.js?v=2016073043"></script>

<script src="javascript/sweetalert.min.js"></script>

<script type="text/javascript">

    //验证是否输入
    function checkuserregin() {
        var phone = document.getElementById("tbmobile").value;
        if (phone == "" || phone == null || phone == "请输入手机号") {
            alert("请输入手机号！");
            return false;
        }
        else if (!CheckPhone(phone)) {
            alert("手机号码格式错误！");
            return false;
        }
        var pwd = document.getElementById("tbpassword").value;
        var pwdagain = document.getElementById("tbpasswordagain").value;
        if (pwd == "" || pwdagain == "请输入密码" || pwdagain == "" || pwdagain == "请输入确认密码") {
            alert("密码不能为空！");
            return false;
        }

        if ($.trim(pwd) != $.trim(pwdagain)) {
            alert("两次输入的密码不一致！");
            return false;
        }

        WeUI.showLoading();

        var options = {
            success: function (data) {

                WeUI.hideLoadingslowly();

                var json = eval("(" + data + ")");

                if (json.state == "1") {
                    swal({
                        title: "温馨提示",
                        text: json.msg,
                        type: "success"
                    },
                            function () {
                                window.location = "login.aspx?name=" + phone;
                            }

                    );
                }
                else {
                    swal({
                        title: "温馨提示",
                        text: json.msg,
                        type: "warning"
                    });
                }
            }
        };




        // ajaxForm
        $("#addform").ajaxForm(options);


        return true;
    }


    //发送验证码
    var section = 60;//倒计时秒数
    function sentcode() {
        var phone = document.getElementById("tbmobile").value;
        if (phone == "" || phone == null || phone == "请输入手机号") {
            alert("请输入手机号！");
            return false;
        }
        else if (!CheckPhone(phone)) {
            alert("手机号码格式错误！");
            return false;
        }

        var input = document.getElementById('btsentcode');
        input.setAttribute('disabled', 'disabled');
        document.getElementById('btsentcode').value = '正在发送验证码';

        jQuery.ajax(
            {
                type: "post",
                url: "Ajax/SendGmsCode.aspx",
                data: "phone=" + phone + "&fuc=auth&t=" + new Date().getTime(),
                success: function (msg) {
                    switch (msg) {
                        case "0":
                            alert('服务器繁忙，请稍后再试！');
                            document.getElementById('btsentcode').value = '重新发送';
                            $("#btsentcode").removeAttr("disabled");
                            break;
                        case "1":
                            document.getElementById('btsentcode').value = '发送成功';
                            section = 60;
                            delayURL();

                            var isofficial = $("#isofficial").val();
                            if (isofficial == "0") {
                                $("#tbGsmCode").val(handlecookie("gsmcode"));
                            }

                            break;
                        case "-2":
                            alert('此手机号已经注册过了，请重新输入');
                            $("#btsentcode").removeAttr("disabled");
                            document.getElementById('btsentcode').value = '重新发送';
                            break;
                        case "-3":
                            alert('此手机号已经注册过了，不能获取验证码！');
                            $("#btsentcode").removeAttr("disabled");
                            document.getElementById('btsentcode').value = '重新发送';
                            break;
                    }
                }
            })
    }


    //倒计时
    function delayURL() {
        var delay = section;
        if (delay > 0) {
            delay--;
            section = delay;
            $("#btsentcode").val(delay + "秒后可重发");
        }
        else {
            $("#btsentcode").removeAttr("disabled");
            section = 0;
            document.getElementById('btsentcode').value = '重新发送';
        }
        setTimeout("delayURL()", 1000);
    }


</script>

