/*商家提交跑腿订单 20151202*/

$(function () {

    $("input[type=text],textarea").one("focus", function () {
        $(this).val("");
    });

    $("[tip]").focus(function () {
        $(this).nextAll(".warning").remove();

        if ($("#hf_IsLogin").val() == "") {
            jtipsWindown('请登陆', 'id:divShowContent', '520', '260', 'true', '', 'true', 'text');
        }
    }).blur(function () {
        if ($(this).val() == "") {
            var $warnDiv = $("<div class='warning' style='display:none;'>" + $(this).attr("tip") + "</div>");
            $(this).parent().append($warnDiv);
            $(this).nextAll(".warning").show();
        }
    });


    //提交跑腿订单按钮
    $("#bt_Request1").click(function () {

        var tbInve2 = $("#tbInve2").val();

        showload_super();

        if (j_submitdata("form-list")) {

            if ($("#cbsendfee").attr("checked")) {
                var reg = new RegExp("^([0-9]|[0-9].[0-9]{0-2}|[1-9][0-9]*.[0-9]{0,2})$");
                var fee = $("#tbsendfee").val();
                if (!reg.test(fee)) {
                    alert($("#tbsendfee").attr("tip"));
                    return false;
                }
                $("#hidsendfee").val(fee);
            }
            return true;
        }
        return false;

    });


    //初始化商家位置 2015-12-2 
    serchAddFrom($("#hidflat").val(), $("#hidflng").val());

});


//搜索寄件人地址
function addfrom() {
    var addressfrom = $("#tbAddress").val();
    if (addressfrom == "请输入发件人地址" || addressfrom == "") {
        alert("请输入发件人地址");
    }
    else {
        serchAddFrom(addressfrom);
    }
}

//搜索收件人地址
function addto() {
    var addressto = $("#tbOorderid").val();
    if (addressto == "请输入收件人地址" || addressto == "") {
        alert("请输入收件人地址");
    }
    else {
        serchAddTo(addressto);
    }
}

//保存收件人地址
function saveSend() {
    var userid = $("#userid").val();
    if (userid == '') {
        alertLogin();
        return;
    }
    myGeo.getLocation(markerpoint, function (rs) {
        var addComp = rs.addressComponents;
        var sendAddress = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        $("#sendLng").val(markerpoint.lng);
        $("#sendLat").val(markerpoint.lat);
        $("#sendAddress").val(sendAddress);
        marker.closeInfoWindow();
        if ($("#receAddress") != '' && $("#receAddress") != null) {
            calDistance();
        }
    });
}

//保存发件人地址并计算时间和距离
function saveRece() {
    var userid = $("#userid").val();
    if (userid == '') {
        alertLogin();
        return;
    }
    myGeo.getLocation(markerpoint, function (rs) {
        var addComp = rs.addressComponents;
        var receAddress = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        $("#receLng").val(markerpoint.lng);
        $("#receLat").val(markerpoint.lat);
        $("#receAddress").val(receAddress);
        marker.closeInfoWindow();
        if ($("#sendAddress").val() != '' && $("#sendAddress").val() != null) {
            calDistance();
        }
    });
}

//搜索地址
function go_search() {
    var addresskey = $("#run-searchAddress").val();
    if (addresskey == "请输入写字楼，小区，学校等地址信息" || addresskey == "") {
        alert("请输入写字楼，小区，学校等地址信息");
    }
    else {
        serchAdd(addresskey);
    }
}




//开始提交数据：mycontainter 是一个Class,表示中只验证这个元素内的控件
function j_submitdata(mycontainter) {
    var comflag = true;
    $("." + mycontainter).find("[reg],[url]:not([reg])").each(function () {
        if ($(this).attr("reg") != undefined) {
            if (!validate($(this))) {
                comflag = false;
                return false;
            }
        }
    });

    if (comflag == false) {
        return false;
    }
    return true;
}


//验证,根据当前对像设置的正则表达式，判断是否通，不通过alert(当前对像的tip属性)
//CanBeNull="n" 表示不可为空，格式按reg 
//CanBeNull="y" 表示可为空，如果不空，格式按reg 
function validate(obj) {
    var reg = new RegExp(obj.attr("reg"));
    var objValue = obj.attr("value");
    var CanBeNull = "";
    if (obj.attr("canbenull") != undefined && obj.attr("canbenull") == "y") {

        CanBeNull = "y"; //可为空，如果不空，才判断
        if (objValue != "" && objValue != obj.attr("tip")) {
            if (!reg.test(objValue)) {
                alert(obj.attr("tip"));
                return false;
            }
            return true;
        }
        else {
            return true;
        }
    }
    else {
        CanBeNull = "n"; //不能为空
       // alert(obj.attr("tip"));

        if (objValue == ""|| objValue == obj.attr("tip"))
        {
            tipsWindown('提示信息', 'text:' + obj.attr("tip") + '', '250', '150', 'true', '2000', 'true', 'text');
            return false;
        }
        else {

            if (!reg.test(objValue))  //符合
            {
                tipsWindown('提示信息', 'text:' + obj.attr("tip") + '', '250', '150', 'true', '2000', 'true', 'text');
                //alert(obj.attr("tip"));
                return false;
            }

        }
        return true;
    }

}


function changesendfee(event)
{
    if ($(event).attr("checked")) {
        $(".divsendfee").show();
    }
    else {
        $(".divsendfee").hide();

    }
}

function tbchangesendfee(event)
{
    var fee = $("#tbsendfee").val();
    if (fee == "")
    {
        fee = "0";  
    }
    $("#hidsendfee").val(fee);
}