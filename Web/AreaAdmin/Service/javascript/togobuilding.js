
/******************************************** 升级版  ******************************************/
//通过区域获取.
function getbuild(flag , bysection , ch ,key , page , sort , uc) {
    //选择样式
    $(".sbuild").removeClass("hover");
    var url = "/ajax/TogoBuilding.aspx";
    var objpath = window.location + "";
    if (objpath.toLowerCase().indexOf("localhost") > 0) {
        url = "/web/ajax/TogoBuilding.aspx";
    }
    var para = "fuc=getbuild&page=1";
    if (typeof flag != 'undefined') {
        para += "&flag="+flag;
    }
    if (typeof bysection != 'undefined' && bysection != -1) {
        para += "&s=" + bysection;
        $("#one" + bysection).attr("class", "sbuild hover") 
        $("#curSection").val(bysection);
    }
    else {
        $("#one0").attr("class", "sbuild hover") 
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
    var key = escape( $("#bkey").val() + "");
    getbuild(0, -1, -1, key, 1, -1 , type);
}

//字母
function GetByCFix(ch) {
    var sid = $("#curSection").val();
    if (sid == 0 || sid == undefined)
    {
        sid = -1;
    }
    getbuild(0, sid, ch, -1, 1, -1 , 0);
}

//字母->用户中心
function GetByCFixUC(ch) {
    var sid = $("#curSection").val();
    if (sid == 0 || sid == undefined) {
        sid = -1;
    }
    getbuild(0, sid, ch, -1, 1, -1, 0);
}

//搜索
function showpageFix(pages, flag, section, ch, key, sort, pagecount ,uc) {
    var url = "/ajax/TogoBuilding.aspx";
    var objpath = window.location + "";
    if (objpath.toLowerCase().indexOf("localhost") > 0) {
        url = "/web/ajax/TogoBuilding.aspx";
    }
    var para = "fuc=getbuild&page=" + pages;
    para += "&flag=" + flag;
    para += "&s=" + section;
    para += "&c=" + ch;
    para += "&key=" + escape(key);
    para += "&sort=" + sort;
    para += "&uc=" + uc;
   $("#curSection").val(section);
    setBuilddiv(url, para);
}

function preFix(flag, section, ch, key, sort, pagecount ,uc) {
    var oldpage = parseInt(jQuery("#myfocus").html());
    if (oldpage == 1) {
        return;
    }
    else {
        var newpage = parseInt(oldpage - 1) + "";
        showpageFix(newpage, flag, section, ch, key, sort, pagecount ,uc);
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
        $(".div_section").html(msg);
        }
    })
}

function closediv() {
    $("#tabpopup1").hide();
    $("#windownbg").hide();
    $("#build-box").remove();
}

function InitAddress(name, id) {
    $("#left_tbbuildname").val(name);
    $("#hfbid").val(id);
    handlecookie("crm_bid", id, { expires: 1, path: "/", secure: false });
    closediv();
}




/*弹出选择写字楼的table*/
function showBuild_Box(content, width, height, showbg) {
    $("#build-box").remove(); //清除内容
    var width = width >= 950 ? this.width = 950 : this.width = width;     //设置最大窗口宽度
    var height = height >= 527 ? this.height = 527 : this.height = height;   //设置最大窗口高度
    if (showWindown == true) {
        var simpleWindown_html = new String;
        simpleWindown_html = "<div id=\"windownbg\" style=\"height:" + $(document).height() + "px;filter:alpha(opacity=0.8);opacity:0.8;z-index: 999901;background: none repeat scroll 0% 0% rgb(50, 50, 50); \"></div>";
        simpleWindown_html += "<div id=\"build-box\" style=' display:none'>";
        simpleWindown_html += "<div id=\"build-content\"><img src='http://www.ihangjing.com/images/loading.gif' class='loading' /></div>";
        simpleWindown_html += "</div>";
        $("body").append(simpleWindown_html);
        show = false;
    }
    contentType = content.substring(0, content.indexOf(":"));
    content = content.substring(content.indexOf(":") + 1, content.length);
    switch (contentType) {
        case "url":
            var content_array = content.split("?");
            $.ajax({
                type: content_array[0],
                url: content_array[1],
                data: content_array[2],
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html(html);
                }
            });
            break;
        case "iframe":
            $.ajax({
                error: function() {
                    $("#build-content").html("<p class='windown-error'>加载数据出错...</p>");
                },
                success: function(html) {
                    $("#build-content").html("<iframe src=\"" + content + "\" width=\"100%\" height=\"" + parseInt(height) + "px" + "\" scrolling=\"auto\" frameborder=\"0\" marginheight=\"0\" marginwidth=\"0\"></iframe>");
                }
            });
    }
    if (showbg == "true") {
        $("#windownbg").show();
    }
    else {
        $("#windownbg").remove();
    };
    $("#windownbg").animate({ opacity: "0.8" }, "normal"); //设置透明度  
    if (height >= 527) {
        $("#build-content").css({ width: (parseInt(width) + 17) + "px", height: height + "px" });
    } else {
        $("#build-content").css({ width: width + "px", height: height + "px" });
    }
    var cw = document.documentElement.clientWidth, ch = document.documentElement.clientHeight, est = document.documentElement.scrollTop;
    var _version = $.browser.version;
    if (_version == 6.0) {

        $("#build-box").css({ left: "50%", top: (parseInt((ch) / 2) + est) + "px", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    } else {
        $("#build-box").css({ left: "50%", top: "50%", marginTop: -((parseInt(height) + 53) / 2) + "px", marginLeft: -((parseInt(width) + 32) / 2) + "px", zIndex: "999999" });
    };
    $("#build-box").show();
}

function jshow_box() {
    var url = "/Admin/Service/buildtable_1.aspx";
    var objpath = window.location + "";
    if (objpath.toLowerCase().indexOf("localhost") > 0) {
        url = "/web/Admin/Service/buildtable_1.aspx";
    }
    showBuild_Box('url:get?' + url, 702, 450, 'true');
}

function closediv() {
    $("#build-box").remove();
    $("#windownbg").remove();
}

function getsectionbycity(cid) {
    $(".cityitem").removeClass("hover");
    var url = "/ajax/getsectionshtml.aspx";
    var objpath = window.location + "";
    if (objpath.toLowerCase().indexOf("localhost") > 0) {
        url = "/web/ajax/getsectionshtml.aspx";
    }
    var para = "fuc=getbuild&sort=" + cid;
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {
            $("#ul_tab_section").html(msg);
            $("#cone" + cid).attr("class", "cityitem hover");
        }
    })
}