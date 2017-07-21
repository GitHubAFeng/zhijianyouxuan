<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="moneydetail.aspx.cs" Inherits="Html5.moneydetail" %>

<!DOCTYPE html>
<head>
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type">
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= SectionProxyData.GetSetValue(2)%></title>
    <link href="css/getpackages.css" rel="stylesheet" type="text/css">

    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="javascript/jquery.js"></script>
<script src="javascript/moneydetail.js?v=2016081601"></script>


</head>

<html>
<body>
    <input type="hidden" runat="server" id="hidtel" value="">
    <div class="banner">
        <img src="images/banner.png" />
    </div>
    <div class="mainbox">
        <asp:Repeater runat="server" ID="rptpromotion">
            <ItemTemplate>
                <div class="column_top">
                    <div class="red_packets">
                        <h1><span>红</span><span>包</span></h1>
                        <h2>￥</h2>
                        <h3><%# Math.Floor(Convert.ToDouble(Eval("alltotal")))%></h3>
                        <h4>在线支付专享，满<%# Math.Floor(Convert.ToDouble(Eval("moneyline")))%>元可用</h4>
                    </div>
                    <div class="cleaerfix"></div>
                </div>
                <div class="column_two">
                    <div class="content">
                        <div class="concent_qb">
                            <h1>红包已放入账户！快订餐吧，不要过期噢~</h1>
                            <h2><%# Eval("ReveVar")%></h2>
                            <%--<h3><a href="">修改></a></h3>--%>
                        </div>
                        <div class="cleaerfix"></div>
                        <span class="buttom"><a href="TogoList.aspx">去订餐</a></span>
                        <div class="concent_rule">


                            <%= SectionProxyData.GetSetValue(62) %>
                        </div>
                        <div class="content_luck" style="display: none;">

                            <h1>看朋友手气如何</h1>
                            <img src="images/pic.png" />
                            <h2>陈</h2>
                            <span>2016-02-18 10:53:31</span>
                            <h4>红包的金额和你的颜值一样高哦</h4>
                            <img src="images/pic.png" />
                            <h2>大神</h2>
                            <span>2016-02-18 10:53:31</span>
                            <h4>红包的金额和你的颜值一样高哦</h4>

                        </div>
                        <div class="cleaerfix"></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="cleaerfix"></div>
    </div>
    <div class="mainbox1">
        <form method="post" action="moneydetail.aspx?urlKey=<%= Request["urlKey"]  %>" data-ajax="false">
            <span class="tel">
                <input name="tbphone" type="text" id="tbphone" placeholder="请输入您的手机号">
            </span>
            <span class="notice" style="display: none;">请输入正确的手机号
            </span>
            <span class="button">
                <input type="submit" id="butpack" onclick="return checkphone()" value="马上领取" />
            </span>
            <div class="content">
                <div class="concent_rule">
                    <%= SectionProxyData.GetSetValue(62) %>
                </div>
            </div>
        </form>
    </div>
</body>
</html>

<script src="javascript/sweetalert.min.js"></script>
<script src="javascript/jCommon.js"></script>
