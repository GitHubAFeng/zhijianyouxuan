
/******************************************** 升级版  ******************************************/
//通过区域获取.
function getbuild(flag, bysection, ch, key, page, sort, uc) {
 
    //选择样式
    $(".sbuild").removeClass("hover");
    var url = "../ajax/TogoBuilding.aspx";
    var para = "fuc=getbuild&page=1";
    if (typeof flag != 'undefined') {
        para += "&flag=" + flag;
    }
    if (typeof bysection != 'undefined' && bysection != -1) {
        para += "&s=" + bysection;
        document.getElementById("one" + bysection).className = "sbuild hover";
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
    setBuilddiv(url, para);
}
//关键字
function getkeyFix(type) {
    var key = $("#bkey").val().trim() + "";
    getbuild(0, -1, -1, key, 1, -1, type);
}

//字母
function GetByCFix(ch) {
    getbuild(0, -1, ch, -1, 1, -1, 1);
}

//字母->用户中心
function GetByCFixUC(ch) {
    getbuild(0, -1, ch, -1, 1, -1, 0);
}

//搜索
function showpageFix(pages, flag, section, ch, key, sort, pagecount, uc) {
    var url = "../ajax/TogoBuilding.aspx";
    var para = "fuc=getbuild&page=" + pages;
    if (section != -1) {
        document.getElementById("curSection").value = section;
    }
    para += "&flag=" + flag;
    para += "&s=" + section;
    para += "&c=" + ch;
    para += "&key=" + escape(key);
    para += "&sort=" + sort;
    para += "&uc=" + uc;
    setBuilddiv(url, para);
}

function preFix(flag, section, ch, key, sort, pagecount, uc) {
    var oldpage = parseInt(jQuery("#myfocus").html());
    if (oldpage == 1) {
        return;
    }
    else {
        var newpage = parseInt(oldpage - 1) + "";
        showpageFix(newpage, flag, section, ch, key, sort, pagecount, uc);
    }
}

function nextFix(flag, section, ch, key, sort, pagecount, uc) {
    var oldpage = parseInt(jQuery("#myfocus").html());
    var t = parseInt(pagecount);
    if (oldpage >= t) {
        return;
    }
    else {
        var newpage = parseInt(oldpage + 1) + "";
        showpageFix(newpage, flag, section, ch, key, sort, pagecount, uc);
    }
}

function firstFix(flag, section, ch, key, sort, pagecount, uc) {
    showpageFix(1, flag, section, ch, key, sort, pagecount, uc);
}

function lastFix(flag, section, ch, key, sort, pagecount, uc) {
    showpageFix(parseInt(pagecount), flag, section, ch, key, sort, pagecount, uc);
}
function setBuilddiv(url, para) {
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {
      
            $("#div_section").html(msg);
        }
    })
}