<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myshops.aspx.cs" Inherits="Html5.myshops" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css" />
    <link type="text/css" rel="stylesheet" href="css/home.css" />
    <link type="text/css" rel="stylesheet" href="css/page.css" />
    <link href="css/pictip.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <input id="hfpage" runat="server" type="hidden" />
    <input type="hidden" id="hfod" runat="server" value="od0" />
    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" id="back" class=" top_left"></a>
            <h1>我的收藏</h1>
          

        </div>
      
        <div class="container ">

            <ul class="shoplist">
                <asp:Repeater runat="server" ID="rptJoinTogolist">
                    <ItemTemplate>
                        <li>
                            <a href="ShowTogo.aspx?id=<%# Eval("Unid")%>" title="<%# Eval("Name")%>">
                                <div class=" pic">
                                    <img class="img" src="<%# WebUtility.ShowPic(Eval("Picture").ToString()) %>" />
                                    <%# ParseBisness(Eval("Status"),Eval("isbisness"))%>
                                </div>
                                <div class="info">
                                    <h2 class="shop-marker">
                                        <%# WebUtility.Left( Eval("Name"),8)%>
                                        <asp:Repeater runat="server" ID="rpttags" DataSource='<%#Eval("pictags")%>'>
                                            <ItemTemplate>
                                                <i style="background: url('<%# WebUtility.ShowPic(Eval("Picture").ToString())%>') no-repeat scroll 0 0 rgba(0, 0, 0, 0); margin: 0; padding: 0; width: 15px;"></i>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </h2>
                                    <p class="des"><%# WebUtility.Left( Eval("Address"),12)%></p>
                                    <p class="des">配送费：<%# WebUtility.getOnepoint( Eval("SendFee"))%>元 | 起送价：<%# Eval("SendLimit") %>元</p>
                                    <p class="des re">
                                        <i class="icon-time"></i>
                                        <%# WebUtility.Left(Eval("opentimestr"),15) %>
                                        <span class="distance"><i class="icon-address"></i>&lt;<%# Eval("Inve1") %>公里</span>
                                    </p>
                                </div>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="con-btn" id="pages" runat="server">
            </div>
        </div>
        <%--        <div class="bom_menu">
            <ul>
                <li class="hover"><a href="#"><i class="icon-fj"></i>附近</a></li>
                <li><a href="#"><i class="icon-search"></i>搜索</a></li>
                <li><a href="#"><i class="icon-good"></i>推荐</a></li>
                <li><a href="#"><i class="icon-more"></i>更多</a></li>
            </ul>
        </div>--%>
    </div>
</body>
</html>

<script src="javascript/jquery.js" type="text/javascript"></script>

<script src="javascript/jCommon.js" type="text/javascript"></script>
<script type="text/javascript">
    var hfod = $("#hfod").val();
    $("." + hfod).addClass("hover");
</script>


