<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mobile_phone.aspx.cs" Inherits="tloginmobile_phone" %>

<%@ Register Src="Banner.ascx" TagName="Banner" TagPrefix="uc3" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>手机版-<%= SectionProxyData.GetSetValue(3)%>
    </title>
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="css/mobile_phone.css" />

    <script type="text/javascript" src="javascript/jquery-1.8.2.js"></script>
    <style type="text/css">
        #nav {
            margin-bottom: 0;
        }
    </style>
</head>
<body>

    <input type="hidden" id="androidurl" value="<%= SectionProxyData.GetSetValue(40) %>" />
    <input type="hidden" id="iphoneurl" value="<%= SectionProxyData.GetSetValue(41) %>" />

    <!--top start-->
    <uc3:Banner ID="Banner2" runat="server" />
    <uc1:header ID="header" runat="server" />
    <!-- top end-->
    <div class="appwarp">
        <img class="app_bg" src="images/APP_download01.png" />
        <img class="app_bg" src="images/APP_download02.png" />
        <div class="app_qrcode">
            <div class="app_cpm_logo">
            </div>
            <ul>
                <a href="<%= SectionProxyData.GetSetValue(41) %>">
                    <li class="iphone">iphone免费下载</li>
                </a>
                <a href="<%= SectionProxyData.GetSetValue(40) %>">
                    <li class="android">android免费下载</li>
                </a>
            </ul>
            <span>
                <img class="" src="images/app_img.png" /></span>
        </div>
    </div>
    <div class="wrap" style="display: none">

        <div class="app_down_content">
            <div class="down_app_bg">
                <div class="app-dlg-win">
                    <div class="app-down-evt">
                        <p>手机APP下载</p>
                        <div class="app-down-btn" id="app-down-btn"></div>
                        <div class="ye-option-link">
                            <a href="javascript:void(0)" id="app_android">Android</a><a href="javascript:void(0)" class="mid" id="app_iphone">iPhone</a><a href="javascript:void(0)" id="app_weixin">微信版</a>
                        </div>
                    </div>
                </div>

            </div>

            <div class="app-bom-ad">
                <ul class="clearfix">
                    <li>
                        <img src="images/app_bottom_01.png" />
                        <p>手机上网，随时随地订餐更方便</p>
                    </li>
                    <li>
                        <img src="images/app_bottom_02.png" />
                        <p>最快享受各种优惠，各种活动</p>
                    </li>
                    <li>
                        <img src="images/app_bottom_03.png" />
                        <p>足不出户，动动手指即可享受美食</p>
                    </li>
                    <li>
                        <img src="images/app_bottom_04.png" />
                        <p>美食随意选，范围更广，选择更多</p>
                    </li>
                </ul>
            </div>
        </div>
        <div id="J_dialog" class="ks-overlay dlg-android" style="z-index: 29999; position: fixed; width: 800px; display: none; left: 50%; height: 546px; overflow: hidden; padding: 20px; margin-left: -400px; top: 34px;">
            <div class="ks-dialog ks-ext-position ks-dialog-shown ks-overlay-shown ks-dialog-focused ks-overlay-focused ">
                <a id="close" class="ks-ext-close" style="z-index: 9" role="button" href="javascript:void('关闭')" tabindex="0">
                    <span class="ks-ext-close-x" style="top: 7px; right: 7px">关闭</span>
                </a>
                <div class="ks-contentbox">

                    <div id="ks-dialog-header714" class="ks-stdmod-header"></div>

                    <div class="ks-stdmod-body">

                        <div class="ks-stdmod-body-brand clearfix">
                            <span class="brand-android J_Md " dest="android" data-md="http://log.mmstat.com/wap.11.22">
                                <img src="images/version_android.png">
                            </span>
                            <span class="brand-iphone J_Md " dest="iphone" data-md="http://log.mmstat.com/wap.11.23">
                                <img src="images/version_iphone.png">
                            </span>
                            <span class="brand-weixin J_Md " dest="ipad" data-md="http://log.mmstat.com/wap.11.24">
                                <img src="images/version_wx.png">
                            </span>

                        </div>

                        <div class="ks-stdmod-body-download">
                            <div class="ks-download">

                                <div id="dlandroid" class="ks-dl-cont ks-dl-cont-android">
                                    <div class="ks-download-left">
                                        <span class="t J_Md" data-md="http://log.mmstat.com/wap.11.1">直接下载</span>
                                        <div class="m clearfix">
                                            <div class="app-step-num-one"></div>
                                            <div class="app-step-btn">
                                                <a class="J_Md" href="<%= SectionProxyData.GetSetValue(40) %>">
                                                    <img src="images/T1be1zFe0fXXaA8EDe-161-48.png" /></a>
                                            </div>
                                            <div class="app-step-num-two"></div>
                                        </div>
                                        <div style="display: none">
                                            <hr>
                                            <span class="f">短信发送:
                  <em></em>
                                            </span>
                                            <span class="s">
                                                <input class="st" type="text" topic="1025d11" placeholder="输入手机号，免费发送下载地址到手机">
                                                <input class=" J_Md" type="button" data-md="http://log.mmstat.com/wap.11.12" value="发送">
                                            </span>
                                        </div>
                                    </div>
                                    <div class="ks-download-right">
                                        <div class="qrcode">
                                            <img src="images/app_img.jpg">
                                        </div>
                                        <div class="qrtxt">手机扫描二维码下载 </div>
                                    </div>
                                </div>


                                <div id="dliphone" class="ks-dl-cont ks-dl-cont-iphone">
                                    <div class="ks-download-left">
                                        <span class="t J_Md" data-md="http://log.mmstat.com/wap.11.1">直接下载</span>
                                        <div class="m clearfix">
                                            <div class="app-step-num-one"></div>
                                            <div class="app-step-btn">
                                                <a class="J_Md" href="<%= SectionProxyData.GetSetValue(41) %>">
                                                    <img src="images/T1ZCUlFaheXXarFq6X-135-40.png">
                                                </a>
                                            </div>
                                            <div class="app-step-num-two"></div>


                                        </div>
                                        <div style="display: none">
                                            <hr>
                                            <span class="f">短信发送:
                  <em></em>
                                            </span>
                                            <span class="s">
                                                <input class="st" type="text" topic="1025d11" placeholder="输入手机号，免费发送下载地址到手机">
                                                <input class=" J_Md" type="button" data-md="http://log.mmstat.com/wap.11.15" value="发送">
                                            </span>
                                        </div>
                                    </div>

                                    <div class="ks-download-right">
                                        <div class="qrcode">
                                            <img src="images/app_img.jpg">
                                        </div>
                                        <div class="qrtxt">手机扫描二维码下载 </div>
                                    </div>
                                </div>


                                <div id="dliweixin" class="ks-dl-cont ks-dl-cont-weixin">
                                    <div class="ks-download-left">
                                        <span class="t J_Md" data-md="http://log.mmstat.com/wap.11.1">直接搜索</span>
                                        <div class="m clearfix">
                                            <div class="app-step-num-one"></div>
                                            <div class="app-step-btn al-tc">
                                                <label class="public-number">公众号：<%= SectionProxyData.GetSetValue(13) %></label>
                                            </div>
                                            <div class="app-step-num-two"></div>
                                        </div>
                                    </div>
                                    <div class="ks-download-right">
                                        <div class="qrcode">
                                            <img src="images/wx_img.jpg">
                                        </div>
                                        <div class="qrtxt">手机扫描二维码下载 </div>
                                    </div>
                                </div>

                            </div>



                        </div>
                    </div>
                    <div class="ks-stdmod-footer"></div>
                </div>

                <div style="position: absolute;" tabindex="0"></div>
            </div>
        </div>
        <div id="J_bg" class="ks-ext-mask ks-dialog-mask ks-overlay-mask ks-dialog-mask-shown ks-overlay-mask-shown" style="width: 100%; left: 0px; top: 0px; height: 100%; position: fixed; z-index: 19999; display: none;"></div>
    </div>
    <foot:foot ID="Foot1" runat="server" />

</body>
</html>
<script type="text/javascript">
    $(".brand-android").click(function () {
        $("#J_dialog").addClass("dlg-android");
        $("#J_dialog").removeClass("dlg-iphone");
        $("#J_dialog").removeClass("dlg-weixin");
    }

        );
    $(".brand-iphone").click(function () {

        $("#J_dialog").addClass("dlg-iphone");
        $("#J_dialog").removeClass("dlg-android");
        $("#J_dialog").removeClass("dlg-weixin");
    }

        );
    $(".brand-weixin").click(function () {
        $("#J_dialog").addClass("dlg-weixin");
        $("#J_dialog").removeClass("dlg-android");
        $("#J_dialog").removeClass("dlg-iphone");
    }

        );
    $("#close").click(function () {
        $("#J_bg").hide();
        $("#J_dialog").hide();
    }

        );
    $("#app-down-btn").click(function () {

        $("#J_dialog").show();
        $("#J_dialog").addClass("dlg-android");
        $("#J_dialog").removeClass("dlg-iphone");
        $("#J_dialog").removeClass("dlg-weixin");
        $("#J_bg").show();
    }

        );
    $("#app_android").click(function () {

        $("#J_dialog").show();
        $("#J_dialog").addClass("dlg-android");
        $("#J_dialog").removeClass("dlg-iphone");
        $("#J_dialog").removeClass("dlg-weixin");
        $("#J_bg").show();
    }

        );
    $("#app_iphone").click(function () {


        $("#J_dialog").show();
        $("#J_dialog").addClass("dlg-iphone");
        $("#J_dialog").removeClass("dlg-android");
        $("#J_dialog").removeClass("dlg-weixin");
        $("#J_bg").show();
    }

        );
    $("#app_weixin").click(function () {

        $("#J_dialog").show();
        $("#J_dialog").addClass("dlg-weixin");
        $("#J_dialog").removeClass("dlg-android");
        $("#J_dialog").removeClass("dlg-iphone");
        $("#J_bg").show();
    }

        );

</script>


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

    // 根据不同的终端，跳转到不同的地址
    var theUrl = '';
    if (Terminal.platform.android) {    //安卓版
        theUrl = $("#androidurl").val();
    }
    else if (Terminal.platform.iPhone) {//iPhone版
        theUrl = $("#iphoneurl").val();
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

</script>
