/// <reference path="../../javascript/JSintellisense/jquery-1.3.2-vsdoc2.js" />
//权限处理相关fuc

//选择此角色有的权限
function initpersion() {
    var jsonstr = $("#hidhas").val();
    var json = eval('(' + jsonstr + ')');
    var length = json.rp.length;
    var ncount = 0;
    for (var i = 0; i < length; i++) {
        var pagecode = json.rp[i].P_PageCode;
        var pvalue = parseInt(json.rp[i].P_Value);
        $("tr[pc='" + pagecode + "']").each(function() {
            //原理:当前权限值与2(查)按位与，如果值=2(查)表示有查看权限
            //N种权限
            ncount = $("tr[pc='" + pagecode + "'] .op_item").length;
            for (var j = 0; j < ncount; j++) {
                var cvalue = Math.pow(2, parseInt(j) + 1) + "";
                //表示当前权限包含，设置选中
                if ((pvalue & cvalue) == cvalue) {
                    $(this).find("input[value='" + cvalue + "']").attr("checked", true);
                }
            }
        })
    }
}

//保存当前选择的。（首先删除原来的）
function rp_save() {
    var length = $("input[class='myitem']:checked").length;
    if (length == "0") {
        alert("请选择相关权限");
        return false;
    }

    //拼接json
    var json = "[";
    $(".datatd").each(function() {
        var _pagecode = $(this).attr("pc");
        var _pvalue = parseInt(0);
        //计算权限
        $(this).find("input[class='myitem']:checked").each(function() {
            _pvalue += parseInt($(this).attr("value"));
        })
        if (_pvalue > 0) {
            json += "{P_PageCode:\"" + _pagecode + "\",P_Value:\"" + _pvalue + "\"},";
        }
    });

    var temp = json.replace(/,$/, "");
    temp += "]";
    $("#hidhas").val(temp);
    //弹出加载层
    showload_super();
    return true;
}