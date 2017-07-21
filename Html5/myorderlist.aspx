<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myorderlist.aspx.cs" Inherits="Html5.myorderlist" %>


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
             color:#f39800;
        }
    </style>


</head>
<body>
    <div class="page">

        <div id="page_title">
            <a href="myinfolist.aspx" id="back" class=" top_left" data-ajax="false"></a>
            <h1>订单列表</h1>
        </div>

        <div class="container ">
            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rptorder">
                    <ItemTemplate>
                        <li>
                            <a href="showtogoorder.aspx?id=<%# Eval("orderid") %>" data-ajax="false">
                                <div class="order-tit">
                                    <span class="time"><%# Eval("OrderDateTime")%></span>
                                    <span class="state"><%# WebUtility.TurnOrderState(Eval("OrderStatus")) %></span>
                                </div>
                                <div class="order-info">
                                    <p class="f14">订单编号：<span class="red"><%# Eval("orderid") %></span></p>
                                    <p class="grey">总金额：￥<%# Eval("OrderSums")%></p>
                                </div>
                            </a>

                      

                            <div class="view_back_con" style="<%# Convert.ToInt32(Eval("cityid")) == 0 ? "display:none": ""  %>; margin-bottom: 10px;">
                                <input type="submit" value="立即支付"   class="view_back_btn"  data-orderid="<%# Eval("orderid") %>" data-price="<%# Convert.ToDecimal(Eval("OrderSums")) - Convert.ToDecimal(Eval("cardpay")) - Convert.ToDecimal(Eval("promotionmoney")) %>" onclick="payagain(this); return false;" data-ajax="false" style="padding: 4px 18px; border-radius: 5px;" />
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

