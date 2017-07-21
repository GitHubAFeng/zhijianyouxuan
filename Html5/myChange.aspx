<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myChange.aspx.cs" Inherits="Html5.myChange" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <script type="text/javascript" src="javascript/jquery.js"></script>

    <style type="text/css">
        .my_order_list li{
            margin-bottom:25px;
        }
        .my_order_list li .order-tit{
            padding:5px 0px;
        }
    </style>

</head>


<body>
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" data-ajax="false" id="back" class=" top_left"></a>
            <h1>我的礼品</h1>
        </div>

        <div class="container">
            <ul class="my_order_list">
                <asp:Repeater runat="server" ID="rptComment">
                    <ItemTemplate>
                        <li>
                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>礼品名称：</strong>
                                    <label class="w_txt">
                                        <%# WebUtility.Left(Eval("GiftName"), 16)%>
                                    </label>
                                </span>
                            </div>
                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>所用积分：</strong>
                                    <label class="w_txt">
                                        <%# Eval("PayIntegral")%>
                                    </label>
                                </span>
                            </div>

                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>礼品状态：</strong>
                                    <label class="w_txt">
                                        <%# GetState( Eval("State")) %>
                                    </label>
                                </span>
                            </div>

                            <div class="order-tit">
                                <span class="time" data-icon="false">
                                    <strong>兑换时间：</strong>
                                    <label class="w_txt">
                                        <%# Convert.ToDateTime(Eval("Cdate")).ToShortDateString()%>
                                    </label>
                                </span>
                            </div>

                            <div class="order-tit" style="border-bottom: none;height:initial;min-height:30px;">
                                <span class="time" data-icon="false" style="float:initial;">
                                    <strong>详细信息：</strong>
                                    <label class="w_txt">
                                        <span>收货人：<%#Eval("Person")%></span> <span>电话：<%#Eval("Phone")%></span> <span>地址：<%#Eval("Address")%></span>
                                    </label>
                                </span>
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

