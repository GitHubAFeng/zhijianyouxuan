<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myshopcard.aspx.cs" Inherits="Html5.myshopcard" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
     <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?" />
    <link type="text/css" rel="stylesheet" href="css/page.css?" />
    <script type="text/javascript" src="javascript/jquery.js"></script>

</head>


<body>
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" data-ajax="false" id="back" class=" top_left"></a>
            <h1>我的优惠券</h1>

            <a href="addshopcard.aspx" class="reg top_right">绑定优惠券</a>

        </div>

        <div class="container">
            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rptorder">
                    <ItemTemplate>



                        <li>

                            <div class="order-tit">
                                <span class="time"><%# Eval("usergettime")%></span>
                                <span class="state"><%#Convert.ToInt32(Eval("isused")) == 1 ? "已用" : "未用"%></span>
                            </div>
                            <div class="order-info" style="background-image:none;">
                                <p class="f14">券号：<span class="red"><%# Eval("ckey") %></span></p>
                                <p class="grey">说明：<%# Eval("ReveVar1")%></p>
                            </div>



                        </li>

                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>


    <div class="con-btn" id="pages" runat="server"></div>


</body>
</html>

