<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="header" %>


<link href="/javascript/jbox/Skins/jbox.css" rel="stylesheet" type="text/css" />
<div class="header">
    <div class="wrap clearfix">
        <div class="header-left">
            <a href="#" runat="server" id="logolink">
                <img src="#" runat="server" id="logoimg" /></a>
        </div>
        <div class="header-site">
            <div class="city" runat="server" id="lbcityname">全国</div>
            <a href="/Index.aspx?change=1" class="check-city">[切换城市]</a>
        </div>
        <div class="header-right">
            <p style="height: 25px; line-height: 25px;">
                <asp:Repeater runat="server" ID="rptqq">
                    <ItemTemplate>

                        <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%# Eval("value") %>&site=qq&menu=yes" class="qq_btn">
                            <%# Eval("classname") %>
                        </a>

                    </ItemTemplate>
                </asp:Repeater>
            </p>

            <p class="f14" style="padding: 14px 0 0"><span class="phonenumber"><%=SectionProxyData.GetSetValue(53)%></span></p>
        </div>
    </div>
</div>


<div id="nav">
    <div class="wrap">
        <ul>
            <li id="my_nav_li0"><a href="<%=WebUtility.GetUrl("~/Index.aspx?change=1")%>" class="small">首页</a></li>
           

            <li id="my_nav_li2"><a href="<%=WebUtility.GetUrl("~/shoplist.aspx") %>">外卖送餐</a></li>
            <li id="my_nav_li3"><a href="<%=WebUtility.GetUrl("~/market.aspx") %>" class="small">超市</a></li>
            <li id="my_nav_li4"><a href="<%=WebUtility.GetUrl("~/Gift/Gift.aspx")%>">积分商城</a></li>
            <li id="my_nav_li5"><a href="<%=WebUtility.GetUrl("/applyshop.aspx")%>">商家加盟</a></li>
            <li id="my_nav_li6"><a href="<%=WebUtility.GetUrl("~/bbs.aspx")%>">留言版</a></li>
            <li id="my_nav_li7"><a href="<%=WebUtility.GetUrl("~/express.aspx")%>">跑腿</a></li>
            <li id="my_nav_li8"><a href="<%=WebUtility.GetUrl("~/Popularfood.aspx")%>">大家都在吃</a></li>
        </ul>
    </div>
</div>



