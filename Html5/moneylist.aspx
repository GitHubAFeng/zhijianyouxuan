<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="moneylist.aspx.cs" Inherits="Html5.moneylist" %>

<!DOCTYPE html>
<head>
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type">
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>

    <link href="css/moneylist.css" rel="stylesheet" type="text/css">

    <script src="javascript/jquery.js"></script>
</head>
<html>
<body>
    <div class="tips">
        红包使用说明
    </div>
    <div class="mainbox">
        <asp:Repeater runat="server" ID="rptpromotion">
            <ItemTemplate>
                <a href="javascript:ordersend(<%# Eval("id")%>);">
                    <div class="content">
                        <img src="images/packet_bg.png" />
                        <h1>￥</h1>
                        <h2><%# Math.Floor(Convert.ToDouble(Eval("alltotal")))%></h2>
                        <h3>满<%# Math.Floor(Convert.ToDouble(Eval("moneyline")))%>元可用</h3>
                        <h4>支付红包</h4>
                        <h5>微信下单，在线支付专享</h5>
                        <div style="clear:both;"></div>
                        <h6><span class="redorange">还有<%# getdate(Convert.ToDateTime(Eval("validitytime")))%>天过期</span>
                           <span> 有效期至：<%# Convert.ToDateTime(Eval("validitytime")).ToShortDateString()%></span>
                        </h6>
                        <div class="cleaerfix"></div>
                    </div>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</body>
</html>
<script src="/javascript/jCommon.js?v=1" type="text/javascript"></script>
<script type="text/javascript">
    function ordersend(id)
    {
        var url = request("returnurl");
        if (url != "")
        {
            url += "&pid=" + id;
            window.location = url
        }
        
    }



</script>


