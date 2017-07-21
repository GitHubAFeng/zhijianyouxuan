<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Html5.ForgotPassword" %>

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
            <a href="login.aspx" id="back" class=" top_left"></a>
            <h1>找回密码</h1>
        </div>

        <input id="isofficial" type="hidden" value="<%= SectionProxyData.GetSetValue(39) %>" />

        <form method="post" action="ForgotPassword.aspx" data-ajax="false">
            <input type="hidden" id="CountZero" value="0" runat="server" />
            <div class="container">
                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>手机号</strong>
                                <input name="tbmobile" type="text" id="tbmobile" class="w_txt" placeholder="请输入手机号" />
                            </span>
                        </div>
                        <div class="order-tit" style="border-bottom:none;">
                            <span class="time"><strong>验证码</strong>

                                  <input name="tbphonevalid" type="text" id="tbphonevalid" style="width:100px"  class="w_txt" placeholder="请输入验证码">

                                <input type="button" value="发送验证码" data-theme="green" id="btsentcode" onclick="sentcode();"
                            data-ajax="false" />
                               
                            </span>
                        </div>
                    </li>
                </ul>

                <div id="divError" runat="server" class="error_list" style="color: #FF6000; margin-left: 15px;"></div>
                <div class="view_back_con" runat="server">
                    <input id="submit_btn" type="submit" value="确定" onclick="return checkuserregin()" class="view_back_btn" data-ajax="false" />
                </div>

            </div>
        </form>

    </div>
</body>
</html>

<script src="javascript/spin.min.js"></script>
<script src="javascript/jCommon.js"></script>

<script type="text/javascript">

    function checkuserregin() {
     
        var phone = document.getElementById("tbmobile").value;
        if (phone.length == 0) {
            alert("手机号码不能为空！");
            return false;
        }
        var patrn = /^1\d{10}$/;;
        if (!patrn.test(phone)) {
            alert("手机(电话)号码格式错误。");
            return false;
        }


        var tbphonevalid = document.getElementById("tbphonevalid").value;
        if (tbphonevalid.length == 0) {
            alert("请输入验证码！");
            return false;
        }


        return true;
    }
   

    var mytimeout = null;

    function sentcode() {

        var phone = document.getElementById("tbmobile").value;;

        if (checkMobile(phone)) {
            var input = document.getElementById('btsentcode');
            input.setAttribute('disabled', 'disabled');
            document.getElementById('btsentcode').value = '正在发送';

           
            Loader.show("#btsentcode");

            jQuery.ajax(
                {
                    type: "get",
                    url: "Ajax/SendGmsCode.aspx",
                    data: "phone=" + phone + "&fuc=checkphone&t=" + new Date().getTime(),
                    success: function (msg) {

                        switch (msg) {
                            case "0":
                                alert('服务器繁忙，请联系客服');
                                document.getElementById('btsentcode').value = '重新发送';
                                $("#btsentcode").removeAttr("disabled");
                                break;
                            case "1":
                                document.getElementById('btsentcode').value = '发送成功';

                                var isofficial = $("#isofficial").val();
                                if (isofficial == "0") {
                                    $("#tbphonevalid").val(handlecookie("gsmcode"));
                                }



                                break;
                            case "-2":
                                alert('此手机号已经注册过了，请重新输入');
                                $("#btsentcode").removeAttr("disabled");
                                document.getElementById('btsentcode').value = '重新发送';
                                break;
                            case "-3":
                                alert('此手机号不存在，请重新输入');
                                $("#btsentcode").removeAttr("disabled");
                                break;
                        }
                        Loader.hide();
                    }
                })

        }
    }

    function checkMobile(mobile) {
        var reg0 = /^1\d{10}$/;   //130--139。至少7位
        var my = false;
        if (reg0.test(mobile)) my = true;
        if (!my) {
            alert('对不起，您输入的手机号码错误。')
            return false;
        } else {
            return true;
        }
    }

    $(document).ready(function () {

        var err = request("tel");
        if (err != "") {
            $("#tbmobile").val(err);
        }

        var err = request("tip");
        if (err == "1") {
            alert("验证码错误，请重新输入.");
            return false;
        }
        if (err == "2") {
            alert("手机号码不存在，请重新输入。");
            return false;
        }


    })

   

</script>

