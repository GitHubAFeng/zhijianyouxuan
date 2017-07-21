/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
//列表界面js

$(document).ready(function () {
    initnav(2);
    var hfod = $("#hfod").val();
    $("." + hfod).addClass("cul");

    hfod = $("#hfcursid").val();
    $("#sectionid" + hfod).addClass("cul");

    hfod = $("#hfcursortid").val();
    $("#sortid" + hfod).addClass("cul");

    hfod = $("#hfcursaleid").val();
    $("input[name='saleradio']").removeAttr("checked");
    if (hfod === "0") {
        $("#allshopradio").attr("checked", "checked");
    }
    else {
        $("#saleshopradio").attr("checked", "checked");
    }

    hfod = $("#hfcursendmoney").val();
    $("#minmoney" + hfod).addClass("cul");

    $(".hisbg").mouseover(function () {
        $(".history_option").show();
        $(".hisbg").addClass("option-open");

    });
    $(".history_option").mouseover(function () {
        $(".history_option").show();
        $(".hisbg").addClass("option-open");

    });
    $(".history_option").mouseout(function () {
        $(".history_option").hide();
        $(".hisbg").removeClass("option-open")

    });

    //var from = request("from");
    //if (from === "m") {
    //    var pos = $("#shopboxdiv").offset().top;
    //    $("html,body").animate({ scrollTop: pos }, 0);
    //}

    //没定位转到首页
    var noaddress = request("lat");
    if (noaddress === "") {
        $(".guid-map").show();
    }

})
