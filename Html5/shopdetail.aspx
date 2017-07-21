<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopdetail.aspx.cs" Inherits="Html5.shopdetail" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=1" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=1" />
    <link href="css/sweetalert.css" rel="stylesheet" />
</head>

<body>
    <input type="hidden" id="tid" value="0" runat="server" />
    <input type="hidden" id="collect" value="0" runat="server" />
    <input type="hidden" id="hidUid" value="0" runat="server" />

    <asp:Repeater ID="rptshop" runat="server">
        <ItemTemplate>
            <div class="page">
                <div id="page_title">
                    <a href="ShowTogo.aspx?id=<% =Request["id"] %>" id="back" class=" top_left"></a>
                    <h1 id="h1togoname" runat="server">
                        <%# WebUtility.Left(Eval("Name"), 5) %>
                    </h1>
                    <a class="top_right iscollect" href="javascript:"></a>
                </div>

                <div class="shop_detail">
                    <ul>
                        <li><a href="ShowTogo.aspx?id=<%=Request["id"] %>">点菜</a></li>
                        <li><a href="Feedback.aspx?id=<%=Request["id"] %>">评价</a></li>
                        <li class="cur_con"><a href="shopdetail.aspx?id=<%=Request["id"] %>">商家</a></li>
                    </ul>
                </div>

                <div class="shopdetail_con">
                    <div class="shopimg_con">
                        <p>
                            <img alt="" src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>"  width="100" height="100" />
                        </p>
                        <h4 ><%# Eval("Name") %></h4>
                    </div>

                    <ul class="shop-info clearfix">
                        <li>
                            <p>餐厅好评</p>
                            <p class="figure" id="review" runat="server"><%# Eval("review") %></p>
                        </li>
                        <li>
                            <p>配送费</p>
                            <p class="figure"><%# Eval("SendFee") %>元</p>
                        </li>
                        <li>
                            <p>起送金额</p>
                            <p class="figure"><%# Eval("SendLimit") %>元</p>
                        </li>
                    </ul>


                    <ul class="my_order_list shop-detail">
                        <li>
                            <%--<a href="Feedback.aspx?id=<% =Request["id"] %>">
                        <div class="order-info re" ><i class="icon-comment"></i>查看评价 <span class="mess" id="sugcut" runat="server">0</span></div>
                    </a>--%>

                            <a href="Feedback.aspx?id=<% =Request["id"] %>">
                                <div class="order-info re" style="border-bottom: 1px solid #dfdfdf;">
                                    <i class="icon-comment"></i>
                                    查看评价 <span class="mess"><%# Eval("reviewtimes") %></span>
                                </div>
                            </a>
                            <a href="showshopic.aspx?id=<% =Request["id"] %>">
                                <div class="order-info re" style="border-bottom: 1px solid #dfdfdf;">
                                    <i class="icon-shopevpic"></i>
                                    商家图片 <span class="mess"></span>
                                </div>
                            </a>
                            <a href="shopidcard.aspx?id=<% =Request["id"] %>" >
                                <div class="order-info re">
                                    <i class="icon-shopevpic" style="background-image: url(/images/shop_idcard.png)"></i>
                                    资质证照<span class="mess" id="Span2" runat="server"></span>
                                </div>
                            </a>
                        </li>
                        <li>
                            <div class="order-tit">
                                <span class="state" style="float: right;">
                                    <a href="tel:<%# Eval("Comm") %>" class="icon-call"></a>
                                </span>
                                <span class="time">商家地址：<%# Eval("Address") %></span>
                            </div>

                            <div class="order-info" style="background: none;">
                                <p class="grey f14">
                                    <i class="icon-time"></i>
                                    <span><%# Eval("opentimestr") %></span>
                                </p>
                            </div>
                        </li>
                        <li>
                            <asp:Repeater runat="server" ID="rpttags" DataSource='<%#Eval("pictags") %>'>
                                <ItemTemplate>
                                    <div class="order-tit shop-marker">
                                        <i class="icon-new" style="background: url('<%# WebUtility.ShowPic(Eval("Picture").ToString())%>') no-repeat scroll 0 0 rgba(0, 0, 0, 0); margin: 0; padding: 0; width: 15px;"></i>
                                        <%# WebUtility.Left(Eval("Title"),30) %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ul>
                </div>
                <div class="shop_bot">
                    <a class="go_order_btn" href="ShowTogo.aspx?id=<% =Request["id"] %>" >去订餐</a>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</body>
</html>
<script type="text/javascript" src="javascript/jquery.js"></script>
<script type="text/javascript" src="javascript/collect.js?v=1104"></script>

<script src="javascript/sweetalert.min.js"></script>
