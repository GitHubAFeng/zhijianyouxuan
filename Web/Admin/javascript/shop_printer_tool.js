/// <reference path="../../javascript/JSintellisense/jquery-1.3.2-vsdoc2.js" />
/************************************* 此js主要完成配送点下每个打印机分配的功能 ********************************/
//初始化事件
function sortinit() {
    $(".shopcontainer").dragsort({
        dragSelector: "div",
        dragBetween: true,
        dragEnd: saveOrder,
        placeHolderTemplate: "<li class='placeHolder'><div></div></li>",
        scrollSpeed: 5
    });
}
//一个拖动完成后回调
function saveOrder() {
    var data = $(".shopcontainer li").map(function() {
        return $(this).children().html();
    }).get();
    $("input[name=list1SortOrder]").val(data.join("|"));
};

//所有完成后，保存：获取每个打印机后面的container中的记录，保存数据库
function allsave() {
    var jsonstr = "[";

    $(".hasset").each(function() {
        jsonstr += "{'DataId':'" + $(this).attr("data-id") + "','PrinterSn':'" + $(this).attr("data-num") + "'";
        jsonstr += ",'ShopList':[";

        $(this).find("li").each(
            function() {
                jsonstr += "{'shopid':'" + $(this).attr("data-id") + "'},";
            }
        )
        jsonstr = jsonstr.replace(/,$/, "");
        jsonstr += "]},";
    })
    jsonstr = jsonstr.replace(/,$/, "");
    jsonstr += "]";

    $("#hfjson").val(jsonstr);
    showload_super();
    
    return false;
    
}