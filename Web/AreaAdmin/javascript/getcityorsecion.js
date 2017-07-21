
/******************************************** 升级版  ******************************************/
//获取城市或者区域
//flag =1表示全部,sectionid未用,ch首字母,key关键字,sort表示父编号.主要是用来选择区域（根据城市获取）,uc表示级数（省是1,城市是2,区域是3）
function getcityorsecion(flag, bysection, ch, key, page, sort, uc) {
    //选择样式
    $(".sbuild").removeClass("hover");
    var url = "/Admin/ajax/getcitytable.aspx";
    var objpath = window.location + "";
    if (objpath.toLowerCase().indexOf("localhost") > 0) {
        url = "/web/Admin/ajax/getcitytable.aspx";
    }
    var para = "fuc=GetCityOrSection&page=1";
    if (typeof flag != 'undefined') {
        para += "&flag=" + flag;
    }
    if (typeof bysection != 'undefined' && bysection != -1) {
        para += "&s=" + bysection;
    }
    if (typeof ch != 'undefined') {
        para += "&c=" + ch;
    }
    if (typeof key != 'undefined') {
        para += "&key=" + escape(key);
    }
    if (typeof sort != 'undefined') {
        para += "&sort=" + sort;
    }
    if (typeof uc != 'undefined') {
        para += "&uc=" + uc;
    }
    else {
        para += "&uc=" + 0;
    }
    setBuilddiv_city(url, para);
}
//关键字
function getkeyFix_city(type) {
    var key = escape($("#bcitykey").val() + "");

    getcityorsecion(0, -1, -1, key, 1, -1, 2);
}

//字母
function GetByCFix_city(ch) {
    var sid = $("#curSection").val();
    if (sid == 0 || sid == undefined) {
        sid = -1;
    }
    getcityorsecion(0, sid, ch, -1, 1, -1, 2);
}


//搜索
function showpageFix_city(pages, flag, section, ch, key, sort, pagecount, uc) {
    var url = "/Admin/ajax/getcitytable.aspx";
    var objpath = window.location + "";
    if (objpath.toLowerCase().indexOf("localhost") > 0) {
        url = "/web/Admin/ajax/getcitytable.aspx";
    }
    var para = "fuc=GetCityOrSection&page="+pages;
    para += "&flag=" + flag;
    para += "&s=" + section;
    para += "&c=" + ch;
    para += "&key=" + escape(key);
    para += "&sort=" + sort;
    para += "&uc=" + uc;
    setBuilddiv_city(url, para);
}

function preFix_city(flag, section, ch, key, sort, pagecount, uc) {
    var oldpage = parseInt(jQuery("#myfocus").html());
    if (oldpage == 1) {
        return;
    }
    else {
        var newpage = parseInt(oldpage - 1) + "";
        showpageFix_city(newpage, flag, section, ch, key, sort, pagecount, uc);
    }
}

function nextFix_city(flag, section, ch, key, sort, pagecount, uc) {
    var oldpage = parseInt(jQuery("#myfocus").html());
    var t = parseInt(pagecount);
    if (oldpage >= t) {
        return;
    }
    else {
        var newpage = parseInt(oldpage + 1) + "";
        showpageFix_city(newpage, flag, section, ch, key, sort, pagecount, uc);
    }
}

function firstFix_city(flag, section, ch, key, sort, pagecount, uc) {
    showpageFix_city(1, flag, section, ch, key, sort, pagecount, uc);
}

function lastFix_city(flag, section, ch, key, sort, pagecount, uc) {
    showpageFix_city(parseInt(pagecount), flag, section, ch, key, sort, pagecount, uc);
}
function setBuilddiv_city(url, para) {
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {

            $("#div_citylist").html(msg);
        }
    })
}

function closediv() {
    $("#tabpopup1").remove();
    $("#windownbg").remove();
    $("#build-box").remove();
}
