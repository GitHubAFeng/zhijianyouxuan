<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeList.aspx.cs" Inherits="Html5.mRechargeList" %>


<%@ Register Src="~/footer.ascx" TagName="Foot" TagPrefix="uc2" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>

    <link type="text/css" rel="stylesheet" href="css/style.css?v=708" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=708" />

    <script type="text/javascript" src="javascript/jquery.js"></script>

    <style type="text/css">
        .bom_menu .icon-order {
            background-image: url(/images/ico_b_order_cur.png);
            color: #f39800;
        }
    </style>


</head>
<body>
    <div class="page">

        <div id="page_title">
            <a href="myinfolist.aspx" id="back" class=" top_left" data-ajax="false"></a>
            <h1>充值记录</h1>
        </div>

        <div class="container ">
            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rptorder">
                    <ItemTemplate>
                        <li>

                            <div class="order-tit">
                                <span class="time"><%# Eval("PayDate")%></span>
                                 <span class="state"><%# Hangjing.WebCommon.WebHelper.Recharge(Eval("paytype").ToString()) %></span>
                            </div>
                            <div class="order-info" style="background-image:none;">
                                <p class="f14">金额：<span class="red"><%# Eval("AddMoney") %></span>元</p>
                                <p class="grey">备注：<%# Eval("Inve2")%></p>
                            </div>



                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>

            <div class="con-btn" id="pages" runat="server" style="display: none;">
            </div>
        </div>
    </div>
    <uc2:Foot runat="server" ID="foot" />
</body>
</html>


<script type="text/javascript">
    function payagain(target) {
        var orderid = $(target).attr("data-orderid");
        var price = $(target).attr("data-price");


        window.location = "/weixinpay/wxpay.aspx?orderid=" + orderid + "&price=" + price;
        return false;
    }

    function orderdetail(orderid, ReveInt2) {
        if (parseInt(ReveInt2) == 50) {
            window.location = "lifeshowtogoorder.aspx?id=" + orderid;
        } else {
            window.location = "showtogoorder.aspx?id=" + orderid;
        }
    }
</script>

