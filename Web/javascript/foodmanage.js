/// <reference path="JSintellisense/jquery-1.3.2-vsdoc2.js" />
String.prototype.trim = function () {
    return this.replace(/(\s*$)|(^\s*)/g, '');
}

/********************************
商品规格，属性相关管理
****************************/

function addsytle() {
    var fid = $("#hidDataId").val();
    if (fid == 0 || fid.length == 0) {
        alert("请先添加商品，保存后再添加规格");
        return;
    }


    var tid = $("#hidTogoId").val();


    $.jBox("iframe:/Admin/shop/addFoodStyle.aspx?fid=" + fid + "&tid=" + tid, {
        title: "添加规格",
        width: 640,
        height: 400,
        buttons: { '关闭': true }
    });


}

function edit(did) {
    var fid = $("#hidDataId").val();
    var tid = $("#hidTogoId").val();
 
    $.jBox("iframe:/Admin/shop/addFoodStyle.aspx?fid=" + fid + "&tid=" + tid + "&id=" + did, {
        title: "编辑属性",
        width: 640,
        height: 400,
        buttons: { '关闭': true }
    });


}

function addattr() {
    var fid = $("#hidDataId").val();
    if (fid == 0 || fid.length == 0) {
        alert("请先添加商品，保存后再添加商品属性");
        return;
    }

    var tid = $("#hidTogoId").val();



    $.jBox("iframe:/Admin/shop/addFoodAttributes.aspx?fid=" + fid + "&tid="+tid, {
        title: "添加属性",
        width: 640,
        height: 400,
        buttons: { '关闭': true }
    });
}

function editattr(did) {
    var fid = $("#hidDataId").val();
    var tid = $("#hidTogoId").val();


    $.jBox("iframe:/Admin/shop/addFoodAttributes.aspx?fid=" + fid + "&tid=" + tid+"&id="+did, {
        title: "编辑属性",
        width: 640,
        height: 400,
        buttons: { '关闭': true }
    });

}



var RowNum = 0;
var tid = $("#hidTogoId").val();


foodTable = {
    //添加行
    addrow: function () {
        RowNum++;
        var tr = $("#myfooditem").render({ index: RowNum.toString() });
        $(tr).appendTo("#foodtable");
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
    if ($(".fooditem").length < 1) {
        alert("请至少添加2个选项");
        return false;
    }
    return j_submitdata("attrbox");
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
            fdStyle += "{'classname':'" + $("#tbfoodname" + i).val() + "','Pic':'" + getdata($("#tbfoodcount" + i).val()) + "'";
            fdStyle += "},";
        }
    }
    fdStyle = fdStyle.replace(/,$/, "");
    fdStyle += "]";
    document.getElementById("hidStyle").value = fdStyle;
}
