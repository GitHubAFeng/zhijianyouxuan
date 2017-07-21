/// <reference path="../../javascript/JSintellisense/jquery-1.3.2-vsdoc2.js" />
//套餐相关js

var RowNum = 2;//全局变量。每个商品的id= tbfoodname+foodcount，默认有两个了
var tid = $("#hidTogoId").val();

$(document).ready(function () {

    var searchshopurl = "/ajaxHandler.ashx?method=searchFood&t=" + Math.random(1) + "&tid=" + tid;
    $("#tbfoodname1").autocomplete(searchshopurl);
    $("#tbfoodname2").autocomplete(searchshopurl);

    var id = request("id");
    //编辑时，删除空行
    if (id != "") {
        $("#food_row_1").remove();
        $("#food_row_2").remove();
        RowNum = $(".hasitem").length;
        $(".fooditem").autocomplete(searchshopurl);
    }

});

foodTable = {
    //添加行
    addrow: function () {
        RowNum++;
        var tr = $("#myfooditem").render({ index: RowNum.toString() });
        $(tr).appendTo("#foodtable");
        var searchshopurl = "/ajaxHandler.ashx?method=searchFood&t=" + Math.random(1) + "&tid=" + tid;
        $("#tbfoodname" + RowNum).autocomplete(searchshopurl);
    },
    //删除行
    delRow: function (id) {
        $("#food_row_" + id).remove();
        foodTable.getOldPrice();
    },
    //计算原价
    getOldPrice: function () {
        var allprice = 0.0;
        $(".fooditem").each(function () {
            var index = $(this).attr("id").replace("tbfoodname", "");
            var num = getdata($("#tbfoodcount" + index).val());
            allprice += parseFloat(getdata($(this).attr("data_price"))) * num;
        })
        $("#tboldprice").val(allprice.toFixed(1));
    }
};

//提交验证
function checkdata() {
    GetFoodJson();
    if ($(".fooditem").length < 2) {
        alert("请至少添加2个商品");
        return false;
    }
    return j_submitdata("form-list");
}

///返回数据,为空,或者不存在,返回0
function getdata(pid) {
    if (pid == null || pid == undefined || pid == "") {
        pid = "0";
    }
    return pid;
}

//生成商品的json格式數據
function GetFoodJson() {
    var fdStyle = "[";
    for (var i = 0; i < RowNum + 1; i++) {
        if ($("#food_row_" + i).length > 0) {
            fdStyle += "{'foodname':'" + $("#tbfoodname" + i).val() + "','fid':'" + getdata($("#tbfoodname" + i).attr("data_id")) + "','foodcount':'" + getdata($("#tbfoodcount" + i).val()) + "'";
            fdStyle += ",'ReveVar':'" + getdata($("#tbfoodname" + i).attr("data_price")) + "'";
            fdStyle += "},";
        }
    }
    fdStyle = fdStyle.replace(/,$/, "");
    fdStyle += "]";
    document.getElementById("hidStyle").value = fdStyle;
}
