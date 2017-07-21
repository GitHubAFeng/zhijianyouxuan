<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myAddressdetail.aspx.cs" Inherits="Html5.myAddressdetail" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>
        <%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1" />

    <script src="javascript/jquery.js"></script>

</head>
<body>
    <input type="hidden" runat="server" id="sendmsgcodestate" value="0" />
    <input id="hferrmsg" runat="server" type="hidden" />

    <input id="hfdefaultvalue" runat="server" type="hidden" />

    <div class="page">
        <form method="post" onclick="" action="myAddressdetail.aspx?returnurl=<%=Server.UrlEncode(Request["returnurl"]) %>&id=<%= Request["id"] %>" data-ajax="false">

            <input id="hidlat" runat="server" type="hidden" value="0" />
            <input id="hidlng" runat="server" type="hidden" value="0" />

            <div id="page_title">
                <a href="myAddresslist.aspx" id="back" runat="server" class=" top_left" data-ajax="false"></a>
                <h1 id="updateaddress" runat="server">提交订单</h1>
                <input type="submit" value="保存" class=" top_right" style="margin: 12px 0px; border: none; color: #fff; background: none; font-size: 14px; font-family: 微软雅黑;" onclick="return checkuserregin()" data-ajax="false" />
            </div>
            <div class="container">
                <ul class="my_order_list">
                    <li>
                        <div class="order-tit">
                            <span class="time"><strong>收货人</strong>
                                <input name="tbReceiveName" type="text" id="tbReceiveName" class="w_txt" runat="server" placeholder="请输入收货人姓名" />
                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>联系电话</strong>
                                <input type="text" class="w_txt" name="tbCellPhone" runat="server" id="tbCellPhone" placeholder="请填写收货人的电话" />
                            </span>
                        </div>
                        <div class="order-tit">
                            <span class="time"><strong>收货地址</strong>
                                <input type="text" class="w_txt" name="keyaddress" id="keyaddress" runat="server" placeholder="请填写收货人地址" />
                            </span>
                            <span class="mess_ch" onclick="auto_location()"><i class="ico-open"></i></span>
                        </div>
                        <div class="order-tit" style="border-bottom:none;">
                            <span class="time"><strong>门牌号</strong>
                                <input type="text" class="w_txt" name="tbdoor" id="tbdoor" runat="server" placeholder="请填写详细地址及门牌号" />
                            </span>
                        </div>
                    </li>
                    <li class="delete">
                        <a href="javascript:deladd()">删除该地址</a>
                    </li>
                </ul>
            </div>
        </form>
    </div>
</body>
</html>
<script src="javascript/jCommon.js?v=1" type="text/javascript"></script>
<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=b5d4SSc2CRusF1Pkzm8dVGOg"></script>
<script type="text/javascript" src="javascript/eventwrapper.min.js"></script>
<script type="text/javascript" src="javascript/myAddressdetail.js?v=0210"></script>


<script type="text/javascript">
    $(document).ready(function () {
        initialize();
        var aid = request("id");
        if (aid == "0" || aid == "") {
            $(".delete").hide();
        }
        else {
            $(".delete").show();
        }
    })



    //提交订单post错误信息
    var err = $("#hferrmsg").val();
    if (err != "" && err != "undefined") {
        alert(err);
        if (err == "修改地址成功" || err == "添加地址成功") {
            window.location.href = 'myAddresslist.aspx?returnurl=' + request("returnurl");
        }
    }

    function checkuserregin() {
        var tbAddress = $("#keyaddress").val();
        var tbdoor = $("#tbdoor").val();
        var receiver = document.getElementById("tbReceiveName").value;

        if (receiver == "" || receiver == null || receiver == "请输入收货人") {
            alert("收货人不能为空！");
            return false;
        }

        var phone = document.getElementById("tbCellPhone").value;
        if (phone == "" || phone == null || phone == "请输入手机号") {
            alert("手机号码不能为空！");
            return false;
        }
        var patrn = /^[0-9,]*$/;

        if (!patrn.exec(phone)) {
            alert("手机(电话)号码格式错误。");
            return false;
        }


        var hidlat = $("#hidlat").val();
        if (hidlat == "0" || hidlat.length == 0) {

            alert("请在收货地址栏下拉选择确定您的位置！");
            return false;
        }

        if (tbAddress == "" || tbAddress == null || tbAddress == "请输入收货地址") {
            alert("请输入收货地址！");
            return false;
        }
        if (tbdoor == "请填写详细地址及门牌号" || tbdoor == "") {
            alert("请输入门牌号！");
            return false;
        }
        return true;
    }
    function deladd() {
        var aid = request("id");
        if (aid == "" || aid == null) {
            alert("删除失败！");
            return;
        }
        jQuery.ajax(
        {
            type: "post",
            url: "ajaxHandler.ashx",
            data: "method=delete&aid=" + aid,
            success: function (msg) {
                if (msg == "1") {
                    alert("删除成功！");
                    if (request("returnurl") != "" && request("returnurl") != null) {
                        window.location.href = unescape(request("returnurl"));
                        return;
                    }
                    else {
                        window.location.href = "myAddresslist.aspx";
                        return;
                    }
                }
                else (msg == "0")
                {
                    alert("默认地址请勿删除！");
                }
            }
        })
    }


</script>

