/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
//首页js

$(function () {

    $(".map-bar-close").click(function () {
        $(".mapwrap").hide();
        $("#J_bg").hide();
    })

    $("#divhistorylist").hover(
        function () {
            $(".selecthistoryCity").show();
        },
        function () {
            $(".selecthistoryCity").hide();
        })


    $("#divcitylist").click(function () {
        $(".city-dropdown").toggle();
    })

    $("#tbindexsearchbox").click(function () {
        $(".city-dropdown").hide();
    })

    var change = request("change");
    var cid = request("cid");
    if (change == "" && cid == "") {
        if ($(".myadd").length > 0)
        {
            $(".myadd")[0].click();
        }
        return false;
    }

})

///首页搜索地址
function indexsearchAddress() {
    var key = $("#tbindexsearchbox").val();
    if (key == "" || key == "请确定您的配送地址") {
        alert("请确定您的配送地址");
        return;
    }
    window.location = "shoplist.aspx?addr=" + encodeURI(encodeURI(key)) + "&type=search";
}

function gomapsearch() {

    var key = $("#tbindexsearchbox").val();
    if (key == "" || key == "请确定您的配送地址") {
        alert("请确定您的配送地址");
        return;
    }
    $(".mapwrap").show();
    $("#J_bg").show();

    $("#tbkeywork").val($("#tbindexsearchbox").val());
    go_search();

}

$("#divcitylist").mouseover(function () {
    $(".selectListCity").show();
});

$("#divcitylist").mouseout(function () {
    $(".selectListCity").hide();
});
