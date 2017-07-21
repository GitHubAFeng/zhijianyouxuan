function setsort() {
    var sort = $("#hfsort").val() + "";
    $(".sort").removeClass("current");
    $("#sort_" + sort).addClass("current");

    var price = $("#hfprice").val() + "";
    $(".price").removeClass("current");
    $("#price_" + price).addClass("current");
    var send = $("#hfsend").val() + "";
    $(".send").removeClass("current");
    $("#send_" + send).addClass("current");
    var temp = ($("#hfcurent").val() + "").split(",");
    if (document.getElementById("list_li_" + temp[0]))
    {
        document.getElementById("list_li_" + temp[0]).className = temp[1]
    }
} var m_object; function addfood(obj, foodid, bid) { m_object = obj; var togoid = document.getElementById("hidTogoId").value; jQuery.ajax({ type: "post", url: "Ajax/TogoCollect.aspx", data: "fuc=addfood&togoid=" + togoid + "&foodid=" + foodid + "&bid=" + bid, success: function (msg) { addfood_deal(msg) } }) } function addfood_deal(msg) { if (msg == "1") { cShowTip(document.getElementById(m_object), "dpop", 20, -70, "已经成功加入收藏夹") } else if (msg == "0") { cShowTip(document.getElementById(m_object), "dpop", 20, -70, "加入收藏夹失败") } else if (msg == "2") { cShowTip(document.getElementById(m_object), "dpop", 20, -70, "收藏夹中已经存在") } else if (msg == "-1") { cShowTip(document.getElementById(m_object), "dpop", 20, -70, "请登入后再试") } } function addfood_err() { alert("服务器忙...") } function addtogo(obj, togoid, bid, x, y) { m_object = obj; jQuery.ajax({ type: "post", url: "Ajax/TogoCollect.aspx", data: "fuc=addtogo&togoid=" + togoid + "&bid=" + bid, success: function (msg) { addtogo_deal(msg, x, y) } }) } function addtogo_deal(msg, x, y) { if (msg == "1") { cShowTip(document.getElementById(m_object), "dpop", x, y, "已经成功加入收藏夹") } else if (msg == "0") { cShowTip(document.getElementById(m_object), "dpop", x, y, "加入收藏夹失败") } else if (msg == "2") { cShowTip(document.getElementById(m_object), "dpop", x, y, "收藏夹中已经存在") } else if (msg == "-1") { cShowTip(document.getElementById(m_object), "dpop", x, y, "请登入后再试") } } function addtogo_err() { alert("服务器忙...") }
function cShowTip(obj, objdiv, addx, addy, msg) { if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) { document.getElementById("lbMsg").innerText = msg } else if (navigator.userAgent.indexOf('Firefox') >= 0) { document.getElementById("lbMsg").textContent = msg } document.getElementById("lbMsg").style.color = "red"; var x = getposOffset_top(obj, 'left'); var y = getposOffset_top(obj, 'top'); var div_obj = document.getElementById(objdiv); div_obj.style.left = (x + addx) + 'px'; div_obj.style.top = (y + addy) + 'px'; div_obj.style.display = "inline"; setTimeout(HideTip, 3000) } function HideTip(objdiv) { var div_obj = document.getElementById("dpop"); if (div_obj) { div_obj.style.display = "none" } } function getposOffset_top(what, offsettype) { var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop; var parentEl = what.offsetParent; while (parentEl != null) { totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop; parentEl = parentEl.offsetParent } return totaloffset }
function keyfocus() { if ($("#tbkeywork").val() == "搜索店铺和美食") { $("#tbkeywork").val(""); $("#tbkeywork").css("color", "#000") } else { $("#tbkeywork").css("color", "#999999") } }
function keyblur() { if ($("#tbkeywork").val() == "" || $("#tbkeywork").val() == "搜索店铺和美食") { $("#tbkeywork").val("搜索店铺和美食"); $("#tbkeywork").css("color", "#999999") } else { $("#tbkeywork").css("color", "#000") } }
function checksearch()
{
    if ($("#tbkeywork").val() == "" || $("#tbkeywork").val() == "搜索店铺和美食")
    {
        $("#tbkeywork").val(""); document.getElementById("tbkeywork").focus();
        return false;
    }
    return true;
}
function showtype(x, y) { ShowTip(document.getElementById("typemore"), "divmore", x, y) } function hidetype() { $("#divmore").hide() }
function showborder(target) { $(target).css("border", "1px solid #c00606") }
function hideborder(target) { $(target).css("border", "1px solid #ccc") } function ShowTip(obj, objdiv, addx, addy) { var x = getposOffset_top(obj, 'left'); var y = getposOffset_top(obj, 'top'); var div_obj = document.getElementById(objdiv); div_obj.style.left = (x + addx) + 'px'; div_obj.style.top = (y + addy) + 'px'; div_obj.style.display = "inline" } function showpic(id, x, y) { ShowTip(document.getElementById("aorder_" + id), "divintro_" + id, x, y) }
function hidepic(id) { $("#divintro_" + id).hide() }
function remarkblur() { var d = document.getElementById("tbremark").value; if (d == "送达时间、特殊口味等") { document.getElementById("tbremark").value = "" } else { document.getElementById("tbremark").style.color = "#000" } }