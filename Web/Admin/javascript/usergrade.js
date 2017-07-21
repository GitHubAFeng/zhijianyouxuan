/// <reference path="../../javascript/JSintellisense/jquery-1.3.2-vsdoc2.js" />

//保存当前设置的，注意判断等级之前是否设置过
function rp_save() {

    //判断输入
    if (j_submitdata('data')) {

    }
    else {
        return;
    }
    var _sendmoneyDiscount;
    var _foodmoneyDiscount;
    var _pointrat;
    var _sendprior;
    
    //拼接json
    var json = "[";
    $(".datatd").each(function() {
        _sendmoneyDiscount = $(this).find("input[name='tbsendmoneyDiscount']").val();
        _foodmoneyDiscount = $(this).find("input[name='tbfoodmoneyDiscount']").val();
        _pointrat = $(this).find("input[name='tbpointrat']").val();
        //_sendprior = $(this).find("input[name='sendprior_" + $(this).attr("dataid") + "']:checked").val();
        _sendprior = 0;
        json += "{'pid':'" + $(this).attr("pid") + "','gid':'" + $(this).attr("dataid") + "','sendmoneyDiscount':'" + _sendmoneyDiscount + "','foodmoneyDiscount':'" + _foodmoneyDiscount + "','pointrat':'" + _pointrat + "','sendprior':'" + _sendprior + "','ReveInt':'0','ReveVar':'','ReveFlat':'0'},";
    });

    var temp = json.replace(/,$/, "");
    temp += "]";
    $("#hidhas").val(temp);
    showload_super();
    
}