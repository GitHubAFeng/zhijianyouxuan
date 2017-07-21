<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterByEmail.aspx.cs"
    Inherits="RegisterByEmail" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="Banner" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>注册 - <%= SectionProxyData.GetSetValue(3) %></title>
    <meta name="Keywords" content="<%= WebUtility.GetKeywords() %>" />
    <meta name="Description" content="<%= WebUtility.GetDescription() %>" />

    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link href="css/Regitser.css" rel="stylesheet" />

    <script src="javascript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="javascript/jCommon.js" type="text/javascript"></script>

    <script src="javascript/Register.js?v=20151120" type="text/javascript"></script>

    <script src="javascript/DatePicker/WdatePicker.js" type="text/javascript"></script>

</head>


<body>

        <input id="isofficial" type="hidden" value="<%= SectionProxyData.GetSetValue(39) %>" />


    <form id="form1" runat="server">
        <!--公共部分-->
        <uc3:Banner ID="Banner2" runat="server" />
        <uc1:Banner ID="Banner1" runat="server" />
        <!--主体部分-->
        <input type="hidden" id="hfsection" value="60" />
        <div class="wrap margin_b10">
            <div class="hplace_bg">
                <span class="hplace_right"></span>
                <span class="hplace_house" id="dangqian" runat="server"><a href="index.aspx">首页</a>
                    >> 用户注册 </span>
            </div>

            <div class="register_info">
                <div class="register_bg">
                    <ul>
                        <li class="reg_01_hover">1.填写会员信息</li>
                        <li>2.注册成功</li>
                    </ul>
                </div>

                <div class="reg_infor_bg">
                    <p style="background-color: #fff0d2; width: 610px; height: 50px; line-height: 50px; padding-left: 30px; color: #000; font-size: 14px; margin-bottom: 10px;"
                        id="div_appfriend"
                        runat="server">
                        您的好友，<label runat="server" id="lbusername" style="color: Red;"></label>推荐您注册成为会员
                    </p>

                    <p>
                        <span>手&nbsp;&nbsp;机&nbsp;&nbsp;号</span><input name="" type="text" class="text_style" runat="server" id="tbphone" onfocus="phone_focus()"
                            onblur="phone_check();" /><span class="text_heddin_style" id="phonemsg">请输入您常用的手机号(此号码注册完成后无法修改，请认真填写)</span>
                    </p>

                    <p runat="server" id="p_gsmvalied">
                        <span>验&nbsp;&nbsp;证&nbsp;&nbsp;码</span><input name="" type="text" class="text_style" runat="server" id="tbphonevalid" style="width: 80px" />
                        <input type="button" value="发送短信验证码" id="btsentcode" onclick="sentcode();" runat="server" class="code_style" />
                    </p>

                    <p>
                        <span>设置密码</span><input name="" type="password" class="text_style" id="TBpassword" onfocus="pwd_focus();"
                            onblur="pwd_check();" runat="server" /><span class="text_heddin_style" id="msgPwd">6-16位字符(数字、英文)</span>
                    </p>
                    <p>
                        <span>确认密码</span><input name="" type="password" class="text_style" onfocus="again_focus();" onblur="again_check();"
                            runat="server" id="tbAgain" /><span class="text_heddin_style" id="msgAgain">请再次输入密码</span>
                    </p>

                    <p style="display: none;">
                        <input name="" type="checkbox" value="" id="Jchagree" style="vertical-align: middle; margin-bottom: 2px;" checked="checked" />
                        我已经阅读并接受<span class="text_heddin_style" id="checkboxmsg" style="display: none">不接受服务协议不能注册</span>
                        <a href="Help/index.aspx?id=10" target="_blank">
                            <%= SectionProxyData.GetSetValue(2) %>服务条款</a>
                    </p>

                    <p>
                        <asp:Button runat="server" Text="提 交" ID="btsubmit" OnClientClick="return checkfrom();"
                            OnClick="BYEmailLogin_Click" CssClass="submit_button" />
                    </p>
                </div>
            </div>
        </div>
        <foot:foot ID="foot" runat="server" />
    </form>
</body>
</html>


<script type="text/javascript">

    function sentcode() {
        if (checkMobile($("#tbphone").val())) {
            var input = document.getElementById('btsentcode');
            input.setAttribute('disabled', 'disabled');
            document.getElementById('btsentcode').value = '正在发送验证码';

            jQuery.ajax(
                {
                    type: "post",
                    url: "Ajax/SendGmsCode.aspx",
                    data: "phone=" + $("#tbphone").val() + "&fuc=auth&t=" + new Date().getTime(),
                    success: function (msg) {
                        switch (msg) {
                            case "0":
                                alert('服务器繁忙，请稍后再试！');
                                document.getElementById('btsentcode').value = '重新发送';
                                $("#btsentcode").removeAttr("disabled");
                                break;
                            case "1":
                                document.getElementById('btsentcode').value = '发送成功';
                                $("#hfsection").val(60);
                                delayURL();

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
                                alert('此手机号已经注册过了，不能获取验证码！');
                                $("#btsentcode").removeAttr("disabled");
                                document.getElementById('btsentcode').value = '重新发送';
                                break;
                        }
                    }
                })
        }
    }

    function delayURL() {
        var delay = $("#hfsection").val();
        if (delay > 0) {
            delay--;
            $("#hfsection").val(delay);
            $("#btsentcode").val(delay + "秒后可重发");
            handlecookie("msg_hastime", delay, { expires: 1, path: "/", secure: false });
        }
        else {
            //2分后可以点击按钮
            handlecookie("msg_hastime", "", { expires: 1, path: "/", secure: false });
            $("#btsentcode").removeAttr("disabled");
            $("#hfsection").val(0);
            document.getElementById('btsentcode').value = '重新发送';
        }
        setTimeout("delayURL()", 1000);
    }

    function checkMobile(mobile) {
        var reg0 = /^1\d{10}$/;   //130--139。至少11位
        var my = false;
        if (reg0.test(mobile)) my = true;
        if (!my) {
            alert('对不起，您输入的手机号码错误！')
            return false;
        } else {
            return true;
        }
    }


</script>

