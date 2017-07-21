<%@ Page Language="C#" AutoEventWireup="true" CodeFile="a.aspx.cs" Inherits="appdownload" %>

<!DOCTYPE html >
<html xml:lang="en" lang="en">
<head lang="en">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimum-scale=1.0, maximum-scale=1.0" />
    <meta content="email=no" name="format-detection" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />

    <link href="/images/appdownload/css.css" rel="stylesheet" />


    <title>用户版下载 - <%= SectionProxyData.GetSetValue(3)%></title>
</head>

<body>
    <div id="content">

        <input type="hidden" id="androidurl" value="<%= SectionProxyData.GetSetValue(40) %>" />
        <input type="hidden" id="iphoneurl" value="<%= SectionProxyData.GetSetValue(41) %>" />

        <div class="tip_wrap">
            <img id="ios_tips" src="/images/appdownload/weixin_ios1122.jpg" class="wxtip tip_ios" style="display: none" />
            <img id="android_tips" src="/images/appdownload/weixin_android1122.jpg" class="wxtip tip_android" style="display: none" />

            <div id="weibo_tips_ios" name="weibo_tips_ios" class="weibo_tips_ios" style="display: none; text-align: center; padding-top: 20px;">
                <span style="color: black; font-size: 17px;">请点击右上角,用Safari打开,下载应用</span></div>
            <div id="weibo_tips_android" class="weibo_tips_android" style="display: none; padding-top: 20px; text-align: center;">
                <span style="color: black; font-size: 17px;">请点击右上角,在浏览器中打开,下载应用</span></div>

        </div>
    </div>



</body>
</html>

<script src="javascript/jquery-1.8.2.js"></script>
<script src="javascript/jCommon.js"></script>

<script type="text/javascript">

    //获取终端相关信息
    var Terminal = {
        // 辨别移动终端类型
        platform: function () {
            var u = navigator.userAgent, app = navigator.appVersion;
            return {
                // android终端或者uc浏览器
                android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1,
                // 是否为iPhone或者QQHD浏览器
                iPhone: u.indexOf('iPhone') > -1
            };
        }(),

        // 辨别移动终端的语言：zh-cn、en-us、ko-kr、ja-jp...
        language: (navigator.browserLanguage || navigator.language).toLowerCase()
    }


    function isWeiXin() {
        var ua = window.navigator.userAgent.toLowerCase();
        if (ua.match(/MicroMessenger/i) == 'micromessenger') {
            return true;
        } else {
            return false;
        }
    }


    $(document).ready(function () {
        var android = request("android");
        if (android.length > 0) {
            $("#androidurl").val("http://" + window.location.host + "/app/" + app);
        }

        var ios = request("ios");
        if (ios.length > 0) {
            $("#iphoneurl").val(ios);
        }

        // 根据不同的终端，跳转到不同的地址
        var theUrl = '';
        if (Terminal.platform.android) {    //安卓版
            theUrl = $("#androidurl").val();
            $("#android_tips").show();
        }
        else if (Terminal.platform.iPhone) {//iPhone版
            theUrl = $("#iphoneurl").val();
            $("#ios_tips").show();
        }
        if (theUrl != "") {

            var ua = navigator.userAgent.toLowerCase();
            if (Terminal.platform.android && (ua.match(/MicroMessenger/i) == "micromessenger")) {    //安卓版+微信
                //安卓版+微信不跳转
            }
            else {
                location.href = theUrl;
            }
        }
    })

</script>
