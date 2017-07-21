<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftList.aspx.cs" Inherits="Html5.gift_list" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title><%= SectionProxyData.GetSetValue(2)%></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
</head>

<body>
    <div class="page">
        <div id="page_title">
            <a href="Index.aspx" class="back top_left"></a>
            <h1>积分商城</h1>
            <a href="MyInfoList.aspx?returnurl=<%=Request.RawUrl%>" class="vip top_right"></a>
        </div>

        <div class="container">
            <div class="integral_shop">
                <ul class="gift">
                    <%-- 优惠券 --%>
                    <asp:Repeater runat="server" ID="rptVoucher">
                        <ItemTemplate>
                            <li>
                                <a href="javascript:void(0);" class="pic">
                                    <img style="min-width:90px;" src="<%#WebUtility.ShowPic(Eval("Inve2").ToString())%>" /></a>
                                <div class="info">
                                    <h2><%# WebUtility.Left( Eval("title").ToString() , 11)%></h2>
                                    <p class="des">库存数量：<%# Eval("CardCount")%></p>
                                    <p class="price">所需积分：<%# Eval("mydiscount")%></p>
                                </div>
                                <div class="cart_con">
                                    <a href="GiftInfo.aspx?type=card&id=<%#Eval("dataid")%>">立即兑换</a>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                    <%-- 其他礼品兑换 --%>
                    <asp:Repeater runat="server" ID="rptGiftList">
                        <ItemTemplate>
                            <li>
                                <a href="#" class="pic">
                                    <img style="min-width:90px;" src="<%#WebUtility.ShowPic(Eval("Picture").ToString()) %>" /></a>
                                <div class="info">
                                    <h2><%# Eval("Gname") %></h2>
                                    <p class="des">库存数量：<%# Eval("stocks")%></p>
                                    <p class="price">所需积分：<%# Eval("NeedIntegral")%></p>
                                </div>
                                <div class="cart_con">
                                    <a href="GiftInfo.aspx?type=other&id=<%#Eval("giftsid") %>">立即兑换</a>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                </ul>
            </div>
        </div>
    </div>
</body>
</html>
