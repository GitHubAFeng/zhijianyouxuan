
var m_object = null;
///加入餐馆到收藏夹
function addtogo(obj, togoid, bid, x, y) {
    m_object = obj;
    jQuery.ajax(
    {
        type: "post",
        url: "Ajax/TogoCollect.aspx",
        data: "fuc=addtogo&togoid=" + togoid + "&bid=" + bid,
        success: function(msg) {
         
            addtogo_deal(msg, x, y);
        }
    })
}

function addtogo_deal(msg, x, y) {

    if (msg == "1") {
        cShowTip(document.getElementById(m_object), "dpop", x, y, "成功加入收藏夹");
    }
    else if (msg == "0") {
        cShowTip(document.getElementById(m_object), "dpop", x, y, "加入收藏夹失败");
    }
    else if (msg == "2") {
        cShowTip(document.getElementById(m_object), "dpop", x, y, "收藏夹中已经存在");
    }
    else if (msg == "-1") {
        cShowTip(document.getElementById(m_object), "dpop", x, y, "请登入后再试");
    }
}

///提示套餐添加成功
function addsuccess(m_objectx, x, y) {
    
    var html = " <div class=\"fav_bg\" id=\"dpop_success\" style=\"display: none\"><h3 id=\"lbMsg\">";
    html += "商品已经加入购物车,<a href='OrderDetail.aspx'>去结算</a></h3></div>";
    $("body").append(html);
    cShowTip(m_objectx, "dpop_success", x, y, "商品已经加入购物车,<a href='OrderDetail.aspx'>去结算</a>");

}

//显示一个div
//显示需要定位
//obj是你要显示的div相对的对象，一般是一个按钮或者链接填 this即可 
//addx、addy是相对与obj的偏移量，就是div显示的位置
function cShowTip(obj, objdiv, addx, addy, msg) {

    $("#lbMsg").html(msg);

    document.getElementById("lbMsg").style.color = "red";

    var x = getposOffset_top(obj, 'left');
    var y = getposOffset_top(obj, 'top');

    var div_obj = document.getElementById(objdiv);
    div_obj.style.left = (x + addx) + 'px';
    div_obj.style.top = (y + addy) + 'px';
    div_obj.style.display = "inline";

    setTimeout(HideTip, 3000);
}

//隐藏一个div
function HideTip(objdiv) {
    var div_obj = document.getElementById("dpop");
    if (div_obj) {
        div_obj.style.display = "none";
    }
    $("#dpop_success").hide();
}

//获取偏移量
function getposOffset_top(what, offsettype) {
    var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
    var parentEl = what.offsetParent;
    while (parentEl != null) {
        totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return totaloffset;

}