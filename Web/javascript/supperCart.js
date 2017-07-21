
//购物车操作类
//cartother  已选择加料
//cartmaterial 商品配料

//显示cart
function showdiv(fid, fname, unit, price, stylecount, attrcount) {
    var hnTogoStatus = $("#hnTogoStatus").val() + "";
    if (hnTogoStatus == "0") {
        alert("商家正在休息，请选择别的商家点餐");
        return;
    }
    var uid = $("#hidUid").val();
    //清除上一个选择结果
    $("#cartother").html('');
    $("#selectfood").float({ position: "rm", delay: 500 });
    // 获取数据
    $("#hfcname").val(fname);
    $("#hfpid").val(fid);
    $("#cartname").html(fname);
    $("#lbunit").html(unit);

    $("#styleloader").show();


    if (stylecount > 1) {
        //获取规格
        jQuery.ajax(
        {
            type: "post",
            url: "/Ajax/getstyle.aspx", //返回规格，配料
            dataType: "json",
            data: "id=" + fid,
            success: function (data) {
                if (typeof (data) == "object" && typeof (data.myattr) == "object") {
                    var length = data.myattr.length;
                    var str = "";
                    for (var i = 0; i < length; i++) {
                        if (i == 0) {//选中
                            str += "<li><input type=\"radio\" price='" + data.myattr[i].Price + "' id='" + data.myattr[i].DataId + "' value='" + data.myattr[i].DataId + "' name='mystyle' checked='true' onclick='selectme(this)' stylename='" + data.myattr[i].Title + "' /><label for=\"" + data.myattr[i].DataId + "\">" + data.myattr[i].Title + "(￥" + data.myattr[i].Price + ")</label></li>";
                        }
                        else {
                            str += "<li><input type=\"radio\" price='" + data.myattr[i].Price + "' id='" + data.myattr[i].DataId + "' value='" + data.myattr[i].DataId + "' name='mystyle' onclick='selectme(this)' stylename='" + data.myattr[i].Title + "'  /><label for=\"" + data.myattr[i].DataId + "\">" + data.myattr[i].Title + "(￥" + data.myattr[i].Price + ")</label></li>";
                        }
                    }
                    $("#divstyle").html(str);
                    $("#styleloader").hide();
                }
            }
        })
    }

    if (attrcount > 0) {
        //配料
        jQuery.ajax(
        {
            type: "post",
            url: "/Ajax/getattr.aspx", //返回配料
            dataType: "json",
            data: "id=" + fid,
            success: function (data) {
                if (typeof (data) == "object" && typeof (data.myattr) == "object") {
                    var length = data.myattr.length;
                    var str = "";
                    for (var i = 0; i < length; i++) {
                        var index = i >= 2 ? 6 + "" : parseInt(4 + i) + "";
                        str += "<div class='cartattr lunch_box_0" + index + " clear' ><h4 id='jh4_" + i + "'>" + data.myattr[i].Title + "</h4><ul>";
                        str += atrtohmlt(data.myattr[i].Attributes, data.myattr[i].SelectType, fid, data.myattr[i].DataId, data.myattr[i].Title, i);
                        str += "</ul></div>";
                    }
                    //加取消

                   
                    $("#cartmaterial").html(str);

                   

                    $("#styleloader").hide();
                }
            }
        })
    }

    if (stylecount > 1 || attrcount > 0) {
        $("#selectfood").show();
    }
    else {
        cartok(price);
    }
    
}
///隐藏cart

function closecart() {
    $("#selectfood").hide();
    $("#cartmaterial").html("");
    $("#cartother").html("");
    $("#divstyle").html("");
}

//确定
function cartok(price) {
    //添加一条记录.
    var num = $("#cartnum").val() + ""; //份数
    var remark = escape($("#cartremark").val() + ""); //备注
    var name = escape($("#hfcname").val());
    var tid = $("#hidTogoId").val();
    var uid = $("#hidUid").val();
    var codel = $("#hfcode").val();
    var tname = escape($("#hidTogoName").val());
    var mystyle = $("input[name='mystyle']");

    var sid = 0;
    var sname = "";
    mystyle.each(function () {
        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
            sid = $(this).attr("value");
            sname = escape($(this).attr("stylename"));
            price = $(this).attr("price");
        }
    })
    var pid = $("#hfpid").val();
    var owername = "0";
    var Funit = escape($("#lbunit").html());

    //通过计算
    //加料保存为json
    var jsonstr = "";
    var om = getmaterial();
    var addinfo = $("#cartother").html();

    jsonstr = "{'DataId':'0','UId':'" + uid + "', 'TogoId':'" + tid + "','TogoName':'" + tname + "','PId':'" + pid + "'"
    jsonstr += ",'PName':'" + (sname.length == 0 ? "" : "[" + sname + "]") + name + om.addnames + "','PPrice':'" + price + "','PNum':'" + num + "','Currentprice':'" + price + "','tempcode':'" + codel + "'";
    jsonstr += ",'Remark':'" + remark + "','sid':'" + sid + "',";
    jsonstr += "'sname':'" + sname + "',"
    jsonstr += "'Funit':'" + Funit + "',"
    jsonstr += "'material':'" + om.info + "', 'addprice':'" + om.addp + "','owername':'" + owername + "'";
    jsonstr += " }";

    //传到服务器
    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/addToCart.aspx",
        async: false,      //ajax同步
        data: "id=" + jsonstr + "&t=" + new Date().getTime(),
        success: function (msg) {

            List(); //刷新已经有商品
            $("#cartremark").val("");

            $("#selectfood").hide();
            closecart();

            $.jBox.tip('添加成功', 'success');
        }
    })
}

///加料连接成字符串.
function getmaterial() {
    var l = $(".cartattr").length; //个数
    var str = "";
    var attritems = "";
    var tempprice = 0;
    for (var i = 0; i < l; i++) {
        var o = $("input[cart='jj_" + i + "']");
        var aname = $("#jh4_" + i).html();



        var x = 0;
        //  每个加料中间用'#'分开.比如： 冰?去冰^3#糖?红糠^4@白糠^4
        $("input[cart='jj_" + i + "']").each(function () {



            if ($(this).attr("type") == "radio") //单选
            {

                if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
                    attritems += "%2b" + $(this).attr("myname");
                    str += aname + "?" + $(this).attr("myname") + "^" + $(this).attr("value") + "#"; //拼接一个加料
                    tempprice += parseFloat($(this).attr("value")); //累加加料加价


                }
            }
            else {
                if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
                    attritems += "%2b" + $(this).attr("myname");
                    if (x == 0) {
                        str += aname + "?" + $(this).attr("myname") + "^" + $(this).attr("value") + ""; //拼接一个加料
                    }
                    else {
                        str += "@" + $(this).attr("myname") + "^" + $(this).attr("value") + ""; //拼接一个加料
                    }
                    tempprice += parseFloat($(this).attr("value")) //累加加料加价
                    x = 1;
                }
            }
        })
        if (x == 1) {
            str += "#";
        }
    }
    //去最后一个'#';
    var h = escape(str.replace(/#$/, ""));
    if (attritems.length > 0) {
        attritems = "(" + attritems + ")";
    }

    return { info: h, addp: tempprice, addnames: attritems };
}


///产品配料转成html  mytpe 表示0表示单选，1表示双选 sid 属性
function atrtohmlt(attr, mtype, pid, sid, name, index) {
    var str = "";
    var temmp = attr + "";
    var dd = temmp.split('#');
    for (var i = 0; i < dd.length; i++) {
        var item = dd[i].split('?');
        if (mtype == 0) {
            str += " <li><input  type='radio'   name='attr_" + sid + "' price='" + item[1] + "' cart='jj_" + index + "' value='" + item[1] + "'  myname='" + item[0] + "' id='attr_" + pid + "_" + sid + "' onclick=\"selectme0(" + pid + " , '" + item[0] + "' , '" + item[1] + "','" + sid + "','" + name + "')\" />" + item[0] + "(￥" + item[1] + ")" + "</li>";
        }
        else {
            str += " <li><input type='checkbox' name='box_" + sid + "' price='" + item[1] + "'  cart='jj_" + index + "'  value='" + item[1] + "'  myname='" + item[0] + "' id='attr_" + pid + "_" + sid + "' onclick=\"selectme1(" + pid + " , '" + item[0] + "' , '" + item[1] + "' , '" + sid + "','" + name + "')\" />" + item[0] + "(￥" + item[1] + ")" + "</li>";
        }
    }
    if (mtype == 0) {
        str += "<li><a href=\"javascript:cancelRadio(" + pid + " , " + sid + ")\">取消</a></li>";
    }
    return str;
}

//配料单选
// 商品编号码， 配料子项名称 ， 价格 , sid 配料编号
function selectme0(pid, aname, aprice, sid, parentname) {
    var str = "<strong>" + parentname + "：</strong>+" + aname + "(￥" + aprice + ")";
    var o = $("#other_" + sid);
    if (o.length > 0) { //重新选择
        $("#other_" + sid).html(str);
    }
    else {//第一次选择
        var str = "<div class=\"lunch_box_02\" id=\"other_" + sid + "\"><strong>" + parentname + "：</strong>+" + aname + "(￥" + aprice + ")</div>";
        $("#cartother").append(str);
    }
}

function cancelRadio(pid, sid) {
    var name = "attr_" + sid;
    $("input[name='" + name + "']").each(function () {
        $(this).removeAttr("checked");
        $("#other_" + sid).remove();
    })
}


//配料多选
// 商品编号码， 配料子项名称 ， 价格
function selectme1(pid, aname, aprice, sid, parentname) {
   
    var cid = "attr_" + pid + "_" + sid;
    var checkbox = $("#" + cid);

    var str = "<strong>" + parentname + "：</strong>";
    var o = $("#other_" + sid);
    if (o.length > 0) { //重新选择
        $("#other_" + sid).html(""); //清除 
        $("input[name='box_" + sid + "']").each(function () {
            if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
                str += "+" + $(this).attr("myname") + "(￥" + $(this).attr("value") + "),";
            }
        })
        $("#other_" + sid).html(str); //重新输出
    }
    else {//第一次选择
        var str = "<div class=\"lunch_box_02\" id=\"other_" + sid + "\"><strong>" + parentname + "：</strong>+" + aname + "(￥" + aprice + ")</div>";
        $("#cartother").append(str);
    }
}

function ListCart()
{
    List();
}

///获取已有商品列表
function List() {
    var userid = document.getElementById("hidUid").value;
    var togoid = $("#hidTogoId").val();//商家编号
    if (togoid == undefined) {
        togoid = "0";
    }
    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/TogoShoppingCart.aspx",
        data: "fuc=list&t=" + new Date().getTime() + "&uid=" + userid + "&togoid=" + togoid + "&grade=" + 1,
        success: function (msg) {
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

//删除一个
///删除购物车中商品
function delone(dataid) {
    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/TogoShoppingCart.aspx",
        data: "fuc=del&t=" + new Date().getTime() + "&dataid=" + dataid,
        success: function (msg) {
            if (msg == "0") {
                jerror();
            }
            else {
                List();
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
    var code = $("#hfcode").val();
    jQuery.ajax(
    {
        type: "post",
        url: "/Ajax/TogoShoppingCart.aspx",
        data: "fuc=delall&t=" + new Date().getTime() + "&uc=" + code,
        success: function (msg) {
            if (msg == "0") {
                jerror();
            }
            else {
                List();
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

//提交CheckCart
function CheckCart() {
    if ($(".myshop").length <= 0) {
        alert('你的餐盒是空的,不的提交!');
        return false;
    }
    var togoStatus = $("#hnTogoStatus").val();
    if (togoStatus != "1") {
        alert('商家不在营业，不能提交订单');
        return false;
    }
    var url = window.location.href;
    if (url.indexOf("market.aspx") > -1) {
        window.location = "OrderDetail.aspx?togoid=" + request("id");
    }
    else
    {
        window.location = "submitorder.aspx?id=" + request("id") + "&tel=" + request("tel");
    }
    
    return true;
}
//订单第一步操作。

//返回商店页面
function gotogo() {
    var tid = $("#hftid").val();
    window.location = "ShowShop.aspx?id=" + tid;
}

///提交订单
function checkorder() {
    if ($(".myshop").length <= 0) {
        alert('你的餐盒是空的,不的提交!');
        return false;
    }
    var togoStatus = $("#hnTogoStatus").val();
    if (togoStatus != "1") {
        alert('商家不在营业，不能提交订单');
        return false;
    }


    var togoid = $("hidTogoId").va;
    window.location = "submitorder.aspx?id=" + togoid + "&tel=" + Request("tel");
    return true;
}

function goorder(url) {
    var tid = $("#hftid").val();
    var uid = $("#hfuid").val();
    var aid = $("#hfaid").val();
    window.location = "OrderDetail.aspx?tid=" + tid;
}

function keypress(e) {
    ee = e ? e : window.event ? event : null;
    var keyNum = ee.keyCode == 0 ? ee.which : ee.keyCode;
    // 表示删除
    if ((keyNum >= 48 && keyNum <= 57) || keyNum == 8 || keyNum == 46) {
        return true;
    }
    else {
        return false;
    }
}

function goshop() {
    var tid = $("#hftid").val();
    var uid = $("#hfuid").val();
    var aid = $("#hfaid").val();
    window.location = "orderfood.aspx?uid=" + uid + "&tid=" + tid + "&aid=" + aid;
}

function showsub(tag) {
    var subid = $(tag).attr("sub_id") + "";
    var g = tag;
    var d = document.getElementById(subid);
    var a = false;
    var e = function () {
        var h = $(g).offset();
        $(d).css({ top: h.top + g.offsetHeight, left: h.left });
        $(d).show();
        if ($(d).width() < $(g).width() + 13) {
            $(d).width($(g).width() + 13)
        }
        a = true
    };
    var f = function () {
        if (a == false) {
            $(d).hide()
        }
    };
    var c = function () {
        a = false; window.setTimeout(f, 200)
    };
    e();
    $(tag).hover(e, c);
    $(d).hover(e, c)
    $(d).show();
}

function selectme(tag) {
    var mystyle = $("input[name='mystyle']").attr("checked", false);
    $(tag).attr("checked", true);
}

function cartHiden(flag) {
    var cartimg = document.getElementById("cartimg");
    var carthead = document.getElementById("cartHead").style.display;
    //购物车已经隐藏 , 点击显示
    if (carthead == "none") {
        jQuery("#tablecart tr").css("display", "");
        $("#cartimg").attr("src", "images/arrow_down_03.jpg")
        List();
    }
    else {
        //购物车已经显示 , 点击隐藏
        jQuery("#tablecart tr").css("display", "none");
        jQuery("#tablecart tr:first").css("display", "");
        $("#cartimg").attr("src", "images/arrow_up_03.jpg")
        document.getElementById("cartHead").style.display = "none"
    }
    if (typeof flag != 'undefined') {
        jQuery("#tablecart tr").css("display", "");
        $("#cartimg").attr("src", "images/arrow_down_03.jpg")
    }
}

function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}



//页面滚动
function winscroll() {
    var carttop = $("#mytarget").offset().top;
    //浏览器可视高度
    var _mysrcreen = parseInt($(window).height());
    var cartheight = parseInt($("#basketTitleWrap").height());

    var scrollTop = Math.max(document.documentElement.scrollTop, document.body.scrollTop); //获取滚动条的当前位置 距离页面最顶部

    if (_mysrcreen < cartheight) {
        $("#basketTitleWrap").removeClass("fixed-top");
        if (scrollTop > (carttop + cartheight)) {
            $("#cart_fix_hint").show();
        }
        else {
            $("#cart_fix_hint").hide();
        }
    }
    else {
        $("#cart_fix_hint").hide();
        if (scrollTop >= carttop) {
            $("#basketTitleWrap").addClass("fixed-top");
        }
        else {
            $("#basketTitleWrap").removeClass("fixed-top");
        }
    }
}