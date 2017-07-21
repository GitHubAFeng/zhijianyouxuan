<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftInfo.aspx.cs" Inherits="Html5.GiftInfo" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2)%></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link type="text/css" rel="stylesheet" href="css/jquery.mobile-1.2.0.css" />
    <script type="text/javascript" src="javascript/jquery.js"></script>
</head>


<body>
    <div class="page">
        <div id="page_title">
            <a href="GiftList.aspx" class="back top_left"></a>
            <h1>确认兑换信息</h1>
        </div>

        <div class="container">
            <form id="form1" method="post" action="GiftInfo.aspx" enctype="application/x-www-form-urlencoded" data-ajax="false">
                <input type="hidden" id="IsSHAddress" value="1" runat="server" />
                <input type="hidden" id="IsNeedAddress" value="0" runat="server" />
                <input type="hidden" id="hType" name="type" runat="server" />
                <input type="hidden" id="hId" name="id" runat="server" />
                <input type="hidden" id="hAddressId" name="hAddressId" runat="server" />
                <input type="hidden" id="sumNumber" name="sumNumber" value="1" />
                <input type="hidden" id="hStocks" name="hStocks" value="0" runat="server" />
                <input type="hidden" id="hAddress" name="hAddress" runat="server" />
                <input type="hidden" id="hPerson" name="hPerson" runat="server" />
                <input type="hidden" id="hPhone" name="hPhone" runat="server" />
                <input type="hidden" id="hGiftName" name="hGiftName" runat="server" />
                <input type="hidden" id="hPoint" name="hPoint" runat="server" />
                <ul class="cart_info_list">
                    <li>
                        <span class="item name" id="title" runat="server"></span>
                        <span class="item cart_detail">
                            <span class="cicon"><a href="javascript:Subtract()">-</a></span>
                            <span class="cicon mid" id="number" runat="server">1</span>
                            <span class="cicon"><a href="javascript:AddUp()">+</a></span>
                        </span>
                        <span class="item price">积分：<span id="point" runat="server"></span></span>
                    </li>

                    <li style="border-bottom: none;">
                        <span class="item name">总计</span>
                        <span class="item total">
                            <span class="green" id="number2">1</span> / <span class="green">积分：
                                <span id="point2" runat="server"></span></span>
                        </span>
                    </li>
                </ul>

                <ul class="my_order_list" id="address" runat="server">
                    <li id="tAddress" class="address_style" style="display: block;" onclick="window.location='myAddresslist.aspx?returnurl=<%=Server.UrlEncode(Request.RawUrl)%>';">
                        <div class="address_style_bot" id="addressBackground" runat="server">
                            <div class="order-info">
                                <i class="addresss"></i>
                                <div style="display: none;" id="noAddress" class="order-add-con" runat="server">收货地址：请补充你的收货信息~</div>
                                <div id="addressInfo" class="order-add-con" runat="server">
                                    <p class="grey"><span id="tbPerson" runat="server"></span>，<span id="tbPhone" runat="server"></span></p>
                                    <p class="f14" id="tbAddress" runat="server"></p>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>

                <div class="view_back_con" style="overflow: initial;">
                    <input onclick="return CheckOrder()" type="submit" value="确认兑换" class="view_back_btn">
                </div>

            </form>
        </div>

    </div>


    <%-- loding --%>
    <div id="myordermsgk" class="mModal" style="position: fixed; left: 0; top: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,.5); z-index: 900; display: none;">
    </div>

</body>
</html>

<script type="text/javascript" src="javascript/jCommon.js?v=125"></script>
<script type="text/javascript">

    //提交验证
    function CheckOrder() {
        var IsNeedAddress = $("#IsNeedAddress").val();//是否需要地址
        if (parseInt(IsNeedAddress) == 1) {
            var IsSHAddress = $("#IsSHAddress").val();//是否有收货地址
            if (parseInt(IsSHAddress) == 0) {
                alert("请补充你的收货信息~");
                return false;
            }
        }
        ShowLoding(); 
        return true;
    }

    function ShowLoding() {
        showLoader();
        $("#myordermsgk").show();
    }

    var msg = "<%=msg %>";
    var goUrl = "<%=goUrl%>";
    if ($.trim(msg) != "") {
        var arr = new Array();
        arr = msg.split("+");
        msg = "";
        for (var i = 0; i < arr.length; i++) {
            msg += arr[i] + "\r\n";
        }
        alert(msg);
    }
    if ($.trim(goUrl) != "") {
        window.location = goUrl;
    }

    //减数量
    var point = $("#point").text();
    function Subtract() {
        var number = $("#number").text();
        if (number != "1") {
            number = parseInt(number) - 1;
            $("#number").text(number);
            $("#number2").text(number);
            $("#sumNumber").val(number);
            $("#point2").text(number * point);
        }
    }
    //加数量
    function AddUp() {
        var number = $("#number").text();
        number = parseInt(number) + 1;
        $("#number").text(number);
        $("#number2").text(number);
        $("#sumNumber").val(number);
        $("#point2").text(number * point);
    }


</script>
