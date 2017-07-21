<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatepwd.aspx.cs" Inherits="Html5.updatepwd" %>

<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2)%></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />

    <script src="javascript/jquery.js"></script>

</head>
<body>
    <input type="hidden" runat="server" id="sendmsgcodestate" value="0" />
    <input id="hferrmsg" runat="server" type="hidden" />


    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" id="back" data-ajax="false" class=" top_left"></a>
            <h1>更改密码</h1>
        </div>

        <form method="post" action="updatepwd.aspx" data-ajax="false">
            <div class="container">
                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>旧密码</strong>
                                <input name="tbOldPwd" type="password" id="tbOldPwd" class="w_txt" placeholder="请输入旧密码" />
                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>新密码</strong>
                                <input name="tbNewPwd" type="password" id="tbNewPwd" class="w_txt" placeholder="请输入新密码" />
                            </span>
                        </div>
                        <div class="order-tit" style="border-bottom: none;">
                            <span class="time"><strong>确认密码</strong>
                                <input name="tbPwdagin" type="password" id="tbPwdagin" class="w_txt" placeholder="请再次输入确认密码" />
                            </span>
                        </div>
                    </li>
                </ul>
                <div id="divError" runat="server" style="color:red;"></div>
                <div class="view_back_con">
                    <input type="submit" value="确认修改" onclick="return checkuser()" class="view_back_btn" data-ajax="false" />
                </div>
            </div>
        </form>



        <uc2:Foot runat="server" ID="footer" />
    </div>
</body>
</html>

<script type="text/javascript">
    //提交订单post错误信息
    var err = $("#hferrmsg").val();
    if (err != "" && err != "undefined") {
        alert(err);
        window.location.href = 'login.aspx';
    }
    //检查用户名称
    function checkuser() {
        var name = document.getElementById("tbOldPwd").value;
        var password = document.getElementById("tbNewPwd").value;
        var passwordagain = document.getElementById("tbPwdagin").value;
        if (name == "" || name == "请输入原来密码") {
            alert("请输入原来密码!");
            return false;
        }
        if (password == "" || password == "请输入新密码") {
            alert("请输入新密码!");
            return false;
        }
        if (password != passwordagain) {
            alert("请确认新密码是否相同!");
            return false;
        }
        return true;
    }

</script>

