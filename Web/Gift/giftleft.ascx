<%@ Control Language="C#" AutoEventWireup="true" CodeFile="giftleft.ascx.cs" Inherits="Gift_giftleft" %>

<div class="hgift_left">

    <div class="htitle">
        <div class="htitle_bg">
            <h4>兑换说明</h4>
        </div>
    </div>
    <div class="hgift_left_bot">
        <%=SectionProxyData.GetSetValue(12)%>
    </div>
    <div class="htitle">
        <div class="htitle_bg">
            <h4>兑换公告</h4>
        </div>
    </div>
    <div class="hgift_left_bot">
        <div class="hleft_list_02">
            <ul>
                <asp:Repeater runat="server" ID="rptGetGiftRecord">
                    <ItemTemplate>
                        <li><span>
                            <%# Eval("UserName")%></span>于 [<%# Convert.ToDateTime( Eval("cDate")).ToShortDateString()%>]，成功兑换了<span><a href="ShowGift.aspx?id=<%# Eval("GiftsId") %>">
                                <%# Eval("GiftName")%></a></span></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>

</div>

