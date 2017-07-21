
/******************************************** 升级版  ******************************************/
//通过区域获取.
function getbuild(flag, bysection, ch, key, page, sort, uc) {
    //选择样式

    var sortid = $("#DDLArea").val();
    
    $(".sbuild").removeClass("hover");
    var url = "../ajax/TogoBuilding.aspx"; //
    var para = "fuc=getbuild&page=1";
    if (typeof flag != 'undefined') {
        para += "&flag=" + flag;
    }
    if (typeof bysection != 'undefined' && bysection != -1) {
        para += "&s=" + bysection;
        $("#one" + bysection).attr("class", "sbuild hover")
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
        para += "&sort=" + sortid;
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
    var key = escape($("#bkey").val() + "");
    var sortid = $("#DDLArea").val();
    getbuild(0, -1, -1, key, 1, sortid, type);
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
            checkbox();
        }
    })
}

function closediv() {
    $("#tabpopup1").hide();
    $("#windownbg").hide();
    aa();
}


function mychange(aid, tag, name) {
    var tbBuilding = $("#tbBuilding").html() + "";
    var ids = $("#hfids").val() + "";
    var check = tag.checked;
    if (check == true) {
        if (ids == "") {
            $("#hfids").val(aid);
            $("#tbBuilding").html(name)
        }
        else {
            $("#hfids").val(ids + "," + aid);
            $("#tbBuilding").html(tbBuilding + "," + name)
        }
    }
    else {
        var bnames = tbBuilding.split(',');
        var bids = ids.split(',');
        var m = 0;
        var tempnams = "";
        var tempids = "";
        for (var i = 0; i < bids.length; i++) {
            if (aid == bids[i]) {
                m = i;
            }
            else {
                tempnams += bnames[i] + ",";
                tempids += bids[i] + ","
            }
        }
        var tn1 = tempnams.replace(/,$/, "");
        var ti1 = tempids.replace(/,$/, "");
        $("#hfids").val(ti1);
        $("#tbBuilding").html(tn1)
    }
}

function BuildCheckAll() {
    $(".mychck").each(function() {
        $(this).attr("checked", true);
        var aid = $(this).attr("value");
        var aname = $(this).attr("bname");
        mychange(aid, $(this)[0], aname);
    });
}


function BuildRiCheck() {
    $(".mychck").each(function() {
        var flag = $(this).attr("checked");
        if (flag == true) {
            $(this).attr("checked", false);
        }
        else {
            $(this).attr("checked", true);
        }
        var aid = $(this).attr("value");
        var aname = $(this).attr("bname");
        mychange(aid, $(this)[0], aname);
    });
}

function closediv(ele) {
    $("#" + ele).hide();
}

//选中
function checkbox() {
    var ids = $("#hfids").val() + "";
    $(".mychck").each(function() {
        if (ids.indexOf($(this).attr("value")) >= 0) {
            $(this).attr("checked", true);
        }
    });
}



function show_province()//evt,
{
    ShowTip(document.getElementById("m_select_ad"), "tabpopup_selectbuild", -100, 30);
}

function closediv() {
    document.getElementById("tabpopup_selectbuild").style.display = "none";
}
function InitAddress(name, dataid) {
    //判断有没有
    var ids = document.getElementById("tbBuildingId").value + "";
    var idarray = ids.split(',');
    for (var i = 0; i < idarray.length; i++) {
        if (dataid == idarray[i]) {
            alert('您已经选择过了此建筑物');
            return;
        }
    }
    var textBox = document.getElementById("tbBuilding");
    if (textBox.value == "") {
        textBox.value = name;
    }
    else {
        textBox.value = document.getElementById("tbBuilding").value + "," + name;
    }
    var hidBuildingId = document.getElementById("tbBuildingId");
    if (hidBuildingId.value == "") {
        hidBuildingId.value = dataid;
    }
    else {
        hidBuildingId.value = document.getElementById("tbBuildingId").value + "," + dataid;
    }

}

///根据选择的城市获取区域(返回列表，用来选择楼宇)
function getsections_fix() {
    var cityid = $("#DDLArea").val() + "";
    var url = "../ajax/getsections.aspx";
    var para = "fuc=getbuild&sort=" + cityid;
    jQuery.ajax(
    {
        type: "post",
        url: url,
        data: para,
        success: function(msg) {
            $("#mysection").html(msg);
        }
    })
}

