// TogoShoppingCart.js
// 在线点餐购物车实现脚本
// CopyRight (c) 2009 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010－03－23

var reference_url;
var book_id;
var Jflag = -1;
var JxmlHttp;
var XxmlHttp;
var Xflag = 0;
//将书加入购物车 并跳转到购物车页面 返回页面url
function AddToShoppingCart(pid, pname, pprice, flag) {
    var hfBuildingID = $D("hfBuildingID");
    var hfBuildingName = $D("hfBuildingName");
    var cookiebn = handlecookie("buildingName");
    var cookieid = handlecookie("buildingID");
    var temptogoname = $D("hidTogoName").value;
    var userid = $D("hidUid").value;
    var togoStatus = $D("hnTogoStatus").value;
    var togoid = $D("hidTogoId").value;

    //商家不营业.
    if (togoStatus == 0) {
        alert('抱歉,' + temptogoname + '现在不营业,请稍后再点餐或选择别的餐厅')
        return;
    }

    var uid = userid;

    var togoid = document.getElementById("hidTogoId").value;
    var togoname = escape(document.getElementById("hidTogoName").value);

    jQuery.ajax(
    {
        type: "post",
        url: "../../Ajax/TogoShoppingCart.aspx",
        data: "fuc=add&uid=" + uid + "&togoid=" + togoid + "&togoname=" + togoname + "&pid=" + pid + "&pname=" + escape(pname) + "&pprice=" + pprice + "&pnum=1&time=" + new Date().getTime() + "",
        success: function(msg) {
            if (msg == "1") {
                ListCart();
            }
            if (msg == "-1") {
                alert("添加失败");
            }
            if (msg == "0") {
                alert("请清空购物车再点餐。");
            }
        }
    })
}


//修改购物车中商品数量
function modcart(pid, pnum, lastpum) {
    ///判断输入是否合法.
    try {
        var nowpum = parseInt(pnum);
        if (nowpum < 500 && nowpum > 0) {
            pnum = nowpum;
        }
        else {
            alert("请输入1-500之间的整数");
            pnum = lastpum;
        }
    }
    catch (e) {
        pnum = lastpum;
    }
    var uid = document.getElementById("hidUid").value;

    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/TogoShoppingCart.aspx",
        data: "fuc=mod&uid=" + uid + "&pid=" + pid + "&pnum=" + pnum + "",
        success: function (msg) {
            if (msg == "1") {
                ListCart();
            }
            if (msg == "-1") {
                alert("添加失败");
            }
            if (msg == "0") {
                alert("请清空购物车再点餐。");
            }
        }
    })



}

///删除购物车中商品
function delcart(pid) {
    var uid = document.getElementById("hidUid").value;

    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/TogoShoppingCart.aspx",
        data: "fuc=del&uid=" + uid + "&pid=" + pid + "",
        success: function (msg) {
            if (msg == "1") {
                ListCart();
            }
            if (msg == "-1") {
                alert("添加失败");
            }
            if (msg == "0") {
                alert("请清空购物车再点餐。");
            }
        }
    })

}

///清空购物车中商品
function deleteallcart() {
    if (!confirm('你确认清空购物车?')) {
        return;
    }
    var uid = document.getElementById("hidUid").value;

    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/TogoShoppingCart.aspx",
        data: "fuc=delall&uid=" + uid + "",
        success: function (msg) {
            if (msg == "1") {
                ListCart();
            }
            if (msg == "-1") {
                alert("添加失败");
            }
            if (msg == "0") {
                alert("请清空购物车再点餐。");
            }
        }
    })


}


function ListCart() {
    var userid = document.getElementById("hidUid").value;
    var togoid = $("#hidTogoId").val();//商家编号
    if (togoid == undefined) {
        togoid = "0";
    }


    jQuery.ajax(
    {
        type: "post",
        url: "../../Ajax/TogoShoppingCart.aspx",
        data: "fuc=list&t=" + new Date().getTime() + "&uid=" + userid + "&togoid=" + togoid + "&grade=" + 1,
        success: function(msg) {
            if (msg == "0") {
                alert("读取失败！");
            }
            else {
                //更新右上角数量就问题....
                jQuery("#cartContent").html(msg);
            }
        }
    })
}

function ListCart_deal() {
    if (this.req.responseText != "0") {
        document.getElementById("cartContent").innerHTML = this.req.responseText;
    }
    else {
        //显示失败信息
        alert("读取失败");
    }
}

function ListCart_err() {
    alert("服务器忙...");
    //ShowWindow("服务器忙...", false);
}

function CheckTimebusiness(str) {
    var timelist = new Array(); //得到每个时间段.
    var timeinfo = new Array(); //得到每个时间.
    timelist = str.split("|");
    for (var i = 0; i < timelist.length; i++) {
        timeinfo = timelist[i].split("_");
    }
}

function CheckCart() {

    if ($(".myshop").length <= 0) {
        alert('你的餐盒是空的,不的提交!');
        return false;
    }
    var togoStatus = $D("hnTogoStatus").value;
    if (togoStatus != "1") {
        alert('商家不在营业，不能提交订单');
        return false;
    }
    /*
    var userid = document.getElementById("hidUid").value;
    jQuery.ajax(
    {
        async: false,
        type: "post",
        url: "../../Ajax/CheckuserQulity.aspx",
        data: "t=" + new Date().getTime() + "&uid=" + userid,
        success: function(msg) {
            if (msg != "1") {
                alert(msg);

            }
            else {
             var togoid = $D("hidTogoId").value;
             window.location = "submitorder.aspx?id=" + togoid;
            }
        }
    })*/

    var togoid = $D("hidTogoId").value;
    window.location = "submitorder.aspx?id=" + togoid + "&tel=" + Request("tel");
}

//ajax获取菜品,直接获取
function getmyfood() {
    var tid = $("#hidTogoId").val() + "";
    var hfsortid = $("#hfsortid").val() + "";
    var hfsortidflag = $("#hfsortidflag").val() + "";
    var dids = $("#dids").val() + "";
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/getfoods.aspx",
        data: "t=" + new Date().getTime() + "&tid=" + tid + "&sid=" + hfsortid + "&flag=" + hfsortidflag + "&dids=" + dids,
        success: function(msg) {
            $("#shop_inot_t3").html(msg);
        }
    })
}
///传入id.标志
function getmyfood_id(hfsortid, hfsortidflag) {
    var tid = $("#hidTogoId").val() + "";
    var dids = $("#dids").val() + "";
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/getfoods.aspx",
        data: "t=" + new Date().getTime() + "&tid=" + tid + "&sid=" + hfsortid + "&flag=" + hfsortidflag + "&dids=" + dids,
        success: function(msg) {
            $("#shop_inot_t3").html(msg);
        }
    })
}

//获得URL参数
function Request(){
    var Url=top.window.location.href.toLowerCase();
    var u,g,gg,strRt='';
    if(arguments[arguments.length-1]=="#")
       u=Url.split("#");
    else
       u=Url.split("?");
    if (u.length==1) g='';
    else g=u[1];

    if(g!=''){
       gg=g.split("&");
       var MaxI=gg.length;
       var str = arguments[0].toLowerCase()+"=";
       for(i=0;i<MaxI;i++){
          if(gg[i].indexOf(str)==0) {
            strRt=gg[i].replace(str,"");
            break;
          }
       }
    }
    return strRt;
}