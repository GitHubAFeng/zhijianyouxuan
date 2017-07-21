<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Foot.ascx.cs" Inherits="Foot" %>
<!--页脚-->
<div class="clear">
</div>
<div class="footer">
    <div class="wrap">
        <div class="serive clearfix">
            <asp:Repeater ID="rptsort" runat="server">
                <ItemTemplate>
                    <dl>
                        <dt><%# Eval("name") %></dt>
                        <asp:Repeater ID="rptsub" runat="server" DataSource='<%#Eval("glist") %>'>
                            <ItemTemplate>
                                <dd><a href="<%# WebUtility.GetUrl("~/Help/Index.aspx?id=")%><%# Eval("dataid") %>" title="<%# Eval("title") %>"><%#WebUtility.Left(Eval("Title"),10) %></a></dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                </ItemTemplate>
            </asp:Repeater>
            <dl class="tec" style="padding-left: 0px; width: 210px;">
                <img src="<%=WebUtility.GetUrl("~/images/serive.png") %>" />
                <p class="f14" style="padding-top: 15px;"><%= SectionProxyData.GetSetValue(2) %>客服电话</p>
                <h3 class="f14"><%=SectionProxyData.GetSetValue(16)%></h3>
                <p>周一到周日<%=SectionProxyData.GetSetValue(24)%></p>
            </dl>

        </div>
        <div class="foo">
            <%=SectionProxyData.GetSetValue(26)%>
        </div>
    </div>
</div>

<div class="topbar-nav-link" style="display:none;">
    <div class="dropbox topbar-mobile-dropbox">
        <span>扫一扫, 手机订餐更方便</span>
        <img class="topbar-nav-qrcode" src="/images/wx_img.jpg">
        <a class="topbar-nav-iosbtn" href="<%= SectionProxyData.GetSetValue(41) %>"></a>
        <a class="topbar-nav-androidbtn" href="<%= SectionProxyData.GetSetValue(40) %>"></a>
    </div>
</div>
<script src="/javascript/jbox/jquery.jBox-2.3.min.js" type="text/javascript"></script>

