<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="becomedistributor.aspx.cs" Inherits="Html5.becomedistributor" %>

<%@ Register Src="~/header.ascx" TagName="head" TagPrefix="uc3" %>
<%@ Register Src="~/distributorfooter.ascx" TagName="Foot" TagPrefix="uc2" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/common.css" rel="stylesheet" type="text/css" />
    <title>成为分销商</title>

    <script src="javascript/jquery.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <uc3:head runat="server" ID="head" />
        <div class="main_title">注册分销商</div>
        <!--Start of the Tabmenu1 -->
        <div class="warp" style=" padding-bottom:30px;">

            <input type="hidden" value="<%= SectionProxyData.GetSetValue(57) %>" id="hfmoney"  />

            <input type="hidden" value="" id="hforderid"  runat="server" />

            <!--Start Tabcontent 1 -->
            <div id="tabcontent1">
                <ul>
                    <li>
                        <span style="float: right; width:140px;" runat="server" id="spanorderid"></span><span>订单编号：</span></li>
                    <li>
                        <span style="color: red; float: right;"><%= SectionProxyData.GetSetValue(57) %>元</span><span>支付金额：</span></li>
                </ul>
                <div class="clear"></div>

                <div style="border-top:solid #ccc 1px;">
                    <button type="button" onclick="payagain()">微信支付</button></div>
            </div>

        </div>
    </form>
       <uc2:Foot runat="server" ID="footer" />
</body>
</html>



<script type="text/javascript">
    function payagain() {

        var orderid = $("#hforderid").val();
        var price = $("#hfmoney").val();

        window.location = "/weixinpay/wxpay.aspx?orderid=" + orderid + "&price=" + price;
        return false;
    }

</script>

