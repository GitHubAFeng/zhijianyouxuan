<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="Html5.Feedback" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title><%= SectionProxyData.GetSetValue(2) %></title>
    <link type="text/css" rel="stylesheet" href="css/style.css?v=<%=(new Random()).Next(0000,9999) %>" />
    <link type="text/css" rel="stylesheet" href="css/page.css?v=<%=(new Random()).Next(0000,9999) %>" />
</head>

<body>
    <input type="hidden" id="tid" value="0" runat="server" />
    <input type="hidden" id="collect" value="0" runat="server" />
    <input type="hidden" id="hidUid" value="0" runat="server" />
    <input id="hfpage" runat="server" type="hidden" />

    <div class="page">
        <div id="page_title">
            <a href="myinfolist.aspx" data-ajax="false" id="back" runat="server" class=" top_left"></a>
            <h1 id="tbtogoname" runat="server">我的评论</h1>
        </div>
         
        <div class="shop_detail">
            <ul>
                <li><a id="goOrder" runat="server" href="index.aspx">点菜</a></li>
                <li class="cur_con"><a href="Feedback.aspx?id=<%=Request["id"]%>">评价</a></li>
                <li><a id="goShop" runat="server" href="index.aspx">商家</a></li>
            </ul>
        </div>

        <ul class="comment-list">
            <asp:Repeater ID="rptCommentlist" runat="server">
                <ItemTemplate>
                    <li>
                        <p class="re"><%# WebUtility.Left(Eval("togoname"), 12) %> <span class="time"><%#Convert.ToDateTime(Eval("posttime")).ToLongDateString()%></span></p>
                        <div class="com-info">
                            <span class="star_bg">
                                <span class="star" style="width: <%# Convert.ToInt32(Eval("ServiceGrade"))*100/5 %>%"></span>
                            </span>
                        </div>
                        <p>
                            <%# Eval("Comment")%>
                        </p>
                        <%# Eval("rcontent").ToString() == "" ? "" : "<div class=\"comment_reply\"><p>回复：" + Eval("rcontent") + " - "+Eval("Rtime")+"</p></div>"%>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="con-btn" id="pages" runat="server">
        </div>

    </div>
</body>
</html>
