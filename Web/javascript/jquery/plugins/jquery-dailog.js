(function ($) {
$.extend({
removeDailog: function () {
$("#overlay_bg,#dailog").remove();
$("select[jHide]").show();
},
rePositionDailog: function () {
var marginTop = navigator.userAgent.toLowerCase().match(/msie|firefox|chrome|opera/) == "chrome" ? document.body.scrollTop : document.documentElement.scrollTop;
var win = $("#dailog:first");
win.css("top", document.documentElement.clientHeight / 2 - win.height() / 2 + marginTop + "px").css("left", document.body.clientWidth / 2 - win.width() / 2 + "px").fadeIn(400);
}
});
$.fn.extend({
Dailog: function (options) {
$.removeDailog();
var str = "<div id='overlay_bg' style='position: absolute;background-color: #000;filter: alpha(opacity=70);-moz-opacity: 0.7;opacity: 0.7;top: 0;left: 0; z-index:90;'></div>";
str += "<table id='dailog' style='position: absolute;background-color: #FFF; z-index:91;display:block; border-collapse:collapse; border:none;'>"; //Table
str += "<tr><td style='width:6px; height:29px; background:url(../Images/tbleft.gif) no-repeat;'></td><td style='height:29px;background-color:#F2F2F2; font-weight:bold; padding-left:10px;' id='title'><div style='width:14px; height:13px; background:url(../Images/close.gif) no-repeat; float:right; cursor:pointer;' onclick='javascript:$.removeDailog();'></div></td><td style='width:6px; height:29px; background:url(../Images/tbright.gif) no-repeat;'></td></tr>"; //Head
str += "<tr><td></td><td id='dailogContent' style='min-height:80px;min-width:300px;'><div style='width:300px; text-align:center;line-height:60px;'>请稍候...</div></td><td></td></tr>"; //Content
str += "<tr><td style='width:6px; height:7px; background:url(../Images/tbbottonleft.gif) no-repeat;'></td><td></td><td style='width:6px; height:7px; background:url(../Images/tbbottonright.gif) no-repeat;'></td></tr>"; //Footer
str += "</table>";
$(str).appendTo("body");
$("select").attr("jHide", "hide").hide();
var overlay = $("#overlay_bg");
var dailog = $("#dailog");
//定位背景
var height = $(document).height();
if (document.documentElement.clientHeight > height)
height = document.documentElement.clientHeight;
overlay.css("width", document.documentElement.clientWidth + 'px').css("height", height + 'px');
//填充内容，以并下面定位
if (!$.isEmptyObject(options)) {
if (options.title != undefined)
dailog.find("#title").append(options.title);
if (options.message != undefined)
dailog.find("#dailogContent").html(options.message);
else if (options.url != undefined) {
$.get(options.url, null, function (d) {
dailog.find("#dailogContent").html(d);
$.rePositionDailog();
if (options.success != undefined) {
options.success();
}
});
}
else if (options.frame != undefined) {
var frameWidth = options.width || 300;
var frameHeight = options.height || 150;
dailog.find("#dailogContent").html('<iframe onload="return ' + $.rePositionDailog() + ';" frameborder="0" scrolling="no" src="' + options.frame + '" width="' + frameWidth + 'px" height="' + frameHeight + 'px"></iframe>');
}
else
dailog.find("#dailogContent").html($(this).html());
}
else
dailog.find("#dailogContent").html($(this).html());
//定位窗体
$.rePositionDailog();
window.onscroll = function () {
$.rePositionDailog();
}
//Esc 退出
$(document).keyup(function (e) { if (e.keyCode == 27) $.removeDailog(); });
}
});
})(jQuery);