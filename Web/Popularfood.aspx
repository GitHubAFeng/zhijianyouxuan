<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popularfood.aspx.cs" Inherits="Popularfood" %>

<%@ Register Src="~/Banner.ascx" TagPrefix="top" TagName="banner" %>
<%@ Register Src="header.ascx" TagName="header" TagPrefix="uc1" %>
<%@ Register Src="~/Foot.ascx" TagPrefix="foot" TagName="foot" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>大家都在吃</title>
    <link type="text/css" rel="stylesheet" href="css/common.css" />
    <link type="text/css" rel="stylesheet" href="css/shop.css" />
</head>
<body>
    <form id="form1" runat="server">
        <top:banner ID="Banner" runat="server" />
        <uc1:header ID="header" runat="server" />
        <div class="warp">
            <div class="popularfood clearfix">
                <div class="p_t_tit">大家都在吃</div>
                <asp:Repeater runat="server" ID="rptfoodlist">
                    <ItemTemplate>
                        <a href="shop.aspx?id=<%#Eval("FPMaster")%>">
                            <div class="p_foodlist clearfix">
                                <span class="hotno hotno1"><%#(Container.ItemIndex+1) %></span>
                                <span class="hotno hotno2" style="display: none;"></span>
                                <span class="hotno hotno3" style="display: none;"></span>
                                <div class="pic">
                                    <img src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>" />
                                </div>
                                <ul>
                                    <li class="foodname"><%# Eval("FoodName") %></li>
                                    <li class="shopname"><%# WebUtility.Left(Eval("TogoName").ToString(),7)%></li>
                                    <li class="sales">月销：<%# Eval("SortName") %>份</li>
                                    <li class="price">￥<%#Eval("FPrice")%></li>
                                </ul>
                                <button>来一份</button>
                            </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
    <foot:foot ID="Foot1" runat="server" />
</body>
</html>
